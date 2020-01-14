using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Enum represneting the status a version can have
    /// </summary>
    public enum VersionStatus
    {
        Production = 1,
        Development = 2
    }

    /// <summary>
    /// A class representing a SimSig version. One Version can cover multiple SimSig versions and there should be one Version for each version of the SimSig TimeTable Schema
    /// </summary>
    public class Version
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _ID; //The GroundFrame.SQL database ID
        private readonly GFSqlConnector _SQLConnector; //A connector to the GroundFrame.SQL database

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the GroundFrame.SQL database ID for the version
        /// </summary>
        public int ID { get { return this._ID; } }

        /// <summary>
        /// Gets or sets the name of the version
        /// </summary>
        public string Name{ get; set;}

        /// <summary>
        /// Gets or sets the description of the version
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the lower SimSig version this version covers
        /// </summary>
        public decimal VersionFrom { get; set; }
        /// <summary>
        /// Gets or sets the upper SimSig version this version covers. If <NULL> then the version covers up to the latest SimSig version
        /// </summary>
        public decimal? VersionTo { get; set; }

        /// <summary>
        /// Gets or sets the version statis
        /// </summary>
        public VersionStatus Status { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Version object the supplied values
        /// </summary>
        /// <param name="Name">The name of the verison</param>
        /// <param name="Description">A description of the version</param>
        /// <param name="Version">The version number. This must be greater the latest version stored in the GroundFrame.SQL database</param>
        /// <param name="SQLConnector">A Connector to the GroundFrame.SQL Database</param>
        public Version (string Name, string Description, Decimal Version, GFSqlConnector SQLConnector)
        {
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Creates a copy of the object to prevent conflict with connectios, commands and readers
            this.Name = Name;
            this.Description = Description;
            this.VersionFrom = Version;
            this.VersionTo = null;
            this.Status = VersionStatus.Development;
        }

        /// <summary>
        /// Instantiates a new Version object from the GroundFrame.SQL database for the supplied Record ID
        /// </summary>
        /// <param name="ID">The ID of the version record to get from the GroundFrame.SQL database</param>
        /// <param name="SQLConnector">A Connector to the GroundFrame.SQL Database</param>
        public Version(int ID, GFSqlConnector SQLConnector)
        {
            this._ID = ID;
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Creates a copy of the object to prevent conflict with connectios, commands and readers
            this.GetVersionFromSQLDBByID();
        }

        /// <summary>
        /// Instatiates a Version object from a SqlDataReader object
        /// </summary>
        /// <param name="DataReader">The source SqlDataReader</param>
        /// <param name="SQLConnector">A Connector to the GroundFrame.SQL Database</param>
        public Version (SqlDataReader DataReader, GFSqlConnector SQLConnector)
        {
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Creates a copy of the object to prevent conflict with connectios, commands and readers
            this.ParseSqlDataReader(DataReader);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Saves the Version to the GroundFrame.SQL database
        /// </summary>
        public void SaveToSQLDB()
        {
            this.SaveVersionToSQLDB();
        }
        /// <summary>
        /// Refresh the Version from the GroundFrame.SQL database
        /// </summary>
        public void RefreshFromDB()
        {
            this.GetVersionFromSQLDBByID();
        }

        /// <summary>
        /// Saves the Version to the GroundFrame.SQL database
        /// </summary>
        private void SaveVersionToSQLDB()
        {
            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_UPSERT_TVERSION", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@name", this.Name));
                Cmd.Parameters.Add(new SqlParameter("@description", string.IsNullOrEmpty(this.Description) ? (object)DBNull.Value : this.Description));
                Cmd.Parameters.Add(new SqlParameter("@version", this.VersionFrom));
                Cmd.Parameters.Add(new SqlParameter("@version_status_id", (byte)this.Status));
                Cmd.Parameters.Add(new SqlParameter("@id", (Int16)this._ID));
                Cmd.Parameters["@id"].Direction = ParameterDirection.InputOutput;
                Cmd.Parameters.Add(new SqlParameter("@debug", true));

                //Execute
                Cmd.ExecuteNonQuery();
                this._ID = Convert.ToInt32(Cmd.Parameters["@id"].Value);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to save version record {this.Name} to the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Gets the Version from the GroundFrame.SQL database
        /// </summary>
        private void GetVersionFromSQLDBByID()
        {
            if (this._ID == 0)
            {
                throw new ApplicationException("Cannot load Version from the GroundFrame.SQL Database when the ID = 0.");
            }

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_GET_TVERSION", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@id", (Int16)this._ID));
                Cmd.Parameters.Add(new SqlParameter("@debug", true));

                //Execute
                SqlDataReader DataReader = Cmd.ExecuteReader();

                while (DataReader.Read())
                {
                    this.ParseSqlDataReader(DataReader);
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to get version record {this.ID} to the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Parses a SqlDataReader object into a Version object
        /// </summary>
        /// <param name="DataReader"></param>
        private void ParseSqlDataReader(SqlDataReader DataReader)
        {
            this._ID = DataReader.GetInt16(DataReader.GetOrdinal("id"));
            this.Name = DataReader.GetString(DataReader.GetOrdinal("name"));
            this.Description = DataReader.GetString(DataReader.GetOrdinal("description"));
            this.VersionFrom = DataReader.GetDecimal(DataReader.GetOrdinal("simsig_version_from"));
            this.VersionTo = DataReader.GetNullableDecimal("simsig_version_to");
            this.Status = (VersionStatus)DataReader.GetByte(DataReader.GetOrdinal("version_status_id"));
        }

        #endregion Methods
    }
}
