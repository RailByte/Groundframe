﻿using GroundFrame.Classes.Timetables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Static class containing XML helper methods
    /// </summary>
    public static class XMLMethods
    {
        /// <summary>
        /// Gets the value from an XML Element and returns it converted to the supplied return type. A Default value can also be provided which is return this the element is not found in the XML
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="XML">The Source XML</param>
        /// <param name="ElementName">The name of the element for whose value is to be return</param>
        /// <param name="DefaultValue">The default value which should be returned if the element isn't found in the XML</param>
        /// <param name="AdditionalArguments">An array of additional arguments to passed to the method. For example when getting a WTTTime from an XML you can pass the TimeTable start date as the first object in the array</param>
        /// <returns>The value from the XML converted to a T type object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public static T GetValueFromXElement<T>(XElement XML, string ElementName, object DefaultValue, object[] AdditionalArguments = null)
        {
            //Arguments aren't validated as it's too much of an overhead.

            //Dictionary to store function mapping
            Dictionary<Type, Func<string, object[], object>> ConversionMapping = new Dictionary<Type, Func<string, object[], object>>
            {
                { typeof(string), ((x,y) => x) },
                { typeof(int), ((x,y) => Convert.ToInt32(x)) },
                { typeof(double), ((x,y) => Convert.ToDouble(x)) },
                { typeof(DateTime), ((x,y) => Convert.ToDateTime(x)) },
                { typeof(WTTSpeed), ((x,y) => new WTTSpeed(Convert.ToInt32(x))) },
                { typeof(Length), ((x,y) => new Length(Convert.ToInt32(x))) },
                { typeof(WTTTime), ((x,y) => y == null ? new WTTTime(Convert.ToInt32(x)) : new WTTTime(Convert.ToInt32(x), (DateTime)y[0])) },
                { typeof(WTTDuration), ((x,y) => new WTTDuration(Convert.ToInt32(x))) }
            };

            if (XML.Element(ElementName) == null)
            {
                return (T)DefaultValue;
            }
            else
            {
                //return the parsed value
                return (T)ConversionMapping[typeof(T)](XML.Element(ElementName).Value, AdditionalArguments);
            }
        }
    }
}
