using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Core.Timetables
{
    /// <summary>
    /// An enum representing the SimSig Accel Brake Index
    /// </summary>
    public enum WTTAccelBrakeIndex
    {
        /// <summary>
        /// Very low - Slow freight
        /// </summary>
        VeryLowSlowFreight=0,
        /// <summary>
        /// Low - Standard freight
        /// </summary>
        LowStandardFreight=1,
        /// <summary>
        /// Medium - Intercity
        /// </summary>
        MediumInterCity=2,
        /// <summary>
        /// High - Commuter
        /// </summary>
        HighCommuter=3,
        /// <summary>
        /// Very High - Metro
        /// </summary>
        VeryHighLocoMetro=4
    }

    /// <summary>
    /// An emum representing the SimSig Speed Class
    /// </summary>
    public enum WTTSpeedClassBitWise
    {
        EPSE=1,
        EPSD=2,
        /// <summary>
        /// A High Speed Train (IC125, Meridian, Voyager etc.).
        /// </summary>
        HST = 4,
        /// <summary>
        /// An Electric Multiple Unit
        /// </summary>
        EMU = 8,
        /// <summary>
        /// A Diesel Multiple Unit
        /// </summary>
        DMU = 16,
        SP=32,
        CS=64,
        /// <summary>
        /// An MGR or heavy freigt train
        /// </summary>
        MGR=128,
        /// <summary>
        /// TGV / Eurostar train
        /// </summary>
        TGV=256,
        /// <summary>
        /// Locomotive hauled train
        /// </summary>
        LocoH=512,
        /// <summary>
        /// Metro train
        /// </summary>
        Metro=1024,
        WES=2048,
        /// <summary>
        /// Train fitted with Tripcock (London Underground)
        /// </summary>
        Tripcock=4096,
        /// <summary>
        /// Steam hauled train
        /// </summary>
        Steam=8192,
        /// <summary>
        /// Spare SimSig Speed Class 1
        /// </summary>
        Sim1=16777216,
        /// <summary>
        /// Spare SimSig Speed Class 2
        /// </summary>
        Sim2 = 33554432,
        /// <summary>
        /// Spare SimSig Speed Class 3
        /// </summary>
        Sim3 = 67108864,
        /// <summary>
        /// Spare SimSig Speed Class 4
        /// </summary>
        Sim4 = 134217728
    }

    /// <summary>
    /// An emum representing the SimSig Power To Weight Class
    /// </summary>
    public enum WTTPowerToWeightCategory
    {
        /// <summary>
        /// Default power to weight ratio
        /// </summary>
        Normal=0,
        /// <summary>
        /// Light power to weight ratio
        /// </summary>
        Light = 1,
        /// <summary>
        /// Heavy power to weight ratio
        /// </summary>
        Heavy = 2
    }
}
