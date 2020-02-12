using System;
using System.Collections.Generic;
using System.Text;
using GroundFrame.Classes.SimSig;
using GroundFrame.Classes.Timetables;

namespace GroundFrame.Queuer.Tasks
{
    internal class SeedSimulationFromWTT : IQueuerRequest
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private QueuerResponse _Response; //Private variable to store the response
        private Simulation _Simulation; //Private variable to store the simulation to be seeded
        private WTT _TimeTable; //Prviate variable to store the TimeTableCollection which will be used to Seed the GroundFrame.SQL database

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the process response
        /// </summary>
        public QueuerResponse Response { get { return this._Response; } }

        #endregion Properties

        #region Constructors

        public SeedSimulationFromWTT(string UserName, string BearerToken, string JSON)
        {
            ///Initialise the response
            this._Response = new QueuerResponse();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
