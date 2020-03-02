using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Core.SimSig
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

	/// <summary>
	/// Enum representing the bit wise values for a path edge direction
	/// </summary>
	public enum SimSigPathDirection
	{
		/// <summary>
		/// No direction recorded
		/// </summary>
		None = 0,
		/// <summary>
		/// The down direction
		/// </summary>
		Down = 1,
		/// <summary>
		/// The up direction
		/// </summary>
		Up = 2
	}
}
