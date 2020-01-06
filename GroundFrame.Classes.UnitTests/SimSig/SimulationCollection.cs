using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace GroundFrame.Classes.UnitTests.SimSig
{
    public class SimulationCollection : IDisposable
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
        public SimulationCollection()
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
            Classes.SimulationCollection TestSimCollection = new Classes.SimulationCollection(this._SQLConnection);
            //Check 5 records are returned
            Assert.Equal(5, TestSimCollection.Count);
            //Check a sim has one era
            Assert.Single(TestSimCollection.IndexOf(0).Eras);
        }

        #endregion Methods

    }
}
