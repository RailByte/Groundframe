using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// An object representing the SimSig Electrification options
    /// </summary>
    public class Electrification
    {
        //TODO: Check the SimSig behaviour for non electric traction

        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly CultureInfo _Culture; //Stores the culture info

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the Diesel flag
        /// </summary>
        public bool Diesel { get; set; }

        /// <summary>
        /// Gets or sets the Overhead electrification flag
        /// </summary>
        public bool Overhead { get; set; }

        /// <summary>
        /// Gets or sets the Third Rail electrification flag
        /// </summary>
        public bool ThirdRail {get; set;}

        /// <summary>
        /// Gets or sets the 4th Rail electrification flag
        /// </summary>
        public bool FourthRail { get; set; }

        /// <summary>
        /// Gets or sets the Overhead DC electrification flag
        /// </summary>
        public bool OverheadDC { get; set; }

        /// <summary>
        /// Gets or sets the Tramway electrification flag
        /// </summary>
        public bool Tramway { get; set; }

        /// <summary>
        /// Gets or sets the Sim 1 electrification flag
        /// </summary>
        public bool Sim1 { get; set; }

        /// <summary>
        /// Gets or sets the Sim 2 electrification flag
        /// </summary>
        public bool Sim2 { get; set; }

        /// <summary>
        /// Gets or sets the Sim 1 electrification flag
        /// </summary>
        public bool Sim3 { get; set; }

        /// <summary>
        /// Gets or sets the Sim 4 electrification flag
        /// </summary>
        public bool Sim4 { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constru
        /// </summary>
        [JsonConstructor]
        private Electrification()
        {
        }


        /// <summary>
        /// Instantiates a WTTElectrification object from the supplied SimSig code
        /// </summary>
        /// <param name="SimSigCode">A string representing the available types of electrification</param>
        public Electrification(string SimSigCode, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture);
            //Validate Arguments
            ArgumentValidation.ValidateSimSigCode(SimSigCode, this._Culture);

            this.ConvertSimSigCode(SimSigCode);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Converts the supplied SimSig code into the WTTElectrification object
        /// </summary>
        /// <param name="Code"></param>
        private void ConvertSimSigCode(string Code)
        {
            bool HasMatch = false; //Flag to indicates the character has matched an expected value
            int CharactersParsed = 0; //Variable to store the number of varibles parsed
            string OriginalCode = Code;

            if (string.IsNullOrEmpty(Code))
            {
                Diesel = false;
                Overhead = false;
                ThirdRail = false;
                FourthRail = false;
                OverheadDC = false;
                Tramway = false;
                Sim1 = false;
                Sim2 = false;
                Sim3 = false;
                Sim4 = false;
                HasMatch = true;
            }

            if (Code.IndexOf("X1", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Sim1 = true;
                HasMatch = true;
                CharactersParsed += 2;
                Code = Code.Replace("X1", "", true, this._Culture);
            }

            if (Code.IndexOf("X2", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Sim2 = true;
                HasMatch = true;
                CharactersParsed += 2;
                Code = Code.Replace("X2", "", true, this._Culture);
            }

            if (Code.IndexOf("X3", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Sim3 = true;
                HasMatch = true;
                CharactersParsed += 2;
                Code = Code.Replace("X3", "", true, this._Culture);
            }

            if (Code.IndexOf("X4", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Sim4 = true;
                HasMatch = true;
                CharactersParsed += 2;
                Code = Code.Replace("X4", "", true, this._Culture);
            }

            if (Code.IndexOf("D", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Overhead = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("D", "", true, this._Culture);
            }

            if (Code.IndexOf("O", StringComparison.OrdinalIgnoreCase)>=0)
            {
                Overhead = true;
                HasMatch = true;
                CharactersParsed ++;
                Code = Code.Replace("O", "", true, this._Culture);
            }

            if (Code.IndexOf("3", StringComparison.OrdinalIgnoreCase)>=0)
            {
                ThirdRail = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("3", "", true, this._Culture);
            }

            if (Code.IndexOf("4", StringComparison.OrdinalIgnoreCase)>=0)
            {
                FourthRail = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("4", "", true, this._Culture);
            }

            if (Code.IndexOf("V", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                OverheadDC = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("V", "", true, this._Culture);
            }

            if (Code.IndexOf("T", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Tramway = true;
                HasMatch = true;
                CharactersParsed++;
            }

            if (!HasMatch || (CharactersParsed != OriginalCode.Length))
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("InvalidSimSigCodeError", null, this._Culture));
            }
        }

        #endregion Methods
    }
}
