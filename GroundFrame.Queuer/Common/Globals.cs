using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GroundFrame.Classes;
using Microsoft.Extensions.Configuration;

namespace GroundFrame.Queuer
{
    internal static class Globals
    {
        /// <summary>
        /// Gets a connection to the GroundFrame.SQL database
        /// </summary>
        /// <param name="AppAPIKey">The Application API Key being used to connect the GroundFrame.SQL database</param>
        /// <param name="AppUserAPIKey">The Application User API Key being used to connect the GroundFrame.SQL database</param>
        /// <returns>Returns a GFSqlConnector object connected to the GroundFrame.SQL.database</returns>
        /// <remarks>The server and database name of the GroundFrame.SQL database is configured in the queuer.config.json file</remarks>
        internal static GFSqlConnector GetGFSqlConnector(string AppAPIKey, string AppUserAPIKey, string Environment)
        {
            IConfigurationRoot config = GetConfig(Environment);            
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];

            return new GFSqlConnector(AppAPIKey, AppUserAPIKey, SQLServer, DBName);
        }

        internal static GFMongoConnector GetGFMongoConnector(string Environment)
        {
            IConfigurationRoot config = GetConfig(Environment);
            string MongoDBServer = config["gfMongoServer"];
            string MongoPort = config["gfMongoPort"];
            return new GFMongoConnector(MongoDBServer, MongoPort);
        }

        private static MemoryStream BuildIOStreamFromString(string InputString)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(InputString);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// Gets the configuation for the supplied environment
        /// </summary>
        /// <param name="Environment">The environment for the config</param>
        /// <returns>A IConfigurationRoot inherited object containing the configuration for the supplied environment</returns>
        private static IConfigurationRoot GetConfig(string Environment)
        {
            return new ConfigurationBuilder().AddJsonFile($"queuer.{Environment.ToLower()}.config.json", optional: true, reloadOnChange: true).Build();
        }
    }
}
