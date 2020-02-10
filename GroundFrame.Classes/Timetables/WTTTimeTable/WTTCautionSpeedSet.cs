using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Class representing a SimSig Caution Speed set
    /// </summary>
    public class WTTCautionSpeedSet
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private string _SimSigID; //Private variable to store the Caution Speed Set ID
        private List<WTTCautionSpeed> _CautionSpeeds; //Private variable to store the caution speeds

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Caution Speed Set ID
        /// </summary>
        [JsonProperty("simsigId")]
        public string SimSigID { get { return this._SimSigID; } }

        /// <summary>
        /// Gets or sets the Caution Speed Set name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the list of Caution Speeds
        /// </summary>
        public List<WTTCautionSpeed> CautionSpeeds { get { return this._CautionSpeeds; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor used by the JSON parser
        /// </summary>
        /// <param name="SimSigID">The SimSig ID of the Caution Speed Set</param>
        /// <param name="Name">The Name of the Speed Set</param>
        /// <param name="CautionSpeeds">The List of Caution Speeds for the set</param>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used the Newtonsoft.JSON as the default Deserialization constructor")]
        private WTTCautionSpeedSet(string SimSigID, string Name, List<WTTCautionSpeed> CautionSpeeds)
        {
            this._SimSigID = SimSigID;
            this.Name = Name;
            this._CautionSpeeds = CautionSpeeds;
        }

        /// <summary>
        /// Instantiates a new WTTCautionSpeedSet object from the supplied SimSig XML snippet (as an XElement)
        /// </summary>
        /// <param name="WTTCautionSpeedSetXML">The SimSig XML Snippet (as an XElement) representing a single WTT Caution Speed set</param>
        public WTTCautionSpeedSet(XElement WTTCautionSpeedSetXML)
        {
            //Validate Arguments
            ArgumentValidation.ValidateXElement(WTTCautionSpeedSetXML, Globals.UserSettings.GetCultureInfo());
            //Parse the XML
            this.ParseWTTCautionSpeedSetXML(WTTCautionSpeedSetXML);
        }

        /// <summary>
        /// Instantiates a new WTTCautionSpeedSet object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTSpeedSet</param>
        public WTTCautionSpeedSet(string JSON)
        {
            //Validate Arguments
            ArgumentValidation.ValidateJSON(JSON, Globals.UserSettings.GetCultureInfo());

            //Try deserializing the string
            try
            {
                //Deserialize the JSON string
                this.PopulateFromJSON(JSON);

            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTActivityCollectionJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets a JSON string that represents the WTTCautionSpeedSet
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Adds a new WTTCautionSpeed object to the set
        /// </summary>
        /// <param name="CautionSpeed">The WTTCautionSpeed to get</param>
        public void Add(WTTCautionSpeed CautionSpeed)
        {
            if (this._CautionSpeeds == null)
            {
                this._CautionSpeeds = new List<WTTCautionSpeed>();
            }

            this._CautionSpeeds.Add(CautionSpeed);
        }

        /// <summary>
        /// Removed a WTTCautionSpeed object from the set
        /// </summary>
        /// <param name="Index">The index of the WTTCautionSpeed object to remove from the set</param>
        public void RemoveAt(int Index)
        {
            if (this._CautionSpeeds != null)
            {
                if(Index >= this._CautionSpeeds.Count)
                {
                    throw new IndexOutOfRangeException(ExceptionHelper.GetStaticException("IndexOutOfRangeError", new object[] { "CautionSpeeds" }, Globals.UserSettings.GetCultureInfo()));
                }
                else
                {
                    this._CautionSpeeds.RemoveAt(Index);
                }

                if (this._CautionSpeeds.Count == 0)
                {
                    this._CautionSpeeds = null;
                }
            }
        }


        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTHeader object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                WTTCautionSpeedSet Temp = JsonConvert.DeserializeObject<WTTCautionSpeedSet>(JSON);
                this._SimSigID = Temp.SimSigID;
                this.Name = Temp.Name;
                this._CautionSpeeds = Temp.CautionSpeeds;
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTActivityCollectionJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Parses a WTTCautionSpeedSetXML XElement into this object
        /// </summary>
        /// <param name="WTTCautionSpeedSetXML">The SimSig XML Snippet (as an XElement) representing a single WTT Caution Speed set</param>
        private void ParseWTTCautionSpeedSetXML(XElement WTTCautionSpeedSetXML)
        {
            try
            {
                if (WTTCautionSpeedSetXML.Attribute("ID") == null)
                {
                    this.GenerateSimSigID();
                }
                else
                {
                    this._SimSigID = WTTCautionSpeedSetXML.Attribute("ID").Value;
                }

                this.Name = XMLMethods.GetValueFromXElement<string>(WTTCautionSpeedSetXML, @"Name", string.Empty);

                if (WTTCautionSpeedSetXML.Element("CautionSpeeds") != null)
                {
                    this._CautionSpeeds = new List<WTTCautionSpeed>();

                    foreach(XElement WTTCautionSpeedXML in WTTCautionSpeedSetXML.Element("CautionSpeeds").Elements("CautionSpeed"))
                    {
                        this._CautionSpeeds.Add(new WTTCautionSpeed(WTTCautionSpeedXML));
                    };
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("ParseFromXElementWTTCautionSpeedSetException", null, Globals.UserSettings.GetCultureInfo()), Ex);
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
