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
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public static T GetValueFromXElement<T>(XElement XML, string ElementName, object DefaultValue)
        {
            //Arguments aren't validated as it's too much of an overhead.

            //Dictionary to store function mapping
            Dictionary<Type, Func<string, object>> ConversionMapping = new Dictionary<Type, Func<string, object>>
            {
                { typeof(string), (x => x) },
                { typeof(int), (x => Convert.ToInt32(x)) },
                { typeof(double), (x => Convert.ToDouble(x)) },
                { typeof(DateTime), (x => Convert.ToDateTime(x)) }
            };

            try
            {

            
            if (XML.Element(ElementName) == null)
            {
                return (T)DefaultValue;
            }
            else
            {
                //return the parsed value
                return (T)ConversionMapping[typeof(T)](XML.Element(ElementName).Value);
            }
            }
            catch(Exception Ex)
            {
                string Message = Ex.Message;
                throw Ex;
            }
        }
    }
}
