﻿using Newtonsoft.Json;
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

namespace GroundFrame.Core.Timetables
{
    /// <summary>
    /// Class representing a collection of WTTActivity objects
    /// </summary>
    public class WTTActivityCollection : IEnumerable<WTTActivity>, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<WTTActivity> _Activities = new List<WTTActivity>(); //List to store all the activities

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the WTTActivity collection enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<WTTActivity> GetEnumerator() { return this._Activities.GetEnumerator(); }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Private constructor to handle the deserialization of a WTTActivityCollection object from JSON
        /// </summary>
        /// <param name="WTTActivities">An IEnumerable&lt;WTTActivity&gt; objectwhich represents the collection of trips</param>
        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private WTTActivityCollection(IEnumerable<WTTActivity> WTTActivities)
        {
            this._Activities = new List<WTTActivity>();

            foreach (WTTActivity WTTActivity in WTTActivities)
            {
                WTTActivity NewWTTActivity = WTTActivity;
                this._Activities.Add(NewWTTActivity);
            }
        }

        /// <summary>
        /// Instantiates a WTTActivityCollection from the supplied XElement object represening a SimSig activity collection and timetable start date
        /// </summary>
        /// <param name="WTTActivitiesXML">The XML object representing the SimSig activity collection</param>
        public WTTActivityCollection(XElement WTTActivitiesXML)
        {
            //Validate arguments
            ArgumentValidation.ValidateXElement(WTTActivitiesXML, Globals.UserSettings.GetCultureInfo());

            this._Activities = new List<WTTActivity>();
            ParseWTTActivitysXML(WTTActivitiesXML);
        }

        /// <summary>
        /// Instantiates a WTTActivityCollection from a JSON file.
        /// </summary>
        /// <param name="JSON">A JSON string representing the trip collection</param>
        public WTTActivityCollection(string JSON)
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
        internal WTTActivityCollection()
        {
            this._Activities = new List<WTTActivity>();
        }

        /// <summary>
        /// Instantiates a WTTActivityCollection object from WTTActivityCollectionSurrogate object
        /// </summary>
        /// <param name="SurrogateWTTActivityCollection">The source WTTActivityCollectionSurrogate object</param>
        internal WTTActivityCollection(WTTActivityCollectionSurrogate SurrogateWTTActivityCollection)
        {
            if (SurrogateWTTActivityCollection.Activities != null)
            {
                this._Activities = new List<WTTActivity>();
                foreach (WTTActivity Activity in SurrogateWTTActivityCollection.Activities)
                {
                    this._Activities.Add(Activity);
                }
            }
        }

        /// <summary>
        /// Gets the total number of versions in the collection
        /// </summary>
        public int Count { get { return this._Activities == null ? 0 : this._Activities.Count; } }

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
                WTTActivityCollection Temp = JsonConvert.DeserializeObject<WTTActivityCollection>(JSON, new WTTActivityCollectionConverter());
                this._Activities = Temp.ToList();
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(ExceptionHelper.GetStaticException("ParseWTTActivityCollectionJSONError", null, Globals.UserSettings.GetCultureInfo()), Ex);
            }
        }

        /// <summary>
        /// Parses the WTTActivitiesXML into the list of WTTActivity objects
        /// </summary>
        /// <param name="WTTActivitiesXML"></param>
        private void ParseWTTActivitysXML(XElement WTTActivitiesXML)
        {
            foreach (XElement WTTActivityXML in WTTActivitiesXML.Elements("Activity"))
            {
                this._Activities.Add(new WTTActivity(WTTActivityXML));
            }
        }

        /// <summary>
        /// Gets the List enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the WTTActivity for the supplied Index
        /// </summary>
        /// <param name="Index">The index ordinal</param>
        /// <returns></returns>
        public WTTActivity IndexOf(int Index)
        {
            return this._Activities[Index];
        }

        /// <summary>
        /// Adds at WTTActivity to the collection
        /// </summary>
        /// <param name="Trip">The WTTActivity object to add to the collection</param>
        public void Add(WTTActivity Trip)
        {
            if (this._Activities == null)
            {
                this._Activities = new List<WTTActivity>();
            }
            this._Activities.Add(Trip);
        }

        /// <summary>
        /// Get the list of WTTActivitys
        /// </summary>
        public List<WTTActivity> ToList()
        {
            return this._Activities;
        }

        /// <summary>
        /// Converts this WTTActivityCollection object to a WTTActivityCollectionSurrogate object
        /// </summary>
        /// <returns></returns>
        internal WTTActivityCollectionSurrogate ToWTTActivityCollectionSurrogate()
        {
            return new WTTActivityCollectionSurrogate
            {
                Activities = this._Activities
            };
        }

        /// <summary>
        /// Gets a JSON string that represents the ActivityCollection
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
                //Dispose objects where necessary
            }
        }

        #endregion Methods
    }
}