using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Classes
{
    public enum WTTTimeFormat
    {
        ShortFormat = 1,
        LongFormat = 2
    }

    /// <summary>
    /// Class which represents a SimSig time unit
    /// </summary>
    public class WTTTime
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
        /// Gets the WTTTime formatted as ShortTime (HH:mmC)
        /// </summary>
        public string FormattedShortTime { get { return this.FormatTime(WTTTimeFormat.ShortFormat); } }

        /// <summary>
        /// Gets the WTTTime formatted as LongTime (DD HH:mmC)
        /// </summary>
        public string FormattedLongTime { get { return this.FormatTime(WTTTimeFormat.LongFormat); } }

        /// <summary>
        /// Gets the WTTTime as a DateTime object calculated from the WTT Start Date (if no WTT Start Date is specified then this will be defaulted to 01/01/1850)
        /// </summary>
        public DateTime DateAndTime { get { return this.CalculateDateTime(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initialises a WTTTime object. The WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="HalfMinuteCharacter">A string representing the half minute character</param>
        public WTTTime(int Seconds, string HalfMinuteCharacter, string Culture = "en-GB")
        {
            //Get Exception Messasge Resources
            this._Culture = new CultureInfo(Culture);
            //Valdiate Arguments
            ArgumentValidation.ValidateHalfMinute(HalfMinuteCharacter, this._Culture);
            ArgumentValidation.ValidateSeconds(Seconds, this._Culture);

            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = HalfMinuteCharacter[0];
        }

        /// <summary>
        /// Initialises a WTTTime object. The WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="HalfMinuteCharacterASCII">The ASCII number representing the half minute character</param>
        public WTTTime(int Seconds, int HalfMinuteCharacterASCII, string Culture = "en-GB")
        {
            //Get Exception Messasge Resources
            this._Culture = new CultureInfo(Culture);
            //Valdiate Arguments
            ArgumentValidation.ValidateHalfMinute(HalfMinuteCharacterASCII, this._Culture);
            ArgumentValidation.ValidateSeconds(Seconds, this._Culture);

            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = (char)HalfMinuteCharacterASCII;
        }

        /// <summary>
        /// Initialises a WTTTime object.
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="WTTStartDate">The WTT Start Date - allows the WTTTime object to calculate the correct date for the timetable</param>
        /// <param name="HalfMinuteCharacter">A string representing the half minute character</param>
        public WTTTime(int Seconds, DateTime WTTStartDate, string HalfMinuteCharacter = "H", string Culture = "en-GB")
        {
            //Get Exception Messasge Resources
            this._Culture = new CultureInfo(Culture);
            ArgumentValidation.ValidateHalfMinute(HalfMinuteCharacter, this._Culture);
            ArgumentValidation.ValidateSeconds(Seconds, this._Culture);

            this._Seconds = Seconds;
            this._WTTStartDate = WTTStartDate;
            this._HalfMinuteCharacter = HalfMinuteCharacter[0];
        }

        /// <summary>
        /// Initialises a WTTTime object.
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="WTTStartDate">The WTT Start Date - allows the WTTTime object to calculate the correct date for the timetable</param>
        /// <param name="HalfMinuteCharacterASCII">The ASCII number representing the half minute character</param>
        public WTTTime(int Seconds, DateTime WTTStartDate, int HalfMinuteCharacterASCII = 72, string Culture = "en-GB")
        {
            //Get Exception Messasge Resources
            this._Culture = new CultureInfo(Culture);
            //Valdiate Arguments
            ArgumentValidation.ValidateHalfMinute(HalfMinuteCharacterASCII, this._Culture);
            ArgumentValidation.ValidateSeconds(Seconds, this._Culture);

            this._Seconds = Seconds;
            this._WTTStartDate = WTTStartDate;
            this._HalfMinuteCharacter = (char)HalfMinuteCharacterASCII;
        }

        #endregion Constructors

        #region Methods



        /// <summary>
        /// Caclualtes the DateTime value for the WTTTime object from the WTTStartDate property
        /// </summary>
        /// <returns></returns>
        private DateTime CalculateDateTime()
        {
            return this._WTTStartDate.AddSeconds(this._Seconds);
        }

        /// <summary>
        /// Formats the WTTTime object based on the WTT Format Type and Delimiter passed.
        /// </summary>
        /// <param name="Format">WTTFormatType to determine how the WTTTime object should be formatted</param>
        /// <param name="Delimiter">The hour / minute delimiter. Defaults to ":"</param>
        /// <returns></returns>
        public string FormatTime(WTTTimeFormat Format, string Delimiter = ":")
        {
            ArgumentValidation.ValidateDelimiter(Delimiter, this._Culture);
            TimeSpan TimeDifference = this._WTTStartDate.AddSeconds(this._Seconds).Subtract(this._WTTStartDate);
            int TotalDays = (int)Math.Floor((decimal)TimeDifference.TotalDays);
            int TotalHours = (int)Math.Floor((decimal)TimeDifference.TotalHours);
            int Hours = (int)Math.Floor((decimal)TimeDifference.Hours);
            int Minutes = (int)Math.Floor((decimal)TimeDifference.Minutes);
            int Seconds = (int)Math.Floor((decimal)TimeDifference.Seconds);
            string HalfMinuteCharacter = Seconds == 0 ? "" : this._HalfMinuteCharacter.ToString(this._Culture);    
            string DisplayMinutes = Minutes.ToString(this._Culture).Length == 1 ? string.Format(this._Culture, @"0{0}", Minutes) : Minutes.ToString(this._Culture);

            if (Format == WTTTimeFormat.ShortFormat)
            {
                string DisplayTotalHours = TotalHours.ToString(this._Culture).Length == 1 ? string.Format(this._Culture,@"0{0}", TotalHours) : TotalHours.ToString(this._Culture);
                return string.Format(this._Culture, @"{0}{1}{2}{3}", DisplayTotalHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
            }
            else
            {
                string DisplayHours = Hours.ToString(this._Culture).Length == 1 ? string.Format(this._Culture, @"0{0}", Hours) : Hours.ToString(this._Culture);
                string DisplayDays = FormatDays(TotalDays); //Get the Day format.
                return string.Format(this._Culture, @"{0}{1}{2}{3}{4}", DisplayDays, DisplayHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
            }
        }

        /// <summary>
        /// Converts the number of days to a string. Used the in the FormatTime method(s)
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        private static string FormatDays(int Days)
        {
            if (Days == 0) //No days so we just need to return an empty string
            {
                return string.Empty;
            }
            else if (Days == 1) //1 Days - singular string
            {
                return "(+1 Day) ";
            }
            else
            {
                return "(+2 Days) ";
            }
        }

        #endregion Methods
    }
}
