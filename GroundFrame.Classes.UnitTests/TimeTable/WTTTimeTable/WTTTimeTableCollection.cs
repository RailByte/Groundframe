using GroundFrame.Classes.Timetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Classes.UnitTests.WTT.WTTTimeTableCollection
{
    public class WTTTimeTableCollection
    {
        /// <summary>
        /// Checks that a WTTTimeTableCollection instantiated by an XElement
        /// </summary>
        [Fact]
        public void WTTTimeTableCollection_Constuctor_XElement()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables");
            GroundFrame.Classes.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Classes.Timetables.WTTTimeTableCollection(TestXML, new DateTime(2018,7,1), new UserSettingCollection());
            Assert.Equal(205, TestTimeTableCollection.Count);
        }

        /// <summary>
        /// Checks that the IndexOf methiod within WTTTrainCategoryCollection returns the correct WTTTrainCategory
        /// </summary>
        [Fact]
        public void WTTTimeTableCollection_Method_IndexOf()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement XMLTestTimeTables = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables");
            XElement XMLTestTimeTable = XMLTestTimeTables.Elements("Timetable").First();
            GroundFrame.Classes.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Classes.Timetables.WTTTimeTableCollection(XMLTestTimeTables, new DateTime(2018, 7, 1), new UserSettingCollection());
            GroundFrame.Classes.Timetables.WTTTimeTable TestTimeTable = TestTimeTableCollection.IndexOf(0);
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
        /// Checks that a WTTTrainCategoryCollection instantiated by JSON returns the correct WTTTrainCategoryCollection
        /// </summary>
        [Fact]
        public void WTTTimeTableCollection_Method_ToJSON()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables");
            GroundFrame.Classes.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Classes.Timetables.WTTTimeTableCollection(TestXML, new DateTime(2018, 7, 1), new UserSettingCollection());
            Assert.Equal(205, TestTimeTableCollection.Count);

            //Convert header to JSON
            string JSONTimeTableCollection = TestTimeTableCollection.ToJSON();
            //Deserialize the JSON string back to an WTTHeader object
            GroundFrame.Classes.Timetables.WTTTimeTableCollection JSONWTTTTimeTableCollection = new Timetables.WTTTimeTableCollection(JSONTimeTableCollection, new UserSettingCollection());
            Assert.Equal(205, JSONWTTTTimeTableCollection.Count);
            //Check both WTTHeader objects are equal
            Assert.Equal(TestTimeTableCollection.ToString(), JSONWTTTTimeTableCollection.ToString());
            Assert.Equal(TestTimeTableCollection.StartDate, JSONWTTTTimeTableCollection.StartDate);
            Assert.Equal(TestTimeTableCollection.Count, JSONWTTTTimeTableCollection.Count);
        }

        /// <summary>
        /// Check the Start Date property is read only
        /// </summary>
        [Fact]
        public void WTTTimeTableCollection_Property_StartDate()
        {
            Assert.False(typeof(GroundFrame.Classes.Timetables.WTTTimeTableCollection).GetProperty("StartDate").CanWrite);
        }
    }
}
