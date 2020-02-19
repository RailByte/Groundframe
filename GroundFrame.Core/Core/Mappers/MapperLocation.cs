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

        #endregion Private Variabls

        #region Properties
        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods
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
