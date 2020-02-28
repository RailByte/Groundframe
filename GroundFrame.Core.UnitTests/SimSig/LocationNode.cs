using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using GroundFrame.Core.SimSig;

namespace GroundFrame.Core.UnitTests.SimSig
{
    public class LocationNode : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly GFSqlConnector _SQLConnection;
        private readonly Core.SimSig.Simulation _TestSimulation;
        private readonly Core.SimSig.SimulationExtension _TestSimulationExt;
        private readonly Core.SimSig.Location _TestLocation;
        private readonly Core.SimSig.Version _TestVersion;

        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Location Test object
        /// </summary>
        public LocationNode()
        {
            //Set up the Data Connection
            var config = new ConfigurationBuilder().AddJsonFile("xunit.config.json").Build();
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testadminuserAPIKEY", SQLServer, DBName, true); //Need to log in as admin

            //Create a Test Simulation and save to DB

            this._TestSimulation = new Core.SimSig.Simulation("Test LocationNode Sim Name", "Test LocationNode Sim Desc", null, "TestLocationNodeSimCode", this._SQLConnection);
            this._TestSimulation.SaveToSQLDB();
            this._TestLocation = new Core.SimSig.Location(this._TestSimulation, "Test LocNode Loc Name", null, "TestLocNodeCode", false, Core.SimSig.SimSigLocationType.Station, this._SQLConnection);
            this._TestLocation.SaveToSQLDB();
            this._TestSimulationExt = new SimulationExtension(this._TestLocation.ID, this._SQLConnection);
            this._TestSimulationExt.Locations.Add(this._TestLocation);
            this._TestVersion = new Core.SimSig.Version("Test LocNode Version", "Test LocCode Version", 4.15M, this._SQLConnection);
            this._TestVersion.SaveToSQLDB();
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
        /// Test instantiating a LocationNode with a location SimSigCode etc
        /// </summary>
        [Theory]
        [InlineData(null, "D", SimSigLocationType.Station, null, false, null, null)]
        [InlineData("1", "DX13", SimSigLocationType.Unknown, 100, false, "FL", "SL")]
        [InlineData("1F", "D4", SimSigLocationType.TimingPoint, 200, true, "FL", null)]
        [InlineData("12R", "O3", SimSigLocationType.YardSiding, 15, false, null, "SL")]
        public void LocationNode_Constructor_ByProperties_SimSigCode(string Platform, string Electrification, SimSigLocationType LocationType, int? Length, bool FreightOnly, string Line, string Path)
        {
            Length TestLength = null;

            if (Length != null)
            {
                TestLength = new Length(Convert.ToInt32(Length));
            }

            Core.SimSig.LocationNode TestLocationNode = new Core.SimSig.LocationNode(this._TestSimulation.ID, this._TestLocation.SimSigCode, this._TestSimulation.GetSimulationEras().Find(x => x.Type == EraType.Template).ID, this._TestVersion, Platform, new Core.Electrification(Electrification), LocationType, TestLength, FreightOnly, Line, Path, this._SQLConnection);
            Assert.Equal(LocationType, TestLocationNode.LocationType);
            Assert.Equal(Platform, TestLocationNode.Platform);
            Assert.Equal(Line, TestLocationNode.Line);
            Assert.Equal(Path, TestLocationNode.Path);
            Assert.Equal(this._TestLocation.SimSigCode, TestLocationNode.LocationSimSigCode);
            Assert.Equal(this._TestLocation.ID, TestLocationNode.LocationID);

            if (Length == null)
            {
                Assert.Null(TestLocationNode.Length);
            }
            else
            {
                Assert.Equal(TestLength.Meters, TestLocationNode.Length.Meters);
            }

            Assert.Equal(FreightOnly, TestLocationNode.FreightOnly);
            Assert.Equal(new Electrification(Electrification).BitWise, TestLocationNode.Electrification.BitWise);
            Assert.Equal(0, TestLocationNode.ID);

        }

        /// <summary>
        /// Test instantiating a LocationNode with a location SimSigCode etc and then saving
        /// </summary>
        [Theory]
        [InlineData(null, "D", SimSigLocationType.Station, null, false, null, null)]
        [InlineData("1", "DX13", SimSigLocationType.Unknown, 100, false, "FL", "SL")]
        [InlineData("1F", "D4", SimSigLocationType.TimingPoint, 200, true, "FL", null)]
        [InlineData("12R", "O3", SimSigLocationType.YardSiding, 15, false, null, "SL")]
        public void LocationNode_Method_SavetoDB(string Platform, string Electrification, SimSigLocationType LocationType, int? Length, bool FreightOnly, string Line, string Path)
        {
            Length TestLength = null;

            if (Length != null)
            {
                TestLength = new Length(Convert.ToInt32(Length));
            }

            Core.SimSig.LocationNode TestLocationNode = new Core.SimSig.LocationNode(this._TestSimulation.ID, this._TestLocation.SimSigCode, this._TestSimulation.GetSimulationEras().Find(x => x.Type == EraType.Template).ID, this._TestVersion, Platform, new Core.Electrification(Electrification), LocationType, TestLength, FreightOnly, Line, Path, this._SQLConnection);
            TestLocationNode.SaveToSQLDB();
            Assert.Equal(LocationType, TestLocationNode.LocationType);
            Assert.Equal(Platform, TestLocationNode.Platform);
            Assert.Equal(Line, TestLocationNode.Line);
            Assert.Equal(Path, TestLocationNode.Path);
            Assert.Equal(this._TestLocation.SimSigCode, TestLocationNode.LocationSimSigCode);
            Assert.Equal(this._TestLocation.ID, TestLocationNode.LocationID);

            if (Length == null)
            {
                Assert.Null(TestLocationNode.Length);
            }
            else
            {
                Assert.Equal(TestLength.Meters, TestLocationNode.Length.Meters);
            }

            Assert.Equal(FreightOnly, TestLocationNode.FreightOnly);
            Assert.Equal(new Electrification(Electrification).BitWise, TestLocationNode.Electrification.BitWise);
            Assert.NotEqual(0, TestLocationNode.ID);

        }

        /// <summary>
        /// Test instantiating a LocationNode with a location SimSigCode etc and then saving
        /// </summary>
        [Theory]
        [InlineData(null, "D", SimSigLocationType.Station, null, false, null, null)]
        [InlineData("1", "DX13", SimSigLocationType.Unknown, 100, false, "FL", "SL")]
        [InlineData("1F", "D4", SimSigLocationType.TimingPoint, 200, true, "FL", null)]
        [InlineData("12R", "O3", SimSigLocationType.YardSiding, 15, false, null, "SL")]
        public void LocationNode_Constructor_GroundFrameDBID(string Platform, string Electrification, SimSigLocationType LocationType, int? Length, bool FreightOnly, string Line, string Path)
        {
            Length TestLength = null;

            if (Length != null)
            {
                TestLength = new Length(Convert.ToInt32(Length));
            }

            SimulationEra SimEra = this._TestSimulation.GetSimulationEras().Find(x => x.Type == EraType.Template);
            Core.Electrification ElecObject = new Core.Electrification(Electrification);
            Core.SimSig.LocationNode TestLocationNode = new Core.SimSig.LocationNode(this._TestSimulation.ID, this._TestLocation.SimSigCode, SimEra.ID, this._TestVersion, Platform, ElecObject, LocationType, TestLength, FreightOnly, Line, Path, this._SQLConnection);
            TestLocationNode.SaveToSQLDB();
            Assert.Equal(LocationType, TestLocationNode.LocationType);
            Assert.Equal(Platform, TestLocationNode.Platform);
            Assert.Equal(Line, TestLocationNode.Line);
            Assert.Equal(Path, TestLocationNode.Path);
            Assert.Equal(this._TestLocation.SimSigCode, TestLocationNode.LocationSimSigCode);
            Assert.Equal(this._TestLocation.ID, TestLocationNode.LocationID);

            if (Length == null)
            {
                Assert.Null(TestLocationNode.Length);
            }
            else
            {
                Assert.Equal(TestLength.Meters, TestLocationNode.Length.Meters);
            }

            Assert.Equal(FreightOnly, TestLocationNode.FreightOnly);
            Assert.Equal(new Electrification(Electrification).BitWise, TestLocationNode.Electrification.BitWise);
            Assert.NotEqual(0, TestLocationNode.ID);

            //Load the LocationNode into a new object and compare
            Core.SimSig.LocationNode TestLoadLocationNode = new Core.SimSig.LocationNode(TestLocationNode.ID, this._SQLConnection);

            Assert.Equal(TestLocationNode.LocationType, TestLoadLocationNode.LocationType);
            Assert.Equal(TestLocationNode.Platform, TestLoadLocationNode.Platform);
            Assert.Equal(TestLocationNode.Line, TestLoadLocationNode.Line);
            Assert.Equal(TestLocationNode.Path, TestLoadLocationNode.Path);
            Assert.Equal(TestLocationNode.LocationSimSigCode, TestLoadLocationNode.LocationSimSigCode);
            Assert.Equal(TestLocationNode.LocationID, TestLoadLocationNode.LocationID);

            if (TestLocationNode.Length == null)
            {
                Assert.Null(TestLoadLocationNode.Length);
            }
            else
            {
                Assert.Equal(TestLocationNode.Length.Meters, TestLoadLocationNode.Length.Meters);
            }

            Assert.Equal(TestLocationNode.FreightOnly, TestLoadLocationNode.FreightOnly);
            Assert.Equal(TestLocationNode.Electrification.BitWise, TestLoadLocationNode.Electrification.BitWise);
            Assert.Equal(TestLocationNode.ID, TestLoadLocationNode.ID);
        }

        #endregion Methods
    }
}
