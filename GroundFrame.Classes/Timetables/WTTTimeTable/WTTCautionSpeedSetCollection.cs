using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// Class representing a collection of WTTCautionSpeedSet objects
    /// </summary>
    public class WTTCautionSpeedSetCollection : IEnumerable<WTTCautionSpeedSet>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<WTTCautionSpeedSet> _CautionSpeedSets = new List<WTTCautionSpeedSet>(); //List to store all the caution sppeed sets

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the WTTCautionSpeedSet collection enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<WTTCautionSpeedSet> GetEnumerator() { return this._CautionSpeedSets.GetEnumerator(); }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Private constructor to handle the deserialization of a WTTCautionSpeedSetyCollection object from JSON
        /// </summary>
        /// <param name="WTTCautionSpeedSets">An IEnumerable&lt;WTTCautionSpeedSet&gt; object which represents the collection of caution speed sets</param>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private WTTCautionSpeedSetCollection(IEnumerable<WTTCautionSpeedSet> WTTCautionSpeedSets)
        {
            this._CautionSpeedSets = new List<WTTCautionSpeedSet>();

            foreach (WTTCautionSpeedSet WTTSpeedSet in WTTCautionSpeedSets)
            {
                WTTCautionSpeedSet NewWTTSpeedSet = WTTSpeedSet;
                this._CautionSpeedSets.Add(NewWTTSpeedSet);
            }
        }

        /// <summary>
        /// Instantiates a WTTCautionSpeedSetollection from the supplied XElement object represening a SimSig caution speed set collection
        /// </summary>
        /// <param name="WTTActivitiesXML">The XML object representing the SimSig caution speed set collection</param>
        public WTTCautionSpeedSetCollection(XElement WTTActivitiesXML)
        {
            //Validate arguments
            ArgumentValidation.ValidateXElement(WTTActivitiesXML, Globals.UserSettings.GetCultureInfo());

            this._CautionSpeedSets = new List<WTTCautionSpeedSet>();
            ParseWTTSpeedSetsXML(WTTActivitiesXML);
        }

        /// <summary>
        /// Instantiates a WTTCautionSpeedSetCollection from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing the caution speed set collection</param>
        public WTTCautionSpeedSetCollection(string JSON)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();

            //Validate arguments
            ArgumentValidation.ValidateJSON(JSON, Culture);

            //Try deserializing the string
            try
            {
                //Deserialize the JSON string
                this.PopulateFromJSON(JSON);

            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTActivityCollectionJSONError", null, Culture), Ex);
            }
        }

        /// <summary>
        /// Instantiates a WTTActivityCollection object with the supplied start date. Used by the WTTActivityCollectionConverter class to convert JSON to WTTActivityCollection
        /// </summary>
        internal WTTCautionSpeedSetCollection()
        {
            this._CautionSpeedSets = new List<WTTCautionSpeedSet>();
        }

        /// <summary>
        /// Instantiates a WTTActivityCollection object from WTTCautionSpeedSetCollectionSurrogate object
        /// </summary>
        /// <param name="SurrogateCautionSpeedSetCollection">The source WTTActivityCollectionSurrogate object</param>
        internal WTTCautionSpeedSetCollection(WTTCautionSpeedSetCollectionSurrogate SurrogateCautionSpeedSetCollection)
        {
            if (SurrogateCautionSpeedSetCollection.CautionSpeedSets != null)
            {
                this._CautionSpeedSets = new List<WTTCautionSpeedSet>();
                foreach (WTTCautionSpeedSet CautionSpeedSet in SurrogateCautionSpeedSetCollection.CautionSpeedSets)
                {
                    this._CautionSpeedSets.Add(CautionSpeedSet);
                }
            }
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._CautionSpeedSets.Count; } }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTTHeader object</param>
        private void PopulateFromJSON(string JSON)
        {
            //JSON argument will already have been validated in the constructor
            try
            {
                WTTCautionSpeedSetCollection Temp = JsonConvert.DeserializeObject<WTTCautionSpeedSetCollection>(JSON, new WTTCautionSpeedSetCollectionConverter());
                this._CautionSpeedSets = Temp.ToList();
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseFromXElementWTTCautionSpeedSetException", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Parse an SimSig XML Snippet representing a SimSig Caution Speed Sets into this WTTCautionSpeedSetCollection object
        /// </summary>
        /// <param name="WTTCautionSpeedSetsXML">The XML representing the SimSig Caution Speed Set</param>
        private void ParseWTTSpeedSetsXML(XElement WTTCautionSpeedSetsXML)
        {
            foreach (XElement WTTSpeedSetXML in WTTCautionSpeedSetsXML.Elements("CautionSpeedSet"))
            {
                this._CautionSpeedSets.Add(new WTTCautionSpeedSet(WTTSpeedSetXML));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the WTTCautionSpeedSet for the supplied Index
        /// </summary>
        /// <param name="Index">The index ordinal</param>
        /// <returns></returns>
        public WTTCautionSpeedSet IndexOf(int Index)
        {
            return this._CautionSpeedSets[Index];
        }

        /// <summary>
        /// Adds at WTTCautionSpeedSet to the collection
        /// </summary>
        /// <param name="CautionSpeedSet">The WTTCautionSpeedSet object to add to the collection</param>
        public void Add(WTTCautionSpeedSet CautionSpeedSet)
        {
            if (this._CautionSpeedSets == null)
            {
                this._CautionSpeedSets = new List<WTTCautionSpeedSet>();
            }
            this._CautionSpeedSets.Add(CautionSpeedSet);
        }

        /// <summary>
        /// Get the list of WTTCautionSpeedSets
        /// </summary>
        public List<WTTCautionSpeedSet> ToList()
        {
            return this._CautionSpeedSets;
        }

        /// <summary>
        /// Converts this WTTCautionSpeedSetCollection object to a WTTCautionSpeedSetCollectionSurrogate object
        /// </summary>
        /// <returns></returns>
        internal WTTCautionSpeedSetCollectionSurrogate ToWTTCautionSpeedSetCollectionSurrogate()
        {
            return new WTTCautionSpeedSetCollectionSurrogate
            {
                CautionSpeedSets = this._CautionSpeedSets
            };
        }

        /// <summary>
        /// Gets a JSON string that represents the UserSetting Collection
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new WTTActivityCollectionConverter());
        }

        /// <summary>
        /// Disposes the WTTActivityCollection object
        /// </summary>
        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protect implementation of the Dispose Pattern
        /// </summary>
        /// <param name="disposing">Indivates whether the WTTActivityCollection object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {

            }
            else
            {
                //Dispose any supporting objects here
            }
        }

        #endregion Methods
    }
}