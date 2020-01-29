﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// A surrogate class which mimcs the WTTTimeTable class and is used as part of the custom JsonConverter for a WTTTimeTable object
    /// </summary>
    internal class WTTTimeTableSurrogate
    {
        #region Private Constants
        #endregion Private Constants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the timetable start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the Timetable Headcode
        /// </summary>
        public string Headcode { get; set; }

        /// <summary>
        /// Gets or sets the Acceleration / Break Index
        /// </summary>
        public WTTAccelBrakeIndex AccelBrakeIndex { get; set; }

        /// <summary>
        /// Gets or sets the run as required percentage
        /// </summary>]
        public int RunAsRequiredPercentage { get; set; }

        /// <summary>
        /// Gets or sets the dely of the service
        /// </summary>
        public WTTDuration Delay { get; set; }

        /// <summary>
        /// Gets or sets the departure time of the service
        /// </summary>
        public WTTTime DepartTime { get; set; }

        /// <summary>
        /// Gets or sets the description of the service
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the seeding gap
        /// </summary>
        public WTTDuration SeedingGap { get; set; }

        /// <summary>
        /// Gets or sets the entry point
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        /// Gets or sets the actual entry point
        /// </summary>
        public string ActualEntryPoint { get; set; }

        /// <summary>
        /// Gets or sets the max speed
        /// </summary>
        public WTTSpeed MaxSpeed { get; set; }

        /// <summary>
        /// Gets or sets the speed class
        /// </summary>
        public WTTSpeedClass SpeedClass { get; set; }

        /// <summary>
        /// Gets or sets whether the service has started
        /// </summary>
        public bool Started { get; set; }

        /// <summary>
        /// Gets or sets the train length
        /// </summary>
        public Length TrainLength { get; set; }

        /// <summary>
        /// Gets or sets the electrification
        /// </summary>
        public Electrification Electrification { get; set; }

        /// <summary>
        /// Gets or sets whether run as required has been tested
        /// </summary>
        public bool RunAsRequiredTested { get; set; }

        //TODO: EntryWarned

        /// <summary>
        /// Gets or sets the traction type at the start of the timetable
        /// </summary>
        public Electrification StartTraction { get; set; }

        /// <summary>
        /// Gets or sets the WTTTrainCategory ID
        /// </summary>
        public string SimSigTrainCategoryID { get; set; }

        /// <summary>
        /// Gets or sets the trip for this service
        /// </summary>
        public WTTTripCollection Trip { get; set; }

        #endregion Properties
    }

    /// <summary>
    /// JsonConverter class for a WTTTimeTable object
    /// </summary>
    public class WTTTimeTableConverter : JsonConverter
    {
        #region Private Constants
        #endregion Private Constants

        #region Private Variables

        private readonly UserSettingCollection _UserSettings; //Stores the user settings

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Instantiates a WTTTimeTableConverter object with the supplied user settings
        /// </summary>
        /// <param name="UserSettings"></param>
        public WTTTimeTableConverter(UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings;
        }

        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Flag to indicate whether the class a WTTTimeTable
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WTTTimeTable);
        }

        /// <summary>
        /// Override method to deserialize a JSON string into a WTTTimeTable
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
            WTTTimeTableSurrogate SurrogateTimeTable = Serializer.Deserialize<WTTTimeTableSurrogate>(Reader);
            return new WTTTimeTable(SurrogateTimeTable, this._UserSettings);
        }

        /// <summary>
        /// Override method to serialize a WTTTimeTable object to a JSON string
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

            WTTTimeTable TimeTable= (WTTTimeTable)Value;
            // create the surrogate and serialize it instead 
            // of the collection itself
            Serializer.Serialize(Writer, TimeTable.ToSurrogateWTTTimeTable());
        }

        #endregion Methods
    }
}
