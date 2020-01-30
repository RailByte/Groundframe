using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using Newtonsoft.Json;

namespace GroundFrame.Classes.Timetables
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
        [JsonProperty("simsigId")]
        public string SimSigID { get { return this._SimSigID; } set { this._SimSigID = value; } }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Acceleration / Break Index
        /// </summary>
        [JsonProperty("accelBrakeIndex")]
        public WTTAccelBrakeIndex AccelBrakeIndex { get; set; }

        /// <summary>
        /// Gets or sets the freight flag
        /// </summary>
        [JsonProperty("isFreight")]
        public bool IsFreight { get; set; }

        /// <summary>
        /// Gets or sets the Can Use Goods Lines flag
        /// </summary>
        [JsonProperty("canUseGoodLines")]
        public bool CanUseGoodsLines { get; set; }

        /// <summary>
        /// Gets or sets the max speed
        /// </summary>
        [JsonProperty("maxSpeed")]
        public WTTSpeed MaxSpeed { get; set; }

        /// <summary>
        /// Gets or sets the train length
        /// </summary>
        [JsonProperty("trainLength")]
        public Length TrainLength { get; set; }

        /// <summary>
        /// Gets or sets the speed class
        /// </summary>
        [JsonProperty("speedClass")]
        public WTTSpeedClass SpeedClass { get; set; }

        /// <summary>
        /// Gets or sets the power to weight category
        /// </summary>
        [JsonProperty("powerToWeightCategory")]
        public WTTPowerToWeightCategory PowerToWeightCategory { get; set; }

        /// <summary>
        /// Gets or sets the dwell times
        /// </summary>
        [JsonProperty("dwellTimes")]
        public WTTDwell DwellTimes { get; set; }

        /// <summary>
        /// Gets or sets the electrification
        /// </summary>
        [JsonProperty("electrification")]
        public Electrification Electrification { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        private WTTTrainCategory()
        {
        }

        /// <summary>
        /// Instantiates a new instance of a WTTTrainobject object
        /// </summary>
        public WTTTrainCategory(string Description, int AccelBrakeIndex, bool IsFreight, bool CanUseGoodsLines, int SpeedMPH, int LengthMeters, int SpeedClassBitWise, int PowerToWeightCategory, string Electrification)
        {
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

        /// <summary>
        /// Instantiates a WTTTrainCategort object from the supplied SimSig xml snippet as an XElement
        /// </summary>
        /// <param name="TrainCategoryXML">The SimSig xml snippet</param>
        public WTTTrainCategory(XElement TrainCategoryXML)
        {
             //Valdate Arguments
            ArgumentValidation.ValidateXElement(TrainCategoryXML, Globals.UserSettings.GetCultureInfo());

            //Parse the XML
            this.ParseHeaderXML(TrainCategoryXML);
        }

        /// <summary>
        /// Instantiates a WTTTrainCategory object from the supplied JSON string
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTrainCategory object</param>
        public WTTTrainCategory(string JSON)
        {
            //Valdate Arguments
            ArgumentValidation.ValidateJSON(JSON, Globals.UserSettings.GetCultureInfo());

            //Parse the JSON
            this.PopulateFromJSON(JSON);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses the WTTHeader from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="TrainCategoryXML"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void ParseHeaderXML(XElement TrainCategoryXML)
        {
            try
            {
                this._SimSigID = TrainCategoryXML.Attribute("ID") == null ? null : TrainCategoryXML.Attribute("ID").Value.ToString();
                this.Description = XMLMethods.GetValueFromXElement<string>(TrainCategoryXML, @"Description", string.Empty);
                this.AccelBrakeIndex = (WTTAccelBrakeIndex)XMLMethods.GetValueFromXElement<int>(TrainCategoryXML, @"AccelBrakeIndex", null);
                this.IsFreight = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TrainCategoryXML, @"IsFreight", null));
                this.CanUseGoodsLines = Convert.ToBoolean(XMLMethods.GetValueFromXElement<int>(TrainCategoryXML, @"CanUseGoodsLines", null));
                this.MaxSpeed = new WTTSpeed(XMLMethods.GetValueFromXElement<int>(TrainCategoryXML, @"MaxSpeed", null));
                this.TrainLength = new Length(XMLMethods.GetValueFromXElement<int>(TrainCategoryXML, @"TrainLength", null));
                this.SpeedClass = new WTTSpeedClass(XMLMethods.GetValueFromXElement<int>(TrainCategoryXML, @"SpeedClass", null));
                this.PowerToWeightCategory = (WTTPowerToWeightCategory)XMLMethods.GetValueFromXElement<int>(TrainCategoryXML, @"PowerToWeightCategory", null);
                this.Electrification = new Electrification(XMLMethods.GetValueFromXElement<string>(TrainCategoryXML, @"Electrification", null));

                //Load any dwell times
                XElement DwellXML = TrainCategoryXML.Element("DwellTimes");

                //If the Dwell times XML is <NULL> then just make the DwellTimes property null
                if (!DwellXML.HasElements)
                {
                    this.DwellTimes = null;
                }
                else
                {
                    this.DwellTimes = new WTTDwell(DwellXML);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTTrainCategoryException", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTTrainCategory object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                JsonConvert.PopulateObject(JSON, this);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTrainCategoryJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
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

        /// <summary>
        /// Serializes the WTTTrainCategory object to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        #endregion Methods
    }
}
