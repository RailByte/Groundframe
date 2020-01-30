using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A Class representing a SimSig Trip
    /// </summary>
    public class WTTTrip
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly UserSettingCollection _UserSettings; //Stores the user settings
        private DateTime _StartDate; //Stores the timetable start date

        #endregion Private

        #region Properties

        /// <summary>
        /// Gets or sets the trip location
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the departure / pass time
        /// </summary>
        [JsonProperty("depPassTime")]
        public WTTTime DepPassTime { get; set; }

        /// <summary>
        /// Gets or sets the arrival time
        /// </summary>
        [JsonProperty("arrTime")]
        public WTTTime ArrTime { get; set; }

        /// <summary>
        /// Gets or sets the is pass time flag
        /// </summary>
        [JsonProperty("isPassTime")]
        public bool IsPassTime { get; set; }

        /// <summary>
        /// Gets or sets the platform
        /// </summary>
        [JsonProperty("platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the down direction flag
        /// </summary>
        [JsonProperty("downDirection")]
        public bool DownDirection { get; set; }

        /// <summary>
        /// Gets or sets the previous path end down flag
        /// </summary>
        [JsonProperty("prevPathEndDown")]
        public bool PrevPathEndDown { get; set; }

        /// <summary>
        /// Gets or sets the next path start down flag
        /// </summary>
        [JsonProperty("nextPathStartDown")]
        public bool NextPathStartDown { get; set; }


        /// <summary>
        /// Gets or sets the activities associated with this trip
        /// </summary>
        [JsonProperty("activities")]
        public WTTActivityCollection Activities { get; set; }

        /// <summary>
        /// Gets the start date
        /// </summary>
        [JsonProperty("startDate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        public DateTime StartDate { get { return this._StartDate; } }

        /// <summary>
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this.GetSimulationUserSettings(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a WTTTrip object from an XML element representing a SimSig trip
        /// </summary>
        /// <param name="WTTTripXML">The XML element representing a SimSig trip</param>
        /// <param name="StartDate">The timetable start date</param>
        /// <param name="UserSettings">The users settings. If NULL then default settings are applied</param>
        public WTTTrip (XElement WTTTripXML, DateTime StartDate, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            //Validate Arguments
            ArgumentValidation.ValidateXElement(WTTTripXML, UserSettingHelper.GetCultureInfo(this.UserSettings));
            ArgumentValidation.ValidateWTTStartDate(StartDate, UserSettingHelper.GetCultureInfo(this.UserSettings));

            this._StartDate = StartDate;

            //Parse the XML
            this.ParseWTTTripXML(WTTTripXML);
        }

        /// <summary>
        /// Instantiates a WTTTrip object from the supplied JSON string
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTrip object</param>
        /// <param name="UserSettings">The user settignd</param>
        public WTTTrip(string JSON, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            //Valdate Arguments
            ArgumentValidation.ValidateJSON(JSON, UserSettingHelper.GetCultureInfo(this.UserSettings));

            //Parse the JSON
            this.PopulateFromJSON(JSON);
        }
        
        /// <summary>
        /// Instantiates a WTTTrip from the supplied start date. Used the WTTTRipConverter class
        /// </summary>
        /// <param name="StartDate">The timetable start date</param>
        [JsonConstructor]
        internal WTTTrip(DateTime StartDate)
        {
            this._StartDate = StartDate;
        }

        /// <summary>
        /// Instantiates a WTTTrip object from a WTTTripSurrogate object and start date
        /// </summary>
        /// <param name="SurrogateTrip">The WTTTripSurrogate object</param>
        internal WTTTrip(WTTTripSurrogate SurrogateTrip, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            ParseSurrogateWTTTrip(SurrogateTrip);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses a WTTTripSurrogate object into a WTTTrip Object
        /// </summary>
        /// <param name="SurrogateTrip"></param>
        private void ParseSurrogateWTTTrip(WTTTripSurrogate SurrogateTrip)
        {
            this._StartDate = SurrogateTrip.StartDate;
            this.Location = SurrogateTrip.Location;
            this.DepPassTime = new WTTTime(SurrogateTrip.DepPassTime.Seconds, this.StartDate, this._UserSettings);

            if (SurrogateTrip.ArrTime != null)
            {
                this.ArrTime = new WTTTime(SurrogateTrip.ArrTime.Seconds, this.StartDate, this._UserSettings);
            }

            this.IsPassTime = SurrogateTrip.IsPassTime;
            this.Platform = SurrogateTrip.Platform;
            this.DownDirection = SurrogateTrip.DownDirection;
            this.PrevPathEndDown = SurrogateTrip.PrevPathEndDown;
            this.NextPathStartDown = SurrogateTrip.NextPathStartDown;
            this.Activities = SurrogateTrip.Activities;
        }

        /// <summary>
        /// Converts the WTTTrip object to a WTTTripSurrogate object
        /// </summary>
        /// <returns>WTTTripSurrogate</returns>
        internal WTTTripSurrogate ToSurrogateWTTTrip()
        {
            return new WTTTripSurrogate()
            {
                Location = this.Location,
                DepPassTime = this.DepPassTime,
                ArrTime = this.ArrTime,
                IsPassTime = this.IsPassTime,
                Platform = this.Platform,
                DownDirection = this.DownDirection,
                PrevPathEndDown = this.PrevPathEndDown,
                NextPathStartDown = this.NextPathStartDown,
                StartDate = this.StartDate,
                Activities = this.Activities
            };
        }

        /// <summary>
        /// Parses a WTTTrip SimSig XML Xlement into the WTTTrip object
        /// </summary>
        /// <param name="WTTTripXML"></param>
        private void ParseWTTTripXML(XElement WTTTripXML)
        {
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);

            try
            {
                this.Location = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Location", Culture, string.Empty);
                this.DepPassTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"DepPassTime", Culture, 0), this.StartDate, this.UserSettings);
                this.ArrTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"ArrTime", Culture, 0), this.StartDate, this.UserSettings);
                this.IsPassTime = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"IsPassTime", Culture, 0));
                this.Platform = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Platform", Culture, string.Empty);
                this.DownDirection = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"DownDirection", Culture, 0));
                this.PrevPathEndDown = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"PrevPathEndDown", Culture, 0));
                this.NextPathStartDown = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"NextPathStartown", Culture, 0));

                //Parse Activties

                if (WTTTripXML.Element("Activities") != null)
                {
                    this.Activities = new WTTActivityCollection(WTTTripXML.Element("Activities"), this.UserSettings);
                    this.Activities.OnRequestUserSettings += new Func<UserSettingCollection>(delegate { return this.UserSettings; });
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTripException", null, Culture), Ex);
            }
        }

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTrip object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                WTTTrip TempTrip = JsonConvert.DeserializeObject<WTTTrip>(JSON, new WTTTripConverter(this.UserSettings));
                this._StartDate = TempTrip.StartDate;
                this.Location = TempTrip.Location;

                if (TempTrip.DepPassTime != null)
                {
                    this.DepPassTime = new WTTTime(TempTrip.DepPassTime.Seconds, this.StartDate, this.UserSettings);
                }

                if (TempTrip.ArrTime != null)
                {
                    this.ArrTime = new WTTTime(TempTrip.ArrTime.Seconds, this.StartDate, this.UserSettings);
                }
                
                this.IsPassTime = TempTrip.IsPassTime;
                this.Platform = TempTrip.Platform;
                this.DownDirection = TempTrip.DownDirection;
                this.PrevPathEndDown = TempTrip.PrevPathEndDown;
                this.NextPathStartDown = TempTrip.NextPathStartDown;
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTripJSONError", null, UserSettingHelper.GetCultureInfo(this.UserSettings)), Ex);
            }
        }

        /// <summary>
        /// Serializes the WTTrip object to JSON
        /// </summary>
        /// <returns></returns>s
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new WTTTripConverter(this.UserSettings));
        }

        /// <summary>
        /// Returns the UserSettingCollection from the various sources
        /// </summary>
        /// <returns></returns>
        private UserSettingCollection GetSimulationUserSettings()
        {
            //First check to see if the user settings have been passed down from the parent WTT object via then event function
            if (OnRequestUserSettings == null)
            {
                //If not return the user settings from this object. If this is null then create a default set of user settings
                return this._UserSettings ?? new UserSettingCollection();
            }
            else
            {
                //Otherwise return user settings frm the WTT object
                return OnRequestUserSettings();
            }
        }

        /// <summary>
        /// Function which is defined by the parent object to retreive the user settings from the parent object
        /// </summary>
        internal Func<UserSettingCollection> OnRequestUserSettings;

        #endregion Methods
    }
}
