using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Static class store values and settings which are available globally. These must provide a default value if not set.
    /// </summary>
    public static class Globals
    {
        private static UserSettingCollection _UserSettings;

        /// <summary>
        /// Gets or sets the user's settings. If not provided this will return the default set of user settins in en-GB
        /// </summary>
        public static UserSettingCollection UserSettings { get { return _UserSettings ?? new UserSettingCollection(); } set { _UserSettings = value; } }
    }
}
