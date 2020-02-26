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
        private readonly string _JSON; //Private variable to the store config JSON
        private readonly GFSqlConnector _SQLConnector; //Private variable to the store GroundFrame.SQL connector
        private readonly IConfigurationRoot _Config; //Private variable to the config
        private readonly bool _Authenticated; //Private variable to indicate whether the user was authenticated at the point of queue

        //Task specific variables

        private Simulation _Simulation; //Private variable to store the simulation to be seeded
        private WTT _TimeTable; //Prvate variable to store the timetable
        List<MapperLocation> _LocationMapper = new List<MapperLocation>(); //Private variable to store the Location Mapper for the WTT
        List<MapperLocationNode> _LocationNodeMapper = new List<MapperLocationNode>(); //Private variable to store the Location Node Mapper fro the WTT
        SimulationEra _TemplateSimEra; //Private variable to store the default simulation era
        SimSig.Version _Version; //Private variable to store the latest version

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is captures in the QueuerRequest responses and written to the GroundFrame.MongoDB database")]
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
#if DEBUG
                        Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- Completed reading WTT");
#endif
                        this._TimeTable = SourceWTT.Result;
                    }
                    else if (finished == SimulationExists)
                    {
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed checking if the simulation already exists inthe GroundFrame.SQL database", null));
#if DEBUG
                        Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- Completed checking whether the sim Exists");
#endif
                    }
                    allTasks.Remove(finished);
                }

                //Check is simulation already exists in the data. If it does save the simulation to the GroundFrame.SQL database
                if (SimulationExists.Result == true)
                {
                    this._Responses.Add(new QueuerResponse(QueuerResponseStatus.CompletedWithWarning, "processSimulationAlreadyExists", null));
#if DEBUG
                    Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- The simulation already exists so the process is stopped");
#endif
                    return QueuerResponseStatus.CompletedWithWarning;
                } 
                else
                {
                    try
                    {
                        this._Simulation.SaveToSQLDB();
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, $"Simulation saved to the GroundFrame.SQL database. ID = {this._Simulation.ID}.", null));
#if DEBUG
                        Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- Simulation saved to GroundFrame.SQL database");
#endif
                    }
                    catch (Exception Ex)
                    {
                        ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
                        string ExceptionMessage = ExceptionMessageResources.GetString("QueuerErrorSavingSimulation", Globals.UserSettings.GetCultureInfo());
                        throw new Exception(ExceptionMessage, Ex);
                    }
                }

                //Build the next stage of the task by setting the individual lasts
                Task<List<MapperLocation>> GetLocationMapperTask = this.GetLocationMapperFromWTT(); //Gets the location mapper list from the WTT
                Task<List<MapperLocationNode>> GetLocationNodeMapperTask = this.GetLocationNodeMappperFromWTT(); //Gets the location node mapper list from the WTT
                Task<SimSig.Version> SimVersionTask = this.GetSimSigVersion(); //Gets SimSig version requested from the GroundFrame.SQL database
                Task<SimSig.SimulationEra> TemplateSimEraTask = this.GetSimTemplateEra(); //Gets the era template for the simulation

                //Build All List task
                allTasks = new List<Task> { GetLocationMapperTask, GetLocationNodeMapperTask, SimVersionTask, TemplateSimEraTask };

                //Wait for the tasks to finish
                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks).ConfigureAwait(false);
                    if (finished == GetLocationMapperTask)
                    {
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed getting the Location Mapper from the source WTT", null));
#if DEBUG
                        Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- Completed getting Location Mapper");
#endif
                        _LocationMapper = GetLocationMapperTask.Result;
                    }
                    else if (finished == GetLocationNodeMapperTask)
                    {
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed getting the Location Node Mapper from the source WTT", null));
#if DEBUG
                        Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- Completed getting Location Node Mapper");
#endif
                        _LocationNodeMapper = GetLocationNodeMapperTask.Result;
                    }
                    else if (finished == SimVersionTask)
                    {
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed getting the Simulation version from the GroundFrame.SQL database", null));
#if DEBUG
                        Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- Completed getting SimSig version");
#endif
                        _Version = SimVersionTask.Result;
                    }
                    else if (finished == TemplateSimEraTask)
                    {
                        if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed getting the Simulation template era from the GroundFrame.SQL database", null));
#if DEBUG
                        Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- Completed getting the template era");
#endif
                        _TemplateSimEra = TemplateSimEraTask.Result;
                    }
                    allTasks.Remove(finished);
                }

                //Next create the locations in the GroundFrame.SQL database
                await CreateLocationsFromMap().ConfigureAwait(false);

                if (DebugMode) this._Responses.Add(new QueuerResponse(QueuerResponseStatus.DebugMesssage, "Completed create the locations in the GroundFrame.SQL database", null));
#if DEBUG
                using SimulationExtension SimExtention = new SimulationExtension(this._Simulation.ID, this._SQLConnector);
                Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}:- The Simulation contains {SimExtention.Locations.Count} location(s).");
