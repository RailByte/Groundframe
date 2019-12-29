using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes
{
    public enum WTTTimeFormat
    {
        Short = 1,
        Long = 2
    }

    /// <summary>
    /// Class which represents a SimSig time unit
    /// </summary>
    public class Time
    {
        #region Constants

        #endregion Constants

        #region Private Variables

        private readonly int _Seconds; //Stores the number of seconds since midnight.
        private char _HalfMinuteCharacter; //Stores the character which represents a half minute.
        private DateTime _WTTStartDate; //Stores the start date of the timetable.

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the number of seconds since midnight.
        /// </summary>
        public int Seconds { get { return this._Seconds; } }

        /// <summary>
        /// Gets the WTTTime formatted as ShortTime (HH:mmC)
        /// </summary>
        public string FormattedShortTime { get { return this.FormatTime(WTTTimeFormat.Short); } }

        /// <summary>
        /// Gets the WTTTime formatted as LongTime (DD HH:mmC)
        /// </summary>
        public string FormattedLongTime { get { return this.FormatTime(WTTTimeFormat.Long); } }

        /// <summary>
        /// Gets the WTTTime as a DateTime object calculated from the WTT Start Date (if no WTT Start Date is specified then this will be defaulted to 01/01/1850)
        /// </summary>
        public DateTime DateAndTime { get { return this.CalculateDateTime(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initialises a WTTTime object. The half minute character will be "H" and the WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        public Time(int Seconds)
        {
            //Check Arguments
            if (Seconds<0)
            {
                throw new ArgumentOutOfRangeException("Seconds must be equal or greater than 0");
            }

            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = (char)72; //"H"
        }

        /// <summary>
        /// Initialises a WTTTime object. The WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="HalfMinuteCharacter">A string representing the half minute character</param>
        public Time(int Seconds, string HalfMinuteCharacter = "H")
        {
            //Check the HalfMinuteCharacter argument is only 1 character
            if (HalfMinuteCharacter.Length != 1)
            {
                throw new ArgumentOutOfRangeException("The HalfMinuteCharacter argument must only be 1 character");
            }

            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = HalfMinuteCharacter[0];
        }

        /// <summary>
        /// Initialises a WTTTime object. The WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="HalfMinuteCharacterASCII">The ASCII number representing the half minute character</param>
        public Time(int Seconds, int HalfMinuteCharacterASCII= 72)
        {
            //Check the HalfMinuteCharacterASCII argument is within the valid range
            if (HalfMinuteCharacterASCII < 32 || HalfMinuteCharacterASCII >255)
            {
                throw new ArgumentOutOfRangeException("The HalfMinuteCharacterASCII argument must be between 32 and 255");
            }

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
        public Time(int Seconds, DateTime WTTStartDate, string HalfMinuteCharacter = "H")
        {
            //Check the HalfMinuteCharacter argument is only 1 character
            if (HalfMinuteCharacter.Length != 1)
            {
                throw new ArgumentOutOfRangeException("The HalfMinuteCharacter argument must only be 1 character");
            }

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
        public Time(int Seconds, DateTime WTTStartDate, int HalfMinuteCharacterASCII = 72)
        {
            //Check the HalfMinuteCharacterASCII argument is within the valid range
            if (HalfMinuteCharacterASCII < 32 || HalfMinuteCharacterASCII > 255)
            {
                throw new ArgumentOutOfRangeException("The HalfMinuteCharacterASCII argument must be between 32 and 255");
            }

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
            if (Delimiter == null)
            {
                throw new ArgumentNullException("The Delimiter argument cannot be <NULL>");
            }

            if (Delimiter.Length != 1)
            {
                throw new ArgumentOutOfRangeException("The Delimiter argument must be a single character");
            }

            TimeSpan TimeDifference = this._WTTStartDate.AddSeconds(this._Seconds).Subtract(this._WTTStartDate);
            int TotalDays = (int)Math.Floor((decimal)TimeDifference.TotalDays);
            int TotalHours = (int)Math.Floor((decimal)TimeDifference.TotalHours);
            int Hours = (int)Math.Floor((decimal)TimeDifference.Hours);
            int Minutes = (int)Math.Floor((decimal)TimeDifference.Minutes);
            int Seconds = (int)Math.Floor((decimal)TimeDifference.Seconds);
            string HalfMinuteCharacter = Seconds == 0 ? "" : this._HalfMinuteCharacter.ToString();    
            string DisplayMinutes = Minutes.ToString().Length == 1 ? string.Format(@"0{0}", Minutes) : Minutes.ToString();

            if (Format == WTTTimeFormat.Short)
            {
                string DisplayTotalHours = TotalHours.ToString().Length == 1 ? string.Format(@"0{0}", TotalHours) : TotalHours.ToString();
                return string.Format(@"{0}{1}{2}{3}", DisplayTotalHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
            }
            else
            {
                string DisplayHours = Hours.ToString().Length == 1 ? string.Format(@"0{0}", Hours) : Hours.ToString();
                string DisplayDays = this.FormatDays(TotalDays); //Get the Day format.
                return string.Format(@"{0}{1}{2}{3}{4}", DisplayDays, DisplayHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
            }
        }

        /// <summary>
        /// Converts the number of days to a string. Used the in the FormatTime method(s)
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        private string FormatDays(int Days)
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
