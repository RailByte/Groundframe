using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GroundFrame.Core.SimSig
{
    /// <summary>
    /// A class representing an edge between 2 location nodes
    /// </summary>
    public class PathEdge
    {
        #region Constants
        #endregion Constants

        #region Private variables

        private readonly LocationNode _FromLocation; //Private variable to store the from location
        private readonly LocationNode _ToLocation; //Private variable to store the to location
        private readonly GFSqlConnector _SQLConnector; //Private variable to store the SQL Connector

        #endregion Private variables

        #region Properties

        /// <summary>
        /// Gets the From Location Node of the Path
        /// </summary>
        public LocationNode FromLocation { get { return this._FromLocation; } }

        /// <summary>
        /// Gets the To Location Node of the Path
        /// </summary>
        public LocationNode ToLocation { get { return this._ToLocation; } }

        /// <summary>
        /// Gets or sets the electrification of the path
        /// </summary>
        public Electrification PathElectrification { get; set; }

        /// <summary>
        /// Gets or sets the path direction
        /// </summary>
        public SimSigPathDirection PathDirection { get; set; }

        /// <summary>
        /// Gets or sets the length of the path
        /// </summary>
        public Length PathLength { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new PathEdge object from the supplied values
        /// </summary>
        /// <param name="FromLocation">The start location node of the path</param>
        /// <param name="ToLocation">The end location node of the path</param>
        /// <param name="SQLConnector">A conector to the GroundFrame.SQL database</param>
        public PathEdge (LocationNode FromLocation, LocationNode ToLocation, GFSqlConnector SQLConnector)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());
            this._FromLocation = FromLocation;
            this._ToLocation = ToLocation;
            this._SQLConnector = new GFSqlConnector(SQLConnector);
            this.PathDirection = SimSigPathDirection.None;
            this.PathElectrification = new Electrification(0);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Saves the path to the GroundFrame.SQL database
        /// </summary>
        public void SaveToSQLDB()
        {
            this.SavePathToSQLDB(null);
        }

        /// <summary>
        /// Saves the path to the GroundFrame.SQL database
        /// </summary>
        /// <param name="CurrentDateTime"></param>
        private void SavePathToSQLDB(DateTimeOffset? CurrentDateTime = null)
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_UPSERT_TPATHEDGE", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@from_locationnode_id", this.FromLocation.ID));
                Cmd.Parameters.Add(new SqlParameter("@to_locationnode_id", this.ToLocation.ID));
                Cmd.Parameters.Add(new SqlParameter("@simsig_elec_bitmap", this.PathElectrification.BitWise));
                Cmd.Parameters.Add(new SqlParameter("@length", this.PathLength == null ? (object)DBNull.Value : (Int16)this.PathLength.Meters));
                Cmd.Parameters.Add(new SqlParameter("@datetime", CurrentDateTime == null ? DateTimeOffset.Now : CurrentDateTime));
                Cmd.Parameters.Add(new SqlParameter("@path_direction", (byte)this.PathDirection));
                Cmd.Parameters.Add(new SqlParameter("@debug", true));

                //Execute
                Cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to save path edge record (From Location ID :- {this.FromLocation.ID} | To Location ID :- {this.ToLocation.ID} | to the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        #endregion Methods
    }
}
