using GroundFrame.Classes.Timetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Classes.UnitTests.TimeTable.WTTTimeTable
{
    /// <summary>
    /// Class used to run unit tests aagainst the WTTCautionSpeedSetCollection TimeTable object
    /// </summary>
    public class WTTCautionSpeedSetCollection
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Tests instantiating a new WTTCautionSpeedSetCollection object from a SimSig XML snippet.
        /// </summary>
        [Fact]
        public void WTTCautionSpeedSetCollection_Constructor_XElement()
        {
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeedSetCollection = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets");
            GroundFrame.Classes.Timetables.WTTCautionSpeedSetCollection TestCautionSpeedSetCollection = new Timetables.WTTCautionSpeedSetCollection(XMLTestCautionSpeedSetCollection);

            Assert.Equal(XMLTestCautionSpeedSetCollection.Elements("CautionSpeedSet").Count(), TestCautionSpeedSetCollection.Count());
            }

        /// <summary>
        /// Tests instantiating a new WTTCautionSpeed object from a JSON string.
        /// </summary>
        [Fact]
        public void WTTCautionSpeedSet_Constructor_JSON()
        {
            //Set up test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeedSetCollection = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets");
            GroundFrame.Classes.Timetables.WTTCautionSpeedSetCollection TestCautionSpeedSetCollection = new Timetables.WTTCautionSpeedSetCollection(XMLTestCautionSpeedSetCollection);
            string JSON = TestCautionSpeedSetCollection.ToJSON();
            //Instantiate object from JSON
            GroundFrame.Classes.Timetables.WTTCautionSpeedSetCollection TestJSONCautionSpeedSetCollection = new Timetables.WTTCautionSpeedSetCollection(JSON);

            Assert.Equal(XMLTestCautionSpeedSetCollection.Elements("CautionSpeedSet").Count(), TestJSONCautionSpeedSetCollection.Count);
        }
        
        #endregion Methods
    }
}
