using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// A class that represents a mapping between a SimSig simulatation location node and the GroundFrame.SQL database
    /// </summary>
    public class MapperLocationNode
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the location SimSig code
        /// </summary>
        public string SimSigCode { get; set; }

        /// <summary>
        /// Gets or sets the platform
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the line
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Gets or sets the path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the next location node
        /// </summary>
        public MapperLocationNode NextLocationNode { get; set; }

        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods
        #endregion Methods
    }

    /// <summary>
    /// Class for handling MapperLocationNode equality comparer
    /// </summary>
    internal class MapperLocationNodeEqualityComparer : IEqualityComparer<MapperLocationNode>
    {
        public bool Equals(MapperLocationNode x, MapperLocationNode y)
        {
            // Two items are equal if their keys are equal.

            if (x == null && y != null)
            {
                return false;
            } 
            else if (x != null && y == null)
            {
                return false;

            }
            else if (x == null && y == null)
            {
                return true;

            }
            else if (x.NextLocationNode != null && y.NextLocationNode == null)
            {
                return false;

            }
            else if (x.NextLocationNode == null && y.NextLocationNode != null)
            {
                return false;

            }
            else if (x.NextLocationNode == null && y.NextLocationNode == null)
            {
                return x.SimSigCode == y.SimSigCode && x.Platform == y.Platform && x.Line == y.Line && x.Path == y.Path;
            }
            else
            {
                return x.SimSigCode == y.SimSigCode && x.Platform == y.Platform && x.Line == y.Line && x.Path == y.Path && x.NextLocationNode.SimSigCode == y.NextLocationNode.SimSigCode && x.NextLocationNode.Platform == y.NextLocationNode.Platform;
            }

        }

        /// <summary>
        /// Gets the HashCode
        /// </summary>
        /// <param name="obj">The source object</param>
        /// <returns>Returns the HashCode</returns>
        public int GetHashCode(MapperLocationNode obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new object[] { "MapperLocationNode" }, Globals.UserSettings.GetCultureInfo()));
            }

            return obj.SimSigCode.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}
