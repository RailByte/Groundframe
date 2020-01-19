using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace GroundFrame.Classes
{
    public class WTTTrainCategory
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private string _SimSigID; //Stores the SimSig ID

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the SimSig ID
        /// </summary>
        public string SimSigID { get { return this._SimSigID; } }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of a WTTTraction object
        /// </summary>
        public WTTTrainCategory()
        {
            this.GenerateSimSigID();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Generates a 6 character long random hex string
        /// </summary>
        /// <param name="ReplaceExisting">Flag to indicate whether the existing SimSig should be replaced if it exists</param>
        private void GenerateSimSigID(bool ReplaceExisting = false)
        {
            if (string.IsNullOrEmpty(this._SimSigID) || (string.IsNullOrEmpty(this._SimSigID) == false && ReplaceExisting))
            {
                byte[] Buffer = new byte[3];
                new Random().NextBytes(Buffer);
#pragma warning disable CA1305 // Specify IFormatProvider
                string SimSigID = String.Concat(Buffer.Select(x => x.ToString("X2")).ToArray());
#pragma warning restore CA1305 // Specify IFormatProvider
                if (6 % 2 == 0)
                {
                    this._SimSigID = SimSigID;
                }

                this._SimSigID = SimSigID;
            }
        }

        #endregion Methods
    }
}
