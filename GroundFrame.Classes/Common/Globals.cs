using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes
{
    public static class Globals
    {
        private static UserSettingCollection _UserSettings;

        public static UserSettingCollection UserSettings { get { return _UserSettings ?? new UserSettingCollection(); } set { _UserSettings = value; } }
    }
}
