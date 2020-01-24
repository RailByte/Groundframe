﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace GroundFrame.Classes.WTT
{
    public enum SimSigSimulation
    {
        Royston = 1
    }

    public class WTT
    {
        #region Private Variables

        private readonly string _SourceWTTFileName; //If the WTT is read from a WTT file the source file path will be stored here
        private XDocument _SourceWTTXML;
        private readonly GFSqlConnector _SQLConnector; //Stores the connection to the GroundFrame.SQL database
        private readonly CultureInfo _Culture; //Stores the users culture
        private readonly DateTime _StartDate; //Stores the start date of the timetable

        #endregion Private Variables

        #region Constants
        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets or Sets the XML which was extracted from the Source SimSig WTT
        /// </summary>
        [JsonIgnore]
        public XDocument SourceWTTXML { get { return this._SourceWTTXML; } }

        /// <summary>
        /// Gets the full path which was used as the source SimSig WTT file to instantiate the WTT object
        /// </summary>
        [JsonIgnore]
        public string SourceWTTFileName { get { return this._SourceWTTFileName; } }

        /// <summary>
        /// Gets or sets the WTT Header
        /// </summary>
        [JsonProperty("header")]
        public WTTHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the Simulation
        /// </summary>
        [JsonProperty("simulation")]
        public SimSigSimulation Simulation { get; set; }

        /// <summary>
        /// Gets or sets the Simulation Version the timetable was written for
        /// </summary>
        [JsonProperty("simulationVersion")]
        public string SimulationVersion { get; set; }

        /// <summary>
        /// Gets or sets the WTT train categories
        /// </summary>
        [JsonProperty("trainCategories")]
        public List<WTTTrainCategory> TrainCategories { get; set; }

        /// <summary>
        /// Gets or sets the WTT timetables
        /// </summary>
        [JsonProperty("timeTables")]
        public List<WTTTimeTable> TimeTables { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Inititialises a new WTT object from a SimSig WTT file
        /// </summary>
        /// <param name="Filename">Full path to the WTT file</param>
        /// <param name="Culture">The preferred culture of the user</param>
        public WTT (string Filename, string Culture = "en-GB")
        {
            //Set Culture
            this._Culture = new CultureInfo(string.IsNullOrEmpty(Culture) ? "en-GB" : Culture);
            this._StartDate = new DateTime(1850, 1, 1);
            //Validate Arguments
            ArgumentValidation.ValidateFilename(Filename, this._Culture);

            this._SourceWTTFileName = Filename;
            this.ReadWTTFile(); //Read the WTT
        }

        /// <summary>
        /// Inititialises a new WTT object from a SimSig WTT file
        /// </summary>
        /// <param name="Filename">Full path to the WTT file</param>
        /// <param name="StartDate">The start date of the timetable</param>
        /// <param name="Culture">The preferred culture of the user</param>
        public WTT(string Filename, DateTime StartDate, string Culture = "en-GB")
        {
            //Set Culture
            this._Culture = new CultureInfo(string.IsNullOrEmpty(Culture) ? "en-GB" : Culture);
            this._StartDate = StartDate;
            //Validate Arguments
            ArgumentValidation.ValidateFilename(Filename, this._Culture);

            this._SourceWTTFileName = Filename;
            this.ReadWTTFile(); //Read the WTT
        }

        /// <summary>
        /// Default constructor which is used the Json Deserializer constructor
        /// </summary>
        [JsonConstructor]
        private WTT ()
        {
        }

        /// <summary>
        /// Instantiates an empty WTT object
        /// </summary>
        /// <param name="SQLConnector"></param>
        /// <param name="Culture"></param>
        public WTT (string Culture = "en-GB")
        {
            //Set Culture
            this._Culture = new CultureInfo(string.IsNullOrEmpty(Culture) ? "en-GB" : Culture);
            //Validate Arguments
            //ArgumentValidation.ValidateSQLConnector(SQLConnector, this._Culture);

            //this._SQLConnector = SQLConnector;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Reads a SimSig WTT File and returns an XDocument represent the SavedTimeable.xml file inside the WTT file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>")]
        private void ReadWTTFile()
        {
            bool SaveTimeTableFound = false;

            if (!File.Exists(this._SourceWTTFileName))
            {
                throw new FileNotFoundException($"The WTT file {this._SourceWTTFileName} cannot be found.");
            }

            using (FileStream WTTFile = File.OpenRead(this._SourceWTTFileName))
            {
                using ZipArchive WTTZip = new ZipArchive(WTTFile, ZipArchiveMode.Read);
                foreach (ZipArchiveEntry WTTZipEntry in WTTZip.Entries)
                {
                    if (WTTZipEntry.Name.ToLower() == "savedtimetable.xml")
                    {
                        SaveTimeTableFound = true;
                        this._SourceWTTXML = ConvertStreamToXDocument(WTTZipEntry.Open());
                        this.ParseWTTXML(); //Parse the XML into the object
                    }
                }
            }

            //Throw FileLoadException if no SavedTimetable.xml found
            if (!SaveTimeTableFound)
            {
                throw new FileLoadException($"No SavedTimetable.xml file was found inside the SimSig WTT File {this._SourceWTTFileName}.");
            }
        }

        /// <summary>
        /// Converts a Stream to an XDocument
        /// </summary>
        /// <param name="WTTStream"></param>
        /// <returns></returns>
        private static XDocument ConvertStreamToXDocument(Stream WTTStream)
        {
            return XDocument.Load(WTTStream);
        }

        /// <summary>
        /// Parses a SimSig WTT XML file stored in _SourceWTTXML into the WTT object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        private void ParseWTTXML()
        {
            //Parse the WTT Attributes
            this.Simulation = (SimSigSimulation)Enum.Parse(typeof(SimSigSimulation), this._SourceWTTXML.Element("SimSigTimetable").Attribute("ID").Value.ToString(), true);
            this.SimulationVersion = this._SourceWTTXML.Element("SimSigTimetable").Attribute("Version").Value.ToString();
            //Get the Header
            this.Header = new WTTHeader(this._SourceWTTXML.Element("SimSigTimetable"), this._StartDate, this._Culture.Name);

            //Parse the train categories
            if (this._SourceWTTXML.Element("SimSigTimetable").Element("TrainCategories").Elements("TrainCategory") != null)
            {
                this.ParseWTTTrainCategoriesXML();
            }

            //Parse the timetables
            if (this._SourceWTTXML.Element("SimSigTimetable").Element("Timetables").Elements("Timetable") != null)
            {
                this.ParseWTTTimeTablesXML();
            }
        }

        /// <summary>
        /// Parses the WTT Timetables from the source XML
        /// </summary>
        private void ParseWTTTimeTablesXML()
        {
            this.TimeTables = new List<WTTTimeTable>();

            foreach (XElement XML in this._SourceWTTXML.Element("SimSigTimetable").Element("Timetables").Elements("Timetable"))
            {
                this.TimeTables.Add(new WTTTimeTable(XML, this._StartDate, "en-GB"));
            }
        }

        /// <summary>
        /// Parses the WTT Train Categories from the source XML
        /// </summary>
        private void ParseWTTTrainCategoriesXML()
        {
            this.TrainCategories = new List<WTTTrainCategory>();

            foreach (XElement XML in this._SourceWTTXML.Element("SimSigTimetable").Element("TrainCategories").Elements("TrainCategory"))
            {
                this.TrainCategories.Add(new WTTTrainCategory(XML, "en-GB"));
            }
        }

        /// <summary>
        /// Populates the object from the supplied JSON
        /// </summary>
        /// <param name="JSON">The JSON string representing the WTT object</param>
        private void PopulateFromJSON(string JSON)
        {
            JsonConvert.PopulateObject(JSON, this);
        }

        /// <summary>
        /// Serializes the WTT object to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion Methods
    }
}
