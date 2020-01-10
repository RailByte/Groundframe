using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Class representing a collection of versions
    /// </summary>
    public class VersionCollection : IEnumerable<Version>
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        private List<Version> _Versions = new List<Version>(); //List to store all Versions
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        #endregion Private Variables

        #region Properties
        public IEnumerator<Version> GetEnumerator() { return this._Versions.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a VersionCollection object the supplied GroundFrame.SQL Connection
        /// </summary>
        /// <param name="SQLConnector"></param>
        public VersionCollection(GFSqlConnector SQLConnector)
        {
            //Set the SQL Connector
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiated as a new copy of the SQLConnector to stop conflict issues with open connections, commands and DataReaders
            //Get the simulations
            this.GetAllVersionsFromSQLDB();
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._Versions.Count; } }

        #endregion Constructors

        #region Methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the Version for the supplied Index
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public Version IndexOf(int Index)
        {
            return this._Versions[Index];
        }

        private void GetAllVersionsFromSQLDB()
        {
            this._Versions = new List<Version>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.USp_GET_TVERSION", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@id", 0));
                SqlDataReader DataReader = Cmd.ExecuteReader();

                //Read the records
                while (DataReader.Read())
                {
                    {
                        //Parse the DataReader into the object
                        this._Versions.Add(new Version(DataReader, this._SQLConnector));
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to retrieve all Version records from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        #endregion Methods
    }
}
