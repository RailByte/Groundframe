using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Class which represents the various dwell times for a train category
    /// </summary>
    public class WTTDwell
    {
        #region Constants
        #endregion Constants

        #region Private Variables 

        private readonly CultureInfo _Culture; //Stores the culture the user wishes to use

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the red signal move off dwell time
        /// </summary>
        public WTTDuration RedSignalMoveOff { get; set; }

        /// <summary>
        /// Gets or sets the station forward dwell time
        /// </summary>
        public WTTDuration StationForward { get; set; }

        /// <summary>
        /// Gets or sets the station reverse dwell time
        /// </summary>
        public WTTDuration StationReverse { get; set; }

        /// <summary>
        /// Gets or sets the terminate forward dwell time
        /// </summary>
        public WTTDuration TerminateForward { get; set; }

        /// <summary>
        /// Gets or sets the terminate reverse dwell time
        /// </summary>
        public WTTDuration TerminateReverse { get; set; }

        /// <summary>
        /// Gets or sets the join dwell time
        /// </summary>
        public WTTDuration Join { get; set; }

        /// <summary>
        /// Gets or sets the divide dwell time
        /// </summary>
        public WTTDuration Divide { get; set; }

        /// <summary>
        /// Gets or sets the crew change dwell time
        /// </summary>
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
        /// <param name="Culture">The users culture used to ensure error messages are in the correct language</param>
        public WTTDwell (XElement DwellXML, string Culture = "en-GB")
        {
            //Set the culture
            this._Culture = new CultureInfo(string.IsNullOrEmpty("Culture") ? "en-GB" : Culture);

            //Check Header Argument
            if (DwellXML == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "DwellXML" }, this._Culture));
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
            try
            {
                //If there are no dwell times records then return empty dwell object

                if (!DwellXML.HasElements)
                {
                    return;
                }

                int RedSignalMoveOff = XMLMethods.GetValueFromXElement<int>(DwellXML, @"RedSignalMoveOff", 0, this._Culture.Name);

                if (RedSignalMoveOff != 0)
                {
                    this.RedSignalMoveOff = new WTTDuration(RedSignalMoveOff, "H", this._Culture.Name);
                }

                int StationForward = XMLMethods.GetValueFromXElement<int>(DwellXML, @"StationForward", 0, this._Culture.Name);

                if (StationForward != 0)
                {
                    this.StationForward = new WTTDuration(StationForward, "H", this._Culture.Name);
                }

                int StationReverse = XMLMethods.GetValueFromXElement<int>(DwellXML, @"StationReverse", 0, this._Culture.Name);

                if (StationReverse != 0)
                {
                    this.StationReverse = new WTTDuration(StationReverse, "H", this._Culture.Name);
                }

                int TerminateForward = XMLMethods.GetValueFromXElement<int>(DwellXML, @"TerminateForward", 0, this._Culture.Name);

                if (TerminateForward != 0)
                {
                    this.TerminateForward = new WTTDuration(TerminateForward, "H", this._Culture.Name);
                }

                int TerminateReverse = XMLMethods.GetValueFromXElement<int>(DwellXML, @"TerminateReverse", 0, this._Culture.Name);

                if (TerminateReverse != 0)
                {
                    this.TerminateReverse = new WTTDuration(TerminateReverse, "H", this._Culture.Name);
                }

                int Join = XMLMethods.GetValueFromXElement<int>(DwellXML, @"Join", 0, this._Culture.Name);

                if (Join != 0)
                {
                    this.Join = new WTTDuration(Join, "H", this._Culture.Name);
                }

                int Divide = XMLMethods.GetValueFromXElement<int>(DwellXML, @"Divide", 0, this._Culture.Name);

                if (Divide != 0)
                {
                    this.Divide = new WTTDuration(Divide, "H", this._Culture.Name);
                }

                int CrewChange = XMLMethods.GetValueFromXElement<int>(DwellXML, @"CrewChange", 0, this._Culture.Name);

                if (CrewChange != 0)
                {
                    this.CrewChange = new WTTDuration(CrewChange, "H", this._Culture.Name);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTrainCategoryException", null, this._Culture), Ex);
            }
        }

        #endregion Methods
    }
}
