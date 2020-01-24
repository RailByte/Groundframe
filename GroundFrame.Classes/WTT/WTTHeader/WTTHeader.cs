using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes
{
    public class WTTHeader
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _VersionBuild; //Stores the Build Version Number
        private readonly CultureInfo _Culture; //Stores the culture info
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
        public DateTime StartDate { get { return this._StartDate; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        private WTTHeader(DateTime StartDate)
        {
            this._StartDate = StartDate;
        }

        /// <summary>
        /// Instantiates a WTTHeader object from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header">XElement containing the WTT XML defining this header object</param>
        public WTTHeader(XElement Header, DateTime StartDate, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);
            this._StartDate = StartDate;
            this.ParseHeaderXML(Header); //Parse the header XML
        }

        /// <summary>
        /// Instantiates a new WTTHeader object
        /// </summary>
        /// <param name="Header">The WTT Name</param>
        public WTTHeader(string Name, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);
            this.Name = Name;
            this.StartTime = new WTTTime(0,"H");
            this.FinishTime = new WTTTime(0, "H");
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
            try
            {
                this.Name = XMLMethods.GetValueFromXElement<string>(Header, @"Name");
                this.Description = XMLMethods.GetValueFromXElement<string>(Header, @"Description", string.Empty);
                this.StartTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(Header, @"StartTime", 0), this._StartDate, "H");
                this.FinishTime = new WTTTime(XMLMethods.GetValueFromXElement<int>(Header, @"FinishTime", 0), this._StartDate, "H");
                this.VersionMajor = XMLMethods.GetValueFromXElement<int>(Header, @"VMajor", 1);
                this.VersionMinor = XMLMethods.GetValueFromXElement<int>(Header, @"VMinor", 0);
                this._VersionBuild = XMLMethods.GetValueFromXElement<int>(Header, @"VBuild", 0);
                this.TrainDescriptionTemplate = XMLMethods.GetValueFromXElement<string>(Header, @"TrainDescriptionTemplate", @"$originTime $originName-$destName $operator ($stock)");
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseWTTHeaderException", null, this._Culture), Ex);
            }
        }

        #endregion Methods
    }
}
