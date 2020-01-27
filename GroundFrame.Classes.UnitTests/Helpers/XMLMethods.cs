using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Xunit;
using GroundFrame.Classes;
using System.Reflection;
using System.Globalization;

namespace GroundFrame.Classes.UnitTests.Helpers
{
    public class XMLMethods
    {
        #region Private Variables

        CultureInfo _Culture; //Stores the Culture

        #endregion Private Variables

        public XMLMethods()
        {
            this._Culture = new CultureInfo("en-GB");
        }

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
            Assert.IsType<int>(GroundFrame.Classes.XMLMethods.GetValueFromXElement<int>(SourceXML, ElementName, this._Culture, DefaultValue));
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
            Assert.IsType<string>(GroundFrame.Classes.XMLMethods.GetValueFromXElement<string>(SourceXML, ElementName, this._Culture, DefaultValue));
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
            Assert.IsType<DateTime>(GroundFrame.Classes.XMLMethods.GetValueFromXElement<DateTime>(SourceXML, ElementName, this._Culture, DateTime.Parse(DefaultValue.ToString())));
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
            Assert.Equal(ExpectedValue, GroundFrame.Classes.XMLMethods.GetValueFromXElement<int>(SourceXML, ElementName, this._Culture, DefaultValue));
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
            Assert.Equal(ExpectedValue, GroundFrame.Classes.XMLMethods.GetValueFromXElement<string>(SourceXML, ElementName, this._Culture, DefaultValue));
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
            Assert.Equal(DateTime.Parse(ExpectedValue), GroundFrame.Classes.XMLMethods.GetValueFromXElement<DateTime>(SourceXML, ElementName, this._Culture, DateTime.Parse(DefaultValue.ToString())));
        }

        /// <summary>
        /// Checks that the GetValueFromXElement returns an InvalidCastException when trying to return an int as a DateTime
        /// </summary>
        [Fact]
        public void XMLMethods_Method_GetValueFromXElement_CheckConversionError_DateTimeToInt()
        {
            XDocument SourceXMLDoc = XDocument.Parse("<TestXML><TestString>Test</TestString><TestInt>0</TestInt><TestDate>2000-01-01</TestDate></TestXML>");
            XElement SourceXML = SourceXMLDoc.Element("TestXML");
            Assert.Throws<InvalidCastException>(() => GroundFrame.Classes.XMLMethods.GetValueFromXElement<DateTime>(SourceXML, "TestInt", this._Culture, "1999-12-31"));
        }

        #endregion Methods
    }
}
