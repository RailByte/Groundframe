using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Class to perform generic bitwise operations on Enums
    /// </summary>
    internal static class BitwiseHelper
    {
        /// <summary>
        /// Performs a logical and bitwise operation on the supplied enum and returns a list of the included values
        /// </summary>
        /// <typeparam name="T">The type of enum to be returned by the method</typeparam>
        /// <param name="Mask">An enum representing the bitwise value to be returned as a list</param>
        /// <param name="UserSettings">The user settings. Used to determine the culture of any exception messages</param>
        /// <returns>A list of the supplied enum type of the values included in the Mask</returns>
        public static List<T> MaskToList<T>(Enum Mask, UserSettingCollection UserSettings)
        {
            //Check T type argument is an enum
            if (typeof(T).IsSubclassOf(typeof(Enum)) == false)
                throw new ArgumentException(ExceptionHelper.GetStaticException("EnumTypeException", null, UserSettingHelper.GetCultureInfo(UserSettings ?? new UserSettingCollection())));

            //return the list
            return Enum.GetValues(typeof(T))
                                 .Cast<Enum>()
                                 .Where(m => Mask.HasFlag(m))
                                 .Cast<T>()
                                 .ToList();
        }
    }
}
