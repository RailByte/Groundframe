using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    public class WTTHeader
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _VersionBuild; //Stores the Build Version Number
        private readonly UserSettingCollection _UserSettings; //Stores the user settings
        private readonly DateTime _StartDate; //Stores the timetable start date

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the WTT name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the WTT description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the WTT start time
        /// </summary>
        [JsonProperty("startTime")]
        public WTTTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the WTT finish time
        /// </summary>
        [JsonProperty("finishTime")]
        public WTTTime FinishTime { get; set; }

        /// <summary>
        /// Gets or sets the WTT major version
        /// </summary>
        [JsonProperty("versionMajor")]
        public int VersionMajor { get; set; }

        /// <summary>
        /// Gets or sets the WTT minor version
        /// </summary>
        [JsonProperty("versionMinor")]
        public int VersionMinor { get; set; }

        /// <summary>
        /// Gets or sets the WTT version build number
        /// </summary>
        [JsonProperty("versionBuild")]
        public int VersionBuild { get { return this._VersionBuild; } }

        /// <summary>
        /// Gets or sets the WTT Train Decsrption Template
        /// </summary>
        [JsonProperty("trainDescriptionTemplate")]
        public string TrainDescriptionTemplate { get; set; }

        /// <summary>
        /// Gets the timetable start date
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime StartDate { get { return this._StartDate; } }

        /// <summary>
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this.GetSimulationUserSettings(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private WTTHeader(DateTime StartDate)
        {
            this._StartDate = StartDate;
        }

        /// <summary>
        /// Instantiates a WTTHeader object from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header">XElement containing the WTT XML defining this header object</param>
        /// <param name="StartDate">The start date of the timetable (cannot be before 01/01/1850)</param>
        /// <param name="UserSettings">The users settings</param>
        public WTTHeader(XElement Header, DateTime StartDate, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this._UserSettings);

            //Validate arguments
            ArgumentValidation.ValidateXElement(Header, Culture);
            ArgumentValidation.ValidateWTTStartDate(StartDate, Culture);

            this._StartDate = StartDate;

            //Parse the header XML
            this.ParseHeaderXML(Header);
        }

        /// <summary>
        /// Instantiates an empty WTTheader from a JSON string
        /// </summary>
        /// <param name="UserSettings">The users settings</param>
        public WTTHeader(string JSON, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            //Validate settings
            ArgumentValidation.ValidateJSON(JSON, UserSettingHelper.GetCultureInfo(this._UserSettings));

            //Populate from JSON
            this.PopulateFromJSON(JSON);
        }

        /// <summary>
        /// Instantiates a WTTHeader object from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header">XElement containing the WTT XML defining this header object</param>
        /// <param name="UserSettings">The users settings</param>
        public WTTHeader(XElement Header, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            //Validate arguments
            ArgumentValidation.ValidateXElement(Header, UserSettingHelper.GetCultureInfo(this._UserSettings));

            //Set Start Date to 01/01/1850 which is the earliest date SimSig allows
            this._StartDate = new DateTime(1850,1,1);
            //Parse the header XML
            this.ParseHeaderXML(Header); 
        }

        /// <summary>
        /// Instantiates a new WTTHeader object
        /// </summary>
        /// <param name="Name">The WTT Name</param>
        /// <param name="StartTimeSeconds">The start time of the WTT (number of seconds after midnight)</param>
        /// <param name="UserSettings">The users settings</param>
        public WTTHeader(string Name, int StartTimeSeconds, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            this.Name = Name;
            this.StartTime = new WTTTime(StartTimeSeconds, this.UserSettings);
            this.FinishTime = new WTTTime(0, this.UserSettings);
            this.VersionMajor = 1;
            this.TrainDescriptionTemplate = "$originTime $originName-$destName $operator ($stock)";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses the WTTHeader from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header"></param>
        private void ParseHeaderXML(XElement Header)
        {
            //Set Culture
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);

            try
            {

                //Parse XML
                this.Name = XMLMethods.GetValueFromXElement<string>(Header, @"Name", Culture);
                this.Description = XMLMethods.GetValueFromXElement<string>(Header, @"Description", Culture, string.Empty);
                this.StartTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(Header, @"StartTime", Culture, 0), this._StartDate, this.UserSettings);
                this.FinishTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(Header, @"FinishTime", Culture, 0), this._StartDate, this.UserSettings);
                this.VersionMajor = XMLMethods.GetValueFromXElement<int>(Header, @"VMajor", Culture, 1);
                this.VersionMinor = XMLMethods.GetValueFromXElement<int>(Header, @"VMinor", Culture, 0);
                this._VersionBuild = XMLMethods.GetValueFromXElement<int>(Header, @"VBuild", Culture, 0);
                this.TrainDescriptionTemplate = XMLMethods.GetValueFromXElement<string>(Header, @"TrainDescriptionTemplate", Culture, @"$originTime $originName-$destName $operator ($stock)");
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseWTTHeaderException", null, Culture), Ex);
            }
        }

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTHeader object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                JsonConvert.PopulateObject(JSON, this);
                
                //Pass UserSettings
                if(this.StartTime != null)
                {
                    this.StartTime.OnRequestUserSettings += new Func<UserSettingCollection>(delegate { return this.UserSettings; });
                }

                if (this.FinishTime != null)
                {
                    this.FinishTime.OnRequestUserSettings += new Func<UserSettingCollection>(delegate { return this.UserSettings; });
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseUserSettingsJSONError", null, UserSettingHelper.GetCultureInfo(this._UserSettings)), Ex);
            }
        }

        /// <summary>
        /// Serializes the WTTHeader object to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
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

        #endregion Methods
    }
}
