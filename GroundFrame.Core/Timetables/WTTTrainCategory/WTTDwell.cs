using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Core.Timetables
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
            //Validate Arguements
            ArgumentValidation.ValidateXElement(DwellXML, Globals.UserSettings.GetCultureInfo());

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

                this.RedSignalMoveOff = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"RedSignalMoveOff", null);
                this.StationForward = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"StationForward", null);
                this.StationReverse = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"StationReverse", null);
                this.TerminateForward = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"TerminateForward", null);
                this.TerminateReverse = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"TerminateReverse", null);
                this.Join = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"Join", null);
                this.Divide = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"Divide", null);
                this.CrewChange = XMLMethods.GetValueFromXElement<WTTDuration>(DwellXML, @"CrewChange", null);
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTrainCategoryException", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        #endregion Methods
    }
}
