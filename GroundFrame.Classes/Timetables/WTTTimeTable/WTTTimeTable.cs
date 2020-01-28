using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using Newtonsoft.Json;

namespace GroundFrame.Classes.Timetables
{
    public class WTTTimeTable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly UserSettingCollection _UserSettings; //Stores the user settings
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
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this.GetSimulationUserSettings(); } }

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
        /// <param name="UserSettings">The users settings</param>
        public WTTTimeTable(XElement TimeTableXML, DateTime StartDate, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            this._StartDate = StartDate;

            //Validate arguments
            ArgumentValidation.ValidateXElement(TimeTableXML, UserSettingHelper.GetCultureInfo(this.UserSettings));
            ArgumentValidation.ValidateWTTStartDate(StartDate, UserSettingHelper.GetCultureInfo(this.UserSettings));

            //Parse the XML
            this.ParseWTTTimeTable(TimeTableXML);
        }

        /// <summary>
        /// Instantiates a WTTTimeTable object from the supplied JSON string
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTimeTable object</param>
        /// <param name="UserSettings">The user settignd</param>
        public WTTTimeTable(string JSON, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            //Valdate Arguments
            ArgumentValidation.ValidateJSON(JSON, UserSettingHelper.GetCultureInfo(this.UserSettings));

            //Parse the JSON
            this.PopulateFromJSON(JSON);
        }

        #endregion Constructors

        #region Methods

        //Parses the Run As Required Percentage argument
        private void ParseRunAsRequiredPercentage (int Percentage)
        {
            ArgumentValidation.ValidatePercentage(Percentage, UserSettingHelper.GetCultureInfo(this.UserSettings));
            this._RunAsRequiredPercentage = Percentage;
        }


        /// <summary>
        /// Parses the WTTTimeTable object from the SimSig timeable XML element
        /// </summary>
        /// <param name="TimeTableXML"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void ParseWTTTimeTable(XElement TimeTableXML)
        {
            //Set Culture
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);
            try
            {
                //Parse XML
                this.Headcode = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ID", Culture, string.Empty);
                this.AccelBrakeIndex = (WTTAccelBrakeIndex)XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AccelBrakeIndex", Culture, WTTAccelBrakeIndex.MediumInterCity);
                this.RunAsRequiredPercentage = XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AsRequiredPercent", Culture, 50);
                this.Delay = new WTTDuration(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"Delay", Culture, 0), this.UserSettings);
                this.DepartTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"DepartTime", Culture, 0), this._StartDate, this.UserSettings);
                this.Description = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Description", Culture, string.Empty);
                this.SeedingGap = new WTTDuration(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"SeedingGap", Culture, 0), this.UserSettings);
                this.EntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"EntryPoint", Culture, string.Empty);
                this.ActualEntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ActualEntryPoint", Culture, string.Empty);
                this.MaxSpeed = new WTTSpeed(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"MaxSpeed", Culture, 0), this.UserSettings);
                this.SpeedClass = new WTTSpeedClass(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"SpeedClass", Culture, 0), this.UserSettings);
                this.Started = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"Started", Culture, 0));
                this.TrainLength = new Length(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"TrainLength", Culture, 20));
                this.Electrification = new Electrification(XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Electrification", Culture, null));
                this.RunAsRequiredTested = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AsRequiredTested", Culture, 0));
                //TODO: Entry Warned
                this.StartTraction = new Electrification(XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"StartTraction", Culture, string.Empty));
                this.SimSigTrainCategoryID = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Category", Culture, string.Empty);

                //Parse Trips
                this.ParseTripsXML(TimeTableXML.Element("Trips"));

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTimeTableException", null, Culture), Ex);
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
                    Trip.Add(new WTTTrip(WTTTripXML, this._StartDate, this.UserSettings));
                }
            }
        }

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTimeTable object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                JsonConvert.PopulateObject(JSON, this);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTimeTableJSONError", null, UserSettingHelper.GetCultureInfo(this.UserSettings)), Ex);
            }
        }

        /// <summary>
        /// Serializes the WTTTimeTable object to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
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
