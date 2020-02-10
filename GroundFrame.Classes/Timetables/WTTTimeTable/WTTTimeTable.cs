using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        [JsonProperty("headcode")]
        public string Headcode { get; set; }

        /// <summary>
        /// Gets or sets the Unique ID
        /// </summary>
        [JsonProperty("uid")]
        public string UID { get; set; }

        /// <summary>
        /// Gets or sets the Acceleration / Break Index
        /// </summary>
        [JsonProperty("accelBrakeIndex")]
        public WTTAccelBrakeIndex AccelBrakeIndex { get; set; }

        /// <summary>
        /// Gets or sets the run as required flag
        /// </summary>
        [JsonProperty("runAsRequired")]
        public bool RunAsRequired { get; set; }

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
        public Length SeedingGap { get; set; }

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
        /// Gets or sets the origin location of the service
        /// </summary>
        [JsonProperty("originName")]
        public string OriginName { get; set; }

        /// <summary>
        /// Gets or sets the destination location of the service
        /// </summary>
        [JsonProperty("destinationName")]
        public string DestinationName { get; set; }

        /// <summary>
        /// Gets or sets the origin time of the service
        /// </summary>
        [JsonProperty("originTime")]
        public WTTTime OriginTime { get; set; }

        /// <summary>
        /// Gets or sets the destination time of the service
        /// </summary>
        [JsonProperty("destinationTime")]
        public WTTTime DestinationTime { get; set; }

        /// <summary>
        /// Gets or sets the operator code of the service
        /// </summary>
        [JsonProperty("operatorCode")]
        public string OperatorCode { get; set; }

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
        /// Gets or sets the red signal move off dwell time
        /// </summary>
        [JsonProperty("redSignalMoveOff")]
        public WTTDuration RedSignalMoveOff { get; set; }

        /// <summary>
        /// Gets or sets the station forward dwell time
        /// </summary>
        [JsonProperty("stationForward")]
        public WTTDuration StationForward { get; set; }

        /// <summary>
        /// Gets or sets the station reverse dwell time
        /// </summary>
        [JsonProperty("stationReverse")]
        public WTTDuration StationReverse { get; set; }

        /// <summary>
        /// Gets or sets the terminate forward dwell time
        /// </summary>
        [JsonProperty("terminateForward")]
        public WTTDuration TerminateForward { get; set; }

        /// <summary>
        /// Gets or sets the terminate reverse dwell time
        /// </summary>
        [JsonProperty("terminalReverse")]
        public WTTDuration TerminateReverse { get; set; }

        /// <summary>
        /// Gets or sets the join dwell time
        /// </summary>
        [JsonProperty("join")]
        public WTTDuration Join { get; set; }

        /// <summary>
        /// Gets or sets the divide dwell time
        /// </summary>
        [JsonProperty("divide")]
        public WTTDuration Divide { get; set; }

        /// <summary>
        /// Gets or sets the crew change dwell time
        /// </summary>
        [JsonProperty("crewChange")]
        public WTTDuration CrewChange { get; set; }

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
            this.UID = SurrogateWTTTimeTable.UID;
            this.AccelBrakeIndex = SurrogateWTTTimeTable.AccelBrakeIndex;
            this.RunAsRequired = SurrogateWTTTimeTable.RunAsRequired;
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
            this.SeedingGap = SurrogateWTTTimeTable.SeedingGap;
            this.EntryPoint = SurrogateWTTTimeTable.EntryPoint;
            this.ActualEntryPoint = SurrogateWTTTimeTable.ActualEntryPoint;
            this.MaxSpeed = SurrogateWTTTimeTable.MaxSpeed;
            this.SpeedClass = SurrogateWTTTimeTable.SpeedClass;
            this.Started = SurrogateWTTTimeTable.Started;
            this.TrainLength = SurrogateWTTTimeTable.TrainLength;
            this.Electrification = SurrogateWTTTimeTable.Electrification;
            this.OriginName = SurrogateWTTTimeTable.OriginName;
            this.DestinationName = SurrogateWTTTimeTable.DestinationName;
            this.OriginTime = SurrogateWTTTimeTable.OriginTime;
            this.DestinationTime = SurrogateWTTTimeTable.DestinationTime;
            this.OperatorCode = SurrogateWTTTimeTable.OperatorCode;
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
                this.Headcode = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ID",  null);
                this.UID = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"UID", null);
                this.AccelBrakeIndex = XMLMethods.GetValueFromXElement<WTTAccelBrakeIndex>(TimeTableXML, @"AccelBrakeIndex", WTTAccelBrakeIndex.MediumInterCity);
                this.RunAsRequired = XMLMethods.GetValueFromXElement<bool>(TimeTableXML, @"AsRequired", false);
                this.RunAsRequiredPercentage = XMLMethods.GetValueFromXElement<int>(TimeTableXML, @"AsRequiredPercent", 50);
                this.Delay = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"Delay", null);
                this.DepartTime = XMLMethods.GetValueFromXElement<WTTTime>(TimeTableXML, @"DepartTime", null, new object[] { this._StartDate });
                this.Description = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Description", string.Empty);
                this.SeedingGap = XMLMethods.GetValueFromXElement<Length>(TimeTableXML, @"SeedingGap", new Length(15));
                this.EntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"EntryPoint", string.Empty);
                this.ActualEntryPoint = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"ActualEntryPoint", string.Empty);
                this.MaxSpeed = XMLMethods.GetValueFromXElement<WTTSpeed>(TimeTableXML, @"MaxSpeed", null);
                this.SpeedClass = XMLMethods.GetValueFromXElement<WTTSpeedClass>(TimeTableXML, @"SpeedClass", null);
                this.Started = XMLMethods.GetValueFromXElement<bool>(TimeTableXML, @"Started", false);
                this.TrainLength = XMLMethods.GetValueFromXElement<Length>(TimeTableXML, @"TrainLength", 20);
                this.Electrification = XMLMethods.GetValueFromXElement<Electrification>(TimeTableXML, @"Electrification", new Electrification("D"));
                this.OriginName = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"DestinationName", null);
                this.DestinationName = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"DestinationTime", null);
                this.OriginTime = XMLMethods.GetValueFromXElement<WTTTime>(TimeTableXML, @"OriginTime", null, new object[] { this._StartDate });
                this.DestinationTime = XMLMethods.GetValueFromXElement<WTTTime>(TimeTableXML, @"DestinationTime", null, new object[] { this._StartDate });
                this.OperatorCode = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"OperatorCode", null);
                this.RunAsRequiredTested = XMLMethods.GetValueFromXElement<bool>(TimeTableXML, @"AsRequiredTested", false);
                //TODO: Entry Warned
                this.StartTraction = XMLMethods.GetValueFromXElement<Electrification>(TimeTableXML, @"StartTraction", new Electrification("D"));
                this.SimSigTrainCategoryID = XMLMethods.GetValueFromXElement<string>(TimeTableXML, @"Category", string.Empty);
                this.RedSignalMoveOff = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"RedSignalMoveOff", null);
                this.StationForward = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"StationForward", null);
                this.StationReverse = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"StationReverse", null);
                this.TerminateForward = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"TerminateForward", null);
                this.TerminateReverse = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"TerminateReverse", null);
                this.Join = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"Join", null);
                this.Divide = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"Divide", null);
                this.CrewChange = XMLMethods.GetValueFromXElement<WTTDuration>(TimeTableXML, @"CrewChange", null);

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
                this.UID = TempTimeTable.UID;
                this.AccelBrakeIndex = TempTimeTable.AccelBrakeIndex;
                this.RunAsRequired = TempTimeTable.RunAsRequired;
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
                this.OriginName = TempTimeTable.OriginName;
                this.DestinationName = TempTimeTable.DestinationName;
                this.OriginTime = TempTimeTable.OriginTime;
                this.DestinationTime = TempTimeTable.DestinationTime;
                this.OperatorCode = TempTimeTable.OperatorCode;
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
                UID = this.UID,
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
                OriginName = this.OriginName,
                DestinationName = this.DestinationName,
                OriginTime = this.OriginTime,
                DestinationTime = this.DestinationTime,
                OperatorCode = this.OperatorCode,
                RunAsRequiredTested = this.RunAsRequiredTested,
                StartTraction = this.StartTraction,
                SimSigTrainCategoryID = this.SimSigTrainCategoryID,
                Trip = this.Trip,
                StartDate = this.StartDate
            };
        }

        /// <summary>
        /// Gets a list of the distinct locations in the service
        /// </summary>
        /// <returns>A MapperLocation list of the distinct location</returns>
        public List<MapperLocation> GetMapperLocations()
        {
            List<MapperLocation> LocationMapper = new List<MapperLocation>();

            if (string.IsNullOrEmpty(this.EntryPoint) == false)
            {
                LocationMapper.Add(new MapperLocation { Name = this.EntryPoint, IsEntryPoint = true, SimSigCode = this.EntryPoint });
            }

            //Now add trip locations

            Parallel.ForEach(this.Trip, Trip =>
            {
                LocationMapper.Add(new MapperLocation { Name = Trip.Location, IsEntryPoint = false, SimSigCode = Trip.Location });
            });


            return LocationMapper;
        }

        /// <summary>
        /// Gets a list of the distinct location nodes in the service
        /// </summary>
        /// <returns>A MapperLocationNode list of the distinct location nodes</returns>
        public List<MapperLocationNode> GetMapperLocationNodes()
        {
            List<MapperLocationNode> LocationMapperNode = new List<MapperLocationNode>();

            if (string.IsNullOrEmpty(this.EntryPoint) == false)
            {
                LocationMapperNode.Add(new MapperLocationNode { SimSigCode = this.EntryPoint, Platform = string.Empty, NextLocationNode = new MapperLocationNode { SimSigCode = this.Trip.IndexOf(0).Location, Platform = this.Trip.IndexOf(0).Platform } });

            }

            //Now add trip locations

            for (int i = 0; i < this.Trip.Count - 1; i++)
            {
                if (i < this.Trip.Count - 1)
                {
                    LocationMapperNode.Add(new MapperLocationNode { SimSigCode = this.Trip.IndexOf(i).Location, Platform = this.Trip.IndexOf(i).Platform, NextLocationNode = new MapperLocationNode { SimSigCode = this.Trip.IndexOf(i + 1).Location, Platform = this.Trip.IndexOf(i + 1).Platform } });
                }
                else
                {
                    LocationMapperNode.Add(new MapperLocationNode
                    {
                        SimSigCode = this.Trip.IndexOf(i).Location,
                        Platform = this.Trip.IndexOf(i).Platform,
                        NextLocationNode = new MapperLocationNode { SimSigCode = string.Empty, Platform = string.Empty, NextLocationNode = null}
                        
                    });
                }
            }

            return LocationMapperNode;
        }

        #endregion Methods
    }
}
