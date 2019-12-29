using System;
using System.Collections.Generic;
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
        #endregion Private Variables

        #region Properties

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

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a WTTElectrification object from the supplied SimSig code
        /// </summary>
        /// <param name="SimSimCode"></param>
        public Electrification(string SimSimCode)
        {
            this.ConvertSimSigCode(SimSimCode);
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

            if (string.IsNullOrEmpty(Code))
            {
                Overhead = false;
                ThirdRail = false;
                FourthRail = false;
                HasMatch = true;
            }

            if (Code.ToUpper().IndexOf("O")>=0)
            {
                Overhead = true;
                HasMatch = true;
                CharactersParsed ++;
            }

            if (Code.ToUpper().IndexOf("3")>=0)
            {
                ThirdRail = true;
                HasMatch = true;
                CharactersParsed++;
            }

            if (Code.ToUpper().IndexOf("4")>=0)
            {
                FourthRail = true;
                HasMatch = true;
                CharactersParsed++;
            }

            if (!HasMatch || (CharactersParsed != Code.Length))
            {
                throw new ArgumentException("The Code provided is not a valid SimSig code");
            }
        }

        #endregion Methods
    }
}
