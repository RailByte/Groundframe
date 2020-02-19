using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Core.Timetables
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
        /// Gets or sets the next Approach Next Value
        /// </summary>
        [JsonProperty("approachNextValue")]
        public int ApproachNextValue { get; set; }

        /// <summary>
        /// Gets or sets the next Approach Next Distance
        /// </summary>
        [JsonProperty("approachNextDistance")]
        public Length ApproachNextDistance { get; set; }

        /// <summary>
        /// Gets or sets the No Value type (N/a, Absolute or Percentage)
        /// </summary>
        [JsonProperty("nowValueType")]
        public WTTNumberType NowValueType { get; set; }

        /// <summary>
        /// Gets or sets the Next Valuetype (N/a, Absolute or Percentage)
        /// </summary>
        [JsonProperty("nextValueType")]
        public WTTNumberType NextValueType { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new WTTCautionSpeed object. Used by the JSON deserializer
        /// </summary>
        [JsonConstructor]
        private WTTCautionSpeed()
        {

        }

        /// <summary>
        /// Instantiates a WTTCautionSpeed object from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing the WTTCautionSpeed object</param>
        public WTTCautionSpeed(string JSON)
        {
            //Validate arguments
            ArgumentValidation.ValidateJSON(JSON, Globals.UserSettings.GetCultureInfo());

            //Try deserializing the string
            try
            {
                //Deserialize the JSON string
                this.PopulateFromJSON(JSON);

            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTCautionSpeedJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
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
        /// Populates the WTTCautionSpeed from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTCautionSpeed object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                JsonConvert.PopulateObject(JSON, this);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTCautionSpeedJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Gets a JSON string that represents the WTTCautionSpeed Collection
        /// </summary>
        /// <returns>A formatted JSON string representing the WTTCautionSpeed object</returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Parses a CautionSpeedXML XElement into this object
        /// </summary>
        /// <param name="WTTCautionSpeedXML">The SimSig XML Snippet (as an XElement) representing a single WTT Caution Speed</param>
        private void ParseWTTCautionSpeedXML(XElement WTTCautionSpeedXML)
        {
            try
            {
                //Parse XML
                this.AspectPassed = XMLMethods.GetValueFromXElement<WTTSignalAspect>(WTTCautionSpeedXML, @"AspectPassed", WTTSignalAspect.Red);
                this.FromLineSpeed = XMLMethods.GetValueFromXElement<WTTSpeed>(WTTCautionSpeedXML, @"FromLineSpeed", null);
                this.ReduceNowValue = XMLMethods.GetValueFromXElement<int>(WTTCautionSpeedXML, @"ReduceNowValue", 0);
                this.ApproachNextValue = XMLMethods.GetValueFromXElement<int>(WTTCautionSpeedXML, @"ApproachNextValue", 0);
                this.ApproachNextDistance = XMLMethods.GetValueFromXElement<Length>(WTTCautionSpeedXML, @"ApproachNextDistance", null);
                this.NowValueType = XMLMethods.GetValueFromXElement<WTTNumberType>(WTTCautionSpeedXML, @"NowValueType", WTTNumberType.NotApplicable);
                this.NextValueType = XMLMethods.GetValueFromXElement<WTTNumberType>(WTTCautionSpeedXML, @"NextValueType", WTTNumberType.NotApplicable);
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTCautionSpeedException", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        #endregion Methods
    }
}
