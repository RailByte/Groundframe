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
    /// Class used to run unit tests aagainst the WTTActivityCollection TimeTable object
    /// </summary>
    public class WTTActivityCollection
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
        /// Tests instantiating a new WTTActivityCollection object from a SimSig XML snippet.
        /// </summary>
        [Fact]
        public void WTTActivityCollection_Constructor_XElement()
        {
            //Set Up Test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestActivityCollection = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "2R74").FirstOrDefault().Element("Trips").Elements("Trip").Where(y => y.Element("Location").Value == "ROYSTON").FirstOrDefault().Element("Activities");
            GroundFrame.Core.Timetables.WTTActivityCollection TestActivityCollection  = new Timetables.WTTActivityCollection(XMLTestActivityCollection);

            //Run tests
            Assert.Equal(XMLTestActivityCollection.Elements("Activity").Count(), TestActivityCollection.Count());

            if (TestActivityCollection.Count() > 0)
            {
                for (int i = 0; i < XMLTestActivityCollection.Elements("Activity").Count(); i++)
                {
                    Assert.Equal((WTTActivityType)Convert.ToInt32(XMLTestActivityCollection.Elements("Activity").ElementAt(i).Element("Activity").Value), TestActivityCollection.IndexOf(i).ActivityType);
                    Assert.Equal(XMLTestActivityCollection.Elements("Activity").ElementAt(i).Element("AssociatedTrain") == null ? null : XMLTestActivityCollection.Element("Activity").Element("AssociatedTrain").Value, TestActivityCollection.IndexOf(i).AssociatedTrainHeadCode);
                }
            }
        }

        /// <summary>
        /// Tests instantiating a new WTTActivityCollection object from a JSON String.
        /// </summary>
        [Fact]
        public void WTTActivityCollection_Constructor_JSON()
        {
            //Set Up Test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestActivityCollection = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "2R74").FirstOrDefault().Element("Trips").Elements("Trip").Where(y => y.Element("Location").Value == "ROYSTON").FirstOrDefault().Element("Activities");
            GroundFrame.Core.Timetables.WTTActivityCollection TestActivityCollection = new Timetables.WTTActivityCollection(XMLTestActivityCollection);
            string JSON = TestActivityCollection.ToJSON();

            //Create a new object from the JSON
            GroundFrame.Core.Timetables.WTTActivityCollection TestJSONActivityCollection = new Timetables.WTTActivityCollection(JSON);

            //Run tests
            Assert.Equal(XMLTestActivityCollection.Elements("Activity").Count(), TestActivityCollection.Count());

            if (TestActivityCollection.Count() > 0)
            {
                for (int i = 0; i < XMLTestActivityCollection.Elements("Activity").Count(); i++)
                {
                    Assert.Equal((WTTActivityType)Convert.ToInt32(XMLTestActivityCollection.Elements("Activity").ElementAt(i).Element("Activity").Value), TestJSONActivityCollection.IndexOf(i).ActivityType);
                    Assert.Equal(XMLTestActivityCollection.Elements("Activity").ElementAt(i).Element("AssociatedTrain") == null ? null : XMLTestActivityCollection.Element("Activity").Element("AssociatedTrain").Value, TestJSONActivityCollection.IndexOf(i).AssociatedTrainHeadCode);
                }
            }
            
            Assert.Equal(JSON, TestActivityCollection.ToJSON());
        }
        /// <summary>
        /// Tests the WTTActivityCollection.ToList() method
        /// </summary>
        [Fact]
        public void WTTActivityCollection_Method_ToList()
        {
            //Set Up Test
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestActivityCollection = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "2R74").FirstOrDefault().Element("Trips").Elements("Trip").Where(y => y.Element("Location").Value == "ROYSTON").FirstOrDefault().Element("Activities");
            GroundFrame.Core.Timetables.WTTActivityCollection TestActivityCollection = new Timetables.WTTActivityCollection(XMLTestActivityCollection);

            //Run tests
            Assert.Equal(XMLTestActivityCollection.Elements("Activity").Count(), TestActivityCollection.ToList().Count);
        }

        #endregion Methods
    }
}
