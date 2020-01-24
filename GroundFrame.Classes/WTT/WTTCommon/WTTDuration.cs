using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Classes.WTT
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
        private readonly char _HalfMinuteCharacter; //Stores the character which represents a half minute.
        private readonly DateTime _WTTStartDate; //Stores the start date of the timetable.
        private readonly CultureInfo _Culture; //Stores the culture info

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the number of seconds since midnight.
        /// </summary>
        public int Seconds { get { return this._Seconds; } }

        /// <summary>
        /// Gets the WTTDuration formatted as ShortTime (HH:mmC)
        /// </summary>
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
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="HalfMinuteCharacter">A string representing the half minute character</param>
        public WTTDuration(int Seconds, string HalfMinuteCharacter, string Culture = "en-GB")
        {
            //Get Exception Messasge Resources
            this._Culture = new CultureInfo(Culture);

            //Valdiate Arguments
            ArgumentValidation.ValidateHalfMinute(HalfMinuteCharacter, this._Culture);

            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = HalfMinuteCharacter[0];
        }

        /// <summary>
        /// Initialises a WTTDuration object. The WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="HalfMinuteCharacterASCII">The ASCII number representing the half minute character</param>
        public WTTDuration(int Seconds, int HalfMinuteCharacterASCII, string Culture = "en-GB")
        {
            //Get Exception Messasge Resources
            this._Culture = new CultureInfo(Culture);

            //Valdiate Arguments
            ArgumentValidation.ValidateHalfMinute(HalfMinuteCharacterASCII, this._Culture);

            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = (char)HalfMinuteCharacterASCII;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Formats the WTTTime object based on the WTT Format Type and Delimiter passed.
        /// </summary>
        /// <param name="Format">WTTFormatType to determine how the WTTTime object should be formatted</param>
        /// <param name="Delimiter">The hour / minute delimiter. Defaults to ":"</param>
        /// <returns></returns>
        public string FormatTime(string Delimiter = ":")
        {
            ArgumentValidation.ValidateDelimiter(Delimiter, this._Culture);
            TimeSpan TimeDifference = this._WTTStartDate.AddSeconds(Math.Abs(this._Seconds)).Subtract(this._WTTStartDate);
            int TotalHours = (int)Math.Floor((decimal)TimeDifference.TotalHours);
            int Minutes = (int)Math.Floor((decimal)TimeDifference.Minutes);
            int Seconds = (int)Math.Floor((decimal)TimeDifference.Seconds);
            string HalfMinuteCharacter = Seconds == 0 ? "" : this._HalfMinuteCharacter.ToString(this._Culture);    
            string DisplayMinutes = Minutes.ToString(this._Culture).Length == 1 ? string.Format(this._Culture, @"0{0}", Minutes) : Minutes.ToString(this._Culture);
            string Negative = this._Seconds < 0 ? "-" : "";

            string DisplayTotalHours = TotalHours.ToString(this._Culture).Length == 1 ? string.Format(this._Culture, @"0{0}", TotalHours) : TotalHours.ToString(this._Culture);
            return string.Format(this._Culture, @"{0}{1}{2}{3}{4}", Negative, DisplayTotalHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
        }

        #endregion Methods
    }
}
