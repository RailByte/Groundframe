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
    /// Class representing a collection of path edges
    /// </summary>
    public class PathEdgeCollection : IEnumerable<PathEdge>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        private List<PathEdge> _PathEdges = new List<PathEdge>(); //List to store all Path Edges
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly LocationNode _FromLocationNode; //Stores the ID of the from Location Node to which the paths belong
        #endregion Private Variables

        #region Properties
        /// <summary>
        /// Gets the PathEdgeCollection Enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PathEdge> GetEnumerator() { return this._PathEdges.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a PathEdgeCollection for the supplied start location
        /// </summary>
        /// <param name="FromLocation">The LocationNode object to which the path collection starts</param>
        /// <param name="SQLConnector">A GFSqlConnector to the GroundFrame.SQL database</param>
        public PathEdgeCollection(LocationNode FromLocation, GFSqlConnector SQLConnector)
        {
            CultureInfo culture = Globals.UserSettings.GetCultureInfo();
            ArgumentValidation.ValidateSQLConnector(SQLConnector, culture);

            this._FromLocationNode = FromLocation;
            //Set the SQL Connector
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiated as a new copy of the SQLConnector to stop conflict issues with open connections, commands and DataReaders
            //Get the locations
            this.GetAllPathEdgesFromLocation();
        }

        /// <summary>
        /// Gets the total number of path edges in the collection
        /// </summary>
        public int Count { get { return this._PathEdges.Count; } }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds a path edge to the path edge collection
        /// </summary>
        /// <param name="NewPathEdge"></param>
        public void Add(PathEdge NewPathEdge)
        {
            this._PathEdges.Add(NewPathEdge);
        }

        /// <summary>
        /// Removed the supplied Path Edge from the path edge collection
        /// </summary>
        /// <param name="PathEdge">The Path Edge to remove</param>
        public void Remove(PathEdge PathEdge)
        {
            this._PathEdges.Remove(PathEdge);
        }

        /// <summary>
        /// Finds a path edge which matches the search predicate
        /// </summary>
        /// <param name="match">The search predicate</param>
        public PathEdge Find(Predicate<PathEdge> match)
        {
            return this._PathEdges.Find(match);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the PathEdge for the supplied index
        /// </summary>
        /// <param name="Index">The index of the path edge to be returned</param>
        /// <returns>The location at the supplied index value</returns>
        public PathEdge IndexOf(int Index)
        {
            return this._PathEdges[Index];
        }

        /// <summary>
        /// Gets alls the location nodes from the GroundFrame.SQL database
        /// </summary>
        private void GetAllPathEdgesFromLocation()
        {
            this._PathEdges = new List<PathEdge>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_GET_PATHEDGE_BY_FROM_LOCATIONNODE", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@from_locationnode_id", this._FromLocationNode.ID));
                SqlDataReader DataReader = Cmd.ExecuteReader();

                //Read the records
                while (DataReader.Read())
                {
                    {
                        //Parse the DataReader into the object
                        this._PathEdges.Add(new PathEdge(this._FromLocationNode, DataReader, this._SQLConnector));
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to retrieve all path edge records for start Location Node ID {this._FromLocationNode.ID} from the GroundFrame.SQL database.", Ex);
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
