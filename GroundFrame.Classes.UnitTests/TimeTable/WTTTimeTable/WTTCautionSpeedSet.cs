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
    /// Class used to run unit tests aagainst the WTTCautionSpeedSet TimeTable object
    /// </summary>
    public class WTTCautionSpeedSet
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
        /// Tests instantiating a new WTTCautionSpeed object from a SimSig XML snippet.
        /// </summary>
        [Fact]
        public void WTTCautionSpeedSet_Constructor_XElement()
        {
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeedSet = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets").Element("CautionSpeedSet");
            GroundFrame.Classes.Timetables.WTTCautionSpeedSet TestCautionSpeedSet = new Timetables.WTTCautionSpeedSet(XMLTestCautionSpeedSet);

            Assert.Equal(XMLTestCautionSpeedSet.Attribute("ID").Value, TestCautionSpeedSet.SimSigID);
            Assert.Equal(XMLTestCautionSpeedSet.Element("Name").Value, TestCautionSpeedSet.Name);
            Assert.Equal(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").Count(), TestCautionSpeedSet.CautionSpeeds.Count);
            Assert.Equal((WTTSignalAspect)Convert.ToInt32(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").ElementAt(0).Element("AspectPassed").Value), TestCautionSpeedSet.CautionSpeeds[0].AspectPassed);
        }

        /// <summary>
        /// Tests instantiating a new WTTCautionSpeed object from a JSON string.
        /// </summary>
        [Fact]
        public void WTTCautionSpeedSet_Constructor_JSON()
        {
            //Set up test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeedSet = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets").Element("CautionSpeedSet");
            GroundFrame.Classes.Timetables.WTTCautionSpeedSet TestCautionSpeedSet = new Timetables.WTTCautionSpeedSet(XMLTestCautionSpeedSet);
            string JSON = TestCautionSpeedSet.ToJSON();
            //Instantiate object from JSON
            GroundFrame.Classes.Timetables.WTTCautionSpeedSet TestJSONCautionSpeedSet = new Timetables.WTTCautionSpeedSet(JSON);

            Assert.Equal(XMLTestCautionSpeedSet.Attribute("ID").Value, TestJSONCautionSpeedSet.SimSigID);
            Assert.Equal(XMLTestCautionSpeedSet.Element("Name").Value, TestJSONCautionSpeedSet.Name);
            Assert.Equal(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").Count(), TestJSONCautionSpeedSet.CautionSpeeds.Count);
            Assert.Equal((WTTSignalAspect)Convert.ToInt32(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").ElementAt(0).Element("AspectPassed").Value), TestJSONCautionSpeedSet.CautionSpeeds[0].AspectPassed);
        }

        /// <summary>
        /// Tests the WTTCautionSpeed RemoveAt method
        /// </summary>
        [Fact]
        public void WTTCautionSpeedSet_Method_RemoveAt()
        {
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeedSet = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets").Element("CautionSpeedSet");
            GroundFrame.Classes.Timetables.WTTCautionSpeedSet TestCautionSpeedSet = new Timetables.WTTCautionSpeedSet(XMLTestCautionSpeedSet);

            //Test Before
            Assert.Equal(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").Count(), TestCautionSpeedSet.CautionSpeeds.Count);
            //Remove 1 
            TestCautionSpeedSet.RemoveAt(0);
            //Test After - Should be 1 Less
            Assert.Equal(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").Count()-1, TestCautionSpeedSet.CautionSpeeds.Count);
        }

        /// <summary>
        /// Tests the WTTCautionSpeed Add method
        /// </summary>
        [Fact]
        public void WTTCautionSpeedSet_Method_Add()
        {
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeedSet = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets").Element("CautionSpeedSet");
            GroundFrame.Classes.Timetables.WTTCautionSpeedSet TestCautionSpeedSet = new Timetables.WTTCautionSpeedSet(XMLTestCautionSpeedSet);

            //Test Before
            Assert.Equal(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").Count(), TestCautionSpeedSet.CautionSpeeds.Count);

            Timetables.WTTCautionSpeed RemovedCautionSpeed = TestCautionSpeedSet.CautionSpeeds[0];

            //Remove 1 
            TestCautionSpeedSet.RemoveAt(0);
            //Test After - Should be 1 Less
            Assert.Equal(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").Count() - 1, TestCautionSpeedSet.CautionSpeeds.Count);

            //Add Back
            TestCautionSpeedSet.CautionSpeeds.Add(RemovedCautionSpeed);
            //Test After - Should be same as begining
            Assert.Equal(XMLTestCautionSpeedSet.Elements("CautionSpeeds").Elements("CautionSpeed").Count(), TestCautionSpeedSet.CautionSpeeds.Count);
        }

        #endregion Methods
    }
}
