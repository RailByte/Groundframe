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
        /// Instantiates a new WTTCautionSpeedSet object. Used the the JSON deserializer
        /// </summary>
        public WTTCautionSpeedSet(string Name)
        {
            this.Name = Name;
            //Generate new SimSigID
            this.GenerateSimSigID();
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

        #endregion Constructors

        #region Methods

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

                    Parallel.ForEach(WTTCautionSpeedSetXML.Element("CautionSpeeds").Elements("CautionSpeed"), WTTCautionSpeedXML =>
                    {
                        this._CautionSpeeds.Add(new WTTCautionSpeed(WTTCautionSpeedXML));
                    });
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
