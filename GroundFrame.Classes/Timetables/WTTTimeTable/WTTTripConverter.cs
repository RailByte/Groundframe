using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A surrogate class which mimcs the WTTTrip class and is used as part of the custom JsonConverter for a WTTTrip object
    /// </summary>
    internal class WTTTripSurrogate
    {
        #region Constants
        #endregion Contants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the departure / pass time
        /// </summary>
        public WTTTime DepPassTime { get; set; }

        /// <summary>
        /// Gets or sets the arrival time
        /// </summary>
        public WTTTime ArrTime { get; set; }
        
        /// <summary>
        /// Gets or sets whether the DepPassTime is a pass time
        /// </summary>
        public bool IsPassTime { get; set; }

        /// <summary>
        /// Gets or sets the platform
        /// </summary>
        
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the line
        /// </summary>

        public string Line { get; set; }

        /// <summary>
        /// Gets or sets the path
        /// </summary>

        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the auto line flag
        /// </summary>
        public bool AutoLine { get; set; }

        /// <summary>
        /// Gets or sets the auto path flag
        /// </summary>
        public bool AutoPath { get; set; }

        /// <summary>
        /// Gets or sets whether the trip is in the down direction
        /// </summary>
        public bool DownDirection { get; set; }

        /// <summary>
        /// Gets or sets whether the prev path ends down
        /// </summary>
        public bool PrevPathEndDown { get; set; }

        /// <summary>
        /// Gets or sets whether the next path starts down
        /// </summary>
        public bool NextPathStartDown { get; set; }

        /// <summary>
        /// Gets or sets the timetable start date
        /// </summary>
        public DateTime StartDate { get; set; }


        /// <summary>
        /// The activites associated with this trip
        /// </summary>
        public WTTActivityCollection Activities { get; set; }

        #endregion Properties

        #region Methds
        #endregion Methods

    }

    /// <summary>
    /// JsonConverter class for a WTTTrip object
    /// </summary>
    public class WTTTripConverter : JsonConverter
    {
        #region Constants
        #endregion Contants

        #region Private Variables
        #endregion Private Variables

        #region Constructors
        #endregion Constructors

        /// <summary>
        /// Flag to indicate whether the class a WTTTrip
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WTTTimeTableCollection);
        }

        /// <summary>
        /// Override method to deserialize a JSON string into a WTTTrip
        /// </summary>
        /// <returns>WTTTrip</returns>
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

            //Deserialize reader into a new WTTTrip object
            return new WTTTrip(Serializer.Deserialize<WTTTripSurrogate>(Reader));
        }

        /// <summary>
        /// Override method to serialize a WTTTrip object to a JSON string
        /// </summary>
        /// <returns>void</returns>
        public override void WriteJson(JsonWriter Writer, object Value, JsonSerializer Serializer)
        {
            //Validate Arguments
            if (Serializer == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Serializer" }, Globals.UserSettings.GetCultureInfo()));
            }

            if (Writer == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Writer" }, Globals.UserSettings.GetCultureInfo()));
            }

            if (Value == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Value" }, Globals.UserSettings.GetCultureInfo()));
            }

            WTTTrip OldWTTTrip = (WTTTrip)Value;
            // create the surrogate and serialize it instead 
            Serializer.Serialize(Writer, OldWTTTrip.ToSurrogateWTTTrip());
        }
    }
}
