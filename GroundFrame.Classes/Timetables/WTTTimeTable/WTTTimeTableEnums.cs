using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// An enum describing possible Signal Aspects
    /// </summary>
    public enum WTTSignalAspect
    {
        /// <summary>
        /// Red aspect signal
        /// </summary>
        Red = 1,
        /// <summary>
        /// Shunt signal
        /// </summary>
        Shunt = 2,
        /// <summary>
        /// Single yellow aspect signal
        /// </summary>
        Yellow = 3,
        /// <summary>
        /// Double yello aspect signal
        /// </summary>
        FlashingYellow = 4,
        /// <summary>
        /// Single yellow aspect signal
        /// </summary>
        DoubleYellow = 5,
        /// <summary>
        /// Double yello aspect signal
        /// </summary>
        FlashingDoubleYellow = 6,
        /// <summary>
        /// Double yello aspect signal
        /// </summary>
        Green = 7,
    }

    /// <summary>
    /// An emum used to indicate what a numeric value represents
    /// </summary>
    public enum WTTNumberType
    {
        /// <summary>
        /// Not applicable
        /// </summary>
        NotApplicable = 0,
        /// <summary>
        /// An absolute value
        /// </summary>
        Absolute = 1,
        /// <summary>
        /// A percentage
        /// </summary>
        Percent = 2
    }
}
