using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GroundFrame.Classes.WTT;

namespace GroundFrame.Classes.UnitTests.WTTTrainCategory
{
    /// <summary>
    /// Object representing a WTTSpeed Class unit test
    /// </summary>
    public class WTTSpeedClass
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check instantiating a new WTTSpeed Class Correctly
        /// </summary>
        /// <summary>
        /// Checks that a WTTElectrification object returns the correct flags from the supplied SimSig code
        /// </summary>
        [Theory]
        [InlineData(1, new WTTSpeedClassBitWise[] { WTTSpeedClassBitWise.EPSE })]
        [InlineData(12, new WTTSpeedClassBitWise[] { WTTSpeedClassBitWise.HST, WTTSpeedClassBitWise.EMU })]
        public void WTTSpeed_Prop_BitWise(int BitWise, WTTSpeedClassBitWise[] BitWiseArray)
        {
            GroundFrame.Classes.WTT.WTTSpeedClass TestSpeedClass = new GroundFrame.Classes.WTT.WTTSpeedClass(BitWise);
            Assert.Equal(BitWiseArray, TestSpeedClass.SpeedClassList.ToArray());
            Assert.Equal(BitWise, TestSpeedClass.Bitwise);
        }

        #endregion Methods
    }
}
