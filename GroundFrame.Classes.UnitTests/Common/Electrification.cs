using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroundFrame.Classes.UnitTests.WTT.Common
{
    public class Electrification
    {
        /// <summary>
        /// Checks that a WTTElectrification object returns the correct flags from the supplied SimSig code
        /// </summary>
        [Theory]
        [InlineData("O", true, false, false)]
        [InlineData("O3", true, true, false)]
        [InlineData("O4", true, false, true)]
        [InlineData("O34", true, true, true)]
        [InlineData("34", false, true, true)]
        [InlineData("43", false, true, true)]
        [InlineData("3O", true, true, false)]
        [InlineData("4O", true, false, true)]
        [InlineData("O43", true, true, true)]
        [InlineData("3O4", true, true, true)]
        [InlineData("4O3", true, true, true)]
        [InlineData("34O", true, true, true)]
        [InlineData("43O", true, true, true)]
        public void Electrification_Constructor_SimSigCode(string SimSigCode, bool ExpectedOverhead, bool Expected3rdRail, bool Expected4thRail)
        {
            GroundFrame.Classes.Electrification TestElectrification = new GroundFrame.Classes.Electrification(SimSigCode);
            Assert.Equal(ExpectedOverhead, TestElectrification.Overhead);
            Assert.Equal(Expected3rdRail, TestElectrification.ThirdRail);
            Assert.Equal(Expected4thRail, TestElectrification.FourthRail);
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
