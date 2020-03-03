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
    public class PathEdge : IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private variables

        private readonly LocationNode _FromLocation; //Private variable to store the from location
        private LocationNode _ToLocation; //Private variable to store the to location
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

        /// <summary>
        /// Instantiates a new PathEdge object from the supplied values
        /// </summary>
        /// <param name="FromLocation">The start location node of the path</param>
        /// <param name="ToLocation">The end location node of the path</param>
        /// <param name="SQLConnector">A connector to the GroundFrame.SQL database</param>
        /// <param name="PathElectrification">An electrification object representing the type of electrification along the path</param>
        /// <param name="PathLength">The path length</param>
        /// <param name="PathDirection">The path direction</param>
        public PathEdge(LocationNode FromLocation, LocationNode ToLocation, Electrification PathElectrification, Length PathLength, SimSigPathDirection PathDirection, GFSqlConnector SQLConnector)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());
            this._FromLocation = FromLocation;
            this._ToLocation = ToLocation;
            this._SQLConnector = new GFSqlConnector(SQLConnector);
            this.PathDirection = PathDirection;
            this.PathElectrification = PathElectrification;
            this.PathLength = PathLength;
        }

        /// <summary>
        /// Instantiates a new PathEdge object from the supplied SqlDataReader
        /// </summary>
        /// <param name="FromLocation">The start location node of the path</param>
        /// <param name="DataReader">The SqlDataReader object to parse into this path edge object</param>
        /// <param name="SQLConnector">A conector to the GroundFrame.SQL database</param>
        public PathEdge(LocationNode FromLocation, SqlDataReader DataReader, GFSqlConnector SQLConnector)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, Globals.UserSettings.GetCultureInfo());
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());

            this._FromLocation = FromLocation;
            this._SQLConnector = new GFSqlConnector(SQLConnector);

            //Parse the DataReader object
            this.ParseDataReader(DataReader);
        }

        #endregion Constructors

        #region Methods
        
        /// <summary>
        /// Deletes the Path Edge from the GroundFrame.SQL database
        /// </summary>
        public void DeleteFromSQLDB()
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_DELETE_TPATHEDGE", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@from_locationnode_id", this.FromLocation.ID));
                Cmd.Parameters.Add(new SqlParameter("@to_locationnode_id", this.ToLocation.ID));
                Cmd.Parameters.Add(new SqlParameter("@debug", true));

                //Execute
                Cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to delete path edge record (From Location ID :- {this.FromLocation.ID} | To Location ID :- {this.ToLocation.ID} | to the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Parses a SqlDataReader object into a PathEdge object
        /// </summary>
        /// <param name="DataReader">The SqlDataReader DataReader object to parse</param>
        private void ParseDataReader(SqlDataReader DataReader)
        {
            int toLocationNodeID = DataReader.GetInt32("to_locationnode_id");
            //Instantiate the To LocationNode object
            this._ToLocation = new LocationNode(toLocationNodeID, this._SQLConnector, false); //We don't need to load the Path Edges for a to location node (otherwise we'll end up in an infinite loop
            this.PathLength = DataReader.GetNullableInt16("length") == 0 ? null : new Length(DataReader.GetNullableInt16("length"));
            this.PathElectrification = DataReader.GetNullableByte("simsig_elec_bitmap") == 0 ? null : new Electrification((ElectrificationBitValue)DataReader.GetNullableByte("simsig_elec_bitmap"));
            this.PathDirection = (SimSigPathDirection)DataReader.GetNullableByte("path_direction");
        }

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

        /// <summary>
        /// Disposes the Path Edge object
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
                this._SQLConnector.Dispose();
            }
            else
            {
            }
        }

        #endregion Methods
    }
}
