﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroundFrame.Core.UnitTests.TimeTable.WTTCommon
{
    public class WTTSpeed
    {
        /// <summary>
        /// Check instantiating a new WTTSpeed object assigns the MPH Correctly.
        /// </summary>
        [Fact]
        public void WTTSpeed_Prop_MPH()
        {
            GroundFrame.Core.Timetables.WTTSpeed TestSpeed = new GroundFrame.Core.Timetables.WTTSpeed(100);
            Assert.Equal(100, TestSpeed.MPH);
        }

        /// <summary>
        /// Check instantiating a new WTTSpeed object gets the KPH Correctly.
        /// </summary>
        [Fact]
        public void WTTSpeed_Prop_KPH()
        {
            GroundFrame.Core.Timetables.WTTSpeed TestSpeed = new GroundFrame.Core.Timetables.WTTSpeed(100);
            Assert.Equal(160.900M, TestSpeed.KPH);
        }

        /// <summary>
        /// Check the instantiating a new WTTSpeed object an MPH <= zero throws an exception
        /// </summary>
        [Fact]
        public void WTTSpeed_Prop_ArgumentException_Seconds()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GroundFrame.Core.Timetables.WTTSpeed(0));
        }
    }
}
