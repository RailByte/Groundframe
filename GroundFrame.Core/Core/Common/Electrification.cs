using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GroundFrame.Core
{
    /// <summary>
    /// An enum representing the electrification bit values used in the Electrifiction object bitwise property
    /// </summary>
    public enum ElectrificationBitValue
    {
        /// <summary>
        /// The default value
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Diesel traction type
        /// </summary>
        Diesel = 1,
        /// <summary>
        /// Overhead 25Kv traction type
        /// </summary>
        Overhead = 2,
        /// <summary>
        /// Third Rail 750V DC traction type
        /// </summary>
        ThirdRail = 4,
        /// <summary>
        /// Fourth Rail 650V DC traction type
        /// </summary>
        FourthRail = 8,
        /// <summary>
        /// Overhead DC traction type
        /// </summary>
        OverheadDC = 16,
        /// <summary>
        /// Tramway traction type
        /// </summary>
        Tramway = 32,
        /// <summary>
        /// Spare 1
        /// </summary>
        Sim1 = 64,
        /// <summary>
        /// Spare 2
        /// </summary>
        Sim2 = 128,
        /// <summary>
        /// Spare 3
        /// </summary>
        Sim3 = 256,
        /// <summary>
        /// Spare 4
        /// </summary>
        Sim4 = 1024
    }

    /// <summary>
    /// An object representing the SimSig Electrification options
    /// </summary>
    public class Electrification
    {
        //TODO: Check the SimSig behaviour for non electric traction

        #region Constants
        #endregion Constants

        #region Private Variables
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
        
        /// <summary>
        /// Gets the bit wise value for the selected electrification options
        /// </summary>
        public int BitWise { get { return (int)this.GetBitWise(); } }

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
        /// Instantiates an Electrification object from the supplied BitWise value
        /// </summary>
        /// <param name="BitWise"></param>
        public Electrification(ElectrificationBitValue BitWise)
        {
            this.ParseBitWise(BitWise);
        }

        /// <summary>
        /// Instantiates a WTTElectrification object from the supplied SimSig code
        /// </summary>
        /// <param name="SimSigCode">A string representing the available types of electrification</param>
        public Electrification(string SimSigCode)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSimSigCode(SimSigCode, Globals.UserSettings.GetCultureInfo());

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
            string OriginalCode = Code; //Passes the supplied code to keep a copy for comparison
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo(); //Stores the Users Culture from their usersettings

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
                Code = Code.Replace("X1", "", true, Culture);
            }

            if (Code.IndexOf("X2", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Sim2 = true;
                HasMatch = true;
                CharactersParsed += 2;
                Code = Code.Replace("X2", "", true, Culture);
            }

            if (Code.IndexOf("X3", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Sim3 = true;
                HasMatch = true;
                CharactersParsed += 2;
                Code = Code.Replace("X3", "", true, Culture);
            }

            if (Code.IndexOf("X4", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Sim4 = true;
                HasMatch = true;
                CharactersParsed += 2;
                Code = Code.Replace("X4", "", true, Culture);
            }

            if (Code.IndexOf("D", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Diesel = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("D", "", true, Culture);
            }

            if (Code.IndexOf("O", StringComparison.OrdinalIgnoreCase)>=0)
            {
                Overhead = true;
                HasMatch = true;
                CharactersParsed ++;
                Code = Code.Replace("O", "", true, Culture);
            }

            if (Code.IndexOf("3", StringComparison.OrdinalIgnoreCase)>=0)
            {
                ThirdRail = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("3", "", true, Culture);
            }

            if (Code.IndexOf("4", StringComparison.OrdinalIgnoreCase)>=0)
            {
                FourthRail = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("4", "", true, Culture);
            }

            if (Code.IndexOf("V", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                OverheadDC = true;
                HasMatch = true;
                CharactersParsed++;
                Code = Code.Replace("V", "", true, Culture);
            }

            if (Code.IndexOf("T", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Tramway = true;
                HasMatch = true;
                CharactersParsed++;
            }

            if (!HasMatch || (CharactersParsed != OriginalCode.Length))
            {
                throw new ArgumentException(ExceptionHelper.GetStaticException("InvalidSimSigCodeError", null, Culture));
            }
        }

        /// <summary>
        /// Calculutes the bit wise value fro the selected electrification options
        /// </summary>
        /// <returns></returns>
        private ElectrificationBitValue GetBitWise()
        {
            ElectrificationBitValue ReturnValue = ElectrificationBitValue.Unknown;

            if (this.Diesel) ReturnValue += (int)ElectrificationBitValue.Diesel;
            if (this.Overhead) ReturnValue += (int)ElectrificationBitValue.Overhead;
            if (this.ThirdRail) ReturnValue += (int)ElectrificationBitValue.ThirdRail;
            if (this.FourthRail) ReturnValue += (int)ElectrificationBitValue.FourthRail;
            if (this.OverheadDC) ReturnValue += (int)ElectrificationBitValue.OverheadDC;
            if (this.Tramway) ReturnValue += (int)ElectrificationBitValue.Tramway;
            if (this.Sim1) ReturnValue += (int)ElectrificationBitValue.Sim1;
            if (this.Sim2) ReturnValue += (int)ElectrificationBitValue.Sim2;
            if (this.Sim3) ReturnValue += (int)ElectrificationBitValue.Sim3;
            if (this.Sim4) ReturnValue += (int)ElectrificationBitValue.Sim4;

            return ReturnValue;
        }

        /// <summary>
        /// Parse a BitWise value into the electrification flags
        /// </summary>
        /// <returns></returns>
        private void ParseBitWise(ElectrificationBitValue BitWise)
        {
            this.Diesel = (BitWise & ElectrificationBitValue.Diesel) == ElectrificationBitValue.Diesel;
            this.Overhead = (BitWise & ElectrificationBitValue.Overhead) == ElectrificationBitValue.Overhead;
            this.ThirdRail = (BitWise & ElectrificationBitValue.ThirdRail) == ElectrificationBitValue.ThirdRail;
            this.FourthRail = (BitWise & ElectrificationBitValue.FourthRail) == ElectrificationBitValue.FourthRail;
            this.OverheadDC = (BitWise & ElectrificationBitValue.OverheadDC) == ElectrificationBitValue.OverheadDC;
            this.Tramway = (BitWise & ElectrificationBitValue.Tramway) == ElectrificationBitValue.Tramway;
            this.Sim1 = (BitWise & ElectrificationBitValue.Sim1) == ElectrificationBitValue.Sim1;
            this.Sim2 = (BitWise & ElectrificationBitValue.Sim2) == ElectrificationBitValue.Sim2;
            this.Sim3 = (BitWise & ElectrificationBitValue.Sim3) == ElectrificationBitValue.Sim3;
            this.Sim4 = (BitWise & ElectrificationBitValue.Sim4) == ElectrificationBitValue.Sim4;
        }

        #endregion Methods
    }
}
