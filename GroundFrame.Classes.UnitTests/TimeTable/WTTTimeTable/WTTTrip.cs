using GroundFrame.Classes.Timetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Classes.UnitTests.TimeTable.WTTTimeTable
{
    public class WTTTrip
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
        /// Check instantiating a new WTTTrip object from a SimSig XML snippet.
        /// </summary>
        [Fact]
        public void WTTTrip_Costructor_XElement()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTimeTable = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Descendants().First();
            XElement XMLTestTrip = XMLTestTimeTable.Element("Trips").Descendants().First();

            GroundFrame.Classes.Timetables.WTTTrip TestTrip = new Classes.Timetables.WTTTrip(XMLTestTrip, new DateTime(2018,7,1));
            Assert.Equal(XMLTestTrip.Element("Location").Value.ToString(), TestTrip.Location);
            Assert.Equal(new GroundFrame.Classes.Timetables.WTTTime(Convert.ToInt32(XMLTestTrip.Element("DepPassTime").Value.ToString())).Seconds, TestTrip.DepPassTime.Seconds);

            if (XMLTestTrip.Element("ArrTime") == null)
            {
                Assert.Null(TestTrip.ArrTime);
            }
            else
            {
                Assert.Equal(new GroundFrame.Classes.Timetables.WTTTime(Convert.ToInt32(XMLTestTrip.Element("ArrTime").Value.ToString())).Seconds, TestTrip.ArrTime.Seconds);
            }

            if (XMLTestTrip.Element("IsPassTime") == null)
            {
                Assert.False(TestTrip.IsPassTime);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("IsPassTime").Value.ToString())), TestTrip.IsPassTime);
            }

            if (XMLTestTrip.Element("Platform") == null)
            {
                Assert.Null(TestTrip.Platform);
            }
            else
            {
                Assert.Equal(XMLTestTrip.Element("Platform").Value.ToString(), TestTrip.Platform);
            }

            if (XMLTestTrip.Element("Line") == null)
            {
                Assert.Null(TestTrip.Line);
            }
            else
            {
                Assert.Equal(XMLTestTrip.Element("Line").Value.ToString(), TestTrip.Line);
            }

            if (XMLTestTrip.Element("Path") == null)
            {
                Assert.Null(TestTrip.Path);
            }
            else
            {
                Assert.Equal(XMLTestTrip.Element("Path").Value.ToString(), TestTrip.Path);
            }

            if (XMLTestTrip.Element("AutoLine") == null)
            {
                Assert.False(TestTrip.AutoLine);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("AutoLine").Value.ToString())), TestTrip.AutoLine);
            }


            if (XMLTestTrip.Element("AutoPath") == null)
            {
                Assert.False(TestTrip.AutoPath);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("AutoPath").Value.ToString())), TestTrip.AutoPath);
            }


            if (XMLTestTrip.Element("DownDirection") == null)
            {
                Assert.False(TestTrip.DownDirection);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("DownDirection").Value.ToString())), TestTrip.DownDirection);
            }

            if (XMLTestTrip.Element("PrevPathEndDown") == null)
            {
                Assert.False( TestTrip.PrevPathEndDown);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("PrevPathEndDown").Value.ToString())), TestTrip.PrevPathEndDown);
            }

            if (XMLTestTrip.Element("NextPathStartDown") == null)
            {
                Assert.False(TestTrip.NextPathStartDown);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("NextPathStartDown").Value.ToString())), TestTrip.NextPathStartDown);
            }

            Assert.Equal(XMLTestTrip.Element("StopLocation") == null ? WTTStopLocation.Default : (WTTStopLocation)Convert.ToInt32(XMLTestTrip.Element("StopLocation").Value.ToString()), TestTrip.StopLocation);

            if (XMLTestTrip.Element("DwellTime") == null)
            {
                Assert.Null(TestTrip.DwellTime);
            }
            else
            {
                Assert.Equal(new WTTDuration(Convert.ToInt32(XMLTestTrip.Element("DwellTime").Value.ToString())).Seconds, TestTrip.DwellTime.Seconds);
            }

            if (XMLTestTrip.Element("BerthsHere") == null)
            {
                Assert.False(TestTrip.BerthsHere);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("BerthsHere").Value.ToString())), TestTrip.BerthsHere);
            }

            if (XMLTestTrip.Element("AllowStopsOnThroughLines") == null)
            {
                Assert.False(TestTrip.AllowStopsOnThroughLines);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("AllowStopsOnThroughLines").Value.ToString())), TestTrip.AllowStopsOnThroughLines);
            }

            if (XMLTestTrip.Element("WaitForBookedTime") == null)
            {
                Assert.False(TestTrip.WaitForBookedTime);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("WaitForBookedTime").Value.ToString())), TestTrip.WaitForBookedTime);
            }

            if (XMLTestTrip.Element("SetDownOnly") == null)
            {
                Assert.False(TestTrip.SetDownOnly);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(XMLTestTrip.Element("SetDownOnly").Value.ToString())), TestTrip.SetDownOnly);
            }

            if (XMLTestTrip.Element("Activites") == null)
            {
                Assert.Null(TestTrip.Activities);
            }
            else
            {
                Assert.Equal(XMLTestTrip.Element("Activites").Descendants("Activity").Count(), TestTrip.Activities.Count());
            }
        }

        /// <summary>
        /// Checks passing NULL to the WTTTripXML argument throws a ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTrip_Prop_NullXMLException()
        {
            XElement NullXElement = null;
            Assert.Throws<ArgumentNullException>(() => new GroundFrame.Classes.Timetables.WTTTrip(NullXElement, new DateTime(2018,7,1)));
        }

        /// <summary>
        /// Checks passing an invalid value to the DateStart argument throws an out of range exception
        /// </summary>
        [Fact]
        public void WTTTrip_Prop_InValidStartDateException()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTimeTable = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Descendants().First();
            XElement XMLTestTrip = XMLTestTimeTable.Element("Trips").Descendants().First();
            Assert.Throws<ArgumentOutOfRangeException>(() => new GroundFrame.Classes.Timetables.WTTTrip(XMLTestTimeTable, new DateTime(1750, 1, 1)));
        }

        /// <summary>
        /// Check instantiating a new WTTTrip object from a JSON string
        /// </summary>
        [Fact]
        public void WTTTrip_Constructor_JSON()
        {
            //Get XElement from test .xml
            string TestXMLPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            XElement XMLTestTimeTable = XDocument.Load(TestXMLPath).Element("SimSigTimetable").Element("Timetables").Descendants().First();
            XElement XMLTestTrip = XMLTestTimeTable.Element("Trips").Descendants().First();
            GroundFrame.Classes.Timetables.WTTTrip TestTrip = new Classes.Timetables.WTTTrip(XMLTestTrip, new DateTime(2018, 7, 1));
            //Create JSON
            string TestJSON = TestTrip.ToJSON();
            //Create new ojbect from JSON
            GroundFrame.Classes.Timetables.WTTTrip TestJSONTrip = new Classes.Timetables.WTTTrip(TestJSON);
            Assert.Equal(TestTrip.ToString(), TestJSONTrip.ToString());
            Assert.Equal(TestTrip.StartDate, TestJSONTrip.StartDate);
        }

        /// <summary>
        /// Check the Start Date property is read only
        /// </summary>
        [Fact]
        public void WTTTrip_Property_StartDate()
        {
            Assert.False(typeof(GroundFrame.Classes.Timetables.WTTTrip).GetProperty("StartDate").CanWrite);
        }

        /// <summary>
        /// Checks passing NULL to the JSON argument throws a ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTrip_Constructor_NullJSONException()
        {
            string JSON = null;
            Assert.Throws<ArgumentNullException>(() => new GroundFrame.Classes.Timetables.WTTTrip(JSON));
        }

        /// <summary>
        /// Checks passing invalid JSON to the JSON argument throws a FormatException
        /// </summary>
        [Fact]
        public void WTTTrip_Constructor_InvalidJSONException()
        {
            string JSON = "Invalid JSON";
            Assert.Throws<FormatException>(() => new GroundFrame.Classes.Timetables.WTTTrip(JSON));
        }

        #endregion Methods
    }
}
