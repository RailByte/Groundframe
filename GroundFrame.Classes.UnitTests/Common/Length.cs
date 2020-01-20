using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroundFrame.Classes.UnitTests.WTT.Common
{
    public class Length
    {
        /// <summary>
        /// Checks that a WTTLength object returns the correct decimal miles
        /// </summary>
        [Theory]
        [InlineData(1000, 0.62137000)]
        [InlineData(1, 0.00062137)]
        [InlineData(10000, 6.2137000)]
        public void Length_Prop_DecimalMiles(int Meters, decimal ExpectedValue)
        {
            GroundFrame.Classes.Length TestLength = new GroundFrame.Classes.Length(Meters);
            Assert.Equal(ExpectedValue, TestLength.DecimalMiles);
        }

        /// <summary>
        /// Checks that a WTTLength object returns the correct miles and chains
        /// </summary>
        [Theory]
        [InlineData(1000, "50CH")]
        [InlineData(1, "0CH")]
        [InlineData(10000, "6M 17CH")]
        public void Length_Prop_MilesAndChains(int Meters, string ExpectedValue)
        {
            GroundFrame.Classes.Length TestLength = new GroundFrame.Classes.Length(Meters);
            Assert.Equal(ExpectedValue, TestLength.MilesAndChains);
        }
    }
}
