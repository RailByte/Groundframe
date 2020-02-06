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
        public void WTTTimeTable_Constructor_XElement()
        {
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestThreeBridges_4.8.xml";
            XElement XMLTestCautionSpeedSet = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("CautionSpeedSets").Element("CautionSpeedSet").Element("CautionSpeeds").Element("CautionSpeed");
            GroundFrame.Classes.Timetables.WTTCautionSpeed TestCautionSpeed = new Timetables.WTTCautionSpeed(XMLTestCautionSpeedSet);

            //Run tests
            Assert.Equal(XMLTestCautionSpeedSet.Element("AspectPassed") == null ? WTTSignalAspect.Red : (WTTSignalAspect)Convert.ToInt32(XMLTestCautionSpeedSet.Element("AspectPassed").Value), TestCautionSpeed.AspectPassed);

            if (XMLTestCautionSpeedSet.Element("FromLineSpeed") == null)
            {
                Assert.Null(TestCautionSpeed.FromLineSpeed);
            }
            else
            {
                Assert.Equal(Convert.ToInt32(XMLTestCautionSpeedSet.Element("FromLineSpeed").Value), TestCautionSpeed.FromLineSpeed.MPH);
            }

            Assert.Equal(XMLTestCautionSpeedSet.Element("ReduceNowValue") == null ? 0 : Convert.ToInt32(XMLTestCautionSpeedSet.Element("ReduceNowValue").Value), TestCautionSpeed.ReduceNowValue);
            Assert.Equal(XMLTestCautionSpeedSet.Element("approachNextValue") == null ? 0 : Convert.ToInt32(XMLTestCautionSpeedSet.Element("approachNextValue").Value), TestCautionSpeed.ApproachNextValue);

            if (XMLTestCautionSpeedSet.Element("approachNextDistance") == null)
            {
                Assert.Null(TestCautionSpeed.ApproachNextDistance);
            }
            else
            {
                Assert.Equal(new Length(Convert.ToInt32(XMLTestCautionSpeedSet.Element("approachNextDistance").Value)).Meters, TestCautionSpeed.ApproachNextDistance.Meters);
            }

            Assert.Equal(XMLTestCautionSpeedSet.Element("NowValueType") == null ? WTTNumberType.NotApplicable : (WTTNumberType)Convert.ToInt32(XMLTestCautionSpeedSet.Element("NowValueType").Value), TestCautionSpeed.NowValueType);
            Assert.Equal(XMLTestCautionSpeedSet.Element("NextValueType") == null ? WTTNumberType.NotApplicable : (WTTNumberType)Convert.ToInt32(XMLTestCautionSpeedSet.Element("NextValueType").Value), TestCautionSpeed.NextValueType);
        }

        #endregion Methods
    }
}
