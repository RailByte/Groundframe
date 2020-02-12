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
        private readonly Classes.SimSig.Simulation _TestSimulation;

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

            this._TestSimulation = new Classes.SimSig.Simulation("Test Location Sim Name", "Test Location Sim Desc", null, "Test Location Sim Code", this._SQLConnection);
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
        [InlineData(null, "Test Location Name 1", false, "TestLocCode1", 3)]
        [InlineData("TIPLOC2", "Test Location Name 2", false, "TestLocCode2", 5)]
        public void Location_Constructor_ByProperties(string TIPLOC, string Name, bool EntryPoint, string SimSigCode, int LocationType)
        {
            Classes.SimSig.Location TestLocation = new Classes.SimSig.Location(this._TestSimulation, Name, TIPLOC, SimSigCode, EntryPoint, (Classes.SimSig.SimSigLocationType)LocationType, this._SQLConnection);
            TestLocation.SaveToSQLDB();
            Assert.Equal(Name, TestLocation.Name);
            Assert.Equal(TIPLOC, TestLocation.TIPLOC);
            Assert.Equal(EntryPoint, TestLocation.EntryPoint);
            Assert.Equal(SimSigCode, TestLocation.SimSigCode);
            Assert.Equal((Classes.SimSig.SimSigLocationType)LocationType, TestLocation.LocationType);
            Assert.NotEqual(0, TestLocation.ID);
        }

        /// <summary>
        /// Checks that creating a location and then updating doesn't error
        /// </summary>
        [Theory]
        [InlineData(null, "Test Location Name 3", false, "TestLocCode3", 0)]
        [InlineData("TIPLOC4", "Test Location Name 4", false, "TestLocCode4", 5)]
        public void Location_Method_CheckUpdate(string TIPLOC, string Name, bool EntryPoint, string SimSigCode, int LocationType)
        {
            Classes.SimSig.Location TestLocation = new Classes.SimSig.Location(this._TestSimulation, Name, TIPLOC, SimSigCode, EntryPoint, (Classes.SimSig.SimSigLocationType)LocationType, this._SQLConnection);
            TestLocation.SaveToSQLDB();
            
            Assert.Equal(Name, TestLocation.Name);
            Assert.Equal(TIPLOC, TestLocation.TIPLOC);
            Assert.Equal(EntryPoint, TestLocation.EntryPoint);
            Assert.Equal(SimSigCode, TestLocation.SimSigCode);
            Assert.NotEqual(0, TestLocation.ID);
            Assert.Equal((Classes.SimSig.SimSigLocationType)LocationType, TestLocation.LocationType);

            int LocationID = TestLocation.ID;

            TestLocation.TIPLOC = string.Format(@"{0}_U", TIPLOC);

            TestLocation.SaveToSQLDB();

            Assert.Equal(LocationID, TestLocation.ID);
        }


        /// <summary>
        /// Checks that creating a location and then updating doesn't error
        /// </summary>
        [Theory]
        [InlineData(null, "Test Location Name 5", false, "TestLocCode5", 0)]
        [InlineData("TIPLOC6", "Test Location Name 6", false, "TestLocCode6", 5)]
        public void Location_Method_CheckRefresh(string TIPLOC, string Name, bool EntryPoint, string SimSigCode, int LocationType)
        {
            Classes.SimSig.Location TestLocation = new Classes.SimSig.Location(this._TestSimulation, Name, TIPLOC, SimSigCode, EntryPoint, (Classes.SimSig.SimSigLocationType)LocationType, this._SQLConnection);
            TestLocation.SaveToSQLDB();

            //Now load into a new object and compare

            Classes.SimSig.Location CheckLocation = new Classes.SimSig.Location(TestLocation.ID, this._SQLConnection);
            Assert.Equal(TestLocation.ID, CheckLocation.ID);
            Assert.Equal(TestLocation.Name, CheckLocation.Name);
            Assert.Equal(TestLocation.SimSigCode, CheckLocation.SimSigCode);
            Assert.Equal(TestLocation.EntryPoint, CheckLocation.EntryPoint);
            Assert.Equal(TestLocation.LocationType, CheckLocation.LocationType);

            string TestTIPLOC = string.IsNullOrEmpty(TestLocation.TIPLOC) ? "NULL" : TestLocation.TIPLOC;
            string CheckTIPLOC = string.IsNullOrEmpty(CheckLocation.TIPLOC) ? "NULL" : CheckLocation.TIPLOC;

            Assert.Equal(TestTIPLOC, CheckTIPLOC);
        }

        #endregion Methods
    }
}
