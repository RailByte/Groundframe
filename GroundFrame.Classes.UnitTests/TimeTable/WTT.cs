using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GroundFrame.Classes.Timetables;
using GroundFrame.Classes;
using Xunit;

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
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(JSON, new UserSettingCollection());
            Assert.Equal("Royston Weekday July 2018 Timetable", TestWTT.Header.Name);
            Assert.Equal(3, TestWTT.TrainCategories.Count);
            Assert.Equal(TestWTT.Header.UserSettings.ToJSON(), new UserSettingCollection().ToJSON());
            //Assert.Equal(JSON, TestWTT.ToJSON());
        }

        /// <summary>
        /// Check instantiating a new WTT from JSON but with specific user settings
        /// </summary>
        [Fact]
        public void WTT_Constructor_JSONSpecificUserSettings()
        {
            //Get XElement from test .xml
            string TestJSONPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.json";
            string JSON = File.ReadAllText(TestJSONPath);
            //Create TestWTT with default user settings
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(JSON, new UserSettingCollection());
            Assert.Equal("Royston Weekday July 2018 Timetable", TestWTT.Header.Name);
            Assert.Equal(3, TestWTT.TrainCategories.Count);
            //Create custom settings which are different from the default user settings
            string SpecificUserSettingsJSON = @"[
                {
                            ""key"": ""CULTURE"",
                ""description"": ""The culture of the user"",
                ""value"": ""en-US"",
                ""dataTypeName"": ""System.String""
                },
                {
                            ""key"": ""PASSTIMECHAR"",
                ""description"": ""The character to indicate a passing time"",
                ""value"": "":"",
                ""dataTypeName"": ""System.String""
                },
                {
                            ""key"": ""TIMEHALFCHAR"",
                ""description"": ""The character to indicate a half minute"",
                ""value"": 73,
                ""dataTypeName"": ""System.Int32""
                }
            ]";
            UserSettingCollection SpecificUserSettings = new UserSettingCollection(SpecificUserSettingsJSON);
            Classes.Timetables.WTT UserSettingTestWTT = new Classes.Timetables.WTT(JSON, SpecificUserSettings);
            //Compares JSON
            Assert.Equal(UserSettingTestWTT.Header.UserSettings.ToJSON(), SpecificUserSettings.ToJSON());
            Assert.Equal(UserSettingTestWTT.TimeTables.UserSettings.ToJSON(), SpecificUserSettings.ToJSON());
            Assert.Equal(UserSettingTestWTT.TimeTables.IndexOf(0).UserSettings.ToJSON(), SpecificUserSettings.ToJSON());
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
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(Filename, new UserSettingCollection());
            Assert.Equal("Royston Weekday July 2018 Timetable", TestWTT.Header.Name);
            Assert.Equal(3, TestWTT.TrainCategories.Count);
            Assert.Equal(new DateTime(1850, 1, 1), TestWTT.StartDate);
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
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(Filename, new DateTime(2018, 7, 1), new UserSettingCollection());
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
            Assert.Throws<FormatException>(() => new Classes.Timetables.WTT(JSON, new UserSettingCollection()));
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
            Classes.Timetables.WTT TestWTT = new Classes.Timetables.WTT(Filename, new DateTime(2018, 7, 1), new UserSettingCollection());
            string TestJSON = TestWTT.ToJSON();

            //Get the comparison JSON
            string ComparisonJSONPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Resources\\TestWTT_4.8.json";
            string ComparisonJSON = File.ReadAllText(ComparisonJSONPath);

            Assert.True(TestJSON.Equals(ComparisonJSON));
        }
    }
}
