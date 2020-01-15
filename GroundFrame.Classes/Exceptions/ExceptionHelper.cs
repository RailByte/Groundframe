using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace GroundFrame.Classes
{
    internal static class ExceptionHelper
    {
        /// <summary>
        /// Returns a translated Exception message with arguments
        /// </summary>
        /// <param name="Key">The resouce key for the exception</param>
        /// <param name="Arguments">An array of arguments which can be passed to the string. See comments in RESX file for details</param>
        /// <param name="Culture"></param>
        /// <returns></returns>
#nullable enable
        internal static string GetStaticException(string Key, object?[] Arguments, CultureInfo Culture)
        {
            object?[] Args = Arguments ?? Array.Empty<object>();

            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
            string? ExceptionMessage = ExceptionMessageResources.GetString("InvalidSimSigCodeArgument", Culture);
            
            //Set Format to be the key if the returned string == NULL
            string Format = string.IsNullOrEmpty(ExceptionMessage) ? Key : ExceptionMessage;

            return string.Format(Culture, Format, Args);
        }
#nullable disable
    }
}
