using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace GroundFrame.Classes.SimSig
{
    /// <summary>
    /// A class representing a SimSig simulation
    /// </summary>
    public class Simulation : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _ID; //Stores the Microsoft SQL Database ID of the Simulation record
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private string _Name; //Stores the Simulation Name
        private string _SimSigCode; //Stores the SimSig Code

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Microsoft SQL Database ID of the Simulation record
        /// </summary>
        public int ID { get { return this._ID; } }

        /// <summary>
        /// Gets the Simulation Name
        /// </summary>
        public string Name { get { return this._Name; } }

        /// <summary>
        /// Gets or sets the Simulation Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Simulation Wiki URL
        /// </summary>
        public string SimSigWikiLink { get; set; }
        
        /// <summary>
        /// Gets the SimSig code for the Simulation
        /// </summary>
        public string SimSigCode { get { return this._SimSigCode; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Simulation object from the supplied arguements
        /// </summary>
        /// <param name="Name">The simulation name. This cannot be altered once set</param>
        /// <param name="Description">A description of the simulation</param>
        /// <param name="SimSigWikiLink">The URL to the simulation manula on the SimSig wiki</param>
        /// <param name="SimSigCode">The SimSig code for the simulation. This cannot be altered once set</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public Simulation(string Name, string Description, string SimSigWikiLink, string SimSigCode, GFSqlConnector SQLConnector)
        {
            CultureInfo culture = Globals.UserSettings.GetCultureInfo();

            //Validate arguments
            ArgumentValidation.ValidateName(Name, culture);
            ArgumentValidation.ValidateSimSigCode(SimSigCode, culture);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, culture);


            this._ID = 0;
            this._Name = Name;
            this.Description = Description;
            this.SimSigWikiLink = SimSigWikiLink;
            this._SimSigCode = SimSigCode;
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiates a new copy of the SQLConnector object to stop conflicts between Connections, Commands and Readers
        }

        /// <summary>
        /// Instantiates a new Simulation object from the GroundFrame.SQL database
        /// </summary>
        /// <param name="ID">The ID of the Simulation record to be retreived</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public Simulation(int ID, GFSqlConnector SQLConnector)
        {
            //Validate arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());

            this._ID = ID;
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiates a copy of the SQLConnector object so prevent conflicts on Connections, Commands and DataReaders
            this.GetSimulationFromSQLDBByID();
        }

        /// <summary>
        /// Instantiates a new Simulation object from the supplied SqlDataReader object
        /// </summary>
        /// <param name="DataReader">A SqlDataReader object representing a Simulation</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public Simulation(SqlDataReader DataReader, GFSqlConnector SQLConnector)
        {
            CultureInfo culture = Globals.UserSettings.GetCultureInfo();

            //Validate arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, culture);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, culture);

            //Instantiate a new GFSqlConnector object from the supplied connector. Stops issues with shared connections / commands and readers etc.
            this._SQLConnector = new GFSqlConnector(SQLConnector);
            //Parse Reader
            this.ParseSqlDataReader(DataReader);           
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Refreshes the Simulation data from the GroundFrame.SQL database
        /// </summary>
        public void RefreshFromSQLDB()
        {
            if (this._ID == 0)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("RefreshSimulationError",null,Globals.UserSettings.GetCultureInfo()));
            }

            this.GetSimulationFromSQLDBByID();
        }

        /// <summary>
        /// Saves the Simulation object to the Microsoft SQL database using application DateTimeOffset as the execution date / time
        /// </summary>
        public void SaveToSQLDB()
        {
            this.SaveSimulationToSQLDB(null);
        }
        /// <summary>
        /// Saves the Simulation object to the GroundFrame.SQL database using the supplied DateTimeOffset
        /// </summary>
        /// <param name="ExecutionDateTimeOffSet">The exection DateTimOffset</param>
        public void SaveToSQLDB(DateTimeOffset ExecutionDateTimeOffSet)
        {
            this.SaveSimulationToSQLDB(ExecutionDateTimeOffSet);
        }


        /// <summary>
        /// Deletes the Simulation from the GroundFrame.SQL Database
        /// </summary>
        public void DeleteFromSQLDB()
        {
            if (this._ID <= 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("DeleteSimulationZeroIDError",null,Globals.UserSettings.GetCultureInfo()));
            }

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_DELETE_TSIM", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@id", this._ID));
                //Execute the Query
                Cmd.ExecuteNonQuery();

                this._ID = 0; //Reset the ID
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to delete Simuation Record ID {0} from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Gets the simulation object from the GroundFrame.SQL database based on the ID
        /// </summary>
        private void GetSimulationFromSQLDBByID()
        {

            if (this._ID <= 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("RetrieveSimulationZeroIDError", null, Globals.UserSettings.GetCultureInfo()));
            }

            try
            {
                //Open the Connection
                this._SQLConnector.Open();

                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_GET_TSIM", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@id", this._ID));
                SqlDataReader DataReader = Cmd.ExecuteReader();

                while (DataReader.Read())
                {
                    //Parse the DataReader into the version object
                    this.ParseSqlDataReader(DataReader);
                }

            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to retrieve Simuation Record ID {this.ID} from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Parses a SqlDataReader object into a simulation object
        /// </summary>
        /// <param name="DataReader"></param>
        private void ParseSqlDataReader(SqlDataReader DataReader)
        {
            this._ID = DataReader.GetInt16(DataReader.GetOrdinal("id"));
            this._Name = DataReader.GetString(DataReader.GetOrdinal("name"));
            this.Description = DataReader.GetString(DataReader.GetOrdinal("description"));
            this._SimSigCode = DataReader.GetString(DataReader.GetOrdinal("simsig_code"));
            this.SimSigWikiLink = DataReader.GetString(DataReader.GetOrdinal("simsig_wiki_link"));
        }


        /// <summary>
        /// Saves the object the Microsoft SQL database
        /// </summary>
        /// <param name="CurrentDateTime"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void SaveSimulationToSQLDB(DateTimeOffset? CurrentDateTime = null)
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_UPSERT_TSIM", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@name", this.Name));
                Cmd.Parameters.Add(new SqlParameter("@description", string.IsNullOrEmpty(this.Description) ? (object)DBNull.Value : this.Description));
                Cmd.Parameters.Add(new SqlParameter("@simsig_wiki_link", string.IsNullOrEmpty(this.SimSigWikiLink) ? (object)DBNull.Value : this.SimSigWikiLink));
                Cmd.Parameters.Add(new SqlParameter("@simsig_code", this._SimSigCode));
                Cmd.Parameters.Add(new SqlParameter("@datetime", CurrentDateTime == null ? DateTimeOffset.Now : CurrentDateTime));
                Cmd.Parameters.Add(new SqlParameter("@id", (Int16)this._ID));
                Cmd.Parameters["@id"].Direction = ParameterDirection.InputOutput;
                Cmd.Parameters.Add(new SqlParameter("@debug", true));

                //Execute
                Cmd.ExecuteNonQuery();
                this._ID = Convert.ToInt32(Cmd.Parameters["@id"].Value);

            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to save simulation record {this.Name} to the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
         }

        /// <summary>
        /// Disposes the Simulation object
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
        /// Protected override of the Dispose method
        /// </summary>
        /// <param name="disposing">Indicates whether the object is alreadt being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {

            }
            else
            {
                this._SQLConnector.Dispose();
            }
        }

        #endregion Methods
    }
}
