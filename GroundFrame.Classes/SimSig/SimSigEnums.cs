using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes.SimSig
{
	/// <summary>
	/// Enum representing the different types of Location Types
	/// </summary>
    public enum SimSigLocationType
    {
		/// <summary>
		/// Unknown loction type. Used if the location type is not recorded in the GroundFrame.SQL database
		/// </summary>
        Unknown = 0,
		/// <summary>
		/// Station
		/// </summary>
        Station = 1,
		/// <summary>
		/// Junction
		/// </summary>
		Junction = 2,
		/// <summary>
		/// Yard or Siding
		/// </summary>		 
		YardSiding = 3,
		/// <summary>
		/// Depot
		/// </summary>
		Depot = 4,
		/// <summary>
		/// Timing Point
		/// </summary>
		TimingPoint = 5,
		/// <summary>
		/// Reversing Point
		/// </summary>
		ReversingPoint = 6,
		/// <summary>
		/// A Seeding Point
		/// </summary>
		SeedPoint = 7
	}
}
