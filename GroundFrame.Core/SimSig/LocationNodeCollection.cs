using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GroundFrame.Core.SimSig
{
    /// <summary>
    /// Class representing a collection of location nodes
    /// </summary>
    public class LocationNodeCollection : IEnumerable<LocationNode>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        private List<LocationNode> _LocationNodes = new List<LocationNode>(); //List to store all Location node
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly int _SimID; //Stores the ID of the Simulation to which the collection relates
        #endregion Private Variables

        #region Properties
        /// <summary>
        /// Gets the LocationNodeCollection Enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<LocationNode> GetEnumerator() { return this._LocationNodes.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a LocationNodeCollection for the supplied Simulation from the supplied GroundFrame.SQL Connection
        /// </summary>
        /// <param name="Simulation">The simulation for which you want to inititate the LocationCOllection</param>
        /// <param name="SQLConnector">A GFSqlConnector to the GroundFrame.SQL database</param>
        public LocationNodeCollection(Simulation Simulation, GFSqlConnector SQLConnector)
        {
            CultureInfo culture = Globals.UserSettings.GetCultureInfo();
            ArgumentValidation.ValidateSimulation(Simulation, culture);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, culture);

            this._SimID = Simulation.ID;
            //Set the SQL Connector
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiated as a new copy of the SQLConnector to stop conflict issues with open connections, commands and DataReaders
            //Get the locations
            this.GetAllLocationNodesBySimFromSQLDB();
        }

        /// <summary>
        /// Gets the total number of location nodes in the collection
        /// </summary>
        public int Count { get { return this._LocationNodes.Count; } }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds a location node to the location node collection
        /// </summary>
        /// <param name="NewLocationNode"></param>
        public void Add(LocationNode NewLocationNode)
        {
            this._LocationNodes.Add(NewLocationNode);
        }


        /// <summary>
        /// Checks to see whether the supplied location already exists within the collection
        /// </summary>
        /// <param name="NewLocationNode"></param>
        /// <returns>True if the location node already exists in the collection otherwise false</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Codes wil always be en-GB")]
        public bool Exists(LocationNode NewLocationNode)
        {
            return this._LocationNodes.Any(x => x.SimID == NewLocationNode.SimID
            && x.EraID == NewLocationNode.EraID
            && x.LocationID == NewLocationNode.LocationID
            && x.Version.ID == NewLocationNode.Version.ID
            && String.CompareOrdinal(x.Platform, NewLocationNode.Platform) == 0
            && String.CompareOrdinal(x.Line, NewLocationNode.Line) == 0
            && String.CompareOrdinal(x.Path, NewLocationNode.Path) == 0);
            //&& x.LocationSimSigCode.ToLower() == NewLocationNode.LocationSimSigCode.ToLower());
        }

        /// <summary>
        /// Checks to see whether the supplied location already exists within the collection
        /// </summary>
        /// <param name="NewLocationNode"></param>
        /// <returns>True if the location node already exists in the collection otherwise false</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Codes wil always be en-GB")]
        public LocationNode Find(SimSig.Simulation Simulation, SimSig.SimulationEra Era, SimSig.Version Version, string LocationSimSigCode, string Platform, string Line, string Path )
        {
            return this._LocationNodes.Find(x => x.SimID == Simulation.ID
            && x.EraID == Era.ID
            && x.Version.ID == Version.ID
            && String.CompareOrdinal(x.LocationSimSigCode, LocationSimSigCode) == 0
            && String.CompareOrdinal(x.Platform, Platform) == 0
            && String.CompareOrdinal(x.Line, Line) == 0
            && String.CompareOrdinal(x.Path, Path) == 0);
        }

        /// <summary>
        /// Finds a location node which matches the search predicate
        /// </summary>
        /// <param name="match">The search predicate</param>
        public LocationNode Find(Predicate<LocationNode> match)
        {
            return this._LocationNodes.Find(match);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the LocationNode for the supplied index
        /// </summary>
        /// <param name="Index">The index of the location to be returned</param>
        /// <returns>The location at the supplied index value</returns>
        public LocationNode IndexOf(int Index)
        {
            return this._LocationNodes[Index];
        }

        /// <summary>
        /// Gets alls the location nodes from the GroundFrame.SQL database
        /// </summary>
        private void GetAllLocationNodesBySimFromSQLDB()
        {
            this._LocationNodes = new List<LocationNode>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.USp_GET_TLOCATIONNODE_BY_SIM", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@sim_id", this._SimID));
                SqlDataReader DataReader = Cmd.ExecuteReader();

                //Read the records
                while (DataReader.Read())
                {
                    {
                        //Parse the DataReader into the object
                        this._LocationNodes.Add(new LocationNode(DataReader, this._SQLConnector));
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to retrieve all location node records for Simulation ID {this._SimID} from the GroundFrame.SQL database.", Ex);
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
