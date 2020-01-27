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
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Class representing a collection of user settings
    /// </summary>
    public class WTTTimeTableCollection : IEnumerable<WTTTimeTable>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<WTTTimeTable> _TimeTables = new List<WTTTimeTable>(); //List to store all the time tables
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly UserSettingCollection _UserSettings; //Stores the culture info
        private readonly DateTime _StartDate; //Stores the WTT Start Date

        #endregion Private Variables

        #region Properties
        public IEnumerator<WTTTimeTable> GetEnumerator() { return this._TimeTables.GetEnumerator(); }

        /// <summary>
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this.GetSimulationUserSettings(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Private constructor to handle the deserialization of a WTTTimeTable object from JSON
        /// </summary>
        /// <param name="WTTTimeTables">An IEnumerable<WTTTimeTable> which represents the collection of timetables</param>
        [JsonConstructor]
        private WTTTimeTableCollection(IEnumerable<WTTTimeTable> WTTTimeTables)
        {
            this._TimeTables = new List<WTTTimeTable>();

            foreach (WTTTimeTable WTT in WTTTimeTables)
            {
                WTTTimeTable NewWTTTimeTable = WTT;
                NewWTTTimeTable.OnRequestUserSettings += new Func<UserSettingCollection>(delegate { return this.UserSettings; });
                this._TimeTables.Add(NewWTTTimeTable);
            }
        }

        /// <summary>
        /// Instantiates a WTTTimeTableCollection
        /// </summary>
        /// <param name="User">The name of the user's preferred culture. If an empty string supplied en-GB will be used</param>
        public WTTTimeTableCollection(XElement WTTTimeTableXML, DateTime StartDate, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);
            //Validate arguments
            ArgumentValidation.ValidateXElement(WTTTimeTableXML, Culture);
            ArgumentValidation.ValidateWTTStartDate(StartDate, Culture);

            this._StartDate = StartDate;
            this._TimeTables = new List<WTTTimeTable>();
            ParseWTTTimeTablesXML(WTTTimeTableXML);
        }

        /// <summary>
        /// Instantiates a WTTTimeTableCollection from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing a collection of user settings</param>
        public WTTTimeTableCollection(string JSON, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);

            //Validate arguments
            ArgumentValidation.ValidateJSON(JSON, Culture);

            //Try deserializing the string
            try
            {
                //Deserialize the JSON string
                //TODO: Deserialize JSON string

            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseUserSettingsJSONError", null, Culture), Ex);
            }
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._TimeTables.Count; } }

        #endregion Constructors

        #region Methods

        private void ParseWTTTimeTablesXML(XElement WTTTimeTablesXML)
        {
            foreach (XElement WTTTimeTableXML in WTTTimeTablesXML.Elements("Timetable"))
            {
                this._TimeTables.Add(new WTTTimeTable(WTTTimeTableXML, this._StartDate, this.UserSettings));
            }
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
        public WTTTimeTable IndexOf(int Index)
        {
            return this._TimeTables[Index];
        }

        /// <summary>
        /// Gets a JSON string that represents the UserSetting Collection
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        private UserSettingCollection GetSimulationUserSettings()
        {
            if (OnRequestUserSettings == null)
            {
                return this._UserSettings ?? new UserSettingCollection();
            }
            else
            {
                return OnRequestUserSettings();
            }
        }

        internal Func<UserSettingCollection> OnRequestUserSettings;

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

        ~WTTTimeTableCollection()
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