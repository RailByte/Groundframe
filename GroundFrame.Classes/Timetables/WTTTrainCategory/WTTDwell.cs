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
        public WTTDwell (XElement DwellXML)
        {
            //Check Header Argument
            if (DwellXML == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "DwellXML" }, Globals.UserSettings.GetCultureInfo()));
            }

            //Parse the XML
            this.ParseHeaderXML(DwellXML);
        }

        #endregion Constuctors

        #region Methods

        /// <summary>
        /// Parses Dwell element from the SimSigTimeable ? WTTTrainCategory element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="DwellXML">The source Dwell XML as an XElement</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void ParseHeaderXML(XElement DwellXML)
        {
            try
            {
                //If there are no dwell times records then return empty dwell object

                if (!DwellXML.HasElements)
                {
                    return;
                }

                int RedSignalMoveOff = XMLMethods.GetValueFromXElement<int>(DwellXML, @"RedSignalMoveOff", 0);

                if (RedSignalMoveOff != 0)
                {
                    this.RedSignalMoveOff = new WTTDuration(RedSignalMoveOff);
                }

                int StationForward = XMLMethods.GetValueFromXElement<int>(DwellXML, @"StationForward", 0);

                if (StationForward != 0)
                {
                    this.StationForward = new WTTDuration(StationForward);
                }

                int StationReverse = XMLMethods.GetValueFromXElement<int>(DwellXML, @"StationReverse", 0);

                if (StationReverse != 0)
                {
                    this.StationReverse = new WTTDuration(StationReverse);
                }

                int TerminateForward = XMLMethods.GetValueFromXElement<int>(DwellXML, @"TerminateForward", 0);

                if (TerminateForward != 0)
                {
                    this.TerminateForward = new WTTDuration(TerminateForward);
                }

                int TerminateReverse = XMLMethods.GetValueFromXElement<int>(DwellXML, @"TerminateReverse", 0);

                if (TerminateReverse != 0)
                {
                    this.TerminateReverse = new WTTDuration(TerminateReverse);
                }

                int Join = XMLMethods.GetValueFromXElement<int>(DwellXML, @"Join", 0);

                if (Join != 0)
                {
                    this.Join = new WTTDuration(Join);
                }

                int Divide = XMLMethods.GetValueFromXElement<int>(DwellXML, @"Divide", 0);

                if (Divide != 0)
                {
                    this.Divide = new WTTDuration(Divide);
                }

                int CrewChange = XMLMethods.GetValueFromXElement<int>(DwellXML, @"CrewChange", 0);

                if (CrewChange != 0)
                {
                    this.CrewChange = new WTTDuration(CrewChange);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTrainCategoryException", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        #endregion Methods
    }
}
