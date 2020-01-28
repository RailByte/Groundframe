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
    /// Class representing a collection of WTTTrip objects
    /// </summary>
    public class WTTTripCollection : IEnumerable<WTTTrip>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<WTTTrip> _Trips = new List<WTTTrip>(); //List to store all the time tables
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private readonly UserSettingCollection _UserSettings; //Stores the culture info
        private DateTime _StartDate; //Stores the WTT Start Date

        #endregion Private Variables

        #region Properties
        public IEnumerator<WTTTrip> GetEnumerator() { return this._Trips.GetEnumerator(); }

        /// <summary>
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this.GetSimulationUserSettings(); } }

        /// <summary>
        /// Gets the timetable start date
        /// </summary>
        public DateTime StartDate { get { return this._StartDate; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Private constructor to handle the deserialization of a WTTTripCollection object from JSON
        /// </summary>
        /// <param name="WTTTrips">An IEnumerable<WTTTrip> which represents the collection of trips</param>
        [JsonConstructor]
        private WTTTripCollection(IEnumerable<WTTTrip> WTTTrips)
        {
            this._Trips = new List<WTTTrip>();

            foreach (WTTTrip WTTTrip in WTTTrips)
            {
                WTTTrip NewWTTTrip = WTTTrip;
                NewWTTTrip.OnRequestUserSettings += new Func<UserSettingCollection>(delegate { return this.UserSettings; });
                this._Trips.Add(NewWTTTrip);
            }
        }

        /// <summary>
        /// Instantiates a WTTTripCollection from the supplied XElement object represening a SimSig trip collection and timetable start date
        /// </summary>
        /// <param name="WTTTripXML">The XML object representing the SimSig trip collection</param>
        /// <param name="StartDate">The start date of the timetable. Must be after 01/01/1850</param>
        /// <param name="UserSettings">The user settings</param>
        public WTTTripCollection(XElement WTTTripXML, DateTime StartDate, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);
            //Validate arguments
            ArgumentValidation.ValidateXElement(WTTTripXML, Culture);
            ArgumentValidation.ValidateWTTStartDate(StartDate, Culture);

            this._StartDate = StartDate;
            this._Trips = new List<WTTTrip>();
            ParseWTTTripsXML(WTTTripXML);
        }

        /// <summary>
        /// Instantiates a WTTTripCollection from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing the trip collection</param>
        /// <param name="UserSettings">The user settings</param>
        public WTTTripCollection(string JSON, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);

            //Validate arguments
            ArgumentValidation.ValidateJSON(JSON, Culture);

            //Try deserializing the string
            try
            {
                //Deserialize the JSON string
                this.PopulateFromJSON(JSON);

            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTripCollectionJSONError", null, Culture), Ex);
            }
        }

        /// <summary>
        /// Instantiates a WTTTripCollection object with the supplied start date. Used by the WTTTripCollectionConverter class to convert JSON to WTTTripCollection
        /// </summary>
        /// <param name="StartDate"></param>
        internal WTTTripCollection(DateTime StartDate)
        {
            ArgumentValidation.ValidateWTTStartDate(StartDate, UserSettingHelper.GetCultureInfo(this.UserSettings));
            this._StartDate = StartDate;
            this._Trips = new List<WTTTrip>();
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._Trips.Count; } }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTHeader object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                WTTTripCollection Temp = JsonConvert.DeserializeObject<WTTTripCollection>(JSON, new WTTTripCollectionConverter());
                this._StartDate = Temp.StartDate;
                this._Trips = Temp.ToList();
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTripCollectionJSONError", null, UserSettingHelper.GetCultureInfo(this._UserSettings)), Ex);
            }
        }

        private void ParseWTTTripsXML(XElement WTTTripsXML)
        {
            foreach (XElement WTTTripXML in WTTTripsXML.Elements("Trip"))
            {
                this._Trips.Add(new WTTTrip(WTTTripXML, this._StartDate, this.UserSettings));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the WTTTrip for the supplied Index
        /// </summary>
        /// <param name="Index">The index ordinal</param>
        /// <returns></returns>
        public WTTTrip IndexOf(int Index)
        {
            return this._Trips[Index];
        }

        /// <summary>
        /// Adds at WTTTrip to the collection
        /// </summary>
        /// <param name="Trip">The WTTTrip object to add to the collection</param>
        public void Add(WTTTrip Trip)
        {
            if (this._Trips == null)
            {
                this._Trips = new List<WTTTrip>();
            }
            this._Trips.Add(Trip);
        }

        /// <summary>
        /// Get the list of WTTTrips
        /// </summary>
        public List<WTTTrip> ToList()
        {
            return this._Trips;
        }

        /// <summary>
        /// Gets a JSON string that represents the UserSetting Collection
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new WTTTripCollectionConverter());
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

        ~WTTTripCollection()
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