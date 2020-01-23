using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// A class representing a length. All SimgSig WTT lengths are in meters 
    /// </summary>
    public class Length
    {
        #region Constants

        const decimal MetersToMiles = 0.00062137M;
        const decimal MetersToChains = 0.0497096M;

        #endregion Constants

        #region Private Variables

        private readonly int _Meters; //Stores the length in meters
        private readonly CultureInfo _Culture; //Stores the culture info

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the length in meters
        /// </summary>
        public int Meters { get { return this._Meters; } }

        /// <summary>
        /// Gets the length in decimal miles
        /// </summary>
        public decimal DecimalMiles { get { return this.ConvertToDecimalMiles(); } }

        /// <summary>
        /// Gets the length in miles and chains
        /// </summary>
        public string MilesAndChains { get { return this.ConvertToMilesAndChains(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constru
        /// </summary>
        [JsonConstructor]
        private Length()
        {
        }


        /// <summary>
        /// Instantiates a new instance of a WTTLength from the supplied meters
        /// </summary>
        /// <param name="Meters">The length in meters</param>
        public Length(int Meters, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);

            //Validate arguments
            ArgumentValidation.ValidateMeters(Meters, this._Culture);

            this._Meters = Meters;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Converts the meters to decimal miles
        /// </summary>
        /// <returns></returns>
        private decimal ConvertToDecimalMiles()
        {
            return this._Meters * MetersToMiles;
        }

        /// <summary>
        /// Converts the meters to miles and chains
        /// </summary>
        /// <returns></returns>
        private string ConvertToMilesAndChains()
        {
            const Decimal ChainsPerMile = 79.99984M;
            decimal Chains = this._Meters * MetersToChains;
            int Miles = Convert.ToInt32(Math.Floor(Chains / ChainsPerMile));
            int RemainingChains = Convert.ToInt32(Math.Round(Chains - (Miles * ChainsPerMile)));

            if (Miles == 0)
            {
                return $"{RemainingChains}CH";
            }
            else
            {
                return $"{Miles}M {RemainingChains}CH";
            }
        }

        #endregion Methods
    }
}
