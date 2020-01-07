using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GroundFrame.Classes
{
    public enum EraType
    {
        WTT = 1,
        Template = 2
    }

    /// <summary>
    /// A class representing a Simulation Era
    /// </summary>
    public class SimulationEra
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _ID; //Stores the GroundFrame.SQL Database ID
        private int _SimID; //Stores GroundFrame.SQL Database ID of the simulation to which the era belongs
        private readonly GFSqlConnector _SQLConnector; //Stores the connection to the GroundFrame.SQL Database 

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
        public SimulationEra (SqlDataReader DataReader)
        {
            this.ParseDataReader(DataReader);
        }

        /// <summary>
        /// Instantiates a Simulation Era object for the supplied arguements
        /// </summary>
        /// <param name="Simulation"></param>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        public SimulationEra(Simulation Simulation, EraType Type, string Name, string Description, GFSqlConnector SQLConnector)
        {
            //Check the parent simulation is saved to the GroundFrame.SQL Database
            if (Simulation.ID == 0)
            {
                throw new ArgumentException("You cannot create a Simulation Era for a Simulation which hasn't yet been saved.");
            }

            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException("Name cannot be <NULL> or empty.");
            }

            if (SQLConnector == null)
            {
                throw new ArgumentException("SQLConnector canont be <NULL>.");
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
        private void SaveSimulationEraToSQLDB()
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_UPSERT_TSIMERA", CommandType.StoredProcedure);
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

        #endregion Methods  
    }
}
