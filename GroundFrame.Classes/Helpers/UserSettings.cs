using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Helper class to manage some common UserSetting actions
    /// </summary>
    public static class UserSettingHelper
    {
        /// <summary>
        /// Returns the default settings
        /// </summary>
        /// <returns></returns>
        public static UserSettingCollection GetDefaultSettings()
        {
            //Get the path to the default JSON file
            string JSONPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\defaultUserSettings.json";
            //Read JSON
            string JSON = File.ReadAllText(JSONPath);

            return new UserSettingCollection(JSON);
        }

        /// <summary>
        /// Returns a CultureInfo object from the supplied UserSettingCollection. Will return an "en-GB" object if not found
        /// </summary>
        /// <param name="UserSettings">THe UserSettingCollection from which to extract the CultureInfo</param>
        /// <returns></returns>
        public static CultureInfo GetCultureInfo(UserSettingCollection UserSettings)
        {
            //Validaet the UserSettings argument
            ArgumentValidation.ValidateUserSettings(UserSettings);
            //Get culture name and return CultureInfo object
            string CultureName = UserSettings.GetValueByKey("CULTURE").ToString();
            return new CultureInfo(string.IsNullOrEmpty(CultureName) ? "en-GB" : CultureName);
        }
    }
}
