using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GroundFrame.Classes
{
    public enum EraType
    {
        WTT = 1,
        Template = 2
    }

    public class SimulationEra
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private int _ID; //Stores the GroundFrame.SQL Database ID

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Get the GroundFrame.SQL Database ID
        /// </summary>
        [Key]
        public int ID { get { return this._ID; } }

        /// <summary>
        /// Gets the Name of the Simulation Era
        /// </summary>
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// Gets the Description of the Simulation Era
        /// </summary>
        [Required]
        public string Description { get; set; }

        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods
        #endregion Methods  
    }
}
