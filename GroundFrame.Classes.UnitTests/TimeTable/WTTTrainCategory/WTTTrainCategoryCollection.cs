using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Classes.UnitTests.WTT.WTTTrainCategoryCollection
{
    public class WTTTrainCategoryCollection
    {
        /// <summary>
        /// Checks that a WTTTrainCategoryCollection instantiated by an XElement
        /// </summary>
        [Fact]
        public void WTTTrainCategoryCollection_Constuctor_XElement()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("TrainCategories");
            GroundFrame.Classes.Timetables.WTTTrainCategoryCollection TestTrainCollection = new GroundFrame.Classes.Timetables.WTTTrainCategoryCollection(TestXML, new UserSettingCollection());
            Assert.Equal(3, TestTrainCollection.Count);
        }

        /// <summary>
        /// Checks that the IndexOf methiod within WTTTrainCategoryCollection returns the correct WTTTrainCategory
        /// </summary>
        [Fact]
        public void WTTTrainCategoryCollection_Method_IndexOf()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("TrainCategories");
            GroundFrame.Classes.Timetables.WTTTrainCategoryCollection TestTrainCollection = new GroundFrame.Classes.Timetables.WTTTrainCategoryCollection(TestXML, new UserSettingCollection());
            GroundFrame.Classes.Timetables.WTTTrainCategory TestTrainCategory = TestTrainCollection.IndexOf(0);
            Assert.Equal(TestXML.Elements("TrainCategory").First().Attribute("ID").Value.ToString(), TestTrainCategory.SimSigID);
            Assert.Equal(TestXML.Elements("TrainCategory").First().Element("Description").Value.ToString(), TestTrainCategory.Description);
            Assert.Equal((GroundFrame.Classes.Timetables.WTTAccelBrakeIndex)Convert.ToInt32(TestXML.Elements("TrainCategory").First().Element("AccelBrakeIndex").Value.ToString()), TestTrainCategory.AccelBrakeIndex);
            Assert.Equal(Convert.ToBoolean(Convert.ToInt32(TestXML.Elements("TrainCategory").First().Element("IsFreight").Value.ToString())), TestTrainCategory.IsFreight);
            Assert.Equal(Convert.ToBoolean(Convert.ToInt32(TestXML.Elements("TrainCategory").First().Element("CanUseGoodsLines").Value.ToString())), TestTrainCategory.CanUseGoodsLines);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeed(Convert.ToInt32(TestXML.Elements("TrainCategory").First().Element("MaxSpeed").Value.ToString()), new UserSettingCollection()).MPH, TestTrainCategory.MaxSpeed.MPH);
            Assert.Equal(new GroundFrame.Classes.Length(Convert.ToInt32(TestXML.Elements("TrainCategory").First().Element("TrainLength").Value.ToString())).Meters, TestTrainCategory.TrainLength.Meters);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeedClass(Convert.ToInt32(TestXML.Elements("TrainCategory").First().Element("SpeedClass").Value.ToString()), new UserSettingCollection()).Bitwise, TestTrainCategory.SpeedClass.Bitwise);
            Assert.Equal((GroundFrame.Classes.Timetables.WTTPowerToWeightCategory)Convert.ToInt32(TestXML.Elements("TrainCategory").First().Element("PowerToWeightCategory").Value.ToString()), TestTrainCategory.PowerToWeightCategory);
            Assert.Equal(new GroundFrame.Classes.Electrification(TestXML.Elements("TrainCategory").First().Element("Electrification").Value.ToString()).ToString(), TestTrainCategory.Electrification.ToString());
        }

        /// <summary>
        /// Checks that a WTTTrainCategoryCollection instantiated by JSON returns the correct WTTTrainCategoryCollection
        /// </summary>
        [Fact]
        public void WTTTrainCategoryCollection_Method_ToJSON()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("TrainCategories");
            GroundFrame.Classes.Timetables.WTTTrainCategoryCollection TestTrainCollection= new GroundFrame.Classes.Timetables.WTTTrainCategoryCollection(TestXML, new UserSettingCollection());

            //Convert header to JSON
            string JSONTrainCollection = TestTrainCollection.ToJSON();
            //Deserialize the JSON string back to an WTTHeader object
            GroundFrame.Classes.Timetables.WTTTrainCategoryCollection JSONWTTrainCollection = new Timetables.WTTTrainCategoryCollection(JSONTrainCollection, new UserSettingCollection());
            //Check both WTTHeader objects are equal
            Assert.Equal(TestTrainCollection.ToString(), JSONWTTrainCollection.ToString());
        }
    }
}
