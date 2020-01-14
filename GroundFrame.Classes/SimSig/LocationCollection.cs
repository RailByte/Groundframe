using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Class representing a collection of locations
    /// </summary>
    public class LocationCollection : IEnumerable<Location>
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        private List<Location> _Locations = new List<Location>(); //List to store all Locations
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly int _SimID; //Stores the ID of the Simulation to which the collection relates
        #endregion Private Variables

        #region Properties
        public IEnumerator<Location> GetEnumerator() { return this._Locations.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a LocationCollection collection for the supplied Simulation from the supplied GroundFrame.SQL Connection
        /// </summary>
        /// <param name="SQLConnector"></param>
        public LocationCollection(Simulation Simulation, GFSqlConnector SQLConnector)
        {
            this._SimID = Simulation.ID;
            //Set the SQL Connector
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiated as a new copy of the SQLConnector to stop conflict issues with open connections, commands and DataReaders
            //Get the locations
            this.GetAllLocationssBySimFromSQLDB();
        }

        /// <summary>
        /// Gets the total number of locations in the collection
        /// </summary>
        public int Count { get { return this._Locations.Count; } }

        #endregion Constructors

        #region Methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the Location for the supplied Index
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public Location IndexOf(int Index)
        {
            return this._Locations[Index];
        }

        /// <summary>
        /// Gets alls the locations from the GroundFrame.SQL database
        /// </summary>
        private void GetAllLocationssBySimFromSQLDB()
        {
            this._Locations = new List<Location>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.USp_GET_TLOCATION_BY_SIM", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@sim_id", this._SimID));
                SqlDataReader DataReader = Cmd.ExecuteReader();

                //Read the records
                while (DataReader.Read())
                {
                    {
                        //Parse the DataReader into the object
                        this._Locations.Add(new Location(DataReader, this._SQLConnector));
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to retrieve all location records for Simulation ID {this._SimID} from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        #endregion Methods
    }
}
