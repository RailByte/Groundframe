using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;
using GroundFrame.Classes.Timetables;

namespace GroundFrame.Classes.UnitTests.WTTTrainCategory
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
        /// Check instantiating a new WTTSpeed object gets the KPH Correctly.
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Prop_XElement()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTrainCategory = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("TrainCategories").Descendants().First();
            GroundFrame.Classes.Timetables.WTTTrainCategory TestCategory = new Classes.Timetables.WTTTrainCategory(XMLTestTrainCategory, new UserSettingCollection());
            Assert.Equal(XMLTestTrainCategory.Attribute("ID").Value.ToString(), TestCategory.SimSigID);
            Assert.Equal(XMLTestTrainCategory.Element("Description").Value.ToString(), TestCategory.Description);
            Assert.Equal((WTTAccelBrakeIndex)Convert.ToInt32(XMLTestTrainCategory.Element("AccelBrakeIndex").Value), TestCategory.AccelBrakeIndex);
            Assert.Equal(XMLTestTrainCategory.Element("IsFreight").Value == "0" ? false : true, TestCategory.IsFreight);
            Assert.Equal(XMLTestTrainCategory.Element("CanUseGoodsLines").Value == "0" ? false : true, TestCategory.CanUseGoodsLines);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeed(Convert.ToInt32(XMLTestTrainCategory.Element("MaxSpeed").Value.ToString()), new UserSettingCollection()).MPH, TestCategory.MaxSpeed.MPH);
            Assert.Equal(new GroundFrame.Classes.Length(Convert.ToInt32(XMLTestTrainCategory.Element("TrainLength").Value.ToString())).Meters, TestCategory.TrainLength.Meters);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTSpeedClass(Convert.ToInt32(XMLTestTrainCategory.Element("SpeedClass").Value.ToString()), new UserSettingCollection()).Bitwise, TestCategory.SpeedClass.Bitwise);
            Assert.Equal((WTTPowerToWeightCategory)Convert.ToInt32(XMLTestTrainCategory.Element("PowerToWeightCategory").Value), TestCategory.PowerToWeightCategory);
            Assert.Equal(new GroundFrame.Classes.Electrification(XMLTestTrainCategory.Element("Electrification").Value.ToString()).Overhead, TestCategory.Electrification.Overhead);
            Assert.Null(TestCategory.DwellTimes);
        }

        /// <summary>
        /// Checks passing NULL to the TrainCategoryXML argument throws a ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTrainCategory_Prop_NullXMLException()
        {
            Assert.Throws<ArgumentNullException>(() => new GroundFrame.Classes.Timetables.WTTTrainCategory(null, new UserSettingCollection()));
        }

        #endregion Methods
    }
}
