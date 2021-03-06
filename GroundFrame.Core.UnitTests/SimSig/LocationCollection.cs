﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace GroundFrame.Core.UnitTests.SimSig
{
    public class LocationCollection : IDisposable
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
        /// Instantiates a new Location Collection Test object
        /// </summary>
        public LocationCollection()
        {
            //Set up the Data Connection
            var config = new ConfigurationBuilder().AddJsonFile("xunit.config.json").Build();
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testuserAPIKEY", SQLServer, DBName, true);
            //Generate some test data in the database
            this._SQLConnection.GenerateTestData();
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
        /// Checks that the SimulationCollection loads all the simulation from the test data.
        /// </summary>
        [Fact]
        public void LocationCollection_Constructor_Default()
        {
            //Populate some data
            Core.SimSig.Simulation TestSim = new Core.SimSig.Simulation("LocColl Sim Name", "LocColl Sim Desc", null, "LocCollCode", this._SQLConnection);
            TestSim.SaveToSQLDB();
            Core.SimSig.Location TestLocation1 = new Core.SimSig.Location(TestSim, "LocColl Loc Name", null, "LocCollLocCode1", true, Core.SimSig.SimSigLocationType.Station, this._SQLConnection);
            TestLocation1.SaveToSQLDB();
            Core.SimSig.Location TestLocation2 = new Core.SimSig.Location(TestSim, "LocCol2 Loc Name", "LocCollTIPLOC2", "LocCollLocCode2", false, Core.SimSig.SimSigLocationType.Station, this._SQLConnection);
            TestLocation2.SaveToSQLDB();

            //Get Collection
            Core.SimSig.LocationCollection TestLocCollection = new Core.SimSig.LocationCollection(TestSim, this._SQLConnection);
            //Check 5 records are returned
            Assert.Equal(2, TestLocCollection.Count);
        }

        #endregion Methods

    }
}
