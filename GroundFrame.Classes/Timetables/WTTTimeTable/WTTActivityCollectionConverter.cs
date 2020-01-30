using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A surrogate class which mimics the WTTActivityCollection class and is used as part of the custom JsonConverter for a WTTActivityCollection object
    /// </summary>
    internal class WTTActivityCollectionSurrogate
    {
        #region Constants
        #endregion Contants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// The list of activities
        /// </summary>
        public List<WTTActivity> Activities { get; set; }
        /// <summary>
        /// The timetable start date
        /// </summary>
        public DateTime StartDate { get; set; }

        #endregion Properties

        #region Methods
        #endregion Methods
    }

    /// <summary>
    /// JsonConverter class for a WTTActivityCollection object
    /// </summary>
    public class WTTActivityCollectionConverter : JsonConverter
    {
        #region Constants
        #endregion Contants

        #region Private Variables

        private readonly UserSettingCollection _UserSettings; //Stores the user settings

        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new WTTActivityCollectionSurrogate object with user settings
        /// </summary>
        /// <param name="UserSettings">The user settings</param>
        public WTTActivityCollectionConverter(UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();
        }

        #endregion Conrstructors

        #region Methods

        /// <summary>
        /// Flag to indicate whether the class a WTTActivityCollection
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WTTTimeTableCollection);
        }

        /// <summary>
        /// Override method to deserialize a JSON string into a WTTActivityCollection
        /// </summary>
        /// <returns></returns>
        public override object ReadJson(JsonReader Reader, Type ObjectType, object ExistingValue, JsonSerializer Serializer)
        {
            //Validate Arguments
            if (Serializer == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Serializer" }, new System.Globalization.CultureInfo("en-GB")));
            }

            if (Reader == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Reader" }, new System.Globalization.CultureInfo("en-GB")));
            }

            //Deserialize reader into surrogate object
            WTTActivityCollectionSurrogate SurrogateActivityCollection = Serializer.Deserialize<WTTActivityCollectionSurrogate>(Reader);
            return new WTTActivityCollection(SurrogateActivityCollection, this._UserSettings); ;
        }

        /// <summary>
        /// Override method to serialize a WTTActivityCollection object to a JSON string
        /// </summary>
        /// <returns></returns>
        public override void WriteJson(JsonWriter Writer, object Value, JsonSerializer Serializer)
        {
            //Validate Arguments
            if (Serializer == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Serializer" }, new System.Globalization.CultureInfo("en-GB")));
            }

            if (Writer == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Writer" }, new System.Globalization.CultureInfo("en-GB")));
            }

            if (Value == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Value" }, new System.Globalization.CultureInfo("en-GB")));
            }

            WTTActivityCollection ActivityCollection = (WTTActivityCollection)Value;
            // create the surrogate and serialize it instead 
            // of the collection itself
            Serializer.Serialize(Writer, ActivityCollection.ToWTTActivityCollectionSurrogate());
        }

        #endregion Methods
    }
}
