// -----------------------------------------------------------------------
// 
// <copyright file="ApplicationConfiguration.cs" company="Provoke Solutions">
// 
// Copyright (c) Provoke Solutions. All rights reserved.
// 
// </copyright>
// 
// -----------------------------------------------------------------------
namespace JuniorDevTestFunctionApp.Configuration
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Class ApplicationConfiguration.
    /// </summary>
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Gets the get configuration.
        /// </summary>
        /// <value>The get configuration.</value>
        public static IConfigurationRoot Configuration { get; } = LoadConfiguration();

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <returns>An IConfigurationRoot.</returns>
        private static IConfigurationRoot LoadConfiguration()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            return config;
        }
    }
}
