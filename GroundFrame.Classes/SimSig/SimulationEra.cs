﻿using System;
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
    public enum EraType
    {
        WTT = 1,
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
        private CultureInfo _Culture; //Stores the culture info

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
        /// <param name="DataReader"></param>
        public SimulationEra(SqlDataReader DataReader, string Culture)
        {
            this._Culture = new CultureInfo(Culture);
            //Validate arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, this._Culture);

            this.ParseDataReader(DataReader);
        }

        /// <summary>
        /// Instantiates a Simulation Era object for the supplied arguements
        /// </summary>
        /// <param name="Simulation"></param>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        public SimulationEra(Simulation Simulation, EraType Type, string Name, string Description, GFSqlConnector SQLConnector, string Culture = "en-GB")
        {
            //Load Resources
            this.LoadResource(Culture);

            if (Simulation == null)
            {
                throw new ArgumentNullException(this._ExceptionMessageResources.GetString("InvalidSimulationArgument", this._Culture));
            }

            //Check the parent simulation is saved to the GroundFrame.SQL Database
            if (Simulation.ID == 0)
            {
                throw new ArgumentException(this._ExceptionMessageResources.GetString("InvalidSimulationNotSavedToDBArgument", this._Culture));
            }

            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException(this._ExceptionMessageResources.GetString("InvalidSecondsArgument", this._Culture));
            }

            if (SQLConnector == null)
            {
                throw new ArgumentException(this._ExceptionMessageResources.GetString("InvalidSQLConnectorArgument", this._Culture));
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
        /// <param name="Culture"></param>
        private void LoadResource(string Culture)
        {
            //Get Exception Message Resources
            this._ExceptionMessageResources = new ResourceManager("ExceptionResources.resx", Assembly.GetExecutingAssembly());
            this._Culture = new CultureInfo(Culture);
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

        ~SimulationEra()
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
