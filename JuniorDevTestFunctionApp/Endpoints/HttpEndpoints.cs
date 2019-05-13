// -----------------------------------------------------------------------
// 
// <copyright file="HttpEndpoints.cs" company="Provoke Solutions">
// 
// Copyright (c) Provoke Solutions. All rights reserved.
// 
// </copyright>
// 
// -----------------------------------------------------------------------

namespace JuniorDevTestFunctionApp
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Repositories;

    /// <summary>
    /// Class HttpEndpoints.
    /// TODO: We would like to let the ScheduleFunctions.RunScheduledTask function of this application check multiple cities. Please make a new endpoint that updates our cities list.
    /// </summary>
    public static class HttpEndpoints
    {
        /// <summary>
        /// Gets all weather data stored in the database.
        /// TODO: This endpoint is currently not returning JSON when it is hit in the browser. Please adjust this code to make the return type as JSON.
        /// </summary>
        /// <param name="req">The http request message.</param>
        /// <param name="log">The logger for the azure function.</param>
        /// <returns>An HttpResponseMessage.</returns>
        [FunctionName(nameof(GetData))]
        public static async Task<HttpResponseMessage> GetData(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-data")] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function GetData processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, await WeatherRepository.GetAll());
        }

        /// <summary>
        /// Gets the most recent weather data for each city stored in the database.
        /// TODO: This endpoint is currently not returning JSON when it is hit in the browser. Please adjust this code to make the return type as JSON.
        /// </summary>
        /// <param name="req">The http request message.</param>
        /// <param name="log">The logger for the azure function.</param>
        /// <returns>An HttpResponseMessage.</returns>
        [FunctionName(nameof(GetMostRecentData))]
        public static async Task<HttpResponseMessage> GetMostRecentData(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-most-recent")] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function GetMostRecentData processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, await WeatherRepository.GetMostRecentFromAllPartitions());
        }
    }
}
