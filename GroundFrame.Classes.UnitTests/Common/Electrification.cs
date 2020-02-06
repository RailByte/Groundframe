using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroundFrame.Classes.UnitTests.TimeTable.Common
{
    public class Electrification
    {
        /// <summary>
        /// Checks that a WTTElectrification object returns the correct flags from the supplied SimSig code
        /// </summary>
        [Theory]
        [InlineData("O", true, false, false, false)]
        [InlineData("O3", true, true, false, false)]
        [InlineData("O4", true, false, true, false)]
        [InlineData("O34", true, true, true, false)]
        [InlineData("34", false, true, true, false)]
        [InlineData("43", false, true, true, false)]
        [InlineData("3O", true, true, false, false)]
        [InlineData("4O", true, false, true, false)]
        [InlineData("O43", true, true, true, false)]
        [InlineData("3O4", true, true, true, false)]
        [InlineData("4O3", true, true, true, false)]
        [InlineData("34O", true, true, true, false)]
        [InlineData("43O", true, true, true, false)]
        [InlineData("X4", false, false, false, true)]
        public void Electrification_Constructor_SimSigCode(string SimSigCode, bool ExpectedOverhead, bool Expected3rdRail, bool Expected4thRail, bool Sim4)
        {
            GroundFrame.Classes.Electrification TestElectrification = new GroundFrame.Classes.Electrification(SimSigCode);
            Assert.Equal(ExpectedOverhead, TestElectrification.Overhead);
            Assert.Equal(Expected3rdRail, TestElectrification.ThirdRail);
            Assert.Equal(Expected4thRail, TestElectrification.FourthRail);
            Assert.Equal(Sim4, TestElectrification.Sim4);
        }
        /// <summary>
        /// Checks the passing an invalid SimSig Code will throw an ArgumentException
        /// </summary>
        /// <param name="SimSigCode"></param>
        [Theory]
        [InlineData("6G")]
        [InlineData("34G")]
        public void Electrification_Constructor_SimSigCode_CheckInvalidArgument(string SimSigCode)
        {
            Assert.Throws<ArgumentException>(() => new GroundFrame.Classes.Electrification(SimSigCode));
        }
    }
}
