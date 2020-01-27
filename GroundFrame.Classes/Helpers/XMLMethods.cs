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
        /// <param name="Culture">The culture in which any error message should be return</param>
        /// <param name="DefaultValue">The default value which should be returned if the element isn't found in the XML</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        public static T GetValueFromXElement<T>(XElement XML, string ElementName, CultureInfo Culture, object DefaultValue = null)
        {
            //Validate XML
            ArgumentValidation.ValidateXElement(XML, Culture);

            //Throw an ApplicationException if the element cannot be found and no default value is provided
            if (XML.Element(ElementName) == null && DefaultValue == null)
            {
                throw new ApplicationException($"Cannot find the '{ElementName}' element in the supplied Header XElement");
            }

            //If the element cannot be found return the default value otherwise return the value (converted to the return type T).
            if (XML.Element(ElementName) == null)
            {
                //Test the conversion and throw an InvalidCastException exception if it fails
                if (CanChangeType(DefaultValue, typeof(T)))
                {
                    return (T)Convert.ChangeType(DefaultValue, typeof(T));
                }
                else
                {
                    string DefaultValueString = DefaultValue == null ? "<NULL>" : DefaultValue.ToString();
                    throw new InvalidCastException($"Cannot convert default value '{DefaultValueString}' for Element '{ElementName}' to type of {typeof(T).ToString()}.");
                }
            }
            else
            {
                //Test the conversion and throw an InvalidCastException exception if it fails
                if (CanChangeType(XML.Element(ElementName).Value, typeof(T)))
                {
                    return (T)Convert.ChangeType(XML.Element(ElementName).Value, typeof(T));
                }
                else
                {
                    throw new InvalidCastException($"Cannot convert '{XML.Element(ElementName).Value}' from Element '{ElementName}' to type of {typeof(T).ToString()}.");
                }
            }
        }

        //TODO: Can we make the "CanChangeType" method more efficient Try / Catch is a bit hacky.

        /// <summary>
        /// Helper method to check whether the change won't throw an error
        /// </summary>
        /// <param name="Value">The value to be converted</param>
        /// <param name="ConversionType">The target type of the conversion</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify CultureInfo", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private static bool CanChangeType(object Value, Type ConversionType)
        {
            try
            {
                Convert.ChangeType(Value, ConversionType);
                return true;
            }
            catch
            {
                //Don't need to rethrow the exception - just return false to show it can't convert the type
                return false;
            }
        }
    }
}
