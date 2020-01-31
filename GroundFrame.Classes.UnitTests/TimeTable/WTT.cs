using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GroundFrame.Classes.Timetables;
using GroundFrame.Classes;
using Xunit;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace GroundFrame.Classes.UnitTests.WTT
{
    public class WTT
    {
        /// <summary>
        /// Check instantiating a new WTT from JSON
        /// </summary>
        [Fact]
        public void WTT_Constructor_JSON()
        {
            //Get XElement from test .xml
            string TestJSONPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.json";
            string JSON = File.ReadAllText(TestJSONPath);
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(JSON);
            Assert.Equal("Royston Weekday July 2018 Timetable", TestWTT.Header.Name);
            Assert.Equal(3, TestWTT.TrainCategories.Count);
            Assert.Equal(JSON, TestWTT.ToJSON());
        }

        /// <summary>
        /// Check instantiating a new WTT from a File
        /// </summary>
        [Fact]
        public void WTT_Constructor_FileInfo()
        {
            //Get XElement from test .xml
            string TestFilePath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.WTT";
            FileInfo Filename = new FileInfo(TestFilePath);
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(Filename);
            Assert.Equal("Royston Weekday July 2018 Timetable", TestWTT.Header.Name);
            Assert.Equal(3, TestWTT.TrainCategories.Count);
            Assert.Equal(new DateTime(1850, 1, 1), TestWTT.StartDate);

            List<Classes.Timetables.WTTTimeTable> TimeTables = TestWTT.TimeTables.GetByHeadCode("1R48");
            Classes.Timetables.WTTActivityCollection TestActivities = TimeTables[0].Trip.IndexOf(1).Activities;
            Assert.Equal(1, TestActivities.Count);
        }

        /// <summary>
        /// Check instantiating a new WTT from a File With Date
        /// </summary>
        [Fact]
        public void WTT_Constructor_FileInfoWithDate()
        {
            //Get XElement from test .xml
            string TestFilePath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.WTT";
            FileInfo Filename = new FileInfo(TestFilePath);
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(Filename, new DateTime(2018, 7, 1));
            Assert.Equal("Royston Weekday July 2018 Timetable", TestWTT.Header.Name);
            Assert.Equal(3, TestWTT.TrainCategories.Count);
            Assert.Equal(new DateTime(2018,7,1), TestWTT.StartDate);
        }

        /// <summary>
        /// Check instantiating a new WTT from non JSON file
        /// </summary>
        [Fact]
        public void WTT_Constructor_JSONInvalidJSON()
        {
            //Get JSON from .json file
            string TestJSONPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.xml";
            string JSON = File.ReadAllText(TestJSONPath);
            Assert.Throws<FormatException>(() => new Classes.Timetables.WTT(JSON));
        }

        /// <summary>
        /// Check generating JSON matches the expected output
        /// </summary>
        [Fact]
        public void WTT_Method_ToJSON()
        {
            //Create WTT From test .WTT file
            string TestFilePath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.WTT";
            FileInfo Filename = new FileInfo(TestFilePath);
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(Filename, new DateTime(2018, 7, 1));
            string TestJSON = TestWTT.ToJSON();

            //Parse JSON
            JObject ParsedJSON = JObject.Parse(TestJSON);
            //Test various elements
            Assert.Equal(TestWTT.TimeTables.Count(), ParsedJSON["timeTables"].Count());
            Assert.Equal(TestWTT.TimeTables.IndexOf(0).Trip.Count(), ParsedJSON["timeTables"][0]["trip"].Count());
            Assert.Equal(TestWTT.TimeTables.GetByHeadCode("1R52").FirstOrDefault().Trip.IndexOf(1).Activities.Count(), ParsedJSON["timeTables"].Where(h => h["headcode"].ToString() == "1R52").FirstOrDefault()["trip"][1]["activities"].Count());
        }
    }
}
