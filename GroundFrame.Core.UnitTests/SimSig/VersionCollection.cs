﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace GroundFrame.Core.UnitTests.SimSig
{
    public class VersionCollection : IDisposable
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
        public VersionCollection()
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
        public void SimulationCollection_Constructor_Default()
        {
            Core.SimSig.SimulationCollection TestSimCollection = new Core.SimSig.SimulationCollection(this._SQLConnection);
            //Check 5 records are returned
            Assert.Equal(5, TestSimCollection.Count);
        }

        #endregion Methods

    }
}
