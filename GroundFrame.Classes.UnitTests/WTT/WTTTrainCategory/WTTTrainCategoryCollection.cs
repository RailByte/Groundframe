using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Classes.UnitTests.WTT.WTTTrainCategory
{
    public class WTTTrainCategoryCollection
    {
        /// <summary>
        /// Checks that a  WTTTrainCategoryCollection instantiated by JSON returns the correct WTTTrainCategoryCollection
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
