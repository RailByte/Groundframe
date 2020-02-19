using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Xunit;

namespace GroundFrame.Core.UnitTests.Storage
{
    public class GFSqlConnector
    {

        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly string _SQLServer; //Stores the GroundFrame MS SQL Server. Grabbed from xunit.config.json
        private readonly string _DBName; //Stores the GroundFrame MS SQL Database name. Grabbed from xunit.config.json

        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a new GFSqlConnector object and grabs the Microsoft SQL database settings from xunit.config.json
        /// </summary>
        public GFSqlConnector()
        {
            //Get config values from xunit.config.json
            var config = new ConfigurationBuilder().AddJsonFile("xunit.config.json").Build();
            this._SQLServer = config["gfSqlServer"];
            this._DBName = config["gfDbName"];
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Checks that an ApplcationException is thrown is an invalid AppAPIKey or AppUserAPIKey is passed
        /// </summary>
        [Theory]
        [InlineData("InvalidAppAPIKEY", "testuserAPIKEY")]
        [InlineData("testappAPIKEY", "InvalidUserAPIKEY")]
        public void GFSqlConnector_InvalidApplicationAndUser_CheckException(string AppAPIKey, string AppUserAPIKey)
        {
            Assert.Throws<ArgumentException>(() => new GroundFrame.Core.GFSqlConnector(AppAPIKey, AppUserAPIKey, this._SQLServer, this._DBName).Open());
        }

        /// <summary>
        /// Checks that an ApplcationException is thrown if connection is invalid
        /// </summary>
        [Fact]
        public void GFSqlConnector_InvalidConnection_CheckException()
        {
            string InvalidServerName = $"{this._SQLServer}GHOGODMDORVTO"; //Build a nonsense servername
            Assert.Throws<ApplicationException>(() => new GroundFrame.Core.GFSqlConnector("testappAPIKEY", "testuserAPIKEY", InvalidServerName, this._DBName, false, 1));
        }

        /// <summary>
        /// Checks that the Session Context is fire on Connection
        /// </summary>
        [Fact]
        public void GFSqlConnector_Connection_CheckSessionContext()
        {
            Core.GFSqlConnector TestSQLConnector = new Core.GFSqlConnector("testappAPIKEY", "testuserAPIKEY", this._SQLServer, this._DBName);
            
            TestSQLConnector.Open(); //Open Connection
            SqlCommand cmd = TestSQLConnector.SQLCommand("SELECT test = CONVERT(INT,SESSION_CONTEXT(N'application'));", CommandType.Text); //Generate SqlCommand
            SqlDataReader reader = cmd.ExecuteReader(); //Execute

            int ApplicationID = 0; //Variable to store AppID from GF Microsoft SQL
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApplicationID = reader.GetInt32(0);
                }
            }
            reader.Close();
            TestSQLConnector.Close(); //Close Connection

            Assert.Equal(2, ApplicationID);
        }


        /// <summary>
        /// Checks that the Session Context is fire on Connection
        /// </summary>
        [Fact]
        public void GFSqlConnector_Method_CheckTearDown()
        {
            Core.GFSqlConnector TestSQLConnector = new Core.GFSqlConnector("testappAPIKEY", "testuserAPIKEY", this._SQLServer, this._DBName, true);
            //Generate some test data
            TestSQLConnector.GenerateTestData();
            //Tear the data down
            TestSQLConnector.TearDownData();
            
            //Check the test data has been removed from the database
            TestSQLConnector.Open(); //Open Connection
            SqlCommand Cmd = TestSQLConnector.SQLCommand("SELECT [row_count] = COUNT(*) FROM [simsig].[TSIM] WHERE [testdata_id] = @testdata_id", CommandType.Text); //Generate SqlCommand
            Cmd.Parameters.Add(new SqlParameter("@testdata_id", TestSQLConnector.TestDataID));
            SqlDataReader DataReader = Cmd.ExecuteReader();
            int SimulationRowCount = -1; //Variable to store the simulation row count
            while (DataReader.Read())
            {
                SimulationRowCount = DataReader.GetInt32(0);
            }
 
            TestSQLConnector.Close(); //Close Connection

            Assert.Equal(0, SimulationRowCount);
        }
        /// <summary>
        /// Checks that the Session Context is fire on Connection
        /// </summary>
        [Fact]
        public void GFSqlConnector_Method_GenerateTestData()
        {
            Core.GFSqlConnector TestSQLConnector = new Core.GFSqlConnector("testappAPIKEY", "testuserAPIKEY", this._SQLServer, this._DBName, true);

            TestSQLConnector.GenerateTestData();

            //Check there are 5 simulation records created.
            TestSQLConnector.Open(); //Open Connection
            SqlCommand Cmd = TestSQLConnector.SQLCommand("SELECT [row_count] = COUNT(*) FROM [simsig].[TSIM] WHERE [testdata_id] = @testdata_id", CommandType.Text); //Generate SqlCommand
            Cmd.Parameters.Add(new SqlParameter("@testdata_id", TestSQLConnector.TestDataID));
            SqlDataReader DataReader = Cmd.ExecuteReader();

            //Read the simulation records
            int SimulationRowCount = -1; //Variable to store the simulation row count
            while (DataReader.Read())
            {
                SimulationRowCount = DataReader.GetInt32(0);
            }

            TestSQLConnector.Close(); //Close Connection

            Assert.Equal(5, SimulationRowCount);

            //Tear the data down
            TestSQLConnector.TearDownData();
        }

        #endregion Methods
    }
}
