using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace GroundFrame.Classes.SimSig
{
    public class Location : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _ID; //Stores the Microsoft SQL Database ID of the Location record
        private int _SimID; //Stores the Microsoft SQL Database ID of the Location record
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private string _Name; //Stores the location name
        private readonly CultureInfo _Culture; //Stores the culture info

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Microsoft SQL Database ID of the Location record
        /// </summary>
        public int ID { get { return this._ID; } }

        /// <summary>
        /// Gets the Location Name
        /// </summary>
        public string Name { get { return this._Name; } }

        /// <summary>
        /// Gets ot sets the SimSig code for the Location
        /// </summary>
        public string SimSigCode { get; set; }

        /// <summary>
        /// Gets or sets the TIPLOC code for the location
        /// </summary>
        public string TIPLOC { get; set; }

        /// <summary>
        /// Gets or sets the entry point flag for the Location
        /// </summary>
        public bool EntryPoint { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Location object from the supplied arguements
        /// </summary>
        /// <param name="Simulation">The location to which the location belongs</param>
        /// <param name="Name">The location name</param>
        /// <param name="TIPLOC">The TIPLOC code for the location.</param>
        /// <param name="SimSigCode">The SimSig code for the location.</param>
        /// <param name="EntryPoint">Indicates whether the location is an entry point for the location</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public Location(Simulation Simulation, string Name, string TIPLOC, string SimSigCode, bool EntryPoint, GFSqlConnector SQLConnector, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);

            //Check Arguments
            ArgumentValidation.ValidateName(Name, this._Culture);
            ArgumentValidation.ValidateSimSigCode(SimSigCode, this._Culture, 16);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);
            ArgumentValidation.ValidateSimulation(Simulation, this._Culture);

            if (Simulation.ID == 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CreateLocationUnsavedSimError",null, this._Culture));
            }

            this._ID = 0;
            this._SimID = Simulation.ID;
            this._Name = Name;
            this.SimSigCode = SimSigCode;
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiates a new copy of the SQLConnector object to stop conflicts between Connections, Commands and Readers
            this.TIPLOC = TIPLOC;
            this.EntryPoint = EntryPoint;
        }

        /// <summary>
        /// Instantiates a new Location object from the GroundFrame.SQL database
        /// </summary>
        /// <param name="ID">The ID of the location record to be retreived</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public Location(int ID, GFSqlConnector SQLConnector, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);

            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);

            this._ID = ID;
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiates a copy of the SQLConnector object so prevent conflicts on Connections, Commands and DataReaders
            this.GetLocationFromSQLDBByID();
        }

        /// <summary>
        /// Instantiates a new Location object from the supplied SqlDataReader object
        /// </summary>
        /// <param name="DataReader">A SqlDataReader object representing a location</param>
        public Location(SqlDataReader DataReader, GFSqlConnector SQLConnector, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);

            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, this._Culture);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);

            //Instantiate a new GFSqlConnector object from the supplied connector. Stops issues with shared connections / commands and readers etc.
            this._SQLConnector = new GFSqlConnector(SQLConnector);
            //Parse Reader
            this.ParseSqlDataReader(DataReader);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Saves the location object to the Microsoft SQL database using application DateTimeOffset as the execution date / time
        /// </summary>
        public void SaveToSQLDB()
        {
            this.SaveLocationToSQLDB(null);
        }
        /// <summary>
        /// Saves the location object to the GroundFrame.SQL database using the supplied DateTimeOffset
        /// </summary>
        /// <param name="ExecutionDateTimeOffSet">The exection DateTimOffset</param>
        public void SaveToSQLDB(DateTimeOffset ExecutionDateTimeOffSet)
        {
            this.SaveLocationToSQLDB(ExecutionDateTimeOffSet);
        }


        /// <summary>
        /// Deletes the location from the GroundFrame.SQL Database
        /// </summary>
        public void DeleteFromSQLDB()
        {
            if (this._ID <= 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("DeleteLocationZeroIDError",null,this._Culture));
            }

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_DELETE_TSIM1", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@id", this._ID));
                //Execute the Query
                Cmd.ExecuteNonQuery();

                this._ID = 0; //Reset the ID
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to delete Location Record ID {0} from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Gets the location object from the GroundFrame.SQL database based on the ID
        /// </summary>
        private void GetLocationFromSQLDBByID()
        {

            if (this._ID <= 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("RetrieveLocationZeroIDError", null, this._Culture));
            }

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_GET_TLOCATION", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@id", this.ID));
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
        /// Parses a SqlDataReader object into a location object
        /// </summary>
        /// <param name="DataReader"></param>
        private void ParseSqlDataReader(SqlDataReader DataReader)
        {
            //Validate Argument
            ArgumentValidation.ValidateSqlDataReader(DataReader, this._Culture);

            this._ID = DataReader.GetInt32(DataReader.GetOrdinal("id"));
            this._SimID = DataReader.GetInt16(DataReader.GetOrdinal("sim_id"));
            this._Name = DataReader.GetString(DataReader.GetOrdinal("name"));
            this.TIPLOC = DataReader.GetNullableString("tiploc");
            this.SimSigCode = DataReader.GetString(DataReader.GetOrdinal("simsig_code"));
            this.EntryPoint = DataReader.GetBoolean(DataReader.GetOrdinal("simsig_entry_point"));
        }


        /// <summary>
        /// Saves the object the Microsoft SQL database
        /// </summary>
        /// <param name="CurrentDateTime"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void SaveLocationToSQLDB(DateTimeOffset? CurrentDateTime = null)
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_UPSERT_TLOCATION", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@sim_id", this._SimID));
                Cmd.Parameters.Add(new SqlParameter("@name", this.Name));
                Cmd.Parameters.Add(new SqlParameter("@tiploc", string.IsNullOrEmpty(this.TIPLOC) ? (object)DBNull.Value : this.TIPLOC));
                Cmd.Parameters.Add(new SqlParameter("@simsig_entry_point", this.EntryPoint));
                Cmd.Parameters.Add(new SqlParameter("@simsig_code", this.SimSigCode));
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
                throw new ApplicationException($"An error has occurred trying to save location record {this.Name} to the GroundFrame.SQL database.", Ex);
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

        ~Location()
        {
            // The object went out of scope and finalized is called
            // Lets call dispose in to release unmanaged resources 
            // the managed resources will anyways be released when GC 
            // runs the next time.
            Dispose(false);
        }
        #endregion Methods
    }
}
