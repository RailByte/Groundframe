using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.Timetables
{
    /// <summary>
    /// An enum representing the SimSig Accel Brake Index
    /// </summary>
    public enum WTTAccelBrakeIndex
    {
        VeryLowSlowFreight=0,
        LowStandardFreight=1,
        MediumInterCity=2,
        HighCommuter=3,
        VeryHighLocoMetro=4
    }

    /// <summary>
    /// An emum representing the SimSig Speed Class
    /// </summary>
    public enum WTTSpeedClassBitWise
    {
        EPSE=1,
        EPSD=2,
        HST=4,
        EMU=8,
        DMU=16,
        SP=32,
        CS=64,
        MGR=128,
        TGV=256,
        LocoH=512,
        Metro=1024,
        WES=2048,
        Tripcock=4096,
        Steam=8192,
        Sim1=16777216,
        Sim2=33554432,
        Sim3=67108864,
        Sim4=134217728
    }

    /// <summary>
    /// An emum representing the SimSig Power To Weight Class
    /// </summary>
    public enum WTTPowerToWeightCategory
    {
        Normal=0,
        Light=1,
        Heavy=2
    }
}
