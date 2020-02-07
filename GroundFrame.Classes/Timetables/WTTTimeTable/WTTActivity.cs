using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Enum to describe the different WTT activity types
    /// </summary>
    public enum WTTActivityType
    {
        /// <summary>
        /// The next service this service will form
        /// </summary>
        NextTrain = 0
    }


    /// <summary>
    /// A class which represents a single trip activity.
    /// </summary>
    public class WTTActivity
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the activity type
        /// </summary>
        [JsonProperty("activityType")]
        public WTTActivityType ActivityType { get; set; }

        /// <summary>
        /// Gets or sets the associated train headcode
        /// </summary>
        [JsonProperty("associatedTrainHeaderCode")]
        public string AssociatedTrainHeadCode { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new WTTActivity from the supplied SimSig XML snippet (as an XElement).
        /// </summary>
        /// <param name="WTTActivityXML">The source SimSig XML snippet (as an XElement)</param>
        public WTTActivity (XElement WTTActivityXML)
        {
            //Validate Arguments
            ArgumentValidation.ValidateXElement(WTTActivityXML, Globals.UserSettings.GetCultureInfo());

            //Parse the XML
            this.ParseWTTActivityXML(WTTActivityXML);
        }

        /// <summary>
        /// Instantiates a WTTActivity object from the supplied JSON string
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTrip object</param>
        public WTTActivity(string JSON)
        {
            //Valdate Arguments
            ArgumentValidation.ValidateJSON(JSON, Globals.UserSettings.GetCultureInfo());

            //Parse the JSON
            this.PopulateFromJSON(JSON);
        }

        /// <summary>
        /// Private constructor to be used by JSON deserializer
        /// </summary>
        [JsonConstructor]
        private WTTActivity()
        {

        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Serializes the WTTActivity object to JSON
        /// </summary>
        /// <returns>A JSON string representing the objecct</returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTActivity object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                JsonConvert.PopulateObject(JSON, this);              
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTActivityJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Parses a SimSig XML snippet (as an XElement) which represents a single activity
        /// </summary>
        /// <param name="WTTActivityXML">The SimSig XML snippet (as an XElement) to parse</param>
        private void ParseWTTActivityXML(XElement WTTActivityXML)
        {
            this.ActivityType = (WTTActivityType)XMLMethods.GetValueFromXElement<int>(WTTActivityXML, @"Activity", 0);
            this.AssociatedTrainHeadCode = XMLMethods.GetValueFromXElement<string>(WTTActivityXML, @"AssociatedTrain", null);
        }

        #endregion Methods
    }
}