#endif

                this._Responses.Add(new QueuerResponse(QueuerResponseStatus.Success, "processSuccess", null));
                return QueuerResponseStatus.Success;
            }
            catch (AggregateException Ex)
            {
                ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
                string ExceptionMessage = ExceptionMessageResources.GetString("QueuerGenericFailureMessage", Globals.UserSettings.GetCultureInfo());

                foreach (Exception Inner in Ex.InnerExceptions)
                {
                    this._Responses.Add(new QueuerResponse(QueuerResponseStatus.Failed, ExceptionMessage, Inner));
                }

                return QueuerResponseStatus.Failed;
            }

            catch (Exception Ex)
            {
                ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
                string ExceptionMessage = ExceptionMessageResources.GetString("QueuerGenericFailureMessage", Globals.UserSettings.GetCultureInfo());
                this._Responses.Add(new QueuerResponse(QueuerResponseStatus.Failed, ExceptionMessage, Ex));
                return QueuerResponseStatus.Failed;
            }
        }

        /// <summary>
        /// Creates a location in the simulation from each MapperLocation object in the LocationMapper list. The new location is available in the Location property of the MapperLocation object once created
        /// </summary>
        /// <returns></returns>
        private async Task CreateLocationsFromMap()
        {
            await Task.Run(() =>
            {
                foreach (MapperLocation MappedLoc in this._LocationMapper)
                {
                    MappedLoc.CreateLocation(ref this._Simulation, this._SQLConnector);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Find and returns a location from the LocationMapper list where the SimSig code matches the requested code
        /// </summary>
        /// <param name="SimSigCode">SimSig code for the requested location</param>
        /// <returns>A location object for the supplied SimSig code</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "SimSig codes are always in en-GB")]
        private Location GetLocationFromMapperBySimSigCode(string SimSigCode)
        {
            return this._LocationMapper.Find(x => x.Location.SimSigCode.ToLower() == SimSigCode.ToLower()).Location;
        }

        private async Task CreateLocationNodesFromMap()
        {
            await Task.Run(() =>
            {
                foreach (MapperLocationNode MappedLocNode in this._LocationNodeMapper)
                {
                    //Get the location
                    Location NodeLocation = GetLocationFromMapperBySimSigCode(MappedLocNode.SimSigCode);
                    //Build new LocationNode
                    //TODO: Build the new location node
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Async task to get the latest simulation version in the system
        /// </summary>
        /// <returns>A Task result containing the loaded WTT file</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "The culture is set in the ExceptionHelper.GetStaticExceptio overload")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "The version number will always be in en-GB culture")]
        private async Task<SimSig.Version> GetSimSigVersion()
        {
            decimal VersionNumber = 0M;
            JObject TaskConfig;

            try
            {
                TaskConfig = JObject.Parse(this._JSON);
                VersionNumber = Convert.ToDecimal(TaskConfig["version"]);
                SimSig.Version SimSigVersion = new SimSig.Version(this._SQLConnector);
                await Task.Run(() => { SimSigVersion.GetFromSQLDBByVersionNumber(VersionNumber); }).ConfigureAwait(false);

                return SimSigVersion;
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("QueuerGetVersionFromGroundFrameSQLDB", new object[] { VersionNumber }), Ex);
            }
        }

        /// <summary>
        /// Async task to get the template era for the simulation
        /// </summary>
        /// <returns>A Task result containing the loaded WTT file</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "The culture is set in the ExceptionHelper.GetStaticExceptio overload")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "The version number will always be in en-GB culture")]
        private async Task<SimulationEra> GetSimTemplateEra()
        {
            try
            {
                List<SimSig.SimulationEra> SimEras = null;
                await Task.Run(() => { SimEras = this._Simulation.GetSimulationEras(); }).ConfigureAwait(false);

                return SimEras.Find(x => x.Type == EraType.Template);
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("QueuerGetTemplateSimEraFromGroundFrameSQLDB", new object[] { this._Simulation.Name }), Ex);
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

        /// <summary>
        /// Async task to get the Location Mapper from the TimeTable
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "The culture is set in the ExceptionHelper.GetStaticException overload")]
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
                throw new Exception(ExceptionHelper.GetStaticException("RetrieveLocationMapperError", new object[] { this._Simulation.Name }), Ex);
            }
        }

        /// <summary>
        /// Async task to get the Location Node Mapper from the TimeTable
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "The culture is set in the ExceptionHelper.GetStaticException overload")]
        private async Task<List<MapperLocationNode>> GetLocationNodeMappperFromWTT()
        {
            try
            {
                List<MapperLocationNode> WTTLocationNodeMapper = null;
                await Task.Run(() => { WTTLocationNodeMapper = this._TimeTable.TimeTables.GetMapperLocationNodes(); }).ConfigureAwait(false);
                return WTTLocationNodeMapper;
            }
            catch (Exception Ex)
            {
                throw new Exception(ExceptionHelper.GetStaticException("RetrieveLocationNodeMapperError", new object[] { this._Simulation.Name }), Ex);
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
                this._Simulation.Dispose();
            }
            else
            {
                //Dispose of any supporting objects here
            }
        }

#endregion Methods
    }
}
