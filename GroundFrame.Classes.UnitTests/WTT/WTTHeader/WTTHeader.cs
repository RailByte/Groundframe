using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;
using Xunit;
using GroundFrame.Classes.Timetables;

namespace GroundFrame.Classes.UnitTests.WTT.WTTHeader
{
    public class WTTHeader
    {
        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct name
        /// </summary>
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropName()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Equal("Test Timetable", TestHeader.Name);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct description
        /// </summary>
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropDescription()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Null(TestHeader.Description);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct start time
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropStartTime()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Equal(0, TestHeader.StartTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct finish time
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropFinishTime()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Equal(0, TestHeader.FinishTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct major version
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropMajorVersion()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Equal(1, TestHeader.VersionMajor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct minor version
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropMinorVersion()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Equal(0, TestHeader.VersionMinor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct version build
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropBuildVersion()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Equal(0, TestHeader.VersionBuild);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct train description template
        [Fact]
        public void WTTHeader_Constructor_Name_CheckPropTrainDescriptionTemplate()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            Assert.Equal("$originTime $originName-$destName $operator ($stock)", TestHeader.TrainDescriptionTemplate);
        }


        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct version build
        [Fact]
        public void WTTHeader_Prop_CheckVersionBuildReadOnly()
        {
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader("Test Timetable", 0, new UserSettingCollection());
            AttributeCollection attributes = TypeDescriptor.GetProperties(TestHeader)["VersionBuild"].Attributes;
            Assert.True(attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes));
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct name
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml", @"Test Name 4.8")]
        public void WTTHeader_Constructor_XElement_CheckPropName(string FileName, string ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.Name);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct description
        /// </summary>
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml", @"Test Description 4.8")]
        public void WTTHeader_Constructor_XElement_CheckPropDescription(string FileName, string ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.Description);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct start time
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml", 0)]
        public void WTTHeader_Constructor_XElement_CheckPropStartTime(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.StartTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct finish time
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml", 97200)]
        public void WTTHeader_Constructor_XElement_CheckPropFinishTime(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.FinishTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct major version
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml", 1)]
        public void WTTHeader_Constructor_XElement_CheckPropMajorVersion(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.VersionMajor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct minor version
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml", 0)]
        public void WTTHeader_Constructor_XElement_CheckPropMinorVersion(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.VersionMinor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct version build
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml",0)]
        public void WTTHeader_Constructor_XElement_CheckPropBuildVersion(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.VersionBuild);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct train description template
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml", "$originTime $originName-$destName $operator ($stock)")]
        public void WTTHeader_Constructor_XElement_CheckPropTrainDescriptionTemplate(string FileName, string ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(ExpectedValue, TestHeader.TrainDescriptionTemplate);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct train description template
        /// </summary>
        [Fact]
        public void WTTHeader_Method_ToJSON()
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Resources\TestWTT_4.8.xml").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());

            //Convert header to JSON
            string JSONHeader = TestHeader.ToJSON();
            //Deserialize the JSON string back to an WTTHeader object
            GroundFrame.Classes.Timetables.WTTHeader JSONWTTHeader = new Timetables.WTTHeader(JSONHeader, new UserSettingCollection());
            //Check both WTTHeader objects are equal
            Assert.Equal(TestHeader.ToString(), JSONWTTHeader.ToString());
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by  with a StartDate returns sets the correct value
        /// </summary>
        [Theory]
        [InlineData(@"Resources\TestWTT_4.8.xml")]
        public void WTTHeader_Constructor_XElement_CheckStartDateArgument(string FileName)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.Timetables.WTTHeader TestHeader = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new UserSettingCollection());
            Assert.Equal(new DateTime(1850,1,1), TestHeader.StartDate);

            GroundFrame.Classes.Timetables.WTTHeader TestHeaderWithDate = new GroundFrame.Classes.Timetables.WTTHeader(TestXML, new DateTime(2018,7,1), new UserSettingCollection());
            Assert.Equal(new DateTime(2018, 7, 1), TestHeaderWithDate.StartDate);
        }
    }
}
