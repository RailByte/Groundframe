using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A surrogate class which mimcs the WTTTripCollection class and is used as part of the custom JsonConverter for a WTTTripCollection object
    /// </summary>
    internal class WTTTripCollectionSurrogate
    {
        #region Constants
        #endregion Contants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// The list of trips
        /// </summary>
        public List<WTTTrip> Trips { get; set; }
        /// <summary>
        /// The timetable start date
        /// </summary>
        public DateTime StartDate { get; set; }

        #endregion Properties

        #region Methds
        #endregion Methods
    }

    /// <summary>
    /// JsonConverter class for a WTTTripCollection object
    /// </summary>
    public class WTTTripCollectionConverter : JsonConverter
    {
        #region Constants
        #endregion Contants

        #region Private Variables
        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new WTTTripCollectionConverter object with user settings
        /// </summary>
        public WTTTripCollectionConverter()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Flag to indicate whether the class a WTTTripCollection
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WTTTripCollection);
        }

        /// <summary>
        /// Override method to deserialize a JSON string into a WTTTripCollection
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
            WTTTripCollectionSurrogate SurrogateTripCollection = Serializer.Deserialize<WTTTripCollectionSurrogate>(Reader);
            return new WTTTripCollection(SurrogateTripCollection);
        }

        /// <summary>
        /// Override method to serialize a WTTTripCollection object to a JSON string
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

            WTTTripCollection TripCollection = (WTTTripCollection)Value;
            // create the surrogate and serialize it instead 
            // of the collection itself
            Serializer.Serialize(Writer, TripCollection.ToWTTTripCollectionSurrogate());
        }

        #endregion Methods
    }
}
