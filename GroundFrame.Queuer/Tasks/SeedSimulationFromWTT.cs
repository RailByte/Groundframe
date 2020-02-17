using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GroundFrame.Classes;
using GroundFrame.Classes.SimSig;
using GroundFrame.Classes.Timetables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GroundFrame.Queuer.Tasks
{
    internal class SeedSimulationFromWTT : IQueuerRequest
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private List<QueuerResponse> _Responses; //Private variable to store the list of responses the exection makes
        private Simulation _Simulation; //Private variable to store the simulation to be seeded
        private WTT _TimeTable; //Prviate variable to store the TimeTableCollection which will be used to Seed the GroundFrame.SQL database
        private string _JSON; //Private variable to the store config JSON
        private GFSqlConnector _SQLConnector; //Private variable to the store GroundFrame.SQL connector
        private string _Key; //Private variable to store the key of the parent process
        private string _Environment; //Private variable to store the environment of the parent process

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

        public SeedSimulationFromWTT(string Key, string AppUserAPIKey, string APIKey, string Environment, string JSON)
        {
            //Set private variables
            this._Key = Key;
            this._JSON = JSON;
            this._Environment = Environment;
            this._SQLConnector = Globals.GetGFSqlConnector(APIKey, AppUserAPIKey, Environment);
            ///Initialise the response
            this._Responses = new List<QueuerResponse>();
            this._Responses.Add(new QueuerResponse(this._Key, this._Environment, QueuerResponseStatus.Queued, "Process Queued", null));
        }

        #endregion Constructors

        #region Methods

        private void ParseJSON()
        {
            //Set up simulation
            this._Simulation = new Simulation(this.Config["simName"].ToString(), this.Config["simDescription"].ToString(), this.Config["simWikiLink"].ToString(), this.Config["simSimSigCode"].ToString(), this._SQLConnector);
        }

        public async Task Execute()
        {
            this._Responses.Add(new QueuerResponse(this._Key, this._Environment, QueuerResponseStatus.Running, "Process Started", null));
            this._Responses.Add(new QueuerResponse(this._Key, this._Environment, QueuerResponseStatus.Information, $"Checking to see whether simulation {this._Simulation.Name} already exists in the GroundFrame.SQL database", null));
        }


        #endregion Methods
    }
}
