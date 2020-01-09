using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GroundFrame.Classes.UnitTests.SimSig
{
    public class Version : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly GFSqlConnector _SQLConnection;

        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Simulation Test object
        /// </summary>
        public Version()
        {
            //Set up the Data Connection
            var config = new ConfigurationBuilder().AddJsonFile("xunit.config.json").Build();
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testadminuserAPIKEY", SQLServer, DBName, true);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Disposes the Simulation objects
        /// </summary>
        public void Dispose()
        {
            this._SQLConnection.TearDownData();
            this._SQLConnection.Dispose();
        }

        /// <summary>
        /// Checks that trying to creating version automatically saves the record and all the properties are properly assigned
        /// </summary>
        [Theory]
        [InlineData("Test Version Name 1", "Test Version Description 1", 1.0)]
        public void Version_Constructor_ByProperties(string Name, string Description, Decimal Version)
        {
            Classes.Version TestVersion = new Classes.Version(Name, Description, Version, this._SQLConnection);
            Assert.Equal(Name, TestVersion.Name);
            Assert.Equal(Description, TestVersion.Description);
            Assert.Equal(Version, TestVersion.VersionFrom);
            Assert.Null(TestVersion.VersionTo);
            Assert.NotEqual(0, TestVersion.ID);
        }

        #endregion Methods
    }
}
