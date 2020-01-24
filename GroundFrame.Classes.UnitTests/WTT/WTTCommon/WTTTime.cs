using System;
using Xunit;
using GroundFrame.Classes.WTT;

namespace GroundFrame.Classes.UnitTests.WTT.WTTCommon
{

    public class WTTTime
    {
        /// <summary>
        /// Check the instantiating a new WTTTime object with just seconds throws an ArguemtnOutOfRangeException if seconds less than 0
        /// </summary>
        [Fact]
        public void WTTTime_Prop_ArgumentException_Seconds()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GroundFrame.Classes.WTT.WTTTime(-1, "H"));
        }

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
        public void WTTTime_Prop_FormattedShortTime(int Input, string ExpectedValue)
        {
            GroundFrame.Classes.WTT.WTTTime TestTime = new Classes.WTT.WTTTime(Input, "H");
            Assert.Equal(ExpectedValue, TestTime.FormattedShortTime);
        }

        /// <summary>
        /// Check that FormattedLongTime property returns the correct value when the object is instantiated from just the seconds
        /// </summary>
        [Theory]
        [InlineData(60, "00:01")]
        [InlineData(90, "00:01H")]
        [InlineData(86400, "(+1 Day) 00:00")]
        [InlineData(86460, "(+1 Day) 00:01")]
        [InlineData(86490, "(+1 Day) 00:01H")]
        [InlineData(172800, "(+2 Days) 00:00")]
        [InlineData(172860, "(+2 Days) 00:01")]
        [InlineData(172890, "(+2 Days) 00:01H")]
        public void WTTTime_Prop_FormattedLongTime(int Input, string ExpectedValue)
        {
            GroundFrame.Classes.WTT.WTTTime TestTime = new Classes.WTT.WTTTime(Input, "H");
            Assert.Equal(ExpectedValue, TestTime.FormattedLongTime);
        }

        /// <summary>
        /// Check that DateAndTime property returns the correct value when the object is instantiated from just the seconds
        /// </summary>
        [Fact]
        public void WTTTime_Prop_DateAndTime()
        {
            GroundFrame.Classes.WTT.WTTTime TestTime = new Classes.WTT.WTTTime(60, "H");
            DateTime DateTimeResult = new DateTime(1850, 1, 1, 0, 1, 0);

            Assert.Equal(DateTimeResult, TestTime.DateAndTime);
        }
        /// <summary>
        /// Check that Seconds property returns the correct value when the object is instantiated from just the seconds
        /// </summary>
        [Fact]
        public void WTTTime_Prop_Seconds()
        {
            GroundFrame.Classes.WTT.WTTTime TestTime = new Classes.WTT.WTTTime(60, "H");
            int SecondsResult = 60;

            Assert.Equal(SecondsResult, TestTime.Seconds);
        }

        /// <summary>
        /// Check that passing a null or incorrect length delimiter to the FormatTime method throws an ArgumentOutOfRangeException
        /// </summary>
        [Fact]
        public void WTTTime_Method_ArgumentOutOfRangeException_FormattedTime()
        {
            GroundFrame.Classes.WTT.WTTTime TestTime = new Classes.WTT.WTTTime(60, "H");
            Assert.Throws<ArgumentOutOfRangeException>(() => TestTime.FormatTime(WTTTimeFormat.ShortFormat,""));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestTime.FormatTime(WTTTimeFormat.ShortFormat, "::"));
            Assert.Throws<ArgumentNullException>(() => TestTime.FormatTime(WTTTimeFormat.ShortFormat, null));
        }

        /// <summary>
        /// Check that passing a null or incorrect length delimiter to the FormatTime method throws an ArgumentNullException
        /// </summary>
        [Fact]
        public void WTTTime_Method_ArgumentNullException_FormattedTime()
        {
            GroundFrame.Classes.WTT.WTTTime TestTime = new Classes.WTT.WTTTime(60, "H");
            Assert.Throws<ArgumentNullException>(() => TestTime.FormatTime(WTTTimeFormat.ShortFormat, null));
        }

        /// <summary>
        /// Check that FormatTime method returns the correct value when the object is instantiated from just the seconds
        /// </summary>
        [Theory]
        [InlineData(60, WTTTimeFormat.ShortFormat, @"\", @"00\01")]
        [InlineData(90, WTTTimeFormat.ShortFormat, @"t", "00t01H")]
        [InlineData(86400, WTTTimeFormat.ShortFormat, @"w", "24w00")]
        [InlineData(86460, WTTTimeFormat.LongFormat, @"w", "(+1 Day) 00w01")]
        [InlineData(172860, WTTTimeFormat.ShortFormat, @"/", "48/01")]
        [InlineData(172890, WTTTimeFormat.LongFormat, @"w", "(+2 Days) 00w01H")]
        public void WTTTime_Method_FormattedTime(int Input, WTTTimeFormat Format, string Delimiter, string ExpectedValue)
        {
            GroundFrame.Classes.WTT.WTTTime TestTime = new Classes.WTT.WTTTime(Input, "H");
            Assert.Equal(ExpectedValue, TestTime.FormatTime(Format, Delimiter));
        }
    }
}
