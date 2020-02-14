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
            IConfigurationRoot config;
            
            if (!string.IsNullOrEmpty(Environment))
            {
                config = new ConfigurationBuilder().AddJsonFile("queuer.config.json").AddJsonFile($"queuer.{Environment}.config.json", optional: true).Build();
            }
            else
            {
                config = new ConfigurationBuilder().AddJsonStream(BuildIOStreamFromString("Test")).Build();
            }
            
            
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];

            return new GFSqlConnector(AppAPIKey, AppUserAPIKey, SQLServer, DBName);
        }

        private static MemoryStream BuildIOStreamFromString(string InputString)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(InputString);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            return new MemoryStream(byteArray);
        }

    }
}
