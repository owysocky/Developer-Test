// -----------------------------------------------------------------------
// 
// <copyright file="ScheduledFunctions.cs" company="Provoke Solutions">
// 
// Copyright (c) Provoke Solutions. All rights reserved.
// 
// </copyright>
// 
// -----------------------------------------------------------------------
namespace JuniorDevTestFunctionApp
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using Services;

    /// <summary>
    /// Class ScheduledFunctions.
    /// </summary>
    public static class ScheduledFunctions
    {
        /// <summary>
        /// The cities for weather checks.
        /// </summary>
        public static List<string> Cities = new List<string> {"seattle"};

        /// <summary>
        /// Runs the scheduled task.
        /// TODO: This scheduled task has two issues. Please fix these issues:
        /// TODO: 1) The scheduler is running too slow. We need to run it once every 5 minutes.
        /// TODO: 2) Once we retrieve the data, we need to store the data. Another dev has created a method to do this. Please find it in the WeatherRepository and store this data.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <param name="log">The log.</param>
        /// <returns>Task.</returns>
        [FunctionName(nameof(RunScheduledTask))]
        public static async Task RunScheduledTask(
            [TimerTrigger("0 */15 * * * *")] TimerInfo timer,
            ILogger log)
        {
            foreach (var city in Cities)
            {
                JObject weather = await WeatherApiService.GetWeather(city);
            }
        }
    }
}
