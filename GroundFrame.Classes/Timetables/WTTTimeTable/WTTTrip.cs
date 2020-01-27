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
        private readonly DateTime _StartDate; //Stores the timetable start date

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
        /// Gets the start date
        /// </summary>
        [JsonProperty("startDate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private DateTime StartDate { get { return this._StartDate; } }

        /// <summary>
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this._UserSettings; } }

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
            this._StartDate = StartDate;

            //Check Header Argument
            if (WTTTripXML == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "WTTTripXML" }, UserSettingHelper.GetCultureInfo(this.UserSettings)));
            }

            //Parse the XML
            this.ParseWTTTripXML(WTTTripXML);
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private WTTTrip(DateTime StartDate)
        {
            this._StartDate = StartDate;
        }

        #endregion Constructors

        #region Methods

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
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTripException", null, Culture), Ex);
            }
        }

        #endregion Methods
    }
}
