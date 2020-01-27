﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Classes.Timetables
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
        private readonly int _HalfMinuteCharacter; //Stores the character which represents a half minute.
        private readonly DateTime _WTTStartDate; //Stores the start date of the timetable.
        private readonly UserSettingCollection _UserSettings; //Stores the users settings

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the number of seconds since midnight.
        /// </summary>
        [JsonProperty("seconds")]
        public int Seconds { get { return this._Seconds; } }

        /// <summary>
        /// Gets the WTTTime formatted as ShortTime (HH:mmC)
        /// </summary>
        [JsonProperty("formattedShortTime")]
        public string FormattedShortTime { get { return this.FormatTime(WTTTimeFormat.ShortFormat, null); } }

        /// <summary>
        /// Gets the WTTTime formatted as LongTime (DD HH:mmC)
        /// </summary>
        [JsonProperty("formattedLongTime")]
        public string FormattedLongTime { get { return this.FormatTime(WTTTimeFormat.LongFormat, null); } }

        /// <summary>
        /// Gets the WTTTime as a DateTime object calculated from the WTT Start Date (if no WTT Start Date is specified then this will be defaulted to 01/01/1850)
        /// </summary>
        [JsonProperty("dateAndTime")]
        public DateTime DateAndTime { get { return this.CalculateDateTime(); } }

        /// <summary>
        /// Gets the WTT Start Date
        /// </summary>
        [JsonProperty("wttStartDate")]
        private DateTime WTTStartDate { get { return this._WTTStartDate; } }

        /// <summary>
        /// Gets the user settings
        /// </summary>
        [JsonIgnore]
        public UserSettingCollection UserSettings { get { return this._UserSettings; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private WTTTime(DateTime WTTStartDate, int Seconds)
        {
            this._WTTStartDate = WTTStartDate;
            this._Seconds = Seconds;
            this._HalfMinuteCharacter = 72;
        }

        /// <summary>
        /// Initialises a WTTTime object. The WTT start date 1850-01-01
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="HalfMinuteCharacter">A string representing the half minute character</param>
        public WTTTime(int Seconds, UserSettingCollection UserSettings)
        {
            //Get Exception Messasge Resources
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            //Valdiate Arguments
            ArgumentValidation.ValidateSeconds(Seconds, UserSettingHelper.GetCultureInfo(this._UserSettings));

            this._Seconds = Seconds;
            this._WTTStartDate = new DateTime(1850, 1, 1);
            this._HalfMinuteCharacter = Convert.ToInt32(this._UserSettings.GetValueByKey("TIMEHALFCHAR"), UserSettingHelper.GetCultureInfo(this.UserSettings));
        }


        /// <summary>
        /// Initialises a WTTTime object.
        /// </summary>
        /// <param name="Seconds">The number of seconds since midnight</param>
        /// <param name="WTTStartDate">The WTT Start Date - allows the WTTTime object to calculate the correct date for the timetable</param>
        /// <param name="HalfMinuteCharacter">A string representing the half minute character</param>
        public WTTTime(int Seconds, DateTime WTTStartDate, UserSettingCollection UserSettings)
        {
            //Get Exception Messasge Resources
            this._UserSettings = UserSettings ?? new UserSettingCollection();
            ArgumentValidation.ValidateSeconds(Seconds, UserSettingHelper.GetCultureInfo(this._UserSettings));

            this._Seconds = Seconds;
            this._WTTStartDate = WTTStartDate;
            this._HalfMinuteCharacter = Convert.ToInt32(this._UserSettings.GetValueByKey("TIMEHALFCHAR"), UserSettingHelper.GetCultureInfo(this.UserSettings));
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
        public string FormatTime(WTTTimeFormat Format, string Delimiter)
        {
            if (string.IsNullOrEmpty(Delimiter))
            {
                Delimiter = this.UserSettings.GetValueByKey("TIMEDELIMITER").ToString();
            }
            CultureInfo Culture = UserSettingHelper.GetCultureInfo(this._UserSettings);
            char HalfChar = (char)Convert.ToInt32(this.UserSettings.GetValueByKey("TIMEHALFCHAR"), Culture);
            TimeSpan TimeDifference = this._WTTStartDate.AddSeconds(this._Seconds).Subtract(this._WTTStartDate);
            int TotalDays = (int)Math.Floor((decimal)TimeDifference.TotalDays);
            int TotalHours = (int)Math.Floor((decimal)TimeDifference.TotalHours);
            int Hours = (int)Math.Floor((decimal)TimeDifference.Hours);
            int Minutes = (int)Math.Floor((decimal)TimeDifference.Minutes);
            int Seconds = (int)Math.Floor((decimal)TimeDifference.Seconds);
            string HalfMinuteCharacter = Seconds == 0 ? "" : HalfChar.ToString(Culture);
            string DisplayMinutes = Minutes.ToString(Culture).Length == 1 ? string.Format(Culture, @"0{0}", Minutes) : Minutes.ToString(Culture);

            if (Format == WTTTimeFormat.ShortFormat)
            {
                string DisplayTotalHours = TotalHours.ToString(Culture).Length == 1 ? string.Format(Culture, @"0{0}", TotalHours) : TotalHours.ToString(Culture);
                return string.Format(Culture, @"{0}{1}{2}{3}", DisplayTotalHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
            }
            else
            {
                string DisplayHours = Hours.ToString(Culture).Length == 1 ? string.Format(Culture, @"0{0}", Hours) : Hours.ToString(Culture);
                string DisplayDays = FormatDays(TotalDays); //Get the Day format.
                return string.Format(Culture, @"{0}{1}{2}{3}{4}", DisplayDays, DisplayHours, Delimiter, DisplayMinutes, HalfMinuteCharacter);
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