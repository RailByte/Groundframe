using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace GroundFrame.Core
{
    /// <summary>
    /// An enum to represent the various roles available in the GroundFrame.SQL Database
    /// </summary>
    public enum SQLRole
    {
        /// <summary>
        /// A standard user role with no editing rights on the SimSig side
        /// </summary>
        Standard = 1,
        /// <summary>
        /// The editor role which allows a user to create and edit simulation data. Can only be assigned by a user in the Admin role
        /// </summary>
        Editor = 2,
        /// <summary>
        /// The admin role which allows a user god rights
        /// </summary>
        Admin = 4
    }

    /// <summary>
    /// A class representing a connection to a GroundFrame.SQL database. Designed to enable a consistent approach to connecting to a SQL database
    /// </summary>
    public class GFSqlConnector : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly string _ApplicationAPIKey; //Variable to store the Application API Key
        private readonly string _ApplicationUserAPIKey; //Variable to store the Application User API Key
        private readonly string _SQLServer; //Variable to store the SQL Server
        private readonly string _DBName; //Variable to store the DBName
        private readonly SqlConnection _Connection;
        private readonly bool _IsTest; //Flag to indicate whether the connector is running as part of a test and therefore the TearDown method can be called
        private Guid _TestDataID; //Stores the ID of the test data
        private readonly int _Timeout; //Store the timeout of the connection in seconds

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Microsoft SQL Server name
        /// </summary>
        public string SQLServer { get { return this._SQLServer; } }

        /// <summary>
        /// Gets the database name
        /// </summary>
        public string DBName { get { return this._DBName; } }

        /// <summary>
        /// Gets the Test Data ID. Generated from calling GenerateTestData to generate a set of test data.
        /// </summary>
        public Guid TestDataID { get { return this._TestDataID; } }

        /// <summary>
        /// Gets the Test Data Flag to indicate whether connection is being used for running a test
        /// </summary>
        public bool IsTest { get { return this._IsTest; } }

        /// <summary>
        /// Gets the Application key
        /// </summary>
        public string ApplicationAPIKey { get { return this._ApplicationAPIKey; } }

        /// <summary>
        /// Gets the Application User Key
        /// </summary>
        public string ApplicationUserAPIKey { get { return this._ApplicationUserAPIKey; } }

        /// <summary>
        /// Gets the Timeout of the connection in seconds
        /// </summary>
        public int Timeout { get { return this._Timeout; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new GFSqlConnector object from the supplied arguments
        /// </summary>
        /// <param name="AppAPIKey">The API key of the of the application being used to connecto the GroundFrame.SQL database</param>
        /// <param name="AppUserAPIKey">The users' API key.</param>
        /// <param name="SQLServer">The SQL Server which the GroundFrame.SQL database is hosted</param>
        /// <param name="DBName">The name of the GroundFrame.SQL database to connect to</param>
        /// <param name="IsTest">This is used by the testing environment and will delete any data added whilst the connection is open to leave a clean database. Default = false</param>
        /// <param name="Timeout">The number of seconds before the connection times out. Default 30.</param>
        public GFSqlConnector(string AppAPIKey, string AppUserAPIKey, string SQLServer, string DBName, bool IsTest = false, int Timeout = 30)
        {
            this._ApplicationAPIKey = AppAPIKey;
            this._ApplicationUserAPIKey = AppUserAPIKey;
            this._SQLServer = SQLServer;
            this._DBName = DBName;
            this._IsTest = IsTest;
            this._TestDataID = IsTest ? Guid.NewGuid() : Guid.Empty;
            this._Timeout = Timeout;
            this._Connection = new SqlConnection(this.BuildSQLConnectionString());

            if (!this.TestConnection())
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("CannotConnectGFSQLDb", null, Globals.UserSettings.GetCultureInfo()));
            }
        }

        /// <summary>
        /// Instatiates a new GFSqlConnector as a copy of the supplied one. Stops conflicts between connectors on sub query execution
        /// </summary>
        /// <param name="SQLConnector"></param>
        internal GFSqlConnector(GFSqlConnector SQLConnector)
        {
            //Validate argument
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());

            this._ApplicationAPIKey = SQLConnector.ApplicationAPIKey;
            this._ApplicationUserAPIKey = SQLConnector.ApplicationUserAPIKey;
            this._SQLServer = SQLConnector.SQLServer;
            this._DBName = SQLConnector.DBName;
            this._IsTest = SQLConnector.IsTest;
            this._TestDataID = SQLConnector.TestDataID;
            this._Timeout = SQLConnector.Timeout;
            this._Connection = new SqlConnection(this.BuildSQLConnectionString());
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Builds the SQL Connection String
        /// </summary>
        /// <returns></returns>
        private string BuildSQLConnectionString()
        {
            string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            int Timeout = this.Timeout <= 0 || this.Timeout > 300 ? 30 : this.Timeout; //Stops a silly timeout value being set!
            string ConnectionString =  $"Data Source={this.SQLServer};Initial Catalog={this.DBName};Integrated Security=SSPI;Application Name={AppName};Connect Timeout={Timeout}";
            return ConnectionString;
        }

        /// <summary>
        /// Method to test the connection. 
        /// </summary>
        /// <returns></returns>
        private bool TestConnection()
        {
            try
            {
                this._Connection.Open();
                this._Connection.Close();

                return true;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                //Don't need the method to throw an exception
                return false;
            }
        }

        /// <summary>
        /// Opens the Connection to the SQL database
        /// </summary>
        public void Open()
        {
            //Open the SqlConnection
            this._Connection.Open();

            try
            {
                //Set the Session Context
                SqlCommand cmd = new SqlCommand("common.Usp_SET_SESSIONCONTEXT", this._Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@app_api_key", this._ApplicationAPIKey));
                cmd.Parameters.Add(new SqlParameter("@app_user_api_key", this._ApplicationUserAPIKey));

                if (this._TestDataID != Guid.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@testdata_id", this._TestDataID));
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception Ex)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CannotConnectGFSQLDb", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
            
        }
        /// <summary>
        /// Closes the Connection to the SQL database
        /// </summary>
        public void Close()
        {
            //Close the SqlConnection
            this._Connection.Close();
        }


        /// <summary>
        /// Gets a SqlCommand object build from a command string and command type
        /// </summary>
        /// <param name="CommandString">The command string to execute</param>
        /// <param name="Type">The command type</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "<Pending>")]
        public SqlCommand SQLCommand (string CommandString, CommandType Type)
        {
            SqlCommand Command = new SqlCommand(CommandString, this._Connection)
            {
                CommandType = Type
            };

            return Command;
        }

        /// <summary>
        /// Disposes the GFSqlConnector object
        /// </summary>
        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Tears down any test data but will only work if the GFSqlConnector is configured for a test
        /// </summary>
        public void TearDownData()
        {
            if (this._IsTest == false)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("TearDownError", null, Globals.UserSettings.GetCultureInfo()));
            }
            else
            {
                //Open the SqlConnection
                this.Open();

                //Set Tear Down Flag

                using (SqlCommand cmd = new SqlCommand("sys.sp_set_session_context", this._Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@key", "can_tear_down"));
                    cmd.Parameters.Add(new SqlParameter("@value", true));
                    cmd.ExecuteNonQuery();
                }

                //Clear Down Data
                using (SqlCommand cmd = new SqlCommand("common.Usp_TEARDOWN_TESTDATA", this._Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@testdata_id", this._TestDataID));
                    cmd.ExecuteNonQuery();
                }

                //Clear Test Down Flag
                using (SqlCommand cmd = new SqlCommand("sys.sp_set_session_context", this._Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@key", "can_tear_down"));
                    cmd.Parameters.Add(new SqlParameter("@value", false));
                    cmd.ExecuteNonQuery();
                }

                this.Close();
            }
        }

        /// <summary>
        /// Gets the role enum for the current user
        /// </summary>
        /// <returns>SQLRole enum representing the roles the user has access to</returns>
        public SQLRole GetRole()
        {
            SQLRole Result = 0;

            this._Connection.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT [role] = [app].[Fn_GET_USERROLE]()", this._Connection))
            {
                cmd.CommandType = CommandType.Text;
                SqlDataReader DataReader = cmd.ExecuteReader();

                while (DataReader.Read())
                {
                    Result = (SQLRole)DataReader.GetInt32(DataReader.GetOrdinal("role"));
                }

            }

            return Result;
        }


        /// <summary>
        /// Generates a set of test data and returns an ID which can be passed to the TearDown method
        /// </summary>
        /// <returns></returns>
        public void GenerateTestData(int RecordsToGenerate = 5)
        {
            Guid TestDataID = Guid.Empty;
            this._Connection.Open();
            using (SqlCommand Cmd = new SqlCommand("common.Usp_GENERATE_TESTDATA", this._Connection))
            {
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add(new SqlParameter("@records_to_generate", RecordsToGenerate));
                Cmd.Parameters.Add(new SqlParameter("@testdata_id", TestDataID));
                Cmd.Parameters["@testdata_id"].Direction = ParameterDirection.Output;
                Cmd.ExecuteNonQuery();
                this._Connection.Close();
                this._TestDataID = (Guid)Cmd.Parameters["@testdata_id"].Value; //Set the Simulation ID
            };

        }

        /// <summary>
        /// Protected override of the Dispose method
        /// </summary>
        /// <param name="disposing">A flag to indicate whether the object is already being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //someone want the deterministic release of all resources
                //Let us release all the managed resources
                if (this._Connection != null)
                {
                    this._Connection.Dispose();
                }
            }
            else
            {
                // Do nothing, no one asked a dispose, the object went out of
                // scope and finalized is called so lets next round of GC 
                // release these resources
            }

            // Release the unmanaged resource in any case as they will not be 
            // released by GC
        }

        #endregion Methods
    }
}
