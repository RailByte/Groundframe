using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GroundFrame.Classes;
using GroundFrame.Classes.SimSig;
using GroundFrame.Classes.Timetables;
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

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the process response
        /// </summary>
        public List<QueuerResponse> Responses { get { return this._Responses; } }

        #endregion Properties

        #region Constructors

        public SeedSimulationFromWTT(string AppUserAPIKey, string APIKey, string JSON)
        {
            //Set private variables
            this._JSON = JSON;
            this._SQLConnector = Globals.GetGFSqlConnector(APIKey, AppUserAPIKey);
            ///Initialise the response
            this._Responses = new List<QueuerResponse>();
            Console.WriteLine("Hell");
        }

        #endregion Constructors

        #region Methods

        private void ParseJSON()
        {
            //Parse the JSON into a JOobject
            JObject JSONObject = JObject.Parse(this._JSON);

            //Set up simulation
            this._Simulation = new Simulation(JSONObject["simName"].ToString(), JSONObject["simDescription"].ToString(), JSONObject["simWikiLink"].ToString(), JSONObject["simSimSigCode"].ToString(), this._SQLConnector);
        }

        public async Task Execute()
        {
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Executing");
        }


        #endregion Methods
    }
}
