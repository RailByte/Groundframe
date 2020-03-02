using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroundFrame.Core
{
    /// <summary>
    /// A class that represents a mapping between a SimSig simulatation location node and the GroundFrame.SQL database
    /// </summary>
    public class MapperLocationNode
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private SimSig.LocationNode _LocationNode; //Private variable to store the location node

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
        
        /// <summary>
        /// Gets the created location node
        /// </summary>
        public SimSig.LocationNode LocationNode { get { return this._LocationNode; } }

        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods


        /// <summary>
        /// Creates the mapped location in the Target Simulation
        /// </summary>
        /// <param name="SQLConnector">The target simulation where the location should be created</param>
        /// <param name="TargetSimulation">A GFSqlConnector object connected to the GroundFrame.SQL database</param>
        /// <param name="Version">The version under which the location node was created</param>
        /// <param name="SimEra">The era for which the location node is valid</param>
        /// <param name="Location">The location against which the location node will be created</param>
        public void CreateLocationNode(ref SimSig.SimulationExtension TargetSimulation, GFSqlConnector SQLConnector, SimSig.Location Location, SimSig.Version Version, SimSig.SimulationEra SimEra)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());
            ArgumentValidation.ValidateSimulation(TargetSimulation, Globals.UserSettings.GetCultureInfo());
            ArgumentValidation.ValidateVersion(Version, Globals.UserSettings.GetCultureInfo());

            //Default Value
            Electrification DefaultElectrification = new Electrification("D");

            //Instantiate Location Node
            SimSig.LocationNode NewLocationNode = new SimSig.LocationNode(TargetSimulation.ID, Location.ID, SimEra.ID, Version, this.Platform, DefaultElectrification, SimSig.SimSigLocationType.Unknown, null, false, this.Line, this.Path, SQLConnector);
           
            if (TargetSimulation.LocationNodes.Exists(NewLocationNode) == false)
            {
                //Save to GroundFrame.SQL database
                NewLocationNode.SaveToSQLDB();
                //Add location node to MapperLocationNode
                this._LocationNode = NewLocationNode;
                //Add location to the simulation
                TargetSimulation.LocationNodes.Add(NewLocationNode);
            }

            //Dispose
            NewLocationNode.Dispose();
        }

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
