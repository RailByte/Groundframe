using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Class representing a collection of user settings
    /// </summary>
    public class UserSettingCollection : IEnumerable<UserSetting>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<UserSetting> _UserSettings = new List<UserSetting>(); //List to store all user settings
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly CultureInfo _Culture; //Stores the culture info
        
        #endregion Private Variables

        #region Properties
        public IEnumerator<UserSetting> GetEnumerator() { return this._UserSettings.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a UserSettingCollection object the supplied GroundFrame.SQL Connection
        /// </summary>
        /// <param name="SQLConnector">A SQL Connector the GroundFrame.SQL database</param>
        /// <param name="CultureName">The name of the user's preferred culture. If an empty string supplied en-GB will be used</param>
        public UserSettingCollection(GFSqlConnector SQLConnector)
        {
            this._Culture = new CultureInfo("en-GB");
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);

            //Set the SQL Connector
            this._SQLConnector = new GFSqlConnector(SQLConnector); //Instantiated as a new copy of the SQLConnector to stop conflict issues with open connections, commands and DataReaders
            //Get the simulations
            this.GetAllUserSettingsFromSQLDB();
        }

        /// <summary>
        /// Instantiates a new UserSettingCollection using the default values shipped with the library
        /// </summary>
        public UserSettingCollection()
        {
            this.GetDefaultSettings();
        }

        /// <summary>
        /// Instantiates a UserSettingCollection from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing a collection of user settings</param>
        public UserSettingCollection(string JSON)
        {
            this._Culture = new CultureInfo("en-GB");

            //Validate arguments
            ArgumentValidation.ValidateJSON(JSON, _Culture);

            //Try deserializing the string
            try
            {
                //Deserialize the JSON string
                this._UserSettings = JsonConvert.DeserializeObject<List<UserSetting>>(JSON);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseUserSettingsJSONError", null, this._Culture), Ex);
            }
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._UserSettings.Count; } }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the Default User Settings from the defaultUserSettins.json file shipped with the solution
        /// </summary>
        private void GetDefaultSettings()
        {
            //Get the path to the default JSON file
            string JSONPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\defaultUserSettings.json";
            //Read JSON
            string JSON = File.ReadAllText(JSONPath);
            this._UserSettings = JsonConvert.DeserializeObject<List<UserSetting>>(JSON);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the UserSetting for the supplied Index
        /// </summary>
        /// <param name="Index">The index ordinal</param>
        /// <returns></returns>
        public UserSetting IndexOf(int Index)
        {
            return this._UserSettings[Index];
        }
        /// <summary>
        /// Returns the UserSetting for the supplied key
        /// </summary>
        /// <param name="Key">The key of the setting</param>
        /// <returns></returns>
        public object GetValueByKey(string Key)
        {
            return this._UserSettings.Where(s => s.Key == Key).First().Value;
        }


        /// <summary>
        /// Loads all user settings from the GroundFrame.SQL database
        /// </summary>
        private void GetAllUserSettingsFromSQLDB()
        {
            this._UserSettings = new List<UserSetting>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("app.Usp_GET_USER_SETTINGS", CommandType.StoredProcedure);
                //Add Parameters
                SqlDataReader DataReader = Cmd.ExecuteReader();

                //Read the records
                while (DataReader.Read())
                {
                    {
                        //Parse the DataReader into the object
                        this._UserSettings.Add(new UserSetting(DataReader, this._SQLConnector, this._Culture.Name));
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("RetrieveUserSettingsRecordsError", null, this._Culture), Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        /// <summary>
        /// Gets a JSON string that represents the UserSetting Collection
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
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
                if (this._SQLConnector != null)
                {
                    this._SQLConnector.Dispose();
                }
            }
        }

        ~UserSettingCollection()
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