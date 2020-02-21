using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GroundFrame.Core.UnitTests.SimSig
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
            Core.SimSig.Version TestVersion = new Core.SimSig.Version(Name, Description, Version, this._SQLConnection);
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
            Core.SimSig.Version TestVersion = new Core.SimSig.Version(Name, Description, Version, this._SQLConnection);
            TestVersion.SaveToSQLDB();

            Core.SimSig.Version ComparisonVersion = new Core.SimSig.Version(TestVersion.ID, this._SQLConnection);
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
            Core.SimSig.Version TestVersion = new Core.SimSig.Version(Name, Description, Version, this._SQLConnection);
            TestVersion.SaveToSQLDB();
            Assert.NotEqual(0, TestVersion.ID);
            Assert.Equal(Name, TestVersion.Name);
            Assert.Equal(Description, TestVersion.Description);
            Assert.Equal(Version, TestVersion.VersionFrom);
            Assert.Null(TestVersion.VersionTo);

            //Create and check next verion
            Core.SimSig.Version ComparisonVersion = new Core.SimSig.Version(string.Format(@"{0}_U", Name), string.Format(@"{0}_U", Description), Version + 1, this._SQLConnection);
            ComparisonVersion.SaveToSQLDB();
            Assert.NotEqual(0, ComparisonVersion.ID);
            Assert.Equal(string.Format(@"{0}_U", Name), ComparisonVersion.Name);
            Assert.Equal(string.Format(@"{0}_U", Description), ComparisonVersion.Description);
            Assert.Equal((decimal)Version + 1, ComparisonVersion.VersionFrom);
            Assert.Null(ComparisonVersion.VersionTo);

            //Refresh Initial
            TestVersion.RefreshFromDB();
            Assert.Equal((decimal)3.9900, TestVersion.VersionTo);
        }

        /// <summary>
        /// Checks that passing the GroundFrame.SQL ID of a location it gets correctly instantiated into the object
        /// </summary>
        [Fact]
        public void Version_Constructor_CheckVersionNumber()
        {
            //Create versions
            Core.SimSig.Version TestVersion1 = new Core.SimSig.Version("Test Version Name 4", "Test Version Description 4", 1.0000M, this._SQLConnection);
            TestVersion1.SaveToSQLDB();
            Core.SimSig.Version TestVersion2 = new Core.SimSig.Version("Test Version Name 5", "Test Version Description 5", 2.0000M, this._SQLConnection);
            TestVersion2.SaveToSQLDB();

            decimal VersionNumber1 = 1.5M;
            Core.SimSig.Version TestVersionNumber1 = new Core.SimSig.Version(VersionNumber1, this._SQLConnection);
            Assert.Equal("Test Version Name 4", TestVersionNumber1.Name);
            decimal VersionNumber2 = 3M;
            Core.SimSig.Version TestVersionNumber2 = new Core.SimSig.Version(VersionNumber2, this._SQLConnection);
            Assert.Equal("Test Version Name 5", TestVersionNumber2.Name);
            decimal VersionNumber3 = 0M;
            Core.SimSig.Version TestVersionNumber3 = new Core.SimSig.Version(VersionNumber3, this._SQLConnection);
            Assert.Null(TestVersionNumber3.Name);
        }

        #endregion Methods
    }
}
