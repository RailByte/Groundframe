﻿using GroundFrame.Classes.Timetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Classes.UnitTests.WTT.WTTTimeTable
{
    public class WTTTimeTable
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
        /// Check instantiating a new WTTTimeTable object from a SimSig XML snippet.
        /// </summary>
        [Fact]
        public void WTTTimeTable_Prop_XElement()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTimeTable = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Descendants().First();
            GroundFrame.Classes.Timetables.WTTTimeTable TestTimeTable = new Classes.Timetables.WTTTimeTable(XMLTestTimeTable, new DateTime(2018,7,1), new UserSettingCollection());
            Assert.Equal(XMLTestTimeTable.Element("ID").Value.ToString(), TestTimeTable.Headcode);
            Assert.Equal((WTTAccelBrakeIndex)Convert.ToInt32(XMLTestTimeTable.Element("AccelBrakeIndex").Value), TestTimeTable.AccelBrakeIndex);
            Assert.Equal(Convert.ToInt32(XMLTestTimeTable.Element("AsRequiredPercent").Value.ToString()), TestTimeTable.RunAsRequiredPercentage);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTTime(Convert.ToInt32(XMLTestTimeTable.Element("DepartTime").Value.ToString()), new UserSettingCollection()).Seconds, TestTimeTable.DepartTime.Seconds);
            Assert.Equal(XMLTestTimeTable.Element("Description").Value.ToString(), TestTimeTable.Description);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTDuration(Convert.ToInt32(XMLTestTimeTable.Element("SeedingGap").Value.ToString()), new UserSettingCollection()).Seconds, TestTimeTable.SeedingGap.Seconds);
            Assert.Equal(XMLTestTimeTable.Element("EntryPoint").Value.ToString(), TestTimeTable.EntryPoint);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeed(Convert.ToInt32(XMLTestTimeTable.Element("MaxSpeed").Value.ToString()), new UserSettingCollection()).MPH, TestTimeTable.MaxSpeed.MPH);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeedClass(Convert.ToInt32(XMLTestTimeTable.Element("SpeedClass").Value.ToString()), new UserSettingCollection()).Bitwise, TestTimeTable.SpeedClass.Bitwise);
            Assert.Equal(new GroundFrame.Classes.Length(Convert.ToInt32(XMLTestTimeTable.Element("TrainLength").Value.ToString())).Meters, TestTimeTable.TrainLength.Meters);
            Assert.Equal(new GroundFrame.Classes.Electrification(XMLTestTimeTable.Element("Electrification").Value.ToString()).ToString(), TestTimeTable.Electrification.ToString());
            Assert.Equal(new GroundFrame.Classes.Electrification(XMLTestTimeTable.Element("StartTraction").Value.ToString()).ToString(), TestTimeTable.StartTraction.ToString());
            Assert.Equal(XMLTestTimeTable.Element("Category").Value.ToString(), TestTimeTable.SimSigTrainCategoryID);
            Assert.Equal(XMLTestTimeTable.Element("Trips").Elements("Trip").Count(), TestTimeTable.Trip.Count());

        }

        /// <summary>
        /// Checks passing NULL to the TrainTableXML argument throws a ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTimeTable_Prop_NullXMLException()
        {
            Assert.Throws<ArgumentNullException>(() => new GroundFrame.Classes.Timetables.WTTTimeTable(null, new DateTime(2018,7,1), new UserSettingCollection()));
        }

        /// <summary>
        /// Checks passing an invalid to the DateStart argument throws an out of range exception
        /// </summary>
        [Fact]
        public void WTTTimeTable_Prop_InValidStartDateException()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTimeTable = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Descendants().First();
            Assert.Throws<ArgumentOutOfRangeException>(() => new GroundFrame.Classes.Timetables.WTTTimeTable(XMLTestTimeTable, new DateTime(1750, 1, 1), new UserSettingCollection()));
        }

        /// <summary>
        /// Check instantiating a new WTTTimeTable object from a JSON string
        /// </summary>
        [Fact]
        public void WTTTimeTable_Prop_JSON()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTimeTable = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Descendants().First();
            GroundFrame.Classes.Timetables.WTTTimeTable TestTimeTable = new Classes.Timetables.WTTTimeTable(XMLTestTimeTable, new DateTime(2018, 7, 1), new UserSettingCollection());
            //Create JSON
            string TestJSON = TestTimeTable.ToJSON();
            //Create new ojbect from JSON
            GroundFrame.Classes.Timetables.WTTTimeTable TestJSONTimeTable = new Timetables.WTTTimeTable(TestJSON, new UserSettingCollection());
            Assert.Equal(TestTimeTable.ToString(), TestJSONTimeTable.ToString());
            Assert.Equal(TestTimeTable.StartDate, TestJSONTimeTable.StartDate);
        }

        /// <summary>
        /// Checks passing NULL to the JSON argument throws a ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTimeTable_Constructor_NullJSONException()
        {
            string JSON = null;
            Assert.Throws<ArgumentNullException>(() => new GroundFrame.Classes.Timetables.WTTTimeTable(JSON, new UserSettingCollection()));
        }

        /// <summary>
        /// Checks passing invalid JSON to the JSON argument throws a FormatException
        /// </summary>
        [Fact]
        public void WTTTimeTable_Constructor_InvalidJSONException()
        {
            string JSON = "Invalid JSON";
            Assert.Throws<FormatException>(() => new GroundFrame.Classes.Timetables.WTTTimeTable(JSON, new UserSettingCollection()));
        }

        #endregion Methods
    }
}
