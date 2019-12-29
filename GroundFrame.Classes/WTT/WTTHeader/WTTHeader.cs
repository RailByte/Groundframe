using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes
{
    public class WTTHeader
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _VersionBuild; //Stores the Build Version Number

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the WTT name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the WTT description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the WTT start time
        /// </summary>
        public Time StartTime { get; set; }

        /// <summary>
        /// Gets or sets the WTT finish time
        /// </summary>
        public Time FinishTime { get; set; }

        /// <summary>
        /// Gets or sets the WTT major version
        /// </summary>
        public int VersionMajor { get; set; }

        /// <summary>
        /// Gets or sets the WTT minor version
        /// </summary>
        public int VersionMinor { get; set; }

        /// <summary>
        /// Gets or sets the WTT version build number
        /// </summary>
        public int VersionBuild { get { return this._VersionBuild; } }

        /// <summary>
        /// Gets or sets the WTT Train Decsrption Template
        /// </summary>
        public string TrainDescriptionTemplate { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a WTTHeader object from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header">XElement containing the WTT XML defining this header object</param>
        public WTTHeader(XElement Header)
        {
            this.ParseHeaderXML(Header); //Parse the header XML
        }

        /// <summary>
        /// Instantiates a new WTTHeader object
        /// </summary>
        /// <param name="Header">The WTT Name</param>
        public WTTHeader(string Name)
        {
            this.Name = Name;
            this.StartTime = new Time(0);
            this.FinishTime = new Time(0);
            this.VersionMajor = 1;
            this.TrainDescriptionTemplate = "$originTime $originName-$destName $operator ($stock)";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses the WTTHeader from the SimSigTimeable element from a SimSig SavedTimetable.xml document
        /// </summary>
        /// <param name="Header"></param>
        private void ParseHeaderXML(XElement Header)
        {
            try
            {
                this.Name = XMLMethods.GetValueFromXElement<string>(Header, @"Name");
                this.Description = XMLMethods.GetValueFromXElement<string>(Header, @"Description", string.Empty);
                this.StartTime = new Time(XMLMethods.GetValueFromXElement<int>(Header, @"StartTime", 0));
                this.FinishTime = new Time(XMLMethods.GetValueFromXElement<int>(Header, @"FinishTime", 0));
                this.VersionMajor = XMLMethods.GetValueFromXElement<int>(Header, @"VMajor", 1);
                this.VersionMinor = XMLMethods.GetValueFromXElement<int>(Header, @"VMinor", 0);
                this._VersionBuild = XMLMethods.GetValueFromXElement<int>(Header, @"VBuild", 0);
                this.TrainDescriptionTemplate = XMLMethods.GetValueFromXElement<string>(Header, @"TrainDescriptionTemplate", @"$originTime $originName-$destName $operator ($stock)");
            }
            catch (Exception Ex)
            {
                throw new Exception("Cannot Parse the WTT Header from the Source SimSig WTT XML", Ex);
            }
        }

        #endregion Methods
    }
}
