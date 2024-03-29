﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Xunit;
using GroundFrame.Core;
using System.Reflection;
using System.Globalization;

namespace GroundFrame.Core.UnitTests.Helpers
{
    public class XMLMethods
    {
        #region Private Variables
        #endregion Private Variables

        #region Methods
        /// <summary>
        /// Checks that the GetValueFromXElement method returns an int when specified 
        /// </summary>
        [Theory]
        [InlineData("TestInt", 60)]
        [InlineData("TestInt", null)]
        [InlineData("TestMissingInt", 100)]
        public void XMLMethods_Method_GetValueFromXElement_CheckType_Int(string ElementName, object DefaultValue)
        {
            XDocument SourceXMLDoc = XDocument.Parse("<TestXML><TestString>Test</TestString><TestInt>0</TestInt><TestDate>2000-01-01</TestDate></TestXML>");
            XElement SourceXML = SourceXMLDoc.Element("TestXML");
            Assert.IsType<int>(GroundFrame.Core.XMLMethods.GetValueFromXElement<int>(SourceXML, ElementName, DefaultValue));
        }

        /// <summary>
        /// Checks that the GetValueFromXElement method returns a string when specified 
        /// </summary>
        [Theory]
        [InlineData("TestString", "DefaultValue")]
        [InlineData("TestString", null)]
        [InlineData("TestMissingString", "DefaultValue")]
        public void XMLMethods_Method_GetValueFromXElement_CheckType_String(string ElementName, object DefaultValue)
        {
            XDocument SourceXMLDoc = XDocument.Parse("<TestXML><TestString>Test</TestString><TestInt>0</TestInt><TestDate>2000-01-01</TestDate></TestXML>");
            XElement SourceXML = SourceXMLDoc.Element("TestXML");
            Assert.IsType<string>(GroundFrame.Core.XMLMethods.GetValueFromXElement<string>(SourceXML, ElementName, DefaultValue));
        }

        /// <summary>
        /// Checks that the GetValueFromXElement method returns an DateTime when specified 
        /// </summary>
        [Theory]
        [InlineData("TestDate", "1999-12-31")]
        [InlineData("TestMissingDate", "1999-12-31")]
        public void XMLMethods_Method_GetValueFromXElement_CheckType_DateTime(string ElementName, object DefaultValue)
        {
            XDocument SourceXMLDoc = XDocument.Parse("<TestXML><TestString>Test</TestString><TestInt>0</TestInt><TestDate>2000-01-01</TestDate></TestXML>");
            XElement SourceXML = SourceXMLDoc.Element("TestXML");
            Assert.IsType<DateTime>(GroundFrame.Core.XMLMethods.GetValueFromXElement<DateTime>(SourceXML, ElementName, DateTime.Parse(DefaultValue.ToString())));
        }

        /// <summary>
        /// Checks that the GetValueFromXElement method returns the correct values for int when specified 
        /// </summary>
        [Theory]
        [InlineData("TestInt", 60, 0)]
        [InlineData("TestInt", null, 0)]
        [InlineData("TestMissingInt", 100, 100)]
        public void XMLMethods_Method_GetValueFromXElement_CheckValue_Int(string ElementName, object DefaultValue, int ExpectedValue)
        {
            XDocument SourceXMLDoc = XDocument.Parse("<TestXML><TestString>Test</TestString><TestInt>0</TestInt><TestDate>2000-01-01</TestDate></TestXML>");
            XElement SourceXML = SourceXMLDoc.Element("TestXML");
            Assert.Equal(ExpectedValue, GroundFrame.Core.XMLMethods.GetValueFromXElement<int>(SourceXML, ElementName, DefaultValue));
        }

        /// <summary>
        /// Checks that the GetValueFromXElement method returns the correct values for string when specified 
        /// </summary>
        [Theory]
        [InlineData("TestString", "DefaultValue", "Test")]
        [InlineData("TestString", null, "Test")]
        [InlineData("TestMissingString", "DefaultValue", "DefaultValue")]
        public void XMLMethods_Method_GetValueFromXElement_CheckValue_String(string ElementName, object DefaultValue, string ExpectedValue)
        {
            XDocument SourceXMLDoc = XDocument.Parse("<TestXML><TestString>Test</TestString><TestInt>0</TestInt><TestDate>2000-01-01</TestDate></TestXML>");
            XElement SourceXML = SourceXMLDoc.Element("TestXML");
            Assert.Equal(ExpectedValue, GroundFrame.Core.XMLMethods.GetValueFromXElement<string>(SourceXML, ElementName, DefaultValue));
        }

        /// <summary>
        /// Checks that the GetValueFromXElement method returns the correct values for DateTime when specified 
        /// </summary>
        [Theory]
        [InlineData("TestDate", "1999-12-31", "2000-01-01")]
        [InlineData("TestMissingDate", "1999-12-31", "1999-12-31")]
        public void XMLMethods_Method_GetValueFromXElement_CheckValue_DateTime(string ElementName, object DefaultValue, string ExpectedValue)
        {
            XDocument SourceXMLDoc = XDocument.Parse("<TestXML><TestString>Test</TestString><TestInt>0</TestInt><TestDate>2000-01-01</TestDate></TestXML>");
            XElement SourceXML = SourceXMLDoc.Element("TestXML");
            Assert.Equal(DateTime.Parse(ExpectedValue), GroundFrame.Core.XMLMethods.GetValueFromXElement<DateTime>(SourceXML, ElementName, DateTime.Parse(DefaultValue.ToString())));
        }

        #endregion Methods
    }
}
