using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GroundFrame.Core.SimSig
{
    /// <summary>
    /// A class representing a location node with in SimSig simulation
    /// </summary>
    /// <remarks>A location node is so labelled as a location node forms a node within the SQL Graph database. 
    /// A location can have multiple nodes representing platforms, paths and lines over multiple eras the idea that users can more accurately validate their timetables against the relevant era</remarks>
    public class LocationNode : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _ID; //Stores the GroundFrame.SQL Database ID of the Location record
        private int _SimID; //Stores the GroundFrame.SQL Database ID of the Simulation record
        private int _LocationID; //Stores the GroundFrame.SQL Database ID of the Location record
        private int _EraID; //Stores the GroundFrame.SQL Database ID of the Era record
        private string _LocationSimSigCode; //Stores the SimSig code for the location
        private Version _Version; //Stores the version object
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Microsoft SQL Database ID of the Location record
        /// </summary>
        public int ID { get { return this._ID; } }

        /// <summary>
        /// Gets the Simulation ID
        /// </summary>
        public int SimID { get { return this._SimID; } }

        /// <summary>
        /// Gets the Location ID
        /// </summary>
        public int LocationID { get { return this._LocationID; } }
        
        /// <summary>
        /// Gets the Location SimSig code
        /// </summary>
        public string LocationSimSigCode { get { return this._LocationSimSigCode; } }

        /// <summary>
        /// Gets the Era ID
        /// </summary>
        public int EraID { get { return this._EraID; } }

        /// <summary>
        /// Gets the Version the Location Node was created under
        /// </summary>
        public Version Version { get { return this._Version; } }

        /// <summary>
        /// Gets ot sets the platform for the location node
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the Electrification object for the location node
        /// </summary>
        public Electrification Electrification { get; set; }

        /// <summary>
        /// Gets or sets the location type for the location node
        /// </summary>
        public SimSigLocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the location node length
        /// </summary>
        public Length Length { get; set; }

        /// <summary>
        /// Gets or sets the Freight Only flag for the location node
        /// </summary>
        public bool FreightOnly { get; set; }

        /// <summary>
        /// Gets ot sets the line for the location node
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Gets ot sets the path for the location node
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the GFSqlConnector for the location node
        /// </summary>
        public GFSqlConnector SQLConnector { get { return this._SQLConnector; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Location node object from the supplied arguments
        /// </summary>
        /// <param name="SimulationID">The GroundFrame.SQL database id of the simulation</param>
        /// <param name="LocationID">The GroundFrame.SQL database id of the location</param>
        /// <param name="EraID">The GroundFrame.SQL database id of the simulation era</param>
        /// <param name="Version">The version of SimSig this created under</param>
        /// <param name="Platform">The platform</param>
        /// <param name="Electrification">The valid electrification options for this location node</param>
        /// <param name="LocationType">The location type of for this location node</param>
        /// <param name="Length">A length object representing the length for this location node</param>
        /// <param name="FreightOnly">Flag to indicate whether the location is freight only</param>
        /// <param name="Line">The line code</param>
        /// <param name="Path">The path code</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public LocationNode(int SimulationID, int LocationID, int EraID, Version Version, string Platform, Electrification Electrification, SimSigLocationType LocationType, Length Length, bool FreightOnly, string Line, string Path, GFSqlConnector SQLConnector)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();

            //Check Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Culture);
            this._SQLConnector = new GFSqlConnector(SQLConnector);
            ArgumentValidation.ValidateVersion(Version, Culture);
            

            if (SimulationID == 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CreateLocationNodeUnsavedSimError",null, Culture));
            }

            this._SimID = SimulationID;
            //Load simulation into a SimSig Simulation object
            using SimulationExtension LocNodeSimulation = new SimulationExtension(this.SimID, this._SQLConnector);

            //Validate locations and eras
            if (LocNodeSimulation.Locations.Any(x => x.ID == LocationID) == false)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CreateLocationNodeInvalidLocationError", null, Culture));
            }

            if (LocNodeSimulation.GetSimulationEras().Any(x => x.ID == EraID) == false)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CreateLocationNodeInvalidSimEraError", null, Culture));
            }

            //Set Properties

            this._LocationID = LocationID;
            this._LocationSimSigCode = LocNodeSimulation.Locations.Find(x => x.ID == this._LocationID).SimSigCode;
            this._EraID = EraID;
            this._Version = Version;
            this.Platform = Platform;
            this.Electrification = Electrification;
            this.LocationType = LocationType;
            this.Length = Length;
            this.FreightOnly = FreightOnly;
            this.Line = Line;
            this.Path = Path;
        }

        /// <summary>
        /// Instantiates a new Location node object from the supplied arguments
        /// </summary>
        /// <param name="SimulationID">The GroundFrame.SQL database id of the simulation</param>
        /// <param name="LocationSimSigCode">The SimSig location code of the location</param>
        /// <param name="EraID">The GroundFrame.SQL database id of the simulation era</param>
        /// <param name="Version">The version of SimSig this created under</param>
        /// <param name="Platform">The platform</param>
        /// <param name="Electrification">The valid electrification options for this location node</param>
        /// <param name="LocationType">The location type of for this location node</param>
        /// <param name="Length">A length object representing the length for this location node</param>
        /// <param name="FreightOnly">Flag to indicate whether the location is freight only</param>
        /// <param name="Line">The line code</param>
        /// <param name="Path">The path code</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public LocationNode(int SimulationID, string LocationSimSigCode, int EraID, Version Version, string Platform, Electrification Electrification, SimSigLocationType LocationType, Length Length, bool FreightOnly, string Line, string Path, GFSqlConnector SQLConnector)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();

            //Check Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Culture);
            this._SQLConnector = new GFSqlConnector(SQLConnector);
            ArgumentValidation.ValidateVersion(Version, Culture);


            if (SimulationID == 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CreateLocationNodeUnsavedSimError", null, Culture));
            }

            this._SimID = SimulationID;
            //Load simulation into a SimSig Simulation object
            using SimulationExtension LocNodeSimulation = new SimulationExtension(this.SimID, this._SQLConnector);

            //Validate locations and eras
            if (LocNodeSimulation.Locations.Any(x => x.SimSigCode == LocationSimSigCode) == false)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CreateLocationNodeInvalidLocationError", null, Culture));
            }

            if (LocNodeSimulation.GetSimulationEras().Any(x => x.ID == EraID) == false)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("CreateLocationNodeInvalidSimEraError", null, Culture));
            }

            //Set Properties
            this._LocationSimSigCode = LocationSimSigCode;
            this._LocationID = LocNodeSimulation.Locations.Find(x => x.SimSigCode == this._LocationSimSigCode).ID;
            this._EraID = EraID;
            this._Version = Version;
            this.Platform = Platform;
            this.Electrification = Electrification;
            this.LocationType = LocationType;
            this.Length = Length;
            this.FreightOnly = FreightOnly;
            this.Line = Line;
            this.Path = Path;
        }

        /// <summary>
        /// Instantiates a new Location Node object from the GroundFrame.SQL database
        /// </summary>
        /// <param name="ID">The ID of the location record to be retreived</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public LocationNode(int ID, GFSqlConnector SQLConnector)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());

            this._ID = ID;
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiates a copy of the SQLConnector object so prevent conflicts on Connections, Commands and DataReaders
            this.GetLocationNodeFromSQLDBByID();
        }

        /// <summary>
        /// Instantiates a new Location Node object from the supplied SqlDataReader object
        /// </summary>
        /// <param name="DataReader">A SqlDataReader object representing a location node</param>
        /// <param name="SQLConnector">A GFSqlConnector to the GroundFrame.SQL database</param>
        public LocationNode(SqlDataReader DataReader, GFSqlConnector SQLConnector)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();

            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, Culture);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Culture);

            //Instantiate a new GFSqlConnector object from the supplied connector. Stops issues with shared connections / commands and readers etc.
            this._SQLConnector = new GFSqlConnector(SQLConnector);
            //Parse Reader
            this.ParseSqlDataReader(DataReader);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Saves the location node object to the Microsoft SQL database using application DateTimeOffset as the execution date / time
        /// </summary>
        public void SaveToSQLDB()
        {
            this.SaveLocationNodeToSQLDB(null);
        }
        /// <summary>
        /// Saves the location node object to the GroundFrame.SQL database using the supplied DateTimeOffset
        /// </summary>
        /// <param name="ExecutionDateTimeOffSet">The exection DateTimOffset</param>
        public void SaveToSQLDB(DateTimeOffset ExecutionDateTimeOffSet)
        {
            this.SaveLocationNodeToSQLDB(ExecutionDateTimeOffSet);
        }


        /// <summary>
        /// Deletes the location from the GroundFrame.SQL Database
        /// </summary>
        public void DeleteFromSQLDB()
        {
            if (this._ID <= 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("DeleteLocationZeroIDError",null, Globals.UserSettings.GetCultureInfo()));
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
        /// Gets the location node object from the GroundFrame.SQL database based on the ID
        /// </summary>
        private void GetLocationNodeFromSQLDBByID()
        {

            if (this._ID <= 0)
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("RetrieveLocationZeroIDError", null, Globals.UserSettings.GetCultureInfo()));
            }

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_GET_TLOCATIONNODE", CommandType.StoredProcedure);
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
                throw new ApplicationException($"An error has occurred trying to retrieve LocationNode Record ID {this.ID} from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Parses a SqlDataReader object into a location node object
        /// </summary>
        /// <param name="DataReader"></param>
        private void ParseSqlDataReader(SqlDataReader DataReader)
        {
            //Validate Argument
            ArgumentValidation.ValidateSqlDataReader(DataReader, Globals.UserSettings.GetCultureInfo());

            this._ID = DataReader.GetInt32(DataReader.GetOrdinal("id"));
            this._SimID = DataReader.GetInt16(DataReader.GetOrdinal("sim_id"));
            this._LocationID = DataReader.GetInt32(DataReader.GetOrdinal("location_id"));
            this._EraID = DataReader.GetInt16(DataReader.GetOrdinal("simera_id"));
            this._Version = new Version(DataReader.GetInt16(DataReader.GetOrdinal("version_id")), this._SQLConnector);
            this.Length = DataReader.GetNullableInt16("length") == 0 ? null : new Length(DataReader.GetNullableInt16("length"));
            this.Electrification = DataReader.GetNullableByte("simsig_elec_bitmap") == 0 ? null : new Electrification((ElectrificationBitValue)DataReader.GetNullableByte("simsig_elec_bitmap"));
            this.FreightOnly = DataReader.GetBoolean(DataReader.GetOrdinal("freight_only"));
            this.Platform = DataReader.GetNullableString("simsig_platform_code");
            this.Path = DataReader.GetNullableString("simsig_path");
            this.Line = DataReader.GetNullableString("simsig_line");
            this._LocationSimSigCode = DataReader.GetNullableString("simsig_code");
            this.LocationType = (SimSigLocationType)SqlDataReaderExtensions.GetNullableByte(DataReader, "location_type_id");
        }

        /// <summary>
        /// Saves the object the Microsoft SQL database
        /// </summary>
        /// <param name="CurrentDateTime"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void SaveLocationNodeToSQLDB(DateTimeOffset? CurrentDateTime = null)
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_UPSERT_TLOCATIONNODE", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@sim_id", (Int16)this.SimID));
                Cmd.Parameters.Add(new SqlParameter("@location_id", this.LocationID));
                Cmd.Parameters.Add(new SqlParameter("@simera_id", (Int16)this.EraID));
                Cmd.Parameters.Add(new SqlParameter("@version_id", (Int16)this.Version.ID));
                Cmd.Parameters.Add(new SqlParameter("@simsig_platform_code", string.IsNullOrEmpty(this.Platform) ? (object)DBNull.Value : this.Platform));
                Cmd.Parameters.Add(new SqlParameter("@simsig_elec_bitmap", this.Electrification.BitWise));
                Cmd.Parameters.Add(new SqlParameter("@location_type_id", (byte)this.LocationType));
                Cmd.Parameters.Add(new SqlParameter("@length", this.Length == null ? (object)DBNull.Value : (Int16)this.Length.Meters));
                Cmd.Parameters.Add(new SqlParameter("@freight_only", this.FreightOnly));
                Cmd.Parameters.Add(new SqlParameter("@simsig_line", string.IsNullOrEmpty(this.Line) ? (object)DBNull.Value : this.Line));
                Cmd.Parameters.Add(new SqlParameter("@simsig_path", string.IsNullOrEmpty(this.Path) ? (object)DBNull.Value : this.Path));
                Cmd.Parameters.Add(new SqlParameter("@datetime", CurrentDateTime == null ? DateTimeOffset.Now : CurrentDateTime));
                Cmd.Parameters.Add(new SqlParameter("@id", this._ID));
                Cmd.Parameters["@id"].Direction = ParameterDirection.InputOutput;
                Cmd.Parameters.Add(new SqlParameter("@debug", true));

                //Execute
                Cmd.ExecuteNonQuery();
                this._ID = Convert.ToInt32(Cmd.Parameters["@id"].Value);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to save location node record (Location:- {this.LocationSimSigCode} | Platform:- {this.Platform} | Line:- {this.Line} | Path:- {this.Path}) to the GroundFrame.SQL database.", Ex);
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
        /// <param name="disposing">Indicates whether the object is already being disposted</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                this._Version.Dispose();
            }
            else
            {
                this._SQLConnector.Dispose();
            }
        }

        #endregion Methods
    }
}
