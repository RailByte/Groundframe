using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.WTT
{
    /// <summary>
    /// A Class representing a SimSig Trip
    /// </summary>
    public class WTTTrip
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly CultureInfo _Culture; //Stores the Culture Info for the instance
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

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a WTTTrip object from an XML element representing a SimSig trip
        /// </summary>
        /// <param name="WTTTripXML">The XML element representing a SimSig trip</param>
        /// <param name="StartDate">The timetable start date</param>
        /// <param name="Culture">The culture in which any error messages should be returned</param>
        public WTTTrip (XElement WTTTripXML, DateTime StartDate, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(string.IsNullOrEmpty(Culture) ? "en-GB" : Culture);
            this._StartDate = StartDate;

            //Check Header Argument
            if (WTTTripXML == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "WTTTripXML" }, this._Culture));
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
            try
            {
                this.Location = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Location", string.Empty, this._Culture.Name);
                this.DepPassTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"DepPassTime", 0, this._Culture.Name), this._StartDate, "H", this._Culture.Name);
                this.ArrTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"ArrTime", 0, this._Culture.Name), this._StartDate, "H", this._Culture.Name);
                this.IsPassTime = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"IsPassTime", 0, this._Culture.Name));
                this.Platform = XMLMethods.GetValueFromXElement<string>(WTTTripXML, @"Platform", string.Empty, this._Culture.Name);
                this.DownDirection = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"DownDirection", 0, this._Culture.Name));
                this.PrevPathEndDown = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"PrevPathEndDown", 0, this._Culture.Name));
                this.NextPathStartDown = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(WTTTripXML, @"NextPathStartown", 0, this._Culture.Name));
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTripException", null, this._Culture), Ex);
            }
        }

        #endregion Methods
    }
}
