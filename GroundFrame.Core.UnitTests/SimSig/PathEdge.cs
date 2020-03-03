using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using GroundFrame.Core.SimSig;

namespace GroundFrame.Core.UnitTests.SimSig
{
    public class PathEdge : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly GFSqlConnector _SQLConnection;
        private readonly Core.SimSig.Simulation _TestSimulation;
        private readonly Core.SimSig.SimulationExtension _TestSimulationExt;
        private readonly Core.SimSig.Location _TestLocation1;
        private readonly Core.SimSig.Location _TestLocation2;
        private readonly Core.SimSig.LocationNode _TestLocationNode1;
        private readonly Core.SimSig.LocationNode _TestLocationNode2;
        private readonly Core.SimSig.Version _TestVersion;

        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Location Test object
        /// </summary>
        public PathEdge()
        {
            //Set up the Data Connection
            var config = new ConfigurationBuilder().AddJsonFile("xunit.config.json").Build();
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testadminuserAPIKEY", SQLServer, DBName, true); //Need to log in as admin

            //Create a Test Simulation and save to DB

            this._TestSimulation = new Core.SimSig.Simulation("Test PathEdge Sim Name", "Test PathEdge Sim Desc", null, "TestPESimCode", this._SQLConnection);
            this._TestSimulation.SaveToSQLDB();
            this._TestVersion = new Core.SimSig.Version("Test PathEdge Version", "Test LocCode Version", 4.15M, this._SQLConnection);
            this._TestVersion.SaveToSQLDB();
            this._TestLocation1 = new Core.SimSig.Location(this._TestSimulation, "Test PathEdge Loc Name 1", null, "TestPECode1", false, Core.SimSig.SimSigLocationType.Station, this._SQLConnection);
            this._TestLocation1.SaveToSQLDB();
            this._TestLocationNode1 = new Core.SimSig.LocationNode(this._TestSimulation.ID, this._TestLocation1.SimSigCode, this._TestSimulation.GetSimulationEras().Find(x => x.Type == EraType.Template).ID, this._TestVersion, null, new Core.Electrification(0), SimSigLocationType.Station, null, false, null, null, this._SQLConnection);
            this._TestLocationNode1.SaveToSQLDB();
            this._TestLocation2 = new Core.SimSig.Location(this._TestSimulation, "Test PathEdge Loc Name 2", null, "TestPECode2", false, Core.SimSig.SimSigLocationType.Station, this._SQLConnection);
            this._TestLocation2.SaveToSQLDB();
            this._TestLocationNode2 = new Core.SimSig.LocationNode(this._TestSimulation.ID, this._TestLocation2.SimSigCode, this._TestSimulation.GetSimulationEras().Find(x => x.Type == EraType.Template).ID, this._TestVersion, null, new Core.Electrification(0), SimSigLocationType.Station, null, false, null, null, this._SQLConnection);
            this._TestLocationNode2.SaveToSQLDB();
            this._TestSimulationExt = new SimulationExtension(this._TestSimulation.ID, this._SQLConnection);
            this._TestSimulationExt.Locations.Add(this._TestLocation1);
            this._TestSimulationExt.Locations.Add(this._TestLocation2);
            this._TestSimulationExt.LocationNodes.Add(this._TestLocationNode1);
            this._TestSimulationExt.LocationNodes.Add(this._TestLocationNode2);    
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
        /// Test instantiating a PathEdge between the 2 location nodes
        /// </summary>
        [Theory]
        [InlineData("D", 10000, SimSigPathDirection.None)]
        [InlineData("D3O", null, SimSigPathDirection.Up)]
        public void PathEdge_Constructor_ByProperties_SimSigCode(string Electrification, int? Length, SimSigPathDirection Direction)
        {
            Length TestLength = null;

            if (Length != null)
            {
                TestLength = new Length(Convert.ToInt32(Length));
            }

            Core.SimSig.PathEdge TestPathEdge = new Core.SimSig.PathEdge(this._TestLocationNode1, this._TestLocationNode2, this._SQLConnection);
            TestPathEdge.PathElectrification = new Electrification(Electrification);
            TestPathEdge.PathLength = TestLength;
            TestPathEdge.PathDirection = Direction;

            Assert.Equal(this._TestLocationNode1.ID, TestPathEdge.FromLocation.ID);
            Assert.Equal(this._TestLocationNode2.ID, TestPathEdge.ToLocation.ID);

            if (TestLength != null)
            {
                Assert.Equal(TestLength.Meters, TestPathEdge.PathLength.Meters);
            }
            else
            {
                Assert.Null(TestLength);
            }

            Assert.Equal(Direction, TestPathEdge.PathDirection);
        }

        /// <summary>
        /// Test instantiating a PathEdge with a location SimSigCode etc and then saving
        /// </summary>
        [Theory]
        [InlineData("D", 10000, SimSigPathDirection.None)]
        [InlineData("D3O", null, SimSigPathDirection.Up)]
        public void PathEdge_Method_SavetoDB(string Electrification, int? Length, SimSigPathDirection Direction)
        {
            Length TestLength = null;

            if (Length != null)
            {
                TestLength = new Length(Convert.ToInt32(Length));
            }

            Core.SimSig.PathEdge TestPathEdge = new Core.SimSig.PathEdge(this._TestLocationNode1, this._TestLocationNode2, this._SQLConnection);
            TestPathEdge.PathElectrification = new Electrification(Electrification);
            TestPathEdge.PathLength = TestLength;
            TestPathEdge.PathDirection = Direction;

            Assert.Equal(this._TestLocationNode1.ID, TestPathEdge.FromLocation.ID);
            Assert.Equal(this._TestLocationNode2.ID, TestPathEdge.ToLocation.ID);

            if (TestLength != null)
            {
                Assert.Equal(TestLength.Meters, TestPathEdge.PathLength.Meters);
            }
            else
            {
                Assert.Null(TestLength);
            }

            Assert.Equal(Direction, TestPathEdge.PathDirection);

            TestPathEdge.SaveToSQLDB();
        }

        #endregion Methods
    }
}
