using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Class which represents a SimSig duration unit. This is similar to time but allows negative seconds to be passed. Negative seconds are used to indicate whether a train should run early
    /// </summary>
    public class WTTDuration
    { 
        #region Constants

        #endregion Constants

        #region Private Variables

        private readonly int _Seconds; //Stores the number of seconds since midnight.
        private readonly int _HalfMinuteCharacter; //Stores the character which represents a half minute.
        private readonly DateTime _WTTStartDate; //Stores the start date of the timetable.

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the number of seconds since midnight.
        /// </summary>
        [JsonProperty("seconds")]
        public int Seconds { get { return this._Seconds; } }

        /// <summary>
        /// Gets the WTTDuration formatted as ShortTime (HH:mmC)
        /// </summary>
        [JsonProperty("formattedDuration")]
        public string FormattedDuration { get { return this.FormatTime(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        private WTTDuration()
        {
        }

        /// <summary>
        /// Initialises a WTTDuration object. The WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds (can be negative)</param>
        public WTTDuration(int Seconds)
        {
            //Get Exception Messasge Resources
            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = Convert.ToInt32(Globals.UserSettings.GetValueByKey("TIMEHALFCHAR"), Globals.UserSettings.GetCultureInfo());
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Formats the WTTTime object based on the WTT Format Type and Delimiter passed.
        /// </summary>
        /// <returns></returns>
        public string FormatTime()
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();
            string Delimiter = Globals.UserSettings.GetValueByKey("TIMEDELIMITER").ToString();
            char HalfChar = (char)Convert.ToInt32(Globals.UserSettings.GetValueByKey("TIMEHALFCHAR"), Culture);
            TimeSpan TimeDifference = this._WTTStartDate.AddSeconds(Math.Abs(this._Seconds)).Subtract(this._WTTStartDate);
            int TotalHours = (int)Math.Floor((decimal)TimeDifference.TotalHours);
            int Minutes = (int)Math.Floor((decimal)TimeDifference.Minutes);
            int Seconds = (int)Math.Floor((decimal)TimeDifference.Seconds);
            string HalfMinuteCharacter = Seconds == 0 ? "" : HalfChar.ToString(Culture);
            string DisplayMinutes = Minutes.ToString(Culture).Length == 1 ? string.Format(Culture, @"0{0}", Minutes) : Minutes.ToString(Culture);
            string Negative = this._Seconds < 0 ? "-" : "";

            string DisplayTotalHours = TotalHours.ToString(Culture).Length == 1 ? string.Format(Culture, @"0{0}", TotalHours) : TotalHours.ToString(Culture);
            return string.Format(Culture, @"{0}{1}{2}{3}{4}", Negative, DisplayTotalHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
        }

        #endregion Methods
    }
}
