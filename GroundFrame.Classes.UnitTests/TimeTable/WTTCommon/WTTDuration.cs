using System;
using Xunit;
using GroundFrame.Classes.Timetables;

namespace GroundFrame.Classes.UnitTests.TimeTable.WTTCommon
{
    /// <summary>
    /// Class which represents a set of unit tests for the GroundFrame.Classes.WTTDuration class
    /// </summary>
    public class WTTDuration
    {
        /// <summary>
        /// Check that FormattedShortTime property returns the correct value when the object is instantiated from just the seconds
        /// </summary>
        [Theory]
        [InlineData(60, "00:01")]
        [InlineData(90, "00:01H")]
        [InlineData(86400, "24:00")]
        [InlineData(86460, "24:01")]
        [InlineData(86490, "24:01H")]
        [InlineData(172800, "48:00")]
        [InlineData(172860, "48:01")]
        [InlineData(172890, "48:01H")]
        [InlineData(-60, "-00:01")]
        public void WTTTime_Prop_FormattedDuration(int Input, string ExpectedValue)
        {
            GroundFrame.Classes.Timetables.WTTDuration TestDuration = new Classes.Timetables.WTTDuration(Input);
            Assert.Equal(ExpectedValue, TestDuration.FormattedDuration);
        }
    }
}
