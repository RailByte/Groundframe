using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GroundFrame.Classes.WTT
{
    /// <summary>
    /// Class which represents a SimSig Speed Class
    /// </summary>
    public class WTTSpeedClass
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly CultureInfo _Culture; //Stores the Culture
        private int _Bitwise; //Stores the Bitwise value for the Speed Class

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the bitwise value for the speed class
        /// </summary>
        public int Bitwise { get { return this._Bitwise; } }
        /// <summary>
        /// Gets a list of the selected bitwise values
        /// </summary>
        public List<WTTSpeedClassBitWise> SpeedClassList { get { return BitwiseHelper.MaskToList<WTTSpeedClassBitWise>((WTTSpeedClassBitWise)this._Bitwise, this._Culture); } }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        private WTTSpeedClass()
        {
        }

        /// <summary>
        /// Instantiates a WTTSpeedClass object from the selected Bitwise
        /// </summary>
        /// <param name="Bitwise">The bitwise value representing the selected speed class values</param>
        /// <param name="Culture">Optional - the name of the culture to determine language of error messages</param>
        public WTTSpeedClass (int Bitwise, string Culture = "en-GB")
        {
            this._Bitwise = Bitwise;
            this._Culture = new CultureInfo(Culture ?? "en-GB");
        }

        #endregion Constructor

        #region Methods

        #endregion Methods
    }
}
