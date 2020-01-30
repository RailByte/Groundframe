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
    /// Class representing a collection of WTTTimeTable objects
    /// </summary>
    public class WTTTimeTableCollection : IEnumerable<WTTTimeTable>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<WTTTimeTable> _TimeTables = new List<WTTTimeTable>(); //List to store all the time tables
        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private DateTime _StartDate; //Stores the WTT Start Date

        #endregion Private Variables

        /// <summary>
        /// Gets the WTTTimeTableCollection enumerator
        /// </summary>
        /// <returns></returns>
        #region Properties
        public IEnumerator<WTTTimeTable> GetEnumerator() { return this._TimeTables.GetEnumerator(); }

        /// <summary>
        /// Gets the timetable start date
        /// </summary>
        public DateTime StartDate { get { return this._StartDate; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Private constructor to handle the deserialization of a WTTTimeTable object from JSON
        /// </summary>
        /// <param name="WTTTimeTables">An IEnumerable<WTTTimeTable> which represents the collection of timetables</param>
        [JsonConstructor]
        private WTTTimeTableCollection(IEnumerable<WTTTimeTable> WTTTimeTables)
        {
            this._TimeTables = new List<WTTTimeTable>();

            foreach (WTTTimeTable WTT in WTTTimeTables)
            {
                WTTTimeTable NewWTTTimeTable = WTT;
                this._TimeTables.Add(NewWTTTimeTable);
            }
        }

        /// <summary>
        /// Instantiates a WTTTimeTableCollection from the supplied XElement object represening a SimSig timetable collection and timetable start date
        /// </summary>
        /// <param name="WTTTimeTableXML">The XML object representing the SimSig timetable collection</param>
        /// <param name="StartDate">The start date of the timetable. Must be after 01/01/1850</param>
        public WTTTimeTableCollection(XElement WTTTimeTableXML, DateTime StartDate)
        {
            CultureInfo Culture = Globals.UserSettings.GetCultureInfo();
            //Validate arguments
            ArgumentValidation.ValidateXElement(WTTTimeTableXML, Culture);
            ArgumentValidation.ValidateWTTStartDate(StartDate, Culture);

            this._StartDate = StartDate;
            this._TimeTables = new List<WTTTimeTable>();
            ParseWTTTimeTablesXML(WTTTimeTableXML);
        }

        /// <summary>
        /// Instantiates a WTTTimeTableCollection from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing the timetable collection</param>
        public WTTTimeTableCollection(string JSON)
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
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTimeTableCollectionJSONError", null, Culture), Ex);
            }
        }

        /// <summary>
        /// Instantiates a WTTTimeTableCollection object with the supplied start date. Used by the WTTTimeTableCollectionConverter class to convert JSON to WTTTableCollection
        /// </summary>
        /// <param name="StartDate"></param>
        internal WTTTimeTableCollection(DateTime StartDate)
        {
            ArgumentValidation.ValidateWTTStartDate(StartDate, Globals.UserSettings.GetCultureInfo());
            this._StartDate = StartDate;
            this._TimeTables = new List<WTTTimeTable>();
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._TimeTables.Count; } }

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
                WTTTimeTableCollection Temp = JsonConvert.DeserializeObject<WTTTimeTableCollection>(JSON, new WTTTimeTableCollectionConverter());
                this._StartDate = Temp.StartDate;
                this._TimeTables = Temp.ToList();
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTTimeTableCollectionJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        private void ParseWTTTimeTablesXML(XElement WTTTimeTablesXML)
        {
            foreach (XElement WTTTimeTableXML in WTTTimeTablesXML.Elements("Timetable"))
            {
                this._TimeTables.Add(new WTTTimeTable(WTTTimeTableXML, this._StartDate));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the UserSetting for the supplied Index
        /// </summary>
        /// <param name="Index">The index ordinal</param>
        /// <returns></returns>
        public WTTTimeTable IndexOf(int Index)
        {
            return this._TimeTables[Index];
        }

        /// <summary>
        /// Adds at WTTTimeTable to the collection
        /// </summary>
        /// <param name="TimeTable">The WTTTimeTable object to add to the collection</param>
        public void Add(WTTTimeTable TimeTable)
        {
            if (this._TimeTables == null)
            {
                this._TimeTables = new List<WTTTimeTable>();
            }
            this._TimeTables.Add(TimeTable);
        }

        /// <summary>
        /// Returns a list of WTTTimetables with the supplied headcode
        /// </summary>
        /// <param name="Headcode">The headcode to search</param>
        /// <returns>A list of WTTTimetable objects with the matching headcode</returns>
        public List<WTTTimeTable> GetByHeadCode(string Headcode)
        {
            return this._TimeTables.Where(h => string.Equals(h.Headcode, Headcode, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Get the list of WTTTimetables
        /// </summary>
        public List<WTTTimeTable> ToList()
        {
            return this._TimeTables;
        }

        /// <summary>
        /// Gets a JSON string that represents the UserSetting Collection
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new WTTTimeTableCollectionConverter());
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
                if (this._SQLConnector != null)
                {
                    this._SQLConnector.Dispose();
                }
            }
        }

        ~WTTTimeTableCollection()
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