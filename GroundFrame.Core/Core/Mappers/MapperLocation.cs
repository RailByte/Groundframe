using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Core
{
    /// <summary>
    /// A class that represents a mapping between a SimSig simulatation location and the GroundFrame.SQL database
    /// </summary>
    public class MapperLocation
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private GroundFrame.Core.SimSig.Location _Location; //Private variable to store the saved location

        #region Properties

        /// <summary>
        /// Gets or sets the location name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location SimSig code
        /// </summary>
        public string SimSigCode { get; set; }

        /// <summary>
        /// Gets or sets whether the location is an entry point
        /// </summary>
        public bool IsEntryPoint { get; set; }

        /// <summary>
        /// Gets the location created by the Mapper in the target simulation
        /// </summary>
        public GroundFrame.Core.SimSig.Location Location { get { return this._Location; } }

        #endregion Private Variabls

        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates the mapped location in the Target Simulation
        /// </summary>
        /// <param name="SQLConnector">The target simulation where the location should be created</param>
        /// <param name="TargetSimulation">A GFSqlConnector object connected to the GroundFrame.SQL database</param>
        public void CreateLocation(ref GroundFrame.Core.SimSig.Simulation TargetSimulation, GFSqlConnector SQLConnector)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());
            ArgumentValidation.ValidateSimulation(TargetSimulation, Globals.UserSettings.GetCultureInfo());
            //Instantiate Location
            GroundFrame.Core.SimSig.Location NewLocation = new SimSig.Location(TargetSimulation, this.Name, null, this.SimSigCode, this.IsEntryPoint, SimSig.SimSigLocationType.Unknown, SQLConnector);
            //Save to GroundFrame.SQL database
            NewLocation.SaveToSQLDB();
            //Add location to MapperLocation
            this._Location = NewLocation;
            //Dispose
            NewLocation.Dispose();
        }

        #endregion Methods
    }

    /// <summary>
    /// Class for handling MapperLocation equality comparer
    /// </summary>
    internal class MapperLocationEqualityComparer : IEqualityComparer<MapperLocation>
    {
        public bool Equals(MapperLocation x, MapperLocation y)
        {
            // Two items are equal if their keys are equal.
            return x.Name == y.Name;
        }

        /// <summary>
        /// Gets the HashCode
        /// </summary>
        /// <param name="obj">The source object</param>
        /// <returns>Returns the HashCode</returns>
        public int GetHashCode(MapperLocation obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "MatterLocation" }, Globals.UserSettings.GetCultureInfo()));
            }

            return obj.Name.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}
