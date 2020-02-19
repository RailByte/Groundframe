using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GroundFrame.Core.Timetables
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

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a WTTSpeed object from the the supplied MPH
        /// </summary>
        /// <param name="MPH">The Speed in MPH</param>
        public WTTSpeed (int MPH)
        {
            if (MPH<=0)
            {
                throw new ArgumentOutOfRangeException(ExceptionHelper.GetStaticException("InvalidMPHArgument", null, Globals.UserSettings.GetCultureInfo()));
            }

            this._MPH = MPH;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
