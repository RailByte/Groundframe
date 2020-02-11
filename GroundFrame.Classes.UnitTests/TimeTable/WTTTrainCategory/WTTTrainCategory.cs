using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;
using GroundFrame.Classes.Timetables;

namespace GroundFrame.Classes.UnitTests.TimeTable.WTTTrainCategory
{
    /// <summary>
    /// Object representing a WTT Train Category
    /// </summary>
    public class WTTTrainCategory
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
        /// Check instantiating a new WTTTrainCategory object from a SimSig XML snippet.
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Constructor_XElement()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTrainCategory = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("TrainCategories").Descendants().First();
            GroundFrame.Classes.Timetables.WTTTrainCategory TestCategory = new Classes.Timetables.WTTTrainCategory(XMLTestTrainCategory);
            Assert.Equal(XMLTestTrainCategory.Attribute("ID").Value.ToString(), TestCategory.SimSigID);
            Assert.Equal(XMLTestTrainCategory.Element("Description").Value.ToString(), TestCategory.Description);
            Assert.Equal((WTTAccelBrakeIndex)Convert.ToInt32(XMLTestTrainCategory.Element("AccelBrakeIndex").Value), TestCategory.AccelBrakeIndex);
            Assert.Equal(XMLTestTrainCategory.Element("IsFreight") == null ? false : XMLTestTrainCategory.Element("IsFreight").Value == "-1" ? true : false, TestCategory.IsFreight);
            Assert.Equal(XMLTestTrainCategory.Element("CanUseGoodsLines") == null ? false : XMLTestTrainCategory.Element("CanUseGoodsLines").Value == "-1" ? true : false, TestCategory.CanUseGoodsLines);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeed(Convert.ToInt32(XMLTestTrainCategory.Element("MaxSpeed").Value.ToString())).MPH, TestCategory.MaxSpeed.MPH);
            Assert.Equal(new GroundFrame.Classes.Length(Convert.ToInt32(XMLTestTrainCategory.Element("TrainLength").Value.ToString())).Meters, TestCategory.TrainLength.Meters);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeedClass(Convert.ToInt32(XMLTestTrainCategory.Element("SpeedClass").Value.ToString())).Bitwise, TestCategory.SpeedClass.Bitwise);
            Assert.Equal((WTTPowerToWeightCategory)Convert.ToInt32(XMLTestTrainCategory.Element("PowerToWeightCategory").Value), TestCategory.PowerToWeightCategory);
            Assert.Equal(new GroundFrame.Classes.Electrification(XMLTestTrainCategory.Element("Electrification").Value.ToString()).Overhead, TestCategory.Electrification.Overhead);
            Assert.Equal(XMLTestTrainCategory.Element("CautionSpeedSet")?.Value.ToString(), TestCategory.CautionSpeedSet);

            if (XMLTestTrainCategory.Element("DwellTimes") == null)
            {
                Assert.Null(TestCategory.DwellTimes);
            }
            else
            {
                //TODO: Test Dwell Times
            }
        }

        /// <summary>
        /// Check instantiating a new WTTTrainCategory object from a JSON string
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Constructor_JSON()
        { 
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTrainCategory = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("TrainCategories").Descendants().First();
            GroundFrame.Classes.Timetables.WTTTrainCategory TestCategory = new Classes.Timetables.WTTTrainCategory(XMLTestTrainCategory);
            //Create JSON
            string TestJSON = TestCategory.ToJSON();
            //Create new ojbect from JSON
            GroundFrame.Classes.Timetables.WTTTrainCategory TestJSONCategory = new Timetables.WTTTrainCategory(TestJSON);
            //Test
            Assert.Equal(XMLTestTrainCategory.Attribute("ID").Value.ToString(), TestJSONCategory.SimSigID);
            Assert.Equal(XMLTestTrainCategory.Element("Description").Value.ToString(), TestJSONCategory.Description);
            Assert.Equal((WTTAccelBrakeIndex)Convert.ToInt32(XMLTestTrainCategory.Element("AccelBrakeIndex").Value), TestJSONCategory.AccelBrakeIndex);
            Assert.Equal(XMLTestTrainCategory.Element("IsFreight").Value == "0" ? false : true, TestJSONCategory.IsFreight);
            Assert.Equal(XMLTestTrainCategory.Element("CanUseGoodsLines").Value == "0" ? false : true, TestJSONCategory.CanUseGoodsLines);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeed(Convert.ToInt32(XMLTestTrainCategory.Element("MaxSpeed").Value.ToString())).MPH, TestJSONCategory.MaxSpeed.MPH);
            Assert.Equal(new GroundFrame.Classes.Length(Convert.ToInt32(XMLTestTrainCategory.Element("TrainLength").Value.ToString())).Meters, TestJSONCategory.TrainLength.Meters);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeedClass(Convert.ToInt32(XMLTestTrainCategory.Element("SpeedClass").Value.ToString())).Bitwise, TestJSONCategory.SpeedClass.Bitwise);
            Assert.Equal((WTTPowerToWeightCategory)Convert.ToInt32(XMLTestTrainCategory.Element("PowerToWeightCategory").Value), TestJSONCategory.PowerToWeightCategory);
            Assert.Equal(new GroundFrame.Classes.Electrification(XMLTestTrainCategory.Element("Electrification").Value.ToString()).Overhead, TestJSONCategory.Electrification.Overhead);
            Assert.Equal(XMLTestTrainCategory.Element("CautionSpeedSet")?.Value.ToString(), TestJSONCategory.CautionSpeedSet);

            if (XMLTestTrainCategory.Element("DwellTimes") == null)
            {
                Assert.Null(TestJSONCategory.DwellTimes);
            }
            else
            {
                //TODO: Test Dwell Times
            }
        }

        /// <summary>
        /// Checks the ToJSON Method
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Method_ToJSON()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTrainCategory = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("TrainCategories").Descendants().First();
            GroundFrame.Classes.Timetables.WTTTrainCategory TestCategory = new Classes.Timetables.WTTTrainCategory(XMLTestTrainCategory);
            //Create JSON
            string TestJSON = TestCategory.ToJSON();
            //Create new ojbect from JSON
            GroundFrame.Classes.Timetables.WTTTrainCategory TestJSONCategory = new Timetables.WTTTrainCategory(TestJSON);
            //Test
            Assert.Equal(TestJSON, TestJSONCategory.ToJSON());
            
        }

        /// <summary>
        /// Checks passing NULL to the TrainCategoryXML argument throws a ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Constructor_NullXMLException()
        {
            XElement NullXElement = null;
            Assert.Throws<ArgumentNullException>(() => new GroundFrame.Classes.Timetables.WTTTrainCategory(NullXElement));
        }

        /// <summary>
        /// Checks passing NULL to the JSON argument throws a ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Constructor_NullJSONException()
        {
            string JSON = null;
            Assert.Throws<ArgumentNullException>(() => new GroundFrame.Classes.Timetables.WTTTrainCategory(JSON));
        }

        /// <summary>
        /// Checks passing invalid JSON to the JSON argument throws a FormatException
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Constructor_InvalidJSONException()
        {
            string JSON = "Invalid JSON";
            Assert.Throws<FormatException>(() => new GroundFrame.Classes.Timetables.WTTTrainCategory(JSON));
        }

        #endregion Methods
    }
}
