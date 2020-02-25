using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace GroundFrame.Core.SimSig
{
    /// <summary>
    /// Class representing a collection of locations
    /// </summary>
    public class LocationCollection : IEnumerable<Location>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        private List<Location> _Locations = new List<Location>(); //List to store all Locations
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly int _SimID; //Stores the ID of the Simulation to which the collection relates
        #endregion Private Variables

        #region Properties
        /// <summary>
        /// Gets the LocationCollection Enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Location> GetEnumerator() { return this._Locations.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a LocationCollection collection for the supplied Simulation from the supplied GroundFrame.SQL Connection
        /// </summary>
        /// <param name="Simulation">The simulation for which you want to inititate the LocationCOllection</param>
        /// <param name="SQLConnector">A GFSqlConnector to the GroundFrame.SQL database</param>
        public LocationCollection(Simulation Simulation, GFSqlConnector SQLConnector)
        {
            CultureInfo culture = Globals.UserSettings.GetCultureInfo();
            ArgumentValidation.ValidateSimulation(Simulation, culture);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, culture);

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

        /// <summary>
        /// Adds a location to the location collection
        /// </summary>
        /// <param name="NewLocation"></param>
        public void Add(Location NewLocation)
        {
            this._Locations.Add(NewLocation);
        }

        /// <summary>
        /// Finds a location which matches the search predicate
        /// </summary>
        /// <param name="match">The search predicate</param>
        public Location Find(Predicate<Location> match)
        {
            return this._Locations.Find(match);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the Location for the supplied index
        /// </summary>
        /// <param name="Index">The index of the location to be returned</param>
        /// <returns>The location at the supplied index value</returns>
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
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.USp_GET_TLOCATION_BY_SIM", CommandType.StoredProcedure);
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
        /// <param name="disposing">Flag to indicate whether the object is already being disposed</param>
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
