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
        /// <summary>
        /// The list of trips
        /// </summary>
        public List<WTTTrip> Trips { get; set; }
        /// <summary>
        /// The timetable start date
        /// </summary>
        public DateTime StartDate { get; set; }
    }

    /// <summary>
    /// JsonConverter class for a WTTTripCollection object
    /// </summary>
    public class WTTTripCollectionConverter : JsonConverter
    {
        /// <summary>
        /// Flag to indicate whether the class a WTTTripCollection
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WTTTimeTableCollection);
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
            //Extract properties
            List<WTTTrip> Trips = SurrogateTripCollection.Trips;
            //Instantiate new WTTTripCollection
            WTTTripCollection TripCollection  = new WTTTripCollection(SurrogateTripCollection.StartDate);
            //Populate the trips
            foreach (WTTTrip Trip in Trips)
            {
                TripCollection.Add(Trip);
            }
                
            return TripCollection;
        }

        /// <summary>
        /// Override method to serialize a WTTTimeTableCollection object to a JSON string
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

            WTTTripCollection TripCollection = (WTTTripCollection)Value;
            // create the surrogate and serialize it instead 
            // of the collection itself
            WTTTripCollectionSurrogate SurrogateTripCollection = new WTTTripCollectionSurrogate()
            {
                Trips = TripCollection.ToList(),
                StartDate = TripCollection.StartDate
            };
            Serializer.Serialize(Writer, SurrogateTripCollection);
        }
    }
}
