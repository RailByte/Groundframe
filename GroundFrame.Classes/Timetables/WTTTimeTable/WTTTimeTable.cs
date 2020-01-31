using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A class that represents a single train service
    /// </summary>
    public class WTTTimeTable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _RunAsRequiredPercentage; //Stores the run and required percentage
        private DateTime _StartDate; //Stores the start date of the timetable

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the start date
        /// </summary>
        [JsonProperty("startDate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        public DateTime StartDate { get { return this._StartDate; } }

        /// <summary>
        /// Gets or sets the Timetable Headcode
        /// </summary>
        [Key]
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
        [JsonProperty("trip")]
        public WTTTripCollection Trip { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        internal WTTTimeTable(DateTime StartDate)
        {
            this._StartDate = StartDate;
        }

        /// <summary>
        /// Instantiates a WTTTimeTable object from the supplied SimSig XML timeTable defininition snippet
        /// </summary>
        /// <param name="TimeTableXML">The SimSig timetable XML snippet</param>
        /// <param name="StartDate">The start date of the timetable</param>
        public WTTTimeTable(XElement TimeTableXML, DateTime StartDate)
        {
            this._StartDate = StartDate;

            //Validate arguments
            ArgumentValidation.ValidateXElement(TimeTableXML, Globals.UserSettings.GetCultureInfo());
            ArgumentValidation.ValidateWTTStartDate(StartDate, Globals.UserSettings.GetCultureInfo());

            //Parse the XML
            this.ParseWTTTimeTable(TimeTableXML);
        }

        /// <summary>
        /// Instantiates a WTTTimeTable object from the supplied JSON string
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTimeTable object</param>
        public WTTTimeTable(string JSON)
        {
            //Valdate Arguments
            ArgumentValidation.ValidateJSON(JSON, Globals.UserSettings.GetCultureInfo());

            //Parse the JSON
            this.PopulateFromJSON(JSON);
        }

        /// <summary>
        /// Instantiates a new WTTTimeTable object from a WTTTimeTableSurrogate object
        /// </summary>
        /// <param name="SurrogateWTTTimeTable">The source WTTTimeTableSurrogate object</param>
        internal WTTTimeTable(WTTTimeTableSurrogate SurrogateWTTTimeTable)
        {
            this.ParseSurrogateWTTTimeTable(SurrogateWTTTimeTable);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses a WTTTimwTableSurrogate object into a WTTTimeTable Object
        /// </summary>
        /// <param name="SurrogateWTTTimeTable">The source WTTTimwTableSurrogate object</param>
        private void ParseSurrogateWTTTimeTable(WTTTimeTableSurrogate SurrogateWTTTimeTable)
        {
            this._StartDate = SurrogateWTTTimeTable.StartDate;
            this.Headcode = SurrogateWTTTimeTable.Headcode;
            this.AccelBrakeIndex = SurrogateWTTTimeTable.AccelBrakeIndex;
            this.RunAsRequiredPercentage = SurrogateWTTTimeTable.RunAsRequiredPercentage;

            if (SurrogateWTTTimeTable.Delay != null)
            {
                this.Delay = new WTTDuration(SurrogateWTTTimeTable.Delay.Seconds);
            }

            if (SurrogateWTTTimeTable.DepartTime != null)
            {
                this.DepartTime = new WTTTime(SurrogateWTTTimeTable.DepartTime.Seconds);
            }

            this.Description = SurrogateWTTTimeTable.Description;

            if (SurrogateWTTTimeTable.SeedingGap != null)
            {
                this.SeedingGap = new WTTDuration(SurrogateWTTTimeTable.SeedingGap.Seconds);
            }

            this.EntryPoint = SurrogateWTTTimeTable.EntryPoint;
            this.ActualEntryPoint = SurrogateWTTTimeTable.ActualEntryPoint;
            this.MaxSpeed = SurrogateWTTTimeTable.MaxSpeed;
            this.SpeedClass = SurrogateWTTTimeTable.SpeedClass;
            this.Started = SurrogateWTTTimeTable.Started;
            this.TrainLength = SurrogateWTTTimeTable.TrainLength;
            this.Electrification = SurrogateWTTTimeTable.Electrification;
            this.RunAsRequiredTested = SurrogateWTTTimeTable.RunAsRequiredTested;
            this.StartTraction = SurrogateWTTTimeTable.StartTraction;
            this.SimSigTrainCategoryID = SurrogateWTTTimeTable.SimSigTrainCategoryID;
            this.Trip = new WTTTripCollection(SurrogateWTTTimeTable.StartDate);

            foreach (WTTTrip Trip in SurrogateWTTTimeTable.Trip.ToList())
            {
                this.Trip.Add(new WTTTrip(Trip.ToSurrogateWTTTrip()));
            }
        }

        /// <summary>
        /// Parses the Run As Required Percentage argument
        /// </summary>
        /// <param name="Percentage">The Run as Required Percentage</param>
        private void ParseRunAsRequiredPercentage (int Percentage)
        {
            ArgumentValidation.ValidatePercentage(Percentage, Globals.UserSettings.GetCultureInfo());
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
                //Parse XML
                this.Headcode = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ID",  string.Empty);
                this.AccelBrakeIndex = (WTTAccelBrakeIndex)XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AccelBrakeIndex", WTTAccelBrakeIndex.MediumInterCity);
                this.RunAsRequiredPercentage = XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AsRequiredPercent", 50);
                this.Delay = new WTTDuration(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"Delay", 0));
                this.DepartTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"DepartTime", 0), this._StartDate);
                this.Description = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Description", string.Empty);
                this.SeedingGap = new WTTDuration(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"SeedingGap", 0));
                this.EntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"EntryPoint", string.Empty);
                this.ActualEntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ActualEntryPoint", string.Empty);
                this.MaxSpeed = new WTTSpeed(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"MaxSpeed", 0));
                this.SpeedClass = new WTTSpeedClass(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"SpeedClass", 0));
                this.Started = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"Started", 0));
                this.TrainLength = new Length(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"TrainLength", 20));
                this.Electrification = new Electrification(XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Electrification", null));
                this.RunAsRequiredTested = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AsRequiredTested", 0));
                //TODO: Entry Warned
                this.StartTraction = new Electrification(XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"StartTraction", string.Empty));
                this.SimSigTrainCategoryID = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Category", string.Empty);

                //Parse Trips
                this.ParseTripsXML(TimeTableXML.Element("Trips"));

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTimeTableException", null, Globals.UserSettings.GetCultureInfo()), Ex);
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
                this.Trip = new WTTTripCollection(WTTTripsXML, this.StartDate);
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
                WTTTimeTable TempTimeTable = JsonConvert.DeserializeObject<WTTTimeTable>(JSON, new WTTTimeTableConverter());
                this._StartDate = TempTimeTable.StartDate;
                this.Headcode = TempTimeTable.Headcode;
                this.AccelBrakeIndex = TempTimeTable.AccelBrakeIndex;
                this.RunAsRequiredPercentage = TempTimeTable.RunAsRequiredPercentage;
                this.Delay = TempTimeTable.Delay;
                this.DepartTime = TempTimeTable.DepartTime;
                this.Description = TempTimeTable.Description;
                this.SeedingGap = TempTimeTable.SeedingGap;
                this.EntryPoint = TempTimeTable.EntryPoint;
                this.ActualEntryPoint = TempTimeTable.ActualEntryPoint;
                this.MaxSpeed = TempTimeTable.MaxSpeed;
                this.SpeedClass = TempTimeTable.SpeedClass;
                this.Started = TempTimeTable.Started;
                this.TrainLength = TempTimeTable.TrainLength;
                this.Electrification = TempTimeTable.Electrification;
                this.RunAsRequiredTested = TempTimeTable.RunAsRequiredTested;
                this.StartTraction = TempTimeTable.StartTraction;
                this.SimSigTrainCategoryID = TempTimeTable.SimSigTrainCategoryID;
                this.Trip = TempTimeTable.Trip;
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTimeTableJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Serializes the WTTTimeTable object to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new WTTTimeTableConverter());
        }

        /// <summary>
        /// Converts the WTTTimeTable object to a WTTTimeTableSurrogate object
        /// </summary>
        /// <returns>WTTTimeTableSurrogate</returns>
        internal WTTTimeTableSurrogate ToSurrogateWTTTimeTable()
        {
            return new WTTTimeTableSurrogate
            {
                Headcode = this.Headcode,
                AccelBrakeIndex = this.AccelBrakeIndex,
                RunAsRequiredPercentage = this.RunAsRequiredPercentage,
                Delay = this.Delay,
                DepartTime = this.DepartTime,
                Description = this.Description,
                SeedingGap = this.SeedingGap,
                EntryPoint = this.EntryPoint,
                ActualEntryPoint = this.ActualEntryPoint,
                MaxSpeed = this.MaxSpeed,
                SpeedClass = this.SpeedClass,
                Started = this.Started,
                TrainLength = this.TrainLength,
                Electrification = this.Electrification,
                RunAsRequiredTested = this.RunAsRequiredTested,
                StartTraction = this.StartTraction,
                SimSigTrainCategoryID = this.SimSigTrainCategoryID,
                Trip = this.Trip,
                StartDate = this.StartDate
            };
        }

        #endregion Methods
    }
}
