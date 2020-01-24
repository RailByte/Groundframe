using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace GroundFrame.Classes.SimSig
{
    /// <summary>
    /// Class representing a collection of versions
    /// </summary>
    public class VersionCollection : IEnumerable<Version>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<Version> _Versions = new List<Version>(); //List to store all Versions
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly CultureInfo _Culture; //Stores the culture info
        
        #endregion Private Variables

        #region Properties
        public IEnumerator<Version> GetEnumerator() { return this._Versions.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a VersionCollection object the supplied GroundFrame.SQL Connection
        /// </summary>
        /// <param name="SQLConnector"></param>
        public VersionCollection(GFSqlConnector SQLConnector, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);

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

        /// <summary>
        /// Loads all versions from the GroundFrame.SQL database
        /// </summary>
        private void GetAllVersionsFromSQLDB()
        {
            this._Versions = new List<Version>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.USp_GET_TVERSION", CommandType.StoredProcedure);
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
                throw new ApplicationException(ExceptionHelper.GetStaticException("RetrieveVersionRecordsError", null, this._Culture), Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Disposes the VersionCollection object
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

        ~VersionCollection()
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