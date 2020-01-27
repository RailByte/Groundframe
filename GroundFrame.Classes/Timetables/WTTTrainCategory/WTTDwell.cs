using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Class which represents the various dwell times for a train category
    /// </summary>
    public class WTTDwell
    {
        #region Constants
        #endregion Constants

        #region Private Variables 

        private readonly UserSettingCollection _UserSettings; //Stores the user settings

        #endregion Private Variables

        #region Properties

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
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this._UserSettings; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        private WTTDwell()
        {
        }

        /// <summary>
        /// Instantiates a WTTDwell object from an XML Snippet
        /// </summary>
        /// <param name="DwellXML">The source XML as an XElement</param>
        /// <param name="UserSettings">The users settings. If null is pass then the default user settings will be used</param>
        public WTTDwell (XElement DwellXML, UserSettingCollection UserSettings)
        {
            //Set the culture
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            //Check Header Argument
            if (DwellXML == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "DwellXML" }, UserSettingHelper.GetCultureInfo(this.UserSettings)));
            }

            //Parse the XML
            this.ParseHeaderXML(DwellXML);
        }

        #endregion Constuctors

        #region Methods

        /// <summary>
        /// Parses Dwell element from the SimSigTimeable ? WTTTrainCategory element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void ParseHeaderXML(XElement DwellXML)
        {
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this.UserSettings);

            try
            {
                //If there are no dwell times records then return empty dwell object

                if (!DwellXML.HasElements)
                {
                    return;
                }

                int RedSignalMoveOff = XMLMethods.GetValueFromXElement<int>(DwellXML, @"RedSignalMoveOff", Culture, 0);

                if (RedSignalMoveOff != 0)
                {
                    this.RedSignalMoveOff = new WTTDuration(RedSignalMoveOff, this.UserSettings);
                }

                int StationForward = XMLMethods.GetValueFromXElement<int>(DwellXML, @"StationForward", Culture, 0);

                if (StationForward != 0)
                {
                    this.StationForward = new WTTDuration(StationForward, this.UserSettings);
                }

                int StationReverse = XMLMethods.GetValueFromXElement<int>(DwellXML, @"StationReverse", Culture, 0);

                if (StationReverse != 0)
                {
                    this.StationReverse = new WTTDuration(StationReverse, this.UserSettings);
                }

                int TerminateForward = XMLMethods.GetValueFromXElement<int>(DwellXML, @"TerminateForward", Culture, 0);

                if (TerminateForward != 0)
                {
                    this.TerminateForward = new WTTDuration(TerminateForward, this.UserSettings);
                }

                int TerminateReverse = XMLMethods.GetValueFromXElement<int>(DwellXML, @"TerminateReverse", Culture, 0);

                if (TerminateReverse != 0)
                {
                    this.TerminateReverse = new WTTDuration(TerminateReverse, this.UserSettings);
                }

                int Join = XMLMethods.GetValueFromXElement<int>(DwellXML, @"Join", Culture, 0);

                if (Join != 0)
                {
                    this.Join = new WTTDuration(Join, this.UserSettings);
                }

                int Divide = XMLMethods.GetValueFromXElement<int>(DwellXML, @"Divide", Culture, 0);

                if (Divide != 0)
                {
                    this.Divide = new WTTDuration(Divide, this.UserSettings);
                }

                int CrewChange = XMLMethods.GetValueFromXElement<int>(DwellXML, @"CrewChange", Culture, 0);

                if (CrewChange != 0)
                {
                    this.CrewChange = new WTTDuration(CrewChange, this.UserSettings);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTrainCategoryException", null, UserSettingHelper.GetCultureInfo(this.UserSettings)), Ex);
            }
        }

        #endregion Methods
    }
}
