using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Class representing a WTT Speed object
    /// </summary>
    public class WTTSpeed
    {
        #region Constants

        const decimal _MPHToKPH = 1.609M;

        #endregion Constants

        #region Private Variables

        private readonly int _MPH; //Private variable to store the speed in MPH (the WTTSpeed base unit)
        private readonly UserSettingCollection _UserSettings; //Private variable to store the user settings

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the speed in MPH
        /// </summary>
        [JsonProperty("mph")]
        public int MPH { get { return this._MPH; } }

        /// <summary>
        /// Gets the speed in KPH
        /// </summary>
        [JsonProperty("kph")]
        public decimal KPH { get { return this._MPH * _MPHToKPH;  } }

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
        private WTTSpeed()
        {
        }

        /// <summary>
        /// Instantiates a WTTSpeed object from the the supplied MPH
        /// </summary>
        /// <param name="MPH">The Speed in MPH</param>
        public WTTSpeed (int MPH, UserSettingCollection UserSettings)
        {
            this._UserSettings = UserSettings ?? new UserSettingCollection();

            if (MPH<=0)
            {
                throw new ArgumentOutOfRangeException(ExceptionHelper.GetStaticException("InvalidMPHArgument", null, UserSettingHelper.GetCultureInfo(this.UserSettings)));
            }

            this._MPH = MPH;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
