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
        /// <param name="partitionKey">The partition key to store the data under. This should be the city name.</param>
        /// <returns>Task.</returns>
        public static async Task Update(string partitionKey, JObject weatherData)
        {
            DynamicTableEntity tableEntity =
                new DynamicTableEntity(partitionKey, DateTimeOffset.UtcNow.Ticks.ToString())
                {
                    Properties = { ["data"] = EntityProperty.GeneratePropertyForString(weatherData.ToString()) }
                };

            TableOperation operation = TableOperation.InsertOrReplace(tableEntity);
            await CloudTable.ExecuteAsync(operation);
        }

        /// <summary>
        /// Gets the most recent data entry from each partitioned city.
        /// </summary>
        /// <returns>A list of JObject containing weather data.</returns>
        public static async Task<List<JObject>> GetMostRecentFromAllPartitions()
        {
            TableQuery<DynamicTableEntity> tableQuery =
                new TableQuery<DynamicTableEntity>();
            IEnumerable<DynamicTableEntity> queryResults = await ExecuteQuery(tableQuery);
            return queryResults.GroupBy(x => x.PartitionKey)
                .Select(x => x.FirstOrDefault())
                .Select(x => JsonConvert.DeserializeObject<JObject>(x.Properties["data"].StringValue))
                .ToList();
        }

        /// <summary>
        /// Gets all the data across all partition keys.
        /// </summary>
        /// <returns>A list of JObject containing weather data.</returns>
        public static async Task<List<JObject>> GetAll()
        {
            TableQuery<DynamicTableEntity> tableQuery = new TableQuery<DynamicTableEntity>();
            IEnumerable<DynamicTableEntity> queryResults = await ExecuteQuery(tableQuery);
            return queryResults
                .Select(x => JsonConvert.DeserializeObject<JObject>(x.Properties["data"].StringValue))
                .ToList();
        }

        /// <summary>
        /// Executes the table query.
        /// </summary>
        /// <param name="tableQuery">The query to get data based on.</param>
        /// <returns>An IEnumerable of DynamicTableEntities.</returns>
        private static async Task<IEnumerable<DynamicTableEntity>> ExecuteQuery(TableQuery<DynamicTableEntity> tableQuery)
        {
            TableContinuationToken continuationToken = null;
            List<DynamicTableEntity> queryResults = new List<DynamicTableEntity>();
            do
            {
                TableQuerySegment<DynamicTableEntity> result = await CloudTable.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
                continuationToken = result.ContinuationToken;

                queryResults.AddRange(result.Results);
            }
            while (continuationToken != null);

            return queryResults.OrderByDescending(x => x.Timestamp);
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
