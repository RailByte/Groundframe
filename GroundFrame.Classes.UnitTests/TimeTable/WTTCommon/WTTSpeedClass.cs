using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GroundFrame.Classes.Timetables;

namespace GroundFrame.Classes.UnitTests.WTT.WTTCommon
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
        [Theory]
        [InlineData(1, new WTTSpeedClassBitWise[] { WTTSpeedClassBitWise.EPSE })]
        [InlineData(12, new WTTSpeedClassBitWise[] { WTTSpeedClassBitWise.HST, WTTSpeedClassBitWise.EMU })]
        public void WTTSpeed_Prop_BitWise(int BitWise, WTTSpeedClassBitWise[] BitWiseArray)
        {
            GroundFrame.Classes.Timetables.WTTSpeedClass TestSpeedClass = new GroundFrame.Classes.Timetables.WTTSpeedClass(BitWise);
            Assert.Equal(BitWiseArray, TestSpeedClass.SpeedClassList.ToArray());
            Assert.Equal(BitWise, TestSpeedClass.Bitwise);
        }

        /// <summary>
        /// Check that adding an additional WTTSpeedClassBitWise to a WTTSpeedClass returned the correct array of values
        /// </summary>
        [Theory]
        [InlineData(1, new WTTSpeedClassBitWise[] { WTTSpeedClassBitWise.EPSE, WTTSpeedClassBitWise.DMU })]
        [InlineData(12, new WTTSpeedClassBitWise[] { WTTSpeedClassBitWise.HST, WTTSpeedClassBitWise.EMU, WTTSpeedClassBitWise.DMU })]
        public void WTTSpeed_Method_Add(int BitWise, WTTSpeedClassBitWise[] BitWiseArray)
        {
            GroundFrame.Classes.Timetables.WTTSpeedClass TestSpeedClass = new GroundFrame.Classes.Timetables.WTTSpeedClass(BitWise);

            //Add DMU
            TestSpeedClass.Add(WTTSpeedClassBitWise.DMU);
            //Check DMU added
            Assert.Equal(BitWiseArray, TestSpeedClass.SpeedClassList.ToArray());
            
        }

        #endregion Methods
    }
}
