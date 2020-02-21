using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using GroundFrame.Core;
using GroundFrame.Core.SimSig;
using GroundFrame.Core.Timetables;
using Microsoft.Extensions.Configuration;
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

        private ExtendedList<QueuerResponse> _Responses; //Private variable to store the list of responses the exection makes
        private Simulation _Simulation; //Private variable to store the simulation to be seeded
        private WTT _TimeTable; //Prvate variable to store the timetable
        private readonly string _JSON; //Private variable to the store config JSON
        private readonly GFSqlConnector _SQLConnector; //Private variable to the store GroundFrame.SQL connector
        private readonly IConfigurationRoot _Config; //Private variable to the config
        private readonly bool _Authenticated; //Private variable to indicate whether the user was authenticated at the point of queue

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the process response
        /// </summary>
        [JsonProperty("responses")]
        public ExtendedList<QueuerResponse> Responses { get { return this._Responses; } }

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
        /// <param name="AppUserAPIKey">The application user API key who queued the process</param>
        /// <param name="AppAPIKey">The API Key of the application which queued the process</param>
        /// <param name="Environment">The environment where the task is being executed. This should be proided by the running application</param>
        /// <param name="JSON">The JSON containing the configuration for the task (see documentation)</param>
        /// <param name="Authenticated">A flag to indicate whether the user was authenticated at the point of queuing the process</param>
        internal SeedSimulationFromWTT(string AppUserAPIKey, string AppAPIKey, string Environment, string JSON, bool Authenticated)
        {
            //Set private variables
            this._JSON = JSON;
            this._Authenticated = Authenticated;
            this._SQLConnector = Globals.GetGFSqlConnector(AppAPIKey, AppUserAPIKey, Environment);
            this._Config = Globals.GetConfig(Environment);
            //Initialise the response
            this._Responses = new ExtendedList<QueuerResponse>
            {
                new QueuerResponse(QueuerResponseStatus.Queued, "Process Queued", null)
            };
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Executes the SeedSimulationFromWTT task
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "All conversion will be in English")]
        public async Task<QueuerResponseStatus> Execute()
        {
            bool DebugMode = Convert.ToBoolean(this._Config["debugMode"]);

            //Add process started Response
            this._Responses.Add(new QueuerResponse(QueuerResponseStatus.Running, "processStarted", null));

            //Check user was authenticated at time of queue
            if (this._Authenticated == false)
            {
                this._Responses.Add(new QueuerResponse(QueuerResponseStatus.CompletedWithWarning, "processUserNotAuthenticatedAtQueue", null));
                return QueuerResponseStatus.CompletedWithWarning;
            }

            try
            {
                //Initialise simulation
                this._Simulation = new Simulation(this.Config["simName"].ToString(), this.Config["simDescription"] == null ? string.Empty: this.Config["simDescription"].ToString(), this.Config["simWikiLink"] == null ? string.Empty : this.Config["simWikiLink"].ToString(), this.Config["simSimSigCode"].ToString(), this._SQLConnector);

                //Declare the individual async tasks
                Task<WTT> SourceWTT = this.LoadTimeTable();
                Task<bool> SimulationExists = this.CheckSimulationExists();

                //Declare list of all the async tasks
                List<Task> allTasks = new List<Task> { SourceWTT, SimulationExists };

                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks).ConfigureAwait(false);
                    if (finished == SourceWTT)
                    {
                        if (DebugMode)this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed build the WTT object from the source WTT file", null));
                        Console.WriteLine($"{DateTime.UtcNow.ToShortTimeString()} Reading WTT Complete");
                        this._TimeTable = SourceWTT.Result;
                    }
                    else if (finished == SimulationExists)
                    {
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed checking if the simulation already exists inthe GroundFrame.SQL database", null));
                        Console.WriteLine($"{DateTime.UtcNow.ToShortTimeString()} Checking Sim Exists");
                    }
                    allTasks.Remove(finished);
                }

                //Check is simulation already exists in the data. If it does save the simulation to the GroundFrame.SQL database
                if (SimulationExists.Result == true)
                {
                    this._Responses.Add(new QueuerResponse(QueuerResponseStatus.CompletedWithWarning, "processSimulationAlreadyExists", null));
                    return QueuerResponseStatus.CompletedWithWarning;
                } else
                {
                    try
                    {
                        this._Simulation.SaveToSQLDB();
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, $"Simulation saved to the GroundFrame.SQL database. ID = {this._Simulation.ID}.", null));
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception("GG", Ex);
                    }
                }

                Task<MapperLocation> GetLocations = this._TimeTable.TimeTables.GetMapperLocations();


                this._Responses.Add(new QueuerResponse(QueuerResponseStatus.Success, "processSuccess", null));
                return QueuerResponseStatus.Success;
            }
            catch (AggregateException Ex)
            {
                ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
                string ExceptionMessage = ExceptionMessageResources.GetString("QueuerGenericFailureMessage", Globals.UserSettings.GetCultureInfo());

                foreach (Exception Inner in Ex.InnerExceptions)
                {
                    this._Responses.Add(new QueuerResponse(QueuerResponseStatus.CompletedWithWarning, ExceptionMessage, Inner));
                }

                return QueuerResponseStatus.Failed;
            }

            catch (Exception Ex)
            {
                ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
                string ExceptionMessage = ExceptionMessageResources.GetString("QueuerGenericFailureMessage", Globals.UserSettings.GetCultureInfo());
                this._Responses.Add(new QueuerResponse(QueuerResponseStatus.CompletedWithWarning, ExceptionMessage, Ex));
                return QueuerResponseStatus.Failed;
            }
        }


        /// <summary>
        /// Async task to load the WTT file into a WTT object
        /// </summary>
        /// <returns>A Task result containing the loaded WTT file</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "The culture is set in the ExceptionHelper.GetStaticExceptio overload")]
        private async Task<WTT> LoadTimeTable()
        {
            string WTTFile = null;
            JObject TaskConfig;

            try
            {
                TaskConfig = JObject.Parse(this._JSON);
                WTT SourceWTT = new WTT();
                WTTFile = TaskConfig["sourceWTT"].ToString();
                await Task.Run(() => { SourceWTT.LoadFromWTT(WTTFile); }).ConfigureAwait(false);

                return SourceWTT;
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("QueuerLoadWTTFromFileError", new object[] { WTTFile }), Ex);
            }
        }
        /// <summary>
        /// Async task to check to see whether the timetable exists
        /// </summary>
        /// <returns>A Task result containing the loaded WTT file</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "The culture is set in the ExceptionHelper.GetStaticException overload")]
        private async Task<bool> CheckSimulationExists()
        {
            try
            { 
                bool SimExists = false;
                await Task.Run(() => { SimExists = this._Simulation.Exists(); }).ConfigureAwait(false);           
                return SimExists;
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("QueuerCheckWTTExistsError", new object[] { this._Simulation.Name }), Ex);
            }
        }

        private async Task<List<MapperLocation>> GetLocationMapperFromWTT()
        {
            try
            {
                List<MapperLocation> WTTLocationMapper = null;
                await Task.Run(() => { WTTLocationMapper = this._TimeTable.TimeTables.GetMapperLocations(); }).ConfigureAwait(false);
                return WTTLocationMapper;
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("QueuerCheckWTTExistsError", new object[] { this._Simulation.Name }), Ex);
            }
        }

        /// <summary>
        /// Replaces the responses with an updated list of responses.
        /// </summary>
        /// <param name="Responses">The list of QueuerResponse object to replace the existing responses</param>
        public void ReplaceResponses(ExtendedList<QueuerResponse> Responses)
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
