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

    /// <summary>
    /// Enum to describe the different WTT activity types
    /// </summary>
    public enum WTTActivityType
    {
        /// <summary>
        /// The next service this service will form
        /// </summary>
        NextTrain = 0,
        DividesNewRear = 1,
        DividesNewFront = 2,
        Joins = 3,
        DetachFront = 5,
        PlatformShare = 9,
        CrewChange = 10
    }

    /// <summary>
    /// Enum to describe the timing point (trip) stopping point
    /// </summary>
    public enum WTTStopLocation
    {
        /// <summary>
        /// The default stop location
        /// </summary>
        Default = 0,
        /// <summary>
        /// Stop at the near end of the stopping location
        /// </summary>
        NearEnd = 1,
        /// <summary>
        /// Stop at the far end of the stopping location
        /// </summary>
        FarEnd = 2,
        /// <summary>
        /// Stop at at x meters from the near end of the stopping location. See stop adjusment for the distance
        /// </summary>
        NearEndExact = 3,
        /// <summary>
        /// Stop at at x meters from the far end of the stopping location. See stop adjusment for the distance
        /// </summary>
        FarEndExact
    }

}
