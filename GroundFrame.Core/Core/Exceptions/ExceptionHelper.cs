using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace GroundFrame.Core
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

            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
            string? ExceptionMessage = ExceptionMessageResources.GetString(Key, Culture);
            
            //Set Format to be the key if the returned string == NULL
            string Format = string.IsNullOrEmpty(ExceptionMessage) ? Key : ExceptionMessage;

            return string.Format(Culture, Format, Args);
        }
#nullable disable

        /// <summary>
        /// Returns a translated Exception message with arguments
        /// </summary>
        /// <param name="Key">The resouce key for the exception</param>
        /// <param name="Arguments">An array of arguments which can be passed to the string. See comments in RESX file for details</param>
        /// <returns></returns>
#nullable enable
        internal static string GetStaticException(string Key, object?[] Arguments)
        {
            return GetStaticException(Key, Arguments, Globals.UserSettings.GetCultureInfo());
        }
#nullable disable

        private static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            var innerException = ex;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }

        public static string BuildExceptionMessage(this Exception ex)
        {
            string Messages = $"Exception: {ex.Message}";

            if (ex.InnerException != null)
            {
                Messages += string.Join(" | ", ex.GetInnerExceptions().Select(x => x.Message).ToList());
            }

            return Messages;
        }

    }

}
