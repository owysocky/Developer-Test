// -----------------------------------------------------------------------
// 
// <copyright file="StorageConfiguration.cs" company="Provoke Solutions">
// 
// Copyright (c) Provoke Solutions. All rights reserved.
// 
// </copyright>
// 
// -----------------------------------------------------------------------
namespace JuniorDevTestFunctionApp.Configuration
{
    /// <summary>
    /// Class StorageConfiguration.
    /// </summary>
    public class StorageConfiguration
    {
        /// <summary>
        /// The table name
        /// </summary>
        public const string TableName = "WeatherTable";

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }
    }
}
