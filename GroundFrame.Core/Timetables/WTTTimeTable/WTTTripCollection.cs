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

namespace GroundFrame.Core.Timetables
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
        private DateTime _StartDate; //Stores the WTT Start Date

        #endregion Private Variables

        #region Properties
        /// <summary>
        /// Gets the WTTTripCollection enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<WTTTrip> GetEnumerator() { return this._Trips.GetEnumerator(); }

        /// <summary>
        /// Gets the timetable start date
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime StartDate { get { return this._StartDate; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Private constructor to handle the deserialization of a WTTTripCollection object from JSON
        /// </summary>
        /// <param name="WTTTrips">An IEnumerable&lt;WTTTrip&gt; which represents the collection of trips</param>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private WTTTripCollection(IEnumerable<WTTTrip> WTTTrips)
        {
            this._Trips = new List<WTTTrip>();

            foreach (WTTTrip WTTTrip in WTTTrips)
            {
                WTTTrip NewWTTTrip = WTTTrip;
                this._Trips.Add(NewWTTTrip);
            }
        }

        /// <summary>
        /// Instantiates a WTTTripCollection from the supplied XElement object represening a SimSig trip collection and timetable start date
        /// </summary>
        /// <param name="WTTTripXML">The XML object representing the SimSig trip collection</param>
        /// <param name="StartDate">The start date of the timetable. Must be after 01/01/1850</param>
        public WTTTripCollection(XElement WTTTripXML, DateTime StartDate)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();
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
        public WTTTripCollection(string JSON)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();

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
            ArgumentValidation.ValidateWTTStartDate(StartDate, Globals.UserSettings.GetCultureInfo());
            this._StartDate = StartDate;
            this._Trips = new List<WTTTrip>();
        }

        /// <summary>
        /// Instantiates a WTTTripCollection object from WTTTripCollectionSurrogate object
        /// </summary>
        /// <param name="SurrogateWTTTripCollection">The source WTTTripCollectionSurrogate object</param>
        internal WTTTripCollection(WTTTripCollectionSurrogate SurrogateWTTTripCollection)
        {
            this._StartDate = SurrogateWTTTripCollection.StartDate;

            if (SurrogateWTTTripCollection.Trips != null)
            {
                this._Trips = new List<WTTTrip>();
                foreach (WTTTrip Trip in SurrogateWTTTripCollection.Trips)
                {
                    this._Trips.Add(Trip);
                }
            }
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._Trips.Count; } }
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
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTripCollectionJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        private void ParseWTTTripsXML(XElement WTTTripsXML)
        {
            foreach (XElement WTTTripXML in WTTTripsXML.Elements("Trip"))
            {
                this._Trips.Add(new WTTTrip(WTTTripXML, this._StartDate));
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
        /// Converts this WTTTripCollection object to a WTTTripCollectionSurrogate object
        /// </summary>
        /// <returns></returns>
        internal WTTTripCollectionSurrogate ToWTTTripCollectionSurrogate()
        {
            return new WTTTripCollectionSurrogate
            {
                StartDate = this._StartDate,
                Trips = this._Trips
            };
        }

        /// <summary>
        /// Gets a JSON string that represents the UserSetting Collection
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new WTTTripCollectionConverter());
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
        /// Protected over of the Dispose method
        /// </summary>
        /// <param name="disposing">Indicates where the object is currently disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {

            }
            else
            {
                //Dispose of any supported objects here
            }
        }

        #endregion Methods
    }
}