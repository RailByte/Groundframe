using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GroundFrame.Classes.UnitTests.SimSig
{
    public class Location : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly GFSqlConnector _SQLConnection;
        private readonly Classes.Simulation _TestSimulation;

        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Location Test object
        /// </summary>
        public Location()
        {
            //Set up the Data Connection
            var config = new ConfigurationBuilder().AddJsonFile("xunit.config.json").Build();
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testadminuserAPIKEY", SQLServer, DBName, true); //Need to log in as admin

            //Create a Test Simulation and save to DB

            this._TestSimulation = new Classes.Simulation("Test Location Sim Name", "Test Location Sim Desc", null, "Test Location Sim Code", this._SQLConnection);
            this._TestSimulation.SaveToSQLDB();
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
        /// Checks that trying to creating location and saving assigns properties correctly and issues GroundFrame.SQL database ID
        /// </summary>
        [Theory]
        [InlineData(null, "Test Location Name 1", false, "Test Location Code 1")]
        [InlineData("TIPLOC2", "Test Location Name 2", false, "Test Location Code 2")]
        public void Location_Constructor_ByProperties(string TIPLOC, string Name, bool EntryPoint, string SimSigCode)
        {
            Classes.Location TestLocation = new Classes.Location(this._TestSimulation, Name, TIPLOC, SimSigCode, EntryPoint, this._SQLConnection);
            TestLocation.SaveToSQLDB();
            Assert.Equal(Name, TestLocation.Name);
            Assert.Equal(TIPLOC, TestLocation.TIPLOC);
            Assert.Equal(EntryPoint, TestLocation.EntryPoint);
            Assert.Equal(SimSigCode, TestLocation.SimSigCode);
            Assert.NotEqual(0, TestLocation.ID);
        }

        /// <summary>
        /// Checks that creating a location and then updating doesn't error
        /// </summary>
        [Theory]
        [InlineData(null, "Test Location Name 3", false, "Test Location Code 3")]
        [InlineData("TIPLOC2", "Test Location Name 4", false, "Test Location Code 4")]
        public void Location_Method_CheckUpdate(string TIPLOC, string Name, bool EntryPoint, string SimSigCode)
        {
            Classes.Location TestLocation = new Classes.Location(this._TestSimulation, Name, TIPLOC, SimSigCode, EntryPoint, this._SQLConnection);
            TestLocation.SaveToSQLDB();
            
            Assert.Equal(Name, TestLocation.Name);
            Assert.Equal(TIPLOC, TestLocation.TIPLOC);
            Assert.Equal(EntryPoint, TestLocation.EntryPoint);
            Assert.Equal(SimSigCode, TestLocation.SimSigCode);
            Assert.NotEqual(0, TestLocation.ID);

            int LocationID = TestLocation.ID;

            TestLocation.TIPLOC = string.Format(@"{0}_U", TIPLOC);

            TestLocation.SaveToSQLDB();

            Assert.Equal(LocationID, TestLocation.ID);
        }

        #endregion Methods
    }
}
