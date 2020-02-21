using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Core
{
    /// <summary>
    /// Static class store values and settings which are available globally. These must provide a default value if not set.
    /// </summary>
    public static class Globals
    {
        private static UserSettingCollection _UserSettings;

        /// <summary>
        /// Gets or sets the user's settings. If not provided this will return the default set of user settins in en-GB
        /// </summary>
        public static UserSettingCollection UserSettings { get { return _UserSettings ?? new UserSettingCollection(); } set { _UserSettings = value; } }

        /// <summary>
        /// Gets a connection to the GroundFrame.SQL database
        /// </summary>
        /// <param name="AppAPIKey">The Application API Key being used to connect the GroundFrame.SQL database</param>
        /// <param name="AppUserAPIKey">The Application User API Key being used to connect the GroundFrame.SQL database</param>
        /// <param name="Environment">The environment to connect to</param>
        /// <returns>Returns a GFSqlConnector object connected to the GroundFrame.SQL.database</returns>
        /// <remarks>The server and database name of the GroundFrame.SQL database is configured in the groundframe.config.json file supplied with the calling application</remarks>
        public static GFSqlConnector GetGFSqlConnector(string AppAPIKey, string AppUserAPIKey, string Environment)
        {
            IConfigurationRoot config = GetConfig(Environment);
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];

            return new GFSqlConnector(AppAPIKey, AppUserAPIKey, SQLServer, DBName);
        }

        /// <summary>
        /// Gets a connection to the GroundFrame Mongo database
        /// </summary>
        /// <param name="Environment">The environment to connect to</param>
        /// <returns>Returns a GFMongoConnector object connected to the GroundFrame.MongoDB database</returns>
        /// <remarks>The server and database name of the GroundFrame.SQL database is configured in the groundframe.config.json file with the calling application</remarks>

        public static GFMongoConnector GetGFMongoConnector(string Environment)
        {
            IConfigurationRoot config = GetConfig(Environment);
            string MongoDBServer = config["gfMongoServer"];
            string MongoPort = config["gfMongoPort"];
            return new GFMongoConnector(MongoDBServer, MongoPort);
        }


        /// <summary>
        /// Gets the configuation for the supplied environment
        /// </summary>
        /// <param name="Environment">The environment for the config</param>
        /// <returns>A IConfigurationRoot inherited object containing the configuration for the supplied environment</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Different config file will only ever be declare in English")]
        public static IConfigurationRoot GetConfig(string Environment)
        {
            return new ConfigurationBuilder().AddJsonFile($"groundframe.{Environment.ToLower()}.config.json", optional: true, reloadOnChange: true).Build();
        }
    }
}
