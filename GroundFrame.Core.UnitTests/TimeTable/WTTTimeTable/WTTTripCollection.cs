﻿using GroundFrame.Core.Timetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Core.UnitTests.TimeTable.WTTTripCollection
{
    public class WTTTripCollection
    {
        /// <summary>
        /// Checks that a WTTTripCollection instantiated by an XElement
        /// </summary>
        [Fact]
        public void WTTTripCollection_Constuctor_XElement()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestTripsXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "1R48").FirstOrDefault().Element("Trips");
            GroundFrame.Core.Timetables.WTTTripCollection TestTripCollection = new GroundFrame.Core.Timetables.WTTTripCollection(TestTripsXML, new DateTime(2018,7,1));
            Assert.Equal(2, TestTripCollection.Count);
        }

        /// <summary>
        /// Checks that the IndexOf methiod within WTTTripCollection returns the correct WTTTrip
        /// </summary>
        [Fact]
        public void WTTTripCollection_Method_IndexOf()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestTripsXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "1R48").FirstOrDefault().Element("Trips");
            GroundFrame.Core.Timetables.WTTTripCollection TestTripCollection = new GroundFrame.Core.Timetables.WTTTripCollection(TestTripsXML, new DateTime(2018, 7, 1));

            GroundFrame.Core.Timetables.WTTTrip TestTrip = TestTripCollection.IndexOf(0);
            Assert.Equal(TestTripsXML.Elements("Trip").FirstOrDefault().Element("Location").Value.ToString(), TestTrip.Location);
            Assert.Equal(Convert.ToInt32(TestTripsXML.Elements("Trip").FirstOrDefault().Element("DepPassTime").Value.ToString()), TestTrip.DepPassTime.Seconds);

            if (TestTripsXML.Elements("Trip").FirstOrDefault().Element("ArrTime") == null)
            {
                Assert.Null(TestTrip.ArrTime);
            }
            else
            {
                Assert.Equal(Convert.ToInt32(TestTripsXML.Elements("Trip").FirstOrDefault().Element("ArrTime").Value.ToString()), TestTrip.ArrTime.Seconds);
            }

            if (TestTripsXML.Elements("Trip").FirstOrDefault().Element("Platform") == null)
            {
                Assert.Null(TestTrip.Platform);
            }
            else
            {
                Assert.Equal(TestTripsXML.Elements("Trip").FirstOrDefault().Element("Platform").Value.ToString(), TestTrip.Platform);
            }

            if (TestTripsXML.Elements("Trip").FirstOrDefault().Element("Line") == null)
            {
                Assert.Null(TestTrip.Line);
            }
            else
            {
                Assert.Equal(TestTripsXML.Elements("Trip").FirstOrDefault().Element("Line").Value.ToString(), TestTrip.Line);
            }

            if (TestTripsXML.Elements("Trip").FirstOrDefault().Element("Path") == null)
            {
                Assert.Null(TestTrip.Path);
            }
            else
            {
                Assert.Equal(TestTripsXML.Elements("Trip").FirstOrDefault().Element("Path").Value.ToString(), TestTrip.Path);
            }

            if (TestTripsXML.Elements("Trip").FirstOrDefault().Element("DownDirection") == null)
            {
                Assert.False(TestTrip.DownDirection);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(TestTripsXML.Elements("Trip").FirstOrDefault().Element("DownDirection").Value.ToString())), TestTrip.DownDirection);
            }

            if (TestTripsXML.Elements("Trip").FirstOrDefault().Element("PrevPathEndDown") == null)
            {
                Assert.False(TestTrip.PrevPathEndDown);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(TestTripsXML.Elements("Trip").FirstOrDefault().Element("PrevPathEndDown").Value.ToString())), TestTrip.PrevPathEndDown);
            }

            if (TestTripsXML.Elements("Trip").FirstOrDefault().Element("NextPathStartDown") == null)
            {
                Assert.False(TestTrip.NextPathStartDown);
            }
            else
            {
                Assert.Equal(Convert.ToBoolean(Convert.ToInt32(TestTripsXML.Elements("Trip").FirstOrDefault().Element("NextPathStartDown").Value.ToString())), TestTrip.NextPathStartDown);
            }
        }

        /// <summary>
        /// Checks that a WTTTrainCategoryCollection instantiated by JSON returns the correct WTTTrainCategoryCollection
        /// </summary>
        [Fact]
        public void WTTTripCollection_Method_ToJSON()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestTripsXML = XDocument.Load(FullPath).Element("SimSigTimetable").Element("Timetables").Elements("Timetable").Where(x => x.Element("ID").Value == "1R48").FirstOrDefault().Element("Trips");
            GroundFrame.Core.Timetables.WTTTripCollection TestTripCollection = new GroundFrame.Core.Timetables.WTTTripCollection(TestTripsXML, new DateTime(2018, 7, 1));
            Assert.Equal(2, TestTripCollection.Count);

            //Convert header to JSON
            string JSONTripCollection = TestTripCollection.ToJSON();
            //Deserialize the JSON string back to an WTTHeader object
            GroundFrame.Core.Timetables.WTTTripCollection JSONWTTTTripCollection = new Timetables.WTTTripCollection(JSONTripCollection);
            Assert.Equal(2, JSONWTTTTripCollection.Count);
            //Check both WTTHeader objects are equal
            Assert.Equal(TestTripCollection.ToString(), JSONWTTTTripCollection.ToString());
            Assert.Equal(TestTripCollection.StartDate, JSONWTTTTripCollection.StartDate);
            Assert.Equal(TestTripCollection.Count, JSONWTTTTripCollection.Count);
            Assert.Equal(TestTripCollection.IndexOf(0).Activities == null ? 0 : TestTripCollection.IndexOf(0).Activities.Count, JSONWTTTTripCollection.IndexOf(0).Activities == null ? 0 : JSONWTTTTripCollection.IndexOf(0).Activities.Count);
        }

        /// <summary>
        /// Check the Start Date property is read only
        /// </summary>
        [Fact]
        public void WTTTripCollection_Property_StartDate()
        {
            Assert.False(typeof(GroundFrame.Core.Timetables.WTTTripCollection).GetProperty("StartDate").CanWrite);
        }
    }
}
