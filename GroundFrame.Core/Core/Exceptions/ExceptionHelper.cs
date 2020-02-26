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

        /// <summary>
        /// Generates a IEnumerable collection of inner exceptions from an Exception
        /// </summary>
        /// <param name="Ex">The exception from which the collection of inner exceptions should be extracted</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Culture handled inside ExceptionHelper.GetStaticException")]
        private static IEnumerable<Exception> GetInnerExceptions(this Exception Ex)
        {
            if (Ex == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("InvalidExceptionArgument", null));
            }

            var innerException = Ex;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }

        /// <summary>
        /// Builds a string containing all the messages from an exception including the inner exceptions
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>A string containing all exception message messages</returns>
        public static string BuildExceptionMessage(this Exception ex)
        {
            string Messages = $"Exception: {ex.Message}";

            if (ex.InnerException != null)
            {
                Messages += string.Join(" | Inner Exception: ", ex.GetInnerExceptions().Select(x => x.Message).ToList());
            }

            return Messages;
        }

    }

}
