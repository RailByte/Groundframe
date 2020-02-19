using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GroundFrame.Core;
using GroundFrame.Core.SimSig;
using GroundFrame.Core.Timetables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GroundFrame.Core.Queuer
{
    /// <summary>
    /// A class which manages a Seed Simulation from WTT task that uses a SimSig WTT file to seed the GroundFrame.SQL database with base simulation data
    /// </summary>
    /// <remarks>This task can only be performed once on each simulation so it's recommended the source WTT file is as complex as possible containing the most number of paths.
    /// Only Admins and Editors can perform this task and the resulting data will need to be tidied up (replacing codes with actual names) in the database once complete.
    /// </remarks>
    public class SeedSimulationFromWTT : IQueuerRequest, IDisposable
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<QueuerResponse> _Responses; //Private variable to store the list of responses the exection makes
        private Simulation _Simulation; //Private variable to store the simulation to be seeded
        private readonly string _JSON; //Private variable to the store config JSON
        private readonly GFSqlConnector _SQLConnector; //Private variable to the store GroundFrame.SQL connector

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the process response
        /// </summary>
        [JsonProperty("responses")]
        public List<QueuerResponse> Responses { get { return this._Responses; } }

        /// <summary>
        /// Gets the process configuation
        /// </summary>
        [JsonProperty("config")]
        public JObject Config { get { return JObject.Parse(this._JSON); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new SeedSimulationFromWTT object from the supplied object.
        /// </summary>
        /// <param name="AppUserAPIKey"></param>
        /// <param name="AppAPIKey"></param>
        /// <param name="Environment"></param>
        /// <param name="JSON"></param>
        public SeedSimulationFromWTT(string AppUserAPIKey, string AppAPIKey, string Environment, string JSON)
        {
            //Set private variables
            this._JSON = JSON;
            this._SQLConnector = Globals.GetGFSqlConnector(AppAPIKey, AppUserAPIKey, Environment);
            //Parse the config JSON
            this.ParseJSON();
            //Initialise the response
            this._Responses = new List<QueuerResponse>
            {
                new QueuerResponse(QueuerResponseStatus.Queued, "Process Queued", null)
            };
        }

        #endregion Constructors

        #region Methods

        private void ParseJSON()
        {
            //Set up simulation
            this._Simulation = new Simulation(this.Config["simName"].ToString(), this.Config["simDescription"] == null ? string.Empty: this.Config["simDescription"].ToString(), this.Config["simWikiLink"] == null ? string.Empty : this.Config["simWikiLink"].ToString(), this.Config["simSimSigCode"].ToString(), this._SQLConnector);
        }

        /// <summary>
        /// Executes the SeedSimulationFromWTT task
        /// </summary>
        /// <returns></returns>
        public async Task Execute()
        {
            this._Responses.Add(new QueuerResponse(QueuerResponseStatus.Running, "Process Started", null));
            this._Responses.Add(new QueuerResponse(QueuerResponseStatus.Information, $"Checking to see whether simulation {this._Simulation.Name} already exists in the GroundFrame.SQL database", null));
        }

        /// <summary>
        /// Replaces the responses with an updated list of responses.
        /// </summary>
        /// <param name="Responses">The list of QueuerResponse object to replace the existing responses</param>
        public void ReplaceResponses(List<QueuerResponse> Responses)
        {
            this._Responses = Responses;
        }

        /// <summary>
        /// Disposes the VersionCollection object
        /// </summary>
        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of the Dispose Method
        /// </summary>
        /// <param name="disposing">Indicates whether the object is currently being diposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                this._SQLConnector.Dispose();
            }
            else
            {
                //Dispose of any supporting objects here
            }
        }

        #endregion Methods
    }
}
