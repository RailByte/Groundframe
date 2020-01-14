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
            TestVersion.SaveToSQLDB();
            Assert.Equal(Name, TestVersion.Name);
            Assert.Equal(Description, TestVersion.Description);
            Assert.Equal(Version, TestVersion.VersionFrom);
            Assert.Null(TestVersion.VersionTo);
            Assert.NotEqual(0, TestVersion.ID);
        }

        /// <summary>
        /// Checks that passing the GroundFrame.SQL ID of a location it gets correctly instantiated into the object
        /// </summary>
        [Theory]
        [InlineData("Test Version Name 2", "Test Version Description 2", 2.0)]
        public void Version_Constructor_ByID(string Name, string Description, Decimal Version)
        {
            Classes.Version TestVersion = new Classes.Version(Name, Description, Version, this._SQLConnection);
            TestVersion.SaveToSQLDB();

            Classes.Version ComparisonVersion = new Classes.Version(TestVersion.ID, this._SQLConnection);
            Assert.Equal(TestVersion.ID, ComparisonVersion.ID);
            Assert.Equal(TestVersion.Name, ComparisonVersion.Name);
            Assert.Equal(TestVersion.Description, ComparisonVersion.Description);
            Assert.Equal(Version, ComparisonVersion.VersionFrom);
        }

        /// <summary>
        /// Checks that passing the GroundFrame.SQL ID of a location it gets correctly instantiated into the object
        /// </summary>
        [Theory]
        [InlineData("Test Version Name 3", "Test Version Description 3", 3.0)]
        public void Version_Constructor_CheckVersionToUpdatesCorrectly(string Name, string Description, Decimal Version)
        {
            //Create initial version
            Classes.Version TestVersion = new Classes.Version(Name, Description, Version, this._SQLConnection);
            TestVersion.SaveToSQLDB();
            Assert.NotEqual(0, TestVersion.ID);
            Assert.Equal(Name, TestVersion.Name);
            Assert.Equal(Description, TestVersion.Description);
            Assert.Equal(Version, TestVersion.VersionFrom);
            Assert.Null(TestVersion.VersionTo);

            //Create and check next verion
            Classes.Version ComparisonVersion = new Classes.Version(string.Format(@"{0}_U", Name), string.Format(@"{0}_U", Description), Version + 1, this._SQLConnection);
            ComparisonVersion.SaveToSQLDB();
            Assert.NotEqual(0, ComparisonVersion.ID);
            Assert.Equal(string.Format(@"{0}_U", Name), ComparisonVersion.Name);
            Assert.Equal(string.Format(@"{0}_U", Description), ComparisonVersion.Description);
            Assert.Equal((decimal)Version + 1, ComparisonVersion.VersionFrom);
            Assert.Null(ComparisonVersion.VersionTo);

            //Refresh Initial
            TestVersion.RefreshFromDB();
            Assert.Equal((decimal)3.9, TestVersion.VersionTo);
        }

        #endregion Methods
    }
}
