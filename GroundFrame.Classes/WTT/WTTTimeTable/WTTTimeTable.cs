using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using Newtonsoft.Json;

namespace GroundFrame.Classes
{
    public class WTTTimeTable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly CultureInfo _Culture; //Stores the culture
        private int _RunAsRequiredPercentage; //Stores the run and required percentage
        private readonly DateTime _StartDate; //Stores the start date of the timetable

        #endregion Private Variables

        #region Properties
        /// <summary>
        /// Gets or sets the Timetable Headcode
        /// </summary>
        [JsonProperty("headcode")]
        public string Headcode { get; set; }

        /// <summary>
        /// Gets or sets the Acceleration / Break Index
        /// </summary>
        [JsonProperty("accelBrakeIndex")]
        public WTTAccelBrakeIndex AccelBrakeIndex { get; set; }

        /// <summary>
        /// Gets or sets the run as required percentage
        /// </summary>
        [JsonProperty("runAsRequiredPercentage")]
        public int RunAsRequiredPercentage { get { return this._RunAsRequiredPercentage; } set { this.ParseRunAsRequiredPercentage(value); } }

        /// <summary>
        /// Gets or sets the dely of the service
        /// </summary>
        [JsonProperty("delay")]
        public WTTDuration Delay { get; set; }

        /// <summary>
        /// Gets or sets the departure time of the service
        /// </summary>
        [JsonProperty("departTime")]
        public WTTTime DepartTime { get; set; }

        /// <summary>
        /// Gets or sets the description of the service
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the seeding gap
        /// </summary>
        [JsonProperty("seedingGap")]
        public WTTDuration SeedingGap { get; set; }

        /// <summary>
        /// Gets or sets the entry point
        /// </summary>
        [JsonProperty("entryPoint")]
        public string EntryPoint { get; set; }

        /// <summary>
        /// Gets or sets the actual entry point
        /// </summary>
        [JsonProperty("actualEntryPoint")]
        public string ActualEntryPoint { get; set; }

        /// <summary>
        /// Gets or sets the max speed
        /// </summary>
        [JsonProperty("maxSpeed")]
        public WTTSpeed MaxSpeed { get; set; }

        /// <summary>
        /// Gets or sets the speed class
        /// </summary>
        [JsonProperty("speedClass")]
        public WTTSpeedClass SpeedClass { get; set; }

        /// <summary>
        /// Gets or sets whether the service has started
        /// </summary>
        [JsonProperty("started")]
        public bool Started { get; set; }

        /// <summary>
        /// Gets or sets the train length
        /// </summary>
        [JsonProperty("trainLength")]
        public Length TrainLength { get; set; }

        /// <summary>
        /// Gets or sets the electrification
        /// </summary>
        [JsonProperty("electrification")]
        public Electrification Electrification { get; set; }

        /// <summary>
        /// Gets or sets whether run as required has been tested
        /// </summary>
        [JsonProperty("runAsRequiredTested")]
        public bool RunAsRequiredTested { get; set; }

        //TODO: EntryWarned

        /// <summary>
        /// Gets or sets the traction type at the start of the timetable
        /// </summary>
        [JsonProperty("startTraction")]
        public Electrification StartTraction { get; set; }

        /// <summary>
        /// Gets or sets the WTTTrainCategory ID
        /// </summary>
        [JsonProperty("trainCategoryId")]
        public string SimSigTrainCategoryID { get; set; }

        /// <summary>
        /// Gets or sets the trip for this service
        /// </summary>
        public List<WTTTrip> Trip { get; set; }

        /// <summary>
        /// Gets the start date
        /// </summary>
        [JsonProperty("startDate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private DateTime StartDate { get { return this._StartDate; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private WTTTimeTable(DateTime StartDate)
        {
            this._StartDate = StartDate;
        }

        /// <summary>
        /// Instantiates a WTTTimeTable object from the supplied SimSig XML timeTable defininition snippet
        /// </summary>
        /// <param name="TimeTableXML">The SimSig timetable XML snippet</param>
        /// <param name="StartDate">The start date of the timetable</param>
        /// <param name="Culture">The preferred culture of the user</param>
        public WTTTimeTable(XElement TimeTableXML, DateTime StartDate, string Culture  = "en-GB")
        {
            this._Culture = new CultureInfo(string.IsNullOrEmpty(Culture) ? "en-GB" : Culture);
            this._StartDate = StartDate;

            //Check Header Argument
            if (TimeTableXML == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "TimeTableXML" }, this._Culture));
            }

            //Parse the XML
            this.ParseWTTTimeTable(TimeTableXML);
        }

        #endregion Constructors

        #region Methods

        //Parses the Run As Required Percentage argument
        private void ParseRunAsRequiredPercentage (int Percentage)
        {
            ArgumentValidation.ValidatePercentage(Percentage, this._Culture);
            this._RunAsRequiredPercentage = Percentage;
        }


        /// <summary>
        /// Parses the WTTTimeTable object from the SimSig timeable XML element
        /// </summary>
        /// <param name="TimeTableXML"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void ParseWTTTimeTable(XElement TimeTableXML)
        {
            try
            {
                this.Headcode = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ID", string.Empty, this._Culture.Name);
                this.AccelBrakeIndex = (WTTAccelBrakeIndex)XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AccelBrakeIndex", WTTAccelBrakeIndex.MediumInterCity, this._Culture.Name);
                this.RunAsRequiredPercentage = XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AsRequiredPercent", 50, this._Culture.Name);
                this.Delay = new WTTDuration(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"Delay", 0, this._Culture.Name), "H", this._Culture.Name);
                this.DepartTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"DepartTime", 0, this._Culture.Name), this._StartDate, "H", this._Culture.Name);
                this.Description = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Description", string.Empty, this._Culture.Name);
                this.SeedingGap = new WTTDuration(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"SeedingGap", 0, this._Culture.Name), "H", this._Culture.Name);
                this.EntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"EntryPoint", string.Empty, this._Culture.Name);
                this.ActualEntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ActualEntryPoint", string.Empty, this._Culture.Name);
                this.MaxSpeed = new WTTSpeed(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"MaxSpeed", 0, this._Culture.Name));
                this.SpeedClass = new WTTSpeedClass(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"SpeedClass", 0, this._Culture.Name), this._Culture.Name);
                this.Started = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"Started", 0, this._Culture.Name));
                this.TrainLength = new Length(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"TrainLength", 20, this._Culture.Name));
                this.Electrification = new Electrification(XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Electrification", null, this._Culture.Name));
                this.RunAsRequiredTested = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AsRequiredTested", 0, this._Culture.Name));
                //TODO: Entry Warned
                this.StartTraction = new Electrification(XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"StartTraction", string.Empty, this._Culture.Name));
                this.SimSigTrainCategoryID = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Category", string.Empty, this._Culture.Name);

                //Parse Trips
                this.ParseTripsXML(TimeTableXML.Element("Trips"));

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTimeTableException", null, this._Culture), Ex);
            }
        }

        /// <summary>
        /// Parses the WTT Trips SimSig XML
        /// </summary>
        /// <param name="WTTTripsXML"></param>
        private void ParseTripsXML(XElement WTTTripsXML)
        {
            if (WTTTripsXML.Descendants() != null)
            {
                Trip = new List<WTTTrip>();

                ///Loop around each trip and add to the Trips Collection
                foreach (XElement WTTTripXML in WTTTripsXML.Elements("Trip"))
                {
                    Trip.Add(new WTTTrip(WTTTripXML, this._StartDate, this._Culture.Name));
                }
            }
        }

        #endregion Methods
    }
}
