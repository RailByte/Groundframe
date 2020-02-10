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
    /// Class used to run unit tests aagainst the WTTActivity TimeTable object
    /// </summary>
    public class WTTActivity
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
        /// Tests instantiating a new WTTActivity object from a SimSig XML snippet.
        /// </summary>
        [Fact]
        public void WTTActivity_Constructor_XElement()
        {
            //Set Up Test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestActivity = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "2R74").FirstOrDefault().Element("Trips").Elements("Trip").Where(y => y.Element("Location").Value == "ROYSTON").FirstOrDefault().Element("Activities").Element("Activity");
            GroundFrame.Classes.Timetables.WTTActivity TestActivity  = new Timetables.WTTActivity(XMLTestActivity);

            //Run tests
            Assert.Equal((WTTActivityType)Convert.ToInt32(XMLTestActivity.Element("Activity").Value), TestActivity.ActivityType);
            Assert.Equal(XMLTestActivity.Element("AssociatedTrain") == null ? null : XMLTestActivity.Element("AssociatedTrain").Value, TestActivity.AssociatedTrainHeadCode);
            Assert.Equal(XMLTestActivity.Element("AssociatedUID") == null ? null : XMLTestActivity.Element("AssociatedUID").Value, TestActivity.AssociatedUID);
        }

        /// <summary>
        /// Tests instantiating a new WTTActivity object from a JSON String.
        /// </summary>
        [Fact]
        public void WTTActivity_Constructor_JSON()
        {
            //Set Up Test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestActivity = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "2R74").FirstOrDefault().Element("Trips").Elements("Trip").Where(y => y.Element("Location").Value == "ROYSTON").FirstOrDefault().Element("Activities").Element("Activity");
            GroundFrame.Classes.Timetables.WTTActivity TestActivity = new Timetables.WTTActivity(XMLTestActivity);
            string JSON = TestActivity.ToJSON();

            //Create a new object from the JSON
            GroundFrame.Classes.Timetables.WTTActivity TestJSONActivity = new Timetables.WTTActivity(JSON);

            //Run tests
            Assert.Equal((WTTActivityType)Convert.ToInt32(XMLTestActivity.Element("Activity").Value), TestJSONActivity.ActivityType);
            Assert.Equal(XMLTestActivity.Element("AssociatedTrain") == null ? null : XMLTestActivity.Element("AssociatedTrain").Value, TestJSONActivity.AssociatedTrainHeadCode);
            Assert.Equal(XMLTestActivity.Element("AssociatedUID") == null ? null : XMLTestActivity.Element("AssociatedUID").Value, TestActivity.AssociatedUID);
            Assert.Equal(JSON, TestJSONActivity.ToJSON());
        }

        #endregion Methods
    }
}
