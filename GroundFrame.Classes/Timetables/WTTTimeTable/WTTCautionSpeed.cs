using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Class representing a SimSig Caution Speed
    /// </summary>
    public class WTTCautionSpeed
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the passed Signal Aspect
        /// </summary>
        [JsonProperty("aspectPassed")]
        public WTTSignalAspect AspectPassed { get; set; }

        /// <summary>
        /// Gets or sets the From Line Speed
        /// </summary>
        [JsonProperty("fromLineSpeed")]
        public WTTSpeed FromLineSpeed { get; set; }

        /// <summary>
        /// Gets or sets the Reduce Now Value
        /// </summary>
        [JsonProperty("reduceNowValue")]
        public int ReduceNowValue { get; set; }

        /// <summary>
        /// /Gets or sets the next Approach Next Value
        /// </summary>
        [JsonProperty("approachNextValue")]
        public WTTSpeed ApproachNextValue { get; set; }

        /// <summary>
        /// /Gets or sets the next Approach Next Distance
        /// </summary>
        [JsonProperty("approachNextDistance")]
        public Length ApproachNextDistance { get; set; }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new WTTCautionSpeed object. Used by the JSON deserializer
        /// </summary>
        [JsonConstructor]
        private WTTCautionSpeed ()
        {

        }

        /// <summary>
        /// Instantiates a new WTTCautionSpeed object from the supplied SimSig XML snippet (as an XElement)
        /// </summary>
        /// <param name="WTTCautionSpeedXML">The SimSig XML Snippet (as an XElement) representing a single WTT Caution Speed</param>
        public WTTCautionSpeed (XElement WTTCautionSpeedXML)
        {
            //Validate Arguments
            ArgumentValidation.ValidateXElement(WTTCautionSpeedXML, Globals.UserSettings.GetCultureInfo());
            //Parse the XML
            this.ParseWTTCautionSpeedXML(WTTCautionSpeedXML);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses a CautionSpeedXML XElement into this object
        /// </summary>
        /// <param name="WTTCautionSpeedXML">The SimSig XML Snippet (as an XElement) representing a single WTT Caution Speed</param>
        private void ParseWTTCautionSpeedXML(XElement WTTCautionSpeedXML)
        {
            try
            {
                //Parse XML
                this.AspectPassed = (WTTSignalAspect)XMLMethods.GetValueFromXElement<int>(WTTCautionSpeedXML, @"AspectPassed", (int)WTTSignalAspect.Red);
                this.FromLineSpeed = XMLMethods.GetValueFromXElement<WTTSpeed>(WTTCautionSpeedXML, @"FromLineSpeed", null);
                this.ReduceNowValue = XMLMethods.GetValueFromXElement<int>(WTTCautionSpeedXML, @"ReduceNowValue", 0);
                this.ApproachNextValue = XMLMethods.GetValueFromXElement<WTTSpeed>(WTTCautionSpeedXML, @"ApproachNextValue", null);
                this.ApproachNextDistance = XMLMethods.GetValueFromXElement<Length>(WTTCautionSpeedXML, @"ApproachNextDistance", null);
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTCautionSpeedException", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        #endregion Methods
    }
}
