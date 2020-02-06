using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace GroundFrame.Classes.SimSig
{
    /// <summary>
    /// Enum representing the different types of eras avialable
    /// </summary>
    public enum EraType
    {
        /// <summary>A standard era used by publicly visible era</summary>
        WTT = 1,
        /// <summary>A default era, not publicly visible but can be used as a template from which to generate new WTT era templates</summary>
        Template = 2
    }

    /// <summary>
    /// A class representing a Simulation Era
    /// </summary>
    public class SimulationEra : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _ID; //Stores the GroundFrame.SQL Database ID
        private int _SimID; //Stores GroundFrame.SQL Database ID of the simulation to which the era belongs
        private readonly GFSqlConnector _SQLConnector; //Stores the connection to the GroundFrame.SQL Database
        private ResourceManager _ExceptionMessageResources; //Stores the resources for the class

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Get the GroundFrame.SQL Database ID
        /// </summary>
        [Key]
        public int ID { get { return this._ID; } }

        /// <summary>
        /// Gets the Name of the Simulation Era
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets the Description of the Simulation Era
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets the Type of the Simulation Era
        /// </summary>
        [Required]
        public EraType Type { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a Simulation Era object from the supplied SqlDataReader object
        /// </summary>
        /// <param name="DataReader">The SqlDataReader object</param>
        public SimulationEra(SqlDataReader DataReader)
        {
            //Validate arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, Globals.UserSettings.GetCultureInfo());

            this.ParseDataReader(DataReader);
        }

        /// <summary>
        /// Instantiates a Simulation Era object for the supplied arguements
        /// </summary>
        /// <param name="Simulation">The simulation to which the era belongs</param>
        /// <param name="Type">The era type</param>
        /// <param name="Name">The era name</param>
        /// <param name="Description">The era decription</param>
        /// <param name="SQLConnector">A GFSqlConnector object representing a connection to the GroundFrame.SQL database</param>
        public SimulationEra(Simulation Simulation, EraType Type, string Name, string Description, GFSqlConnector SQLConnector)
        {
            //Load Resources
            this.LoadResource();

            if (Simulation == null)
            {
                throw new ArgumentNullException(this._ExceptionMessageResources.GetString("InvalidSimulationArgument", Globals.UserSettings.GetCultureInfo()));
            }

            //Check the parent simulation is saved to the GroundFrame.SQL Database
            if (Simulation.ID == 0)
            {
                throw new ArgumentException(this._ExceptionMessageResources.GetString("InvalidSimulationNotSavedToDBArgument", Globals.UserSettings.GetCultureInfo()));
            }

            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException(this._ExceptionMessageResources.GetString("InvalidSecondsArgument", Globals.UserSettings.GetCultureInfo()));
            }

            if (SQLConnector == null)
            {
                throw new ArgumentException(this._ExceptionMessageResources.GetString("InvalidSQLConnectorArgument", Globals.UserSettings.GetCultureInfo()));
            }

            this._SQLConnector = new GFSqlConnector(SQLConnector); //Create a new connection object to prevent connection / command conflicts
            this._ID = 0;
            this._SimID = Simulation.ID;
            this.Name = Name;
            this.Description = Description;
            this.Type = Type;
            this.SaveToSQLDB();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Loads resources needed by the class
        /// </summary>
        private void LoadResource()
        {
            //Get Exception Message Resources
            this._ExceptionMessageResources = new ResourceManager("ExceptionResources.resx", Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Parses a SqlDataReader object into a simulation era object
        /// </summary>
        /// <param name="DataReader"></param>
        private void ParseDataReader(SqlDataReader DataReader)
        {
            this._ID = DataReader.GetInt16(DataReader.GetOrdinal(("id")));
            this._SimID = DataReader.GetInt16(DataReader.GetOrdinal(("sim_id")));
            this.Name = DataReader.GetString(DataReader.GetOrdinal(("name")));
            this.Description = DataReader.GetString(DataReader.GetOrdinal(("description")));
            this.Type = (EraType)DataReader.GetByte(DataReader.GetOrdinal(("era_type_id")));
        }
        /// <summary>
        /// Saves the Simualtion Era to the Groundframe.SQL database
        /// </summary>
        public void SaveToSQLDB()
        {
            this.SaveSimulationEraToSQLDB();
        }

        /// <summary>
        /// Saves the Simualtion Era to the Groundframe.SQL database
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void SaveSimulationEraToSQLDB()
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_UPSERT_TSIMERA", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@sim_id", (Int16)this._SimID));
                Cmd.Parameters.Add(new SqlParameter("@name", this.Name));
                Cmd.Parameters.Add(new SqlParameter("@description", string.IsNullOrEmpty(this.Description) ? (object)DBNull.Value : this.Description));
                Cmd.Parameters.Add(new SqlParameter("@era_type_id", (byte)this.Type));
                Cmd.Parameters.Add(new SqlParameter("@id", (Int16)this._ID));
                Cmd.Parameters["@id"].Direction = ParameterDirection.InputOutput;
                Cmd.Parameters.Add(new SqlParameter("@debug", true));

                //Execute
                Cmd.ExecuteNonQuery();
                this._ID = Convert.ToInt32(Cmd.Parameters["@id"].Value);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to save simulation era record {this.Name} to the GroundFrame.SQL database", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }


        /// <summary>
        /// Disposes the Simulation Era object
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
        /// Protect implementation of the Dispose Pattern
        /// </summary>
        /// <param name="disposing">Indivates whether the SimulationEra object is being disposed</param>
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
