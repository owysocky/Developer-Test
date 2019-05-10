// -----------------------------------------------------------------------
// 
// <copyright file="WeatherApiService.cs" company="Provoke Solutions">
// 
// Copyright (c) Provoke Solutions. All rights reserved.
// 
// </copyright>
// 
// -----------------------------------------------------------------------
namespace JuniorDevTestFunctionApp.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class WeatherApiService.
    /// </summary>
    public static class WeatherApiService
    {
        /// <summary>
        /// The configuration.
        /// TODO: We need an API Key for this service's configuration. One of our developers mentioned we can get a key here: https://openweathermap.org/
        /// TODO: Once we have a key, we need to add it to the application's configuration.
        /// </summary>
        private static readonly WeatherServiceConfiguration Configuration =
            ApplicationConfiguration.Configuration.GetSection("WeatherApiConfiguration")
                .Get<WeatherServiceConfiguration>();

        /// <summary>
        /// The client
        /// </summary>
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Gets the weather.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Task JObject.</returns>
        public static async Task<JObject> GetWeather(string query)
        {
            HttpRequestMessage webRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{Configuration.WeatherUrlBase}?q={query}&appid={Configuration.Key}"),
                Method =  HttpMethod.Post
            };

            var response = await Client.SendAsync(webRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<JObject>();
        }
    }
}
