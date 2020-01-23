using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GroundFrame.Classes
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
        private readonly CultureInfo _Culture; //Private variable to store the culture

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the speed in MPH
        /// </summary>
        public int MPH { get { return this._MPH; } }

        /// <summary>
        /// Gets the speed in KPH
        /// </summary>
        public decimal KPH { get { return this._MPH * _MPHToKPH;  } }

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
        public WTTSpeed (int MPH, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture ?? "en-GB");

            if (MPH<=0)
            {
                throw new ArgumentOutOfRangeException(ExceptionHelper.GetStaticException("InvalidMPHArgument", null, this._Culture));
            }

            this._MPH = MPH;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
