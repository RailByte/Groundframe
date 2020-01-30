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

        private readonly UserSettingCollection _UserSettings; //Private variable to store the user settings
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

        /// <summary>
        /// Gets the user settings associated with this activity
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this.GetSimulationUserSettings(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new WTTActivity from the supplied SimSig XML snippet (as an XElement).
        /// </summary>
        /// <param name="WTTActivityXML">The source SimSig XML snippet (as an XElement)</param>
        /// <param name="UserSettings">The user settings</param>
        public WTTActivity (XElement WTTActivityXML, UserSettingCollection UserSettings)
        {
            //Set the user settings
            this._UserSettings = UserSettings ?? new UserSettingCollection(); //Use default user settings if not provided

            //Validate Arguments
            ArgumentValidation.ValidateXElement(WTTActivityXML, this.UserSettings.GetCultureInfo());

            //Parse the XML
            this.ParseWTTActivityXML(WTTActivityXML);
        }

        /// <summary>
        /// Instantiates a WTTActivity object from the supplied JSON string
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTrip object</param>
        /// <param name="UserSettings">The user settignd</param>
        public WTTActivity(string JSON, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            //Valdate Arguments
            ArgumentValidation.ValidateJSON(JSON, UserSettingHelper.GetCultureInfo(this.UserSettings));

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
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTActivityJSONError", null, UserSettingHelper.GetCultureInfo(this.UserSettings)), Ex);
            }
        }

        /// <summary>
        /// Parses a SimSig XML snippet (as an XElement) which represents a single activity
        /// </summary>
        /// <param name="WTTActivityXML">The SimSig XML snippet (as an XElement) to parse</param>
        private void ParseWTTActivityXML(XElement WTTActivityXML)
        {
            this.ActivityType = (WTTActivityType)XMLMethods.GetValueFromXElement<int>(WTTActivityXML, @"Activity", this.UserSettings.GetCultureInfo(), 0);
            this.AssociatedTrainHeadCode = XMLMethods.GetValueFromXElement<string>(WTTActivityXML, @"AssociatedTrain", this.UserSettings.GetCultureInfo(), 0);
        }

        /// <summary>
        /// Returns the UserSettingCollection from the various sources
        /// </summary>
        /// <returns></returns>
        private UserSettingCollection GetSimulationUserSettings()
        {
            //First check to see if the user settings have been passed down from the parent WTT object via then event function
            if (OnRequestUserSettings == null)
            {
                //If not return the user settings from this object. If this is null then create a default set of user settings
                return this._UserSettings ?? new UserSettingCollection();
            }
            else
            {
                //Otherwise return user settings frm the WTT object
                return OnRequestUserSettings();
            }
        }

        /// <summary>
        /// Function which is defined by the parent object to retreive the user settings from the parent object
        /// </summary>
        internal Func<UserSettingCollection> OnRequestUserSettings;

        #endregion Methods
    }
}
