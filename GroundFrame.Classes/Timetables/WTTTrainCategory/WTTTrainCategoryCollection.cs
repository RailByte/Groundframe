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
    /// Class representing a collection of train categories
    /// </summary>
    public class WTTTrainCategoryCollection : IEnumerable<WTTTrainCategory>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        private List<WTTTrainCategory> _TrainCategories = new List<WTTTrainCategory>(); //List to store all the train categories
        #endregion Private Variables

        #region Properties
        public IEnumerator<WTTTrainCategory> GetEnumerator() { return this._TrainCategories.GetEnumerator(); }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Private constructor to handle the deserialization of a WTTTrainCategoryCollection object from JSON
        /// </summary>
        /// <param name="WTTTrainCategories">An IEnumerable<WTTTrainCategory> which represents the collection of timetables</param>
        [JsonConstructor]
        private WTTTrainCategoryCollection(IEnumerable<WTTTrainCategory> WTTTrainCategories)
        {
            this._TrainCategories = new List<WTTTrainCategory>();

            foreach (WTTTrainCategory TrainCategory in WTTTrainCategories)
            {
                WTTTrainCategory NewWTTTrainCategory = TrainCategory;
                this._TrainCategories.Add(NewWTTTrainCategory);
            }
        }

        /// <summary>
        /// Instantiates a WTTTimeTableCollection
        /// </summary>
        /// <param name="WTTTrainCategoryXML">The SimSig XML (as an XElement) that represents the WTTTimeTableCollection</param>
        public WTTTrainCategoryCollection(XElement WTTTrainCategoryXML)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();
            //Validate arguments
            ArgumentValidation.ValidateXElement(WTTTrainCategoryXML, Culture);

            this._TrainCategories = new List<WTTTrainCategory>();
            ParseWTTTrainCategoriesXML(WTTTrainCategoryXML);
        }

        /// <summary>
        /// Instantiates a WTTTrainCategoryCollection from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing a collection of train categories</param>
        public WTTTrainCategoryCollection(string JSON)
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
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseUserSettingsJSONError", null, Culture), Ex);
            }
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._TrainCategories.Count; } }

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
                IEnumerable<WTTTrainCategory> TrainCategoryList = JsonConvert.DeserializeObject<IEnumerable<WTTTrainCategory>>(JSON);
                this._TrainCategories = TrainCategoryList.ToList();
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTrainCategoryCollectionJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Parses an XElement object representing a SimSig collection of train categories
        /// </summary>
        /// <param name="WTTTrainCatoriesXML">The XML representing tthe SimSig collection of train categories</param>
        private void ParseWTTTrainCategoriesXML(XElement WTTTrainCatoriesXML)
        {
            foreach (XElement WTTTimeTableXML in WTTTrainCatoriesXML.Elements("TrainCategory"))
            {
                this._TrainCategories.Add(new WTTTrainCategory(WTTTimeTableXML));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the Train Category for the supplied Index
        /// </summary>
        /// <param name="Index">The index ordinal</param>
        /// <returns></returns>
        public WTTTrainCategory IndexOf(int Index)
        {
            return this._TrainCategories[Index];
        }

        /// <summary>
        /// Gets a JSON string that represents the UserSetting Collection
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Disposes the VersionCollection object
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {

            }
            else
            {

            }
        }

        ~WTTTrainCategoryCollection()
        {
            // The object went out of scope and finalized is called
            // Lets call dispose in to release unmanaged resources 
            // the managed resources will anyways be released when GC 
            // runs the next time.
            Dispose(false);
        }

        #endregion Methods
    }
}