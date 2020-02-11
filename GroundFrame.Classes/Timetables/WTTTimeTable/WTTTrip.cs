using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A Class representing a single SimSig Trip (calling point)
    /// </summary>
    public class WTTTrip
    {
        #region Constants
        #endregion Constants

        #region Private Variables

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
        /// Gets or sets the line
        /// </summary>
        [JsonProperty("line")]
        public string Line { get; set; }

        /// <summary>
        /// Gets or sets the path
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the auto line flag
        /// </summary>
        [JsonProperty("autoLine")]
        public bool AutoLine { get; set; }

        /// <summary>
        /// Gets or sets the auto path flag
        /// </summary>
        [JsonProperty("autoPath")]
        public bool AutoPath { get; set; }

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
        /// Gets or sets the stopping location at the timing point
        /// </summary>
        [JsonProperty("stopLocation")]
        public WTTStopLocation StopLocation { get; set; }

        /// <summary>
        /// Gets or sets the dwell time that a service must wait at this timing point
        /// </summary>
        [JsonProperty("dwellTime")]
        public WTTDuration DwellTime { get; set; }

        /// <summary>
        /// Gets to sets the flag to indicate whether the service berths at this timing point
        /// </summary>
        [JsonProperty("berthsHere")]
        public bool BerthsHere { get; set; }

        /// <summary>
        /// Gets to sets the flag to indicate whether the timing point is a though line stop
        /// </summary>
        [JsonProperty("thruLineStop")]
        public bool AllowStopsOnThroughLines { get; set; }

        /// <summary>
        /// Gets to sets the flag to indicate whether the service should wait for the booked time at this timing point
        /// </summary>
        [JsonProperty("waitForBookedTime")]
        public bool WaitForBookedTime { get; set; }

        /// <summary>
        /// Gets to sets the flag to indicate whether the service should set down only at this timing point
        /// </summary>
        [JsonProperty("setDownOnly")]
        public bool SetDownOnly { get; set; }

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

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a WTTTrip object from an XML element representing a SimSig trip
        /// </summary>
        /// <param name="WTTTripXML">The XML element representing a SimSig trip</param>
        /// <param name="StartDate">The timetable start date</param>
        public WTTTrip (XElement WTTTripXML, DateTime StartDate)
        {
            //Validate Arguments
            ArgumentValidation.ValidateXElement(WTTTripXML, Globals.UserSettings.GetCultureInfo());
            ArgumentValidation.ValidateWTTStartDate(StartDate, Globals.UserSettings.GetCultureInfo());

            this._StartDate = StartDate;

            //Parse the XML
            this.ParseWTTTripXML(WTTTripXML);
        }

        /// <summary>
        /// Instantiates a WTTTrip object from the supplied JSON string
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTrip object</param>
        public WTTTrip(string JSON)
        {
            //Valdate Arguments
            ArgumentValidation.ValidateJSON(JSON, Globals.UserSettings.GetCultureInfo());

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
        internal WTTTrip(WTTTripSurrogate SurrogateTrip)
        {
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
            this.DepPassTime = SurrogateTrip.DepPassTime;
            this.ArrTime = SurrogateTrip.ArrTime;
            this.IsPassTime = SurrogateTrip.IsPassTime;
            this.Platform = SurrogateTrip.Platform;
            this.Line = SurrogateTrip.Line;
            this.Path = SurrogateTrip.Path;
            this.AutoLine = SurrogateTrip.AutoLine;
            this.AutoPath = SurrogateTrip.AutoPath;
            this.DownDirection = SurrogateTrip.DownDirection;
            this.PrevPathEndDown = SurrogateTrip.PrevPathEndDown;
            this.NextPathStartDown = SurrogateTrip.NextPathStartDown;
            this.StopLocation = SurrogateTrip.StopLocation;
            this.DwellTime = SurrogateTrip.DwellTime;
            this.BerthsHere = SurrogateTrip.BerthsHere;
            this.AllowStopsOnThroughLines = SurrogateTrip.AllowStopsOnThroughLines;
            this.WaitForBookedTime = SurrogateTrip.WaitForBookedTime;
            this.SetDownOnly = SurrogateTrip.SetDownOnly;
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
                Line = this.Line,
                Path = this.Path,
                AutoLine = this.AutoLine,
                AutoPath = this.AutoPath,
                DownDirection = this.DownDirection,
                PrevPathEndDown = this.PrevPathEndDown,
                NextPathStartDown = this.NextPathStartDown,
                StopLocation = this.StopLocation,
                DwellTime = this.DwellTime,
                BerthsHere = this.BerthsHere,
                AllowStopsOnThroughLines = this.AllowStopsOnThroughLines,
                WaitForBookedTime = this.WaitForBookedTime,
                SetDownOnly = this.SetDownOnly,
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
            try
            {
                this.Location = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Location", string.Empty);
                this.DepPassTime = XMLMethods.GetValueFromXElement<WTTTime>(WTTTripXML, @"DepPassTime", null, new object[] { this.StartDate });
                this.ArrTime = XMLMethods.GetValueFromXElement<WTTTime>(WTTTripXML, @"ArrTime", null, new object[] { this.StartDate });
                this.IsPassTime = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"IsPassTime", false);
                this.Platform = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Platform", null);
                this.Line = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Line", null);
                this.Path = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Path", null);
                this.AutoPath = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"AutoPath", false);
                this.AutoLine = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"AutoLine", false);
                this.DownDirection = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"DownDirection", false);
                this.PrevPathEndDown = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"PrevPathEndDown", false);
                this.NextPathStartDown = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"NextPathStartown", false);
                this.StopLocation = XMLMethods.GetValueFromXElement<WTTStopLocation>(WTTTripXML, @"StopLocation", WTTStopLocation.Default);
                this.DwellTime = XMLMethods.GetValueFromXElement<WTTDuration>(WTTTripXML, @"DwellTime", null);
                this.BerthsHere = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"BerthsHere", false);
                this.AllowStopsOnThroughLines = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"AllowStopsOnThroughLines", false);
                this.WaitForBookedTime = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"WaitForBookedTime", false);
                this.SetDownOnly = XMLMethods.GetValueFromXElement<bool>(WTTTripXML, @"SetDownOnly", false);

                //Parse Activties

                if (WTTTripXML.Element("Activities") != null)
                {
                    this.Activities = new WTTActivityCollection(WTTTripXML.Element("Activities"));
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTripException", null, Globals.UserSettings.GetCultureInfo()), Ex);
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
                WTTTrip TempTrip = JsonConvert.DeserializeObject<WTTTrip>(JSON, new WTTTripConverter());
                this._StartDate = TempTrip.StartDate;
                this.Location = TempTrip.Location;
                this.DepPassTime = TempTrip.DepPassTime;
                this.ArrTime = TempTrip.ArrTime;
                this.IsPassTime = TempTrip.IsPassTime;
                this.Platform = TempTrip.Platform;
                this.Line = TempTrip.Line;
                this.Path = TempTrip.Path;
                this.AutoLine = TempTrip.AutoLine;
                this.AutoPath = TempTrip.AutoPath;
                this.DownDirection = TempTrip.DownDirection;
                this.PrevPathEndDown = TempTrip.PrevPathEndDown;
                this.NextPathStartDown = TempTrip.NextPathStartDown;
                this.StopLocation = TempTrip.StopLocation;
                this.DwellTime = TempTrip.DwellTime;
                this.BerthsHere = TempTrip.BerthsHere;
                this.AllowStopsOnThroughLines = TempTrip.AllowStopsOnThroughLines;
                this.WaitForBookedTime = TempTrip.WaitForBookedTime;
                this.SetDownOnly = TempTrip.SetDownOnly;
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTripJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Serializes the WTTrip object to JSON
        /// </summary>
        /// <returns></returns>s
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new WTTTripConverter());
        }

        #endregion Methods
    }
}
