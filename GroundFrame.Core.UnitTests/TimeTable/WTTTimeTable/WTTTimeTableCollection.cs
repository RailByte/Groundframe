using GroundFrame.Core.Timetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Core.UnitTests.TimeTable.WTTTimeTableCollection
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
            GroundFrame.Core.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Core.Timetables.WTTTimeTableCollection(TestXML, new DateTime(2018,7,1));
            Assert.Equal(205, TestTimeTableCollection.Count);
        }

        /// <summary>
        /// Checks that the LocationMapper returns the correct number of locations
        /// </summary>
        [Fact]
        public void WTTTimeTableCollection_Method_GetMapperLocations()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables");
            GroundFrame.Core.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Core.Timetables.WTTTimeTableCollection(TestXML, new DateTime(2018, 7, 1));
           
            List<MapperLocation> LocationMapper = TestTimeTableCollection.GetMapperLocations();
            Assert.Equal(7, LocationMapper.Count);
        }

        /// <summary>
        /// Checks that the LocationMapper returns the correct number of location nodes
        /// </summary>
        [Fact]
        public void WTTTimeTableCollection_Method_GetMapperLocationNodes()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables");
            GroundFrame.Core.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Core.Timetables.WTTTimeTableCollection(TestXML, new DateTime(2018, 7, 1));

            List<MapperLocationNode> LocationMapperNodes = TestTimeTableCollection.GetMapperLocationNodes().OrderBy(x => x.SimSigCode).ToList();
            Assert.Equal(24, LocationMapperNodes.Count);
        }

        /// <summary>
        /// Checks that the GetByHeadCode method within WTTTrainCategoryCollection returns the correct WTTTrainCategory
        /// </summary>
        [Fact]
        public void WTTTimeTableCollection_Method_GetByHeadCode()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement XMLTestTimeTables = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables");
            XElement XMLTestTimeTable = XMLTestTimeTables.Elements("Timetable").Where(x => x.Element("ID").Value.ToString() == "1R09").FirstOrDefault();
            GroundFrame.Core.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Core.Timetables.WTTTimeTableCollection(XMLTestTimeTables, new DateTime(2018, 7, 1));
            GroundFrame.Core.Timetables.WTTTimeTable TestTimeTable = TestTimeTableCollection.GetByHeadCode("1R09").First();
            Assert.Equal(XMLTestTimeTable.Element("ID").Value.ToString(), TestTimeTable.Headcode);
            Assert.Equal((WTTAccelBrakeIndex)Convert.ToInt32(XMLTestTimeTable.Element("AccelBrakeIndex").Value), TestTimeTable.AccelBrakeIndex);
            Assert.Equal(Convert.ToInt32(XMLTestTimeTable.Element("AsRequiredPercent").Value.ToString()), TestTimeTable.RunAsRequiredPercentage);

            if (XMLTestTimeTable.Element("DepartTime") != null)
            {
                Assert.Equal(new GroundFrame.Core.Timetables.WTTTime(Convert.ToInt32(XMLTestTimeTable.Element("DepartTime").Value.ToString())).Seconds, TestTimeTable.DepartTime.Seconds);
            }
            else
            {
                Assert.Null(TestTimeTable.DepartTime);
            }
            Assert.Equal(XMLTestTimeTable.Element("Description").Value.ToString(), TestTimeTable.Description);
            Assert.Equal(new GroundFrame.Core.Length(Convert.ToInt32(XMLTestTimeTable.Element("SeedingGap").Value.ToString())).Meters, TestTimeTable.SeedingGap.Meters);
            
            if (XMLTestTimeTable.Element("EntryPoint") != null)
            {
                Assert.Equal(XMLTestTimeTable.Element("EntryPoint").Value.ToString(), TestTimeTable.EntryPoint);
            }
            else
            {
                Assert.Null(TestTimeTable.EntryPoint);
            }
            
            Assert.Equal(new GroundFrame.Core.Timetables.WTTSpeed(Convert.ToInt32(XMLTestTimeTable.Element("MaxSpeed").Value.ToString())).MPH, TestTimeTable.MaxSpeed.MPH);
            Assert.Equal(new GroundFrame.Core.Timetables.WTTSpeedClass(Convert.ToInt32(XMLTestTimeTable.Element("SpeedClass").Value.ToString())).Bitwise, TestTimeTable.SpeedClass.Bitwise);
            Assert.Equal(new GroundFrame.Core.Length(Convert.ToInt32(XMLTestTimeTable.Element("TrainLength").Value.ToString())).Meters, TestTimeTable.TrainLength.Meters);
            Assert.Equal(new GroundFrame.Core.Electrification(XMLTestTimeTable.Element("Electrification").Value.ToString()).ToString(), TestTimeTable.Electrification.ToString());
            Assert.Equal(new GroundFrame.Core.Electrification(XMLTestTimeTable.Element("StartTraction").Value.ToString()).ToString(), TestTimeTable.StartTraction.ToString());
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
            GroundFrame.Core.Timetables.WTTTimeTableCollection TestTimeTableCollection = new GroundFrame.Core.Timetables.WTTTimeTableCollection(TestXML, new DateTime(2018, 7, 1));
            Assert.Equal(205, TestTimeTableCollection.Count);

            //Convert header to JSON
            string JSONTimeTableCollection = TestTimeTableCollection.ToJSON();
            //Deserialize the JSON string back to an WTTHeader object
            GroundFrame.Core.Timetables.WTTTimeTableCollection JSONWTTTTimeTableCollection = new Timetables.WTTTimeTableCollection(JSONTimeTableCollection);
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
            Assert.False(typeof(GroundFrame.Core.Timetables.WTTTimeTableCollection).GetProperty("StartDate").CanWrite);
        }
    }
}
