﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace GroundFrame.Core
{
    /// <summary>
    /// Class which represents a user setting
    /// </summary>
    public class UserSetting : IDisposable
    {
        #region Constants
        #endregion Constatns

        #region Private Variables

        private string _Key; //Stores the key of the setting
        private string _Description; //Stores the description of the setting
        private Type _DataType; //Stores the data type of the setting value
        private readonly GFSqlConnector _SQLConnector; //Stores the connection to the GroundFrame.SQL database
        private readonly CultureInfo _Culture; //Stores the culture

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the key of the user setting
        /// </summary>
        [JsonProperty("key")]
        public string Key { get { return this._Key; } }

        /// <summary>
        /// Gets the description of the user setting
        /// </summary>
        [JsonProperty("description")]
        public string Description { get { return this._Description; } }

        /// <summary>
        /// Gets the value of the user setting
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }

        /// <summary>
        /// Gets the data type of the user setting
        /// </summary>
        [JsonProperty("dataTypeName")]
        public string DataType { get { return this._DataType.FullName; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a user setting from the supplied arguments
        /// </summary>
        /// <param name="Key">The setting key</param>
        /// <param name="Description">The setting description</param>
        /// <param name="Value">The setting value</param>
        /// <param name="DataTypeName">The data type of the setting expressed as a .net full name (e.g. "system.string")</param>
        /// <param name="SQLConnector">A GFSqlConnector object representing a connection to the GroundFrame.SQL database</param>
        public UserSetting (string Key, string Description, string Value, string DataTypeName, GFSqlConnector SQLConnector)
        {
            //Set the culture
            this._Culture = new CultureInfo("en-GB");

            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);

            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiate copy of the SQLConnector to stop conflicts in the connection
            this._Key = Key;
            this._Description = Description;
            //Set the values
            this.SetValues(DataTypeName, Value);       
        }

        /// <summary>
        /// Instantiates a user setting from a SqlDataReader object
        /// </summary>
        /// <param name="DataReader">The SqlDataReader object to parse into the UserSetting object</param>
        /// <param name="SQLConnector">A GFSqlConnector to the GroundFrame.SQL database</param>
        /// <param name="Culture">The culture in which user would receive any exception messages. Defaults to en-GB</param>
        public UserSetting(SqlDataReader DataReader, GFSqlConnector SQLConnector, string Culture)
        {
            //Set the culture
            this._Culture = new CultureInfo(string.IsNullOrEmpty(Culture) ? "en-GB" : Culture);

            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, this._Culture);
            ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);

            this._SQLConnector = new GFSqlConnector(SQLConnector);

            //Parse DataReader
            this.ParseSqlDataReader(DataReader);
        }

        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private UserSetting(string Key, string Description, object Value, string DataTypeName)
        {
            this._Key = Key;
            this._Description = Description;
            this.Value = Value;
            this._DataType = Type.GetType(DataTypeName, true, true);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses a SqlDataReader object into a user setting
        /// </summary>
        /// <param name="DataReader">The SqlDataReader to convert to a user setting</param>
        private void ParseSqlDataReader(SqlDataReader DataReader)
        {
            this._Key = DataReader.GetString(DataReader.GetOrdinal("key"));
            this._Description = DataReader.GetNullableString("description");

            string DataTypeName = DataReader.GetString(DataReader.GetOrdinal("data_type"));
            string Value = DataReader.GetString(DataReader.GetOrdinal("value"));

            //Set the values
            this.SetValues(DataTypeName, Value);
        }

        /// <summary>
        /// Sets the values from the supplied string eviquivalent and converts to the correct type
        /// </summary>
        /// <param name="DataTypeName"></param>
        /// <param name="Value"></param>
        private void SetValues(string DataTypeName, string Value)
        {
            //Set data type
            this._DataType = Type.GetType(DataTypeName, true, true);

            if (DataTypeName == "system.string")
            {
                this.Value = Value;
            }
            else
            {
                this.Value = string.IsNullOrEmpty(Value) ? null : Convert.ChangeType(Value, this._DataType, this._Culture);
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

        /// <summary>
        /// Protected override of the Dispose method
        /// </summary>
        /// <param name="disposing">Flag to indicate whether the object is already disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {

            }
            else
            {
                if (this._SQLConnector != null)
                {
                    this._SQLConnector.Dispose();
                }
            }
        }

        #endregion Methods
    }
}
