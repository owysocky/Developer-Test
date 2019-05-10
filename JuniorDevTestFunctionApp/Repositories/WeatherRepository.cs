// -----------------------------------------------------------------------
// 
// <copyright file="WeatherRepository.cs" company="Provoke Solutions">
// 
// Copyright (c) Provoke Solutions. All rights reserved.
// 
// </copyright>
// 
// -----------------------------------------------------------------------
namespace JuniorDevTestFunctionApp.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class WeatherRepository.
    /// </summary>
    public static class WeatherRepository
    {
        /// <summary>
        /// The graph configuration
        /// </summary>
        private static readonly StorageConfiguration StorageConfiguration =
            ApplicationConfiguration.Configuration.GetSection("StorageConfiguration").Get<StorageConfiguration>();

        /// <summary>
        /// The cloud table
        /// </summary>
        private static readonly CloudTable CloudTable = BuildCloudTable();

        /// <summary>
        /// Updates the specified weather data.
        /// </summary>
        /// <param name="weatherData">The weather data.</param>
        /// <returns>Task.</returns>
        public static async Task Update(JObject weatherData)
        {
            DynamicTableEntity tableEntity =
                new DynamicTableEntity("WeatherData", DateTimeOffset.UtcNow.Ticks.ToString())
                {
                    Properties = {["data"] = EntityProperty.GeneratePropertyForString(weatherData.ToString())}
                };

            TableOperation operation = TableOperation.InsertOrReplace(tableEntity);
            await CloudTable.ExecuteAsync(operation);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Task List JObject.</returns>
        public static async Task<List<JObject>> GetAll()
        {
            TableQuery<DynamicTableEntity> statusQuery =
                new TableQuery<DynamicTableEntity>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "WeatherData"));

            TableContinuationToken continuationToken = null;
            List<DynamicTableEntity> queryResults = new List<DynamicTableEntity>();
            do
            {
                TableQuerySegment<DynamicTableEntity> result = await CloudTable.ExecuteQuerySegmentedAsync(statusQuery, continuationToken);
                continuationToken = result.ContinuationToken;

                queryResults.AddRange(result.Results);
            }
            while (continuationToken != null);

            return queryResults.OrderByDescending(x => x.Timestamp).Select(x => JsonConvert.DeserializeObject<JObject>(x.Properties["data"].StringValue)).ToList();
        }

        /// <summary>
        /// Builds the cloud table.
        /// </summary>
        /// <returns>A CloudTable.</returns>
        private static CloudTable BuildCloudTable()
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(StorageConfiguration.ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(StorageConfiguration.TableName);

            table.CreateIfNotExistsAsync().Wait();

            return table;
        }
    }
}
