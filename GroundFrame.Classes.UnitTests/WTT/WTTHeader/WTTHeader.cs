using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace GroundFrame.Classes.UnitTests.WTT.WTTHeader
{
    public class WTTHeader
    {
        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct name
        /// </summary>
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropName()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Equal("Test Timetable", TestHeader.Name);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct description
        /// </summary>
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropDescription()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Null(TestHeader.Description);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct start time
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropStartTime()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Equal(0, TestHeader.StartTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct finish time
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropFinishTime()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Equal(0, TestHeader.FinishTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct major version
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropMajorVersion()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Equal(1, TestHeader.VersionMajor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct minor version
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropMinorVersion()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Equal(0, TestHeader.VersionMinor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct version build
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropBuildVersion()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Equal(0, TestHeader.VersionBuild);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct train description template
        [Fact]
        public void XMLMethods_WTTHeader_Constructor_Name_CheckPropTrainDescriptionTemplate()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            Assert.Equal("$originTime $originName-$destName $operator ($stock)", TestHeader.TrainDescriptionTemplate);
        }


        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct version build
        [Fact]
        public void XMLMethods_WTTHeader_Prop_CheckVersionBuildReadOnly()
        {
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader("Test Timetable");
            AttributeCollection attributes = TypeDescriptor.GetProperties(TestHeader)["VersionBuild"].Attributes;
            Assert.True(attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes));
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct name
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml", @"Test Name 4.8")]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropName(string FileName, string ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.Name);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct description
        /// </summary>
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml", @"Test Description 4.8")]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropDescription(string FileName, string ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.Description);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct start time
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml", 0)]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropStartTime(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.StartTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct finish time
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml", 97200)]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropFinishTime(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.FinishTime.Seconds);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct major version
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml", 1)]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropMajorVersion(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.VersionMajor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct minor version
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml", 0)]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropMinorVersion(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.VersionMinor);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct version build
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml",0)]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropBuildVersion(string FileName, int ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.VersionBuild);
        }

        /// <summary>
        /// Checks that a WTTHeader instantiated by name returns the correct train description template
        /// </summary>
        [Theory]
        [InlineData(@"UnitTestWTT_4.8.xml", "$originTime $originName-$destName $operator ($stock)")]
        public void XMLMethods_WTTHeader_Constructor_XElement_CheckPropTrainDescriptionTemplate(string FileName, string ExpectedValue)
        {
            string FullPath = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}{FileName}").LocalPath;
            XElement TestXML = XDocument.Load(FullPath).Element("SimSigTimetable");
            GroundFrame.Classes.WTTHeader TestHeader = new GroundFrame.Classes.WTTHeader(TestXML);
            Assert.Equal(ExpectedValue, TestHeader.TrainDescriptionTemplate);
        }
    }
}
