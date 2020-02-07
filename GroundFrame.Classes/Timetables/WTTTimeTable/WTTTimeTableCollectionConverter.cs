using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A surrogate class which mimcs the WTTTimeTableCollection class and is used as part of the custom JsonConverter for a WTTTimeTableCollection object
    /// </summary>
    internal class WTTTimeTableCollectionSurrogate
    {
        /// <summary>
        /// The list of timetables
        /// </summary>
        public List<WTTTimeTable> TimeTables { get; set; }
        /// <summary>
        /// The timetable start date
        /// </summary>
        public DateTime StartDate { get; set; }
    }

    /// <summary>
    /// JsonConverter class for a WTTTimeTableCollection object
    /// </summary>
    public class WTTTimeTableCollectionConverter : JsonConverter
    {
        /// <summary>
        /// Flag to indicate whether the class a WTTTimeTableCollection
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WTTTimeTableCollection);
        }

        /// <summary>
        /// Override method to deserialize a JSON string into a WTTTimeTableCollection
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
            WTTTimeTableCollectionSurrogate SurrogateTimeTableCollection = Serializer.Deserialize<WTTTimeTableCollectionSurrogate>(Reader);
            //Extract properties
            List<WTTTimeTable> TimeTables = SurrogateTimeTableCollection.TimeTables;
            //Instantiate new WTTTimeTableCollection
            WTTTimeTableCollection TimeTableCollection  = new WTTTimeTableCollection(SurrogateTimeTableCollection.StartDate);
            //Populate the timetables and reseed WTTTripCollection Start Date
            foreach (WTTTimeTable TimeTable in TimeTables)
            {
                WTTTripCollection UpdatedTripCollection = new WTTTripCollection(TimeTable.StartDate);
                foreach (WTTTrip Trip in TimeTable.Trip)
                {
                    UpdatedTripCollection.Add(Trip);
                }

                TimeTable.Trip = UpdatedTripCollection;
                TimeTableCollection.Add(TimeTable);
            }
                
            return TimeTableCollection;
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

            WTTTimeTableCollection TimeTableCollection = (WTTTimeTableCollection)Value;

            //Create the surrogate and serialize it instead of the collection itself

            if (TimeTableCollection != null)
            {
                WTTTimeTableCollectionSurrogate SurrogateTimeTableCollection = new WTTTimeTableCollectionSurrogate()
                {
                    TimeTables = TimeTableCollection.ToList(),
                    StartDate = TimeTableCollection.StartDate
                };

                Serializer.Serialize(Writer, SurrogateTimeTableCollection);
            }      
        }
    }
}
