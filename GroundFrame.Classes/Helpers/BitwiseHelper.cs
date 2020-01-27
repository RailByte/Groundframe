using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GroundFrame.Classes
{
    internal static class BitwiseHelper
    {
        //Converts a Bitwise value to a list of the Enum values
        public static List<T> MaskToList<T>(Enum Mask, UserSettingCollection UserSettings)
        {
            //Set default culture
            if (UserSettings == null)
            {
                UserSettings = new UserSettingCollection();
            }

            //Check T type argument is an enum
            if (typeof(T).IsSubclassOf(typeof(Enum)) == false)
                throw new ArgumentException(ExceptionHelper.GetStaticException("EnumTypeException", null, UserSettingHelper.GetCultureInfo(UserSettings)));

            //return the list
            return Enum.GetValues(typeof(T))
                                 .Cast<Enum>()
                                 .Where(m => Mask.HasFlag(m))
                                 .Cast<T>()
                                 .ToList();
        }
    }
}
