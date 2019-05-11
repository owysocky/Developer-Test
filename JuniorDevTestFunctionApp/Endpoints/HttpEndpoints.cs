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
        /// Gets the data.
        /// TODO: This endpoint is currently not returning JSON when it is hit in the browser. Please adjust this code to make the return type as JSON.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="log">The log.</param>
        /// <returns>Task HttpResponseMessage.</returns>
        [FunctionName(nameof(GetData))]
        public static async Task<HttpResponseMessage> GetData(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-data")] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, await WeatherRepository.GetAll());
        }
    }
}
