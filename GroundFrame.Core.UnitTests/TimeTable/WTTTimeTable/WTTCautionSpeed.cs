using GroundFrame.Core.Timetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Core.UnitTests.TimeTable.WTTTimeTable
{
    /// <summary>
    /// Class used to run unit tests aagainst the WTTCautionSpeed TimeTable object
    /// </summary>
    public class WTTCautionSpeed
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
        public void WTTCautionSpeed_Constructor_XElement()
        {
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeed = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets").Element("CautionSpeedSet").Element("CautionSpeeds").Element("CautionSpeed");
            GroundFrame.Core.Timetables.WTTCautionSpeed TestCautionSpeed = new Timetables.WTTCautionSpeed(XMLTestCautionSpeed);

            //Run tests
            Assert.Equal(XMLTestCautionSpeed.Element("AspectPassed") == null ? WTTSignalAspect.Red : (WTTSignalAspect)Convert.ToInt32(XMLTestCautionSpeed.Element("AspectPassed").Value), TestCautionSpeed.AspectPassed);

            if (XMLTestCautionSpeed.Element("FromLineSpeed") == null)
            {
                Assert.Null(TestCautionSpeed.FromLineSpeed);
            }
            else
            {
                Assert.Equal(Convert.ToInt32(XMLTestCautionSpeed.Element("FromLineSpeed").Value), TestCautionSpeed.FromLineSpeed.MPH);
            }

            Assert.Equal(XMLTestCautionSpeed.Element("ReduceNowValue") == null ? 0 : Convert.ToInt32(XMLTestCautionSpeed.Element("ReduceNowValue").Value), TestCautionSpeed.ReduceNowValue);
            Assert.Equal(XMLTestCautionSpeed.Element("approachNextValue") == null ? 0 : Convert.ToInt32(XMLTestCautionSpeed.Element("approachNextValue").Value), TestCautionSpeed.ApproachNextValue);

            if (XMLTestCautionSpeed.Element("approachNextDistance") == null)
            {
                Assert.Null(TestCautionSpeed.ApproachNextDistance);
            }
            else
            {
                Assert.Equal(new Length(Convert.ToInt32(XMLTestCautionSpeed.Element("approachNextDistance").Value)).Meters, TestCautionSpeed.ApproachNextDistance.Meters);
            }

            Assert.Equal(XMLTestCautionSpeed.Element("NowValueType") == null ? WTTNumberType.NotApplicable : (WTTNumberType)Convert.ToInt32(XMLTestCautionSpeed.Element("NowValueType").Value), TestCautionSpeed.NowValueType);
            Assert.Equal(XMLTestCautionSpeed.Element("NextValueType") == null ? WTTNumberType.NotApplicable : (WTTNumberType)Convert.ToInt32(XMLTestCautionSpeed.Element("NextValueType").Value), TestCautionSpeed.NextValueType);
        }

        /// <summary>
        /// Tests instantiating a new WTTCautionSpeed object from a JSON stting
        /// </summary>
        [Fact]
        public void WTTCautionSpeed_Constructor_JSON()
        {
            //Set up test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeed = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets").Element("CautionSpeedSet").Element("CautionSpeeds").Element("CautionSpeed");
            GroundFrame.Core.Timetables.WTTCautionSpeed TestCautionSpeed = new Timetables.WTTCautionSpeed(XMLTestCautionSpeed);
            string JSON = TestCautionSpeed.ToJSON();
            //Instantiate Caution Speed from JSON
            GroundFrame.Core.Timetables.WTTCautionSpeed TestJSONCautionSpeed = new Timetables.WTTCautionSpeed(JSON);

            //Run tests
            Assert.Equal(XMLTestCautionSpeed.Element("AspectPassed") == null ? WTTSignalAspect.Red : (WTTSignalAspect)Convert.ToInt32(XMLTestCautionSpeed.Element("AspectPassed").Value), TestJSONCautionSpeed.AspectPassed);

            if (XMLTestCautionSpeed.Element("FromLineSpeed") == null)
            {
                Assert.Null(TestJSONCautionSpeed.FromLineSpeed);
            }
            else
            {
                Assert.Equal(Convert.ToInt32(XMLTestCautionSpeed.Element("FromLineSpeed").Value), TestJSONCautionSpeed.FromLineSpeed.MPH);
            }

            Assert.Equal(XMLTestCautionSpeed.Element("ReduceNowValue") == null ? 0 : Convert.ToInt32(XMLTestCautionSpeed.Element("ReduceNowValue").Value), TestJSONCautionSpeed.ReduceNowValue);
            Assert.Equal(XMLTestCautionSpeed.Element("approachNextValue") == null ? 0 : Convert.ToInt32(XMLTestCautionSpeed.Element("approachNextValue").Value), TestJSONCautionSpeed.ApproachNextValue);

            if (XMLTestCautionSpeed.Element("approachNextDistance") == null)
            {
                Assert.Null(TestJSONCautionSpeed.ApproachNextDistance);
            }
            else
            {
                Assert.Equal(new Length(Convert.ToInt32(XMLTestCautionSpeed.Element("approachNextDistance").Value)).Meters, TestJSONCautionSpeed.ApproachNextDistance.Meters);
            }

            Assert.Equal(XMLTestCautionSpeed.Element("NowValueType") == null ? WTTNumberType.NotApplicable : (WTTNumberType)Convert.ToInt32(XMLTestCautionSpeed.Element("NowValueType").Value), TestJSONCautionSpeed.NowValueType);
            Assert.Equal(XMLTestCautionSpeed.Element("NextValueType") == null ? WTTNumberType.NotApplicable : (WTTNumberType)Convert.ToInt32(XMLTestCautionSpeed.Element("NextValueType").Value), TestJSONCautionSpeed.NextValueType);
        }

        #endregion Methods
    }
}
