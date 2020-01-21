using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;

namespace GroundFrame.Classes
{
    public class WTTTrainCategory
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private string _SimSigID; //Stores the SimSig ID
        private readonly CultureInfo _Culture; //Stores the culture

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

        /// <summary>
        /// Gets or sets the Acceleration / Break Index
        /// </summary>
        public WTTAccelBrakeIndex AccelBrakeIndex { get; set; }

        /// <summary>
        /// Gets or sets the freight flag
        /// </summary>
        public bool IsFreight { get; set; }

        /// <summary>
        /// Gets or sets the Can Use Goods Lines flag
        /// </summary>
        public bool CanUseGoodsLines { get; set; }


        /// <summary>
        /// Gets or sets the max speed
        /// </summary>
        public WTTSpeed MaxSpeed { get; set; }

        /// <summary>
        /// Gets or sets the train length
        /// </summary>
        public Length TrainLength { get; set; }

        /// <summary>
        /// Gets or sets the speed class
        /// </summary>
        public WTTSpeedClass SpeedClass { get; set; }

        /// <summary>
        /// Gets or sets the power to weight category
        /// </summary>
        public WTTPowerToWeightCategory PowerToWeightCategory { get; set; }

        //TODO: Add Dwell Times    

        /// <summary>
        /// Gets or sets the electrification
        /// </summary>
        public Electrification Electrification { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of a WTTTrainobject object
        /// </summary>
        public WTTTrainCategory(string Description, int AccelBrakeIndex, bool IsFreight, bool CanUseGoodsLines, int SpeedMPH, int LengthMeters, int SpeedClassBitWise, int PowerToWeightCategory, string Electrification, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture ?? "en-GB");
            this.GenerateSimSigID();
            this.Description = Description;
            this.AccelBrakeIndex = (WTTAccelBrakeIndex)AccelBrakeIndex;
            this.IsFreight = IsFreight;
            this.CanUseGoodsLines = CanUseGoodsLines;
            this.MaxSpeed = new WTTSpeed(SpeedMPH);
            this.TrainLength = new Length(LengthMeters);
            this.SpeedClass = new WTTSpeedClass(SpeedClassBitWise);
            this.PowerToWeightCategory = (WTTPowerToWeightCategory)PowerToWeightCategory;
            this.Electrification = new Electrification(Electrification);
        }

        public WTTTrainCategory(XElement TrainCategoryXML, string Culture = "en-GB")
        {
            this._Culture = new CultureInfo(Culture ?? "en-GB");

            //Check Header Argument
            if (TrainCategoryXML == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "TrainCategoryXML" }, this._Culture));
            }

            //Parse the XML
            this.ParseHeaderXML(TrainCategoryXML);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses the WTTHeader from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void ParseHeaderXML(XElement Header)
        {
            try
            {
                this._SimSigID = Header.Attribute("ID") == null ? null : Header.Attribute("ID").Value.ToString();
                this.Description = XMLMethods.GetValueFromXElement<string>(Header, @"Description", string.Empty, this._Culture.Name);
                this.AccelBrakeIndex = (WTTAccelBrakeIndex)XMLMethods.GetValueFromXElement<int>(Header, @"AccelBrakeIndex", null, this._Culture.Name);
                this.IsFreight = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(Header, @"IsFreight", null, this._Culture.Name));
                this.CanUseGoodsLines = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(Header, @"CanUseGoodsLines", null, this._Culture.Name));
                this.MaxSpeed = new WTTSpeed(XMLMethods.GetValueFromXElement<int>(Header, @"MaxSpeed", null, this._Culture.Name));
                this.TrainLength = new Length(XMLMethods.GetValueFromXElement<int>(Header, @"TrainLength", null, this._Culture.Name));
                this.SpeedClass = new WTTSpeedClass(XMLMethods.GetValueFromXElement<int>(Header, @"SpeedClass", null, this._Culture.Name), this._Culture.Name);
                this.PowerToWeightCategory = (WTTPowerToWeightCategory)XMLMethods.GetValueFromXElement<int>(Header, @"PowerToWeightCategory", null, this._Culture.Name);
                this.Electrification = new Electrification(XMLMethods.GetValueFromXElement<string>(Header, @"Electrification", null, this._Culture.Name));

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTrainCategoryException", null, this._Culture), Ex);
            }
        }

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
