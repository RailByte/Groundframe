using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GroundFrame.Classes.UnitTests.SimSig
{
    public class Simulation : IDisposable
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
        public Simulation()
        {
            //Set up the Data Connection
            var config = new ConfigurationBuilder().AddJsonFile("xunit.config.json").Build();
            string SQLServer = config["gfSqlServer"];
            string DBName = config["gfDbName"];
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testuserAPIKEY", SQLServer, DBName, true);
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
        /// Checks that the Simulation Properties get according to the supplied values
        /// </summary>
        [Theory]
        [InlineData("Test Name 1", "Test Description 1", "Test SimSig Wiki Line 1", "Test SimSig Code 1" )]
        [InlineData("Test Name 2", "Test Description 2", "Test SimSig Wiki Line 2", "Test SimSig Code 2")]
        public void Simulation_Constructor_FromValues_CheckProperties(string Name, string Description, string SimSigWikiLink, string SimSigCode)
        {
            Classes.SimSig.Simulation TestSim = new Classes.SimSig.Simulation(Name, Description, SimSigWikiLink, SimSigCode, this._SQLConnection);
            Assert.Equal(Name, TestSim.Name);
            Assert.Equal(Description, TestSim.Description);
            Assert.Equal(SimSigWikiLink, TestSim.SimSigWikiLink);
            Assert.Equal(SimSigCode, TestSim.SimSigCode);

        }

        /// <summary>
        /// Checks that saving a Simulation object to the database generates an ID (with no DateTime supplied)
        /// </summary>
        [Theory]
        [InlineData("Test Name 3", "Test Description 3", "Test SimSig Wiki Line 3", "Test SimSig Code 3")]
        [InlineData("Test Name 4", "Test Description 4", "Test SimSig Wiki Line 4", "Test SimSig Code 4")]
        public void Simulation_Method_SaveToSQLDB_NoDateTime(string Name, string Description, string SimSigWikiLink, string SimSigCode)
        {
            Classes.SimSig.Simulation TestSim = new Classes.SimSig.Simulation(Name, Description, SimSigWikiLink, SimSigCode, this._SQLConnection);
            TestSim.SaveToSQLDB();
            Assert.Equal(Name, TestSim.Name);
            Assert.Equal(Description, TestSim.Description);
            Assert.Equal(SimSigWikiLink, TestSim.SimSigWikiLink);
            Assert.Equal(SimSigCode, TestSim.SimSigCode);
            Assert.NotEqual(0, TestSim.ID);

        }

        /// <summary>
        /// Checks that resaving the same record doesn't create a new object
        /// </summary>
        [Theory]
        [InlineData("Test Name 5", "Test Description 5", "Test SimSig Wiki Line 5", "Test SimSig Code 5")]
        [InlineData("Test Name 6", "Test Description 6", "Test SimSig Wiki Line 6", "Test SimSig Code 6")]
        public void Simulation_Method_SaveToSQLDB_NoDateTime_CheckDuplicateUpdatesExistingRecord(string Name, string Description, string SimSigWikiLink, string SimSigCode)
        {
            Classes.SimSig.Simulation TestSim = new Classes.SimSig.Simulation(Name, Description, SimSigWikiLink, SimSigCode, this._SQLConnection);
            TestSim.SaveToSQLDB();
            int TestID = TestSim.ID;

            //Create a new object with same properties. The ID returned when saving should be the same as the first record saved
            Classes.SimSig.Simulation DupeTestSim = new Classes.SimSig.Simulation(Name, Description, SimSigWikiLink, SimSigCode, this._SQLConnection);
            DupeTestSim.SaveToSQLDB();
            Assert.Equal(TestID, DupeTestSim.ID);
        }

        /// <summary>
        /// Checks that that loading a Simulation and retrieving the same record returns the same data
        /// </summary>
        [Theory]
        [InlineData("Test Name 7", "Test Description 7", "Test SimSig Wiki Line 7", "Test SimSig Code 7")]
        public void Simulation_Method_SaveToSQLDB_NoDateTime_CheckGetFromSQLDatabase(string Name, string Description, string SimSigWikiLink, string SimSigCode)
        {
            Classes.SimSig.Simulation SetUpSim = new Classes.SimSig.Simulation(Name, Description, SimSigWikiLink, SimSigCode, this._SQLConnection);
            SetUpSim.SaveToSQLDB();
            int SetupSimID = SetUpSim.ID;

            //Retrieve the simulation record from the Groundframe.SQL database
            Classes.SimSig.Simulation TestSim = new Classes.SimSig.Simulation(SetupSimID, this._SQLConnection);
            Assert.Equal(Name, TestSim.Name);
            Assert.Equal(Description, TestSim.Description);
            Assert.Equal(SimSigWikiLink, TestSim.SimSigWikiLink);
            Assert.Equal(SimSigCode.ToLower(), TestSim.SimSigCode);
            Assert.Equal(SetupSimID, TestSim.ID);
        }

        /// <summary>
        /// Checks that saving a Simulation object to the database generates an ID (with a DateTime supplied)
        /// </summary>
        [Theory]
        [InlineData("Test Name 8", "Test Description 8", "Test SimSig Wiki Line 8", "Test SimSig Code 8")]
        [InlineData("Test Name 9", "Test Description 9", "Test SimSig Wiki Line 9", "Test SimSig Code 9")]
        public void Simulation_Method_SaveToSQLDB_WithDateTime(string Name, string Description, string SimSigWikiLink, string SimSigCode)
        {
            Classes.SimSig.Simulation TestSim = new Classes.SimSig.Simulation(Name, Description, SimSigWikiLink, SimSigCode, this._SQLConnection);

            DateTimeOffset TestDateTime = new DateTimeOffset(new DateTime(2019, 1, 1), TimeSpan.FromHours(-8));
            TestSim.SaveToSQLDB(TestDateTime);
            Assert.Equal(Name, TestSim.Name);
            Assert.Equal(Description, TestSim.Description);
            Assert.Equal(SimSigWikiLink, TestSim.SimSigWikiLink);
            Assert.Equal(SimSigCode, TestSim.SimSigCode);
            Assert.NotEqual(0, TestSim.ID);

            this._SQLConnection.Open();
            SqlCommand Cmd = this._SQLConnection.SQLCommand("SELECT createdon, modifiedon FROM [simsig].[TSIM] WHERE [id] = @id", System.Data.CommandType.Text);
            Cmd.Parameters.Add(new SqlParameter("@id", TestSim.ID));
            SqlDataReader DataReader = Cmd.ExecuteReader();

            DateTimeOffset TestCreatedOnDateTime = DateTimeOffset.UtcNow;
            DateTimeOffset TestModifiedOnDateTime = DateTimeOffset.UtcNow;

            while (DataReader.Read())
            {
                TestCreatedOnDateTime = DataReader.GetDateTimeOffset(0);
                TestModifiedOnDateTime = DataReader.GetDateTimeOffset(1);
            }

            this._SQLConnection.Close();

            Assert.Equal(TestDateTime, TestCreatedOnDateTime);
            Assert.Equal(TestDateTime, TestModifiedOnDateTime);

        }


        /// <summary>
        /// Checks that trying to create eras against an unsaved Simulation raises an error
        /// </summary>
        [Theory]
        [InlineData("Test Name 10", "Test Description 10", "Test SimSig Wiki Line 10", "Test SimSig Code 10")]
        [InlineData("Test Name 11", "Test Description 11", "Test SimSig Wiki Line 11", "Test SimSig Code 11")]
        public void Simulation_Constructor_Check_CreateEra_On_UnsavedSim(string Name, string Description, string SimSigWikiLink, string SimSigCode)
        {
            Classes.SimSig.Simulation TestSim = new Classes.SimSig.Simulation(Name, Description, SimSigWikiLink, SimSigCode, this._SQLConnection);
            Assert.Equal(Name, TestSim.Name);
            Assert.Equal(Description, TestSim.Description);
            Assert.Equal(SimSigWikiLink, TestSim.SimSigWikiLink);
            Assert.Equal(SimSigCode, TestSim.SimSigCode);
        }

        #endregion Methods

    }
}
