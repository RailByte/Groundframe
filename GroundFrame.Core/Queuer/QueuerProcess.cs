using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroundFrame.Core;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace GroundFrame.Core.Queuer
{
    /// <summary>
    /// A class representing a Queuer process. A process can either be executed immediately or can be executed asynchronously by the Queuer service
    /// </summary>
    /// <remarks>When running async the process key can be called to the Queuer service to get an update. The queuer service should also push a completed message back to the users queue</remarks>
    public class QueuerProcess
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private IQueuerRequest _Request; //Private variable to store the request
        private string _Key; //Private varialbe to store the unique key for this process
        private string _AppAPIKey; //Private variable to store the application API Key which requested the process
        private string _AppUserAPIKey; //Private variable to store the application user API key of the user who requested the process
        private readonly string _BearerToken; //Private variable to store the bearer token. This will be used to authentiate the user
        private bool _Authenticated = false; //Private variable to indicate whether the user has authenticated
        private DateTimeOffset _QueueTime = DateTimeOffset.UtcNow; //Prviate variable to store the 
        private readonly string _JSON; //Private variable to store the config JSON
        private readonly string _Environment; //Private variable to store the environment
        private bool _ExecuteNow; //Private variable to indicuate whether the process should be executed now
        private ObjectId _ID; //Private variable to store the MongoDB _id

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the MongoDB ID of for the process
        /// </summary>
        [BsonId]
        [JsonProperty("_id")]
        public ObjectId ID { get { return this._ID; } }

        /// <summary>
        /// Gets the Request information
        /// </summary>
        [JsonProperty("request")]
        public IQueuerRequest Request { get { return this._Request; } }
        
        /// <summary>
        /// Gets the process response
        /// </summary>
        [JsonIgnore]
        public QueuerResponse Response { get { return this._Request == null ? null : this._Request.Responses == null ? null : this._Request.Responses.Count == 0 ? null : this._Request.Responses.Where(x => x.Status != QueuerResponseStatus.Information).Last();  } }

        /// <summary>
        /// Gets the latest status of the process
        /// </summary>
        [JsonProperty("status")]
        public QueuerResponseStatus Status { get { return this.Response.Status; } }


        /// <summary>
        /// Gets the key of the process
        /// </summary>
        [JsonProperty("key")]
        public string Key { get { return this._Key; } }

        /// <summary>
        /// Gets the Application API Key used to initiate the process
        /// </summary>
        [JsonProperty("appApiKey")]
        public string AppAPIKey { get { return this._AppAPIKey; } }

        /// <summary>
        /// Gets the Application User API Key used to initiate the process
        /// </summary>
        [JsonProperty("appUserApiKey")]
        public string AppUserAPIKey { get { return this._AppUserAPIKey; } }

        /// <summary>
        /// Gets the time the process was queued
        /// </summary>
        [JsonProperty("queueTime")]
        public DateTimeOffset QueueTime { get { return this._QueueTime; } }

        /// <summary>
        /// Gets the flag to indicate whether the process should be executed straight away
        /// </summary>
        [JsonProperty("executeNow")]
        public bool ExecuteNow { get { return this._ExecuteNow; } }

        /// <summary>
        /// Gets the task type full name
        /// </summary>
        [JsonProperty("taskType")]
        public string TaskType { get { return this._Request.GetType().FullName;  } }

        /// <summary>
        /// Gets the flag which indicates whether the users was autenticated at the time the request was queued
        /// </summary>
        [JsonProperty("authenticated")]
        public bool Authenticated { get { return this._Authenticated; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a QueuerProess object from the values provided. The Queue date will be current UTC
        /// </summary>
        /// <param name="AppUserAPIKey">The Users API Key</param>
        /// <param name="AppAPIKey">The API Key of the application requesting a proces</param>
        /// <param name="BearerToken">The bearer token so the user can be authenticated</param>
        /// <param name="JSON">The JSON containing the configuration</param>
        /// <param name="Environment">The environement where the process should be run</param>
        /// <param name="ExecuteNow">A flag to indicate whether the process should be executed now. If false the process will be queued and executed by the Queuer Service</param>
        /// <remarks>The user will be authenticated at the point the process is queued but doesn't need to be authenticated at the process starts</remarks>
        public QueuerProcess(string AppUserAPIKey, string AppAPIKey, string BearerToken, string Environment, string JSON, bool ExecuteNow = false)
        {
            this._AppUserAPIKey = AppUserAPIKey;
            this._AppAPIKey = AppAPIKey;
            this._Key = GenerateKey();
            this._JSON = JSON;
            this._Environment = Environment;
            this._QueueTime = DateTimeOffset.UtcNow;
            this._ExecuteNow = ExecuteNow;
            this._BearerToken = BearerToken;
            this.BuildProcess();
        }

        /// <summary>
        /// Instantiates a QueuerProess object from the values provided. The Queue date will be current UTC
        /// </summary>
        /// <param name="AppUserAPIKey">The Users API Key</param>
        /// <param name="AppAPIKey">The API Key of the application requesting a proces</param>
        /// <param name="JSON">The JSON containing the configuration</param>
        /// <param name="Environment">The environement where the process should be run</param>
        /// <param name="ExecuteNow">A flag to indicate whether the process should be executed now. If false the process will be queued and executed by the Queuer Service</param>
        public QueuerProcess(string AppUserAPIKey, string AppAPIKey, string Environment, string JSON, bool ExecuteNow = false)
        {
            this._AppUserAPIKey = AppUserAPIKey;
            this._AppAPIKey = AppAPIKey;
            this._Key = GenerateKey();
            this._JSON = JSON;
            this._Environment = Environment;
            this._QueueTime = DateTimeOffset.UtcNow;
            this._ExecuteNow = ExecuteNow;
            this.BuildProcess();
        }

        /// <summary>
        /// Instantiates a process from the supplied key and environment
        /// </summary>
        /// <param name="Key">The queue of the process</param>
        /// <param name="Environment">The environment the process is running</param>
        public QueuerProcess(string Key, string Environment)
        {
            this._Key = Key;
            this._Environment = Environment;
            this.GetFromDB();
        }

        [JsonConstructor]
        internal QueuerProcess(QueuerProcessSurrogate SurrogateQueuerProcess, string Environment)
        {
            this._AppAPIKey = SurrogateQueuerProcess.AppAPIKey;
            this._AppUserAPIKey = SurrogateQueuerProcess.AppUserAPIKey;
            this._QueueTime = SurrogateQueuerProcess.QueueTime;
            this._ExecuteNow = SurrogateQueuerProcess.ExecuteNow;
            this._Key = SurrogateQueuerProcess.Key;
            this._Environment = Environment;
            this._ID = new ObjectId(SurrogateQueuerProcess._id);
            this._Authenticated = SurrogateQueuerProcess.Authenticated;
            string ConfigJSON = JsonConvert.SerializeObject(SurrogateQueuerProcess.Request.Config);

            //Dictionary to store function mapping
            Dictionary<string, Action> ProcessMapping = new Dictionary<string, Action>
            {
                { "GroundFrame.Core.Queuer.SeedSimulationFromWTT", (() => this._Request = new SeedSimulationFromWTT(this._AppUserAPIKey, this._AppAPIKey, this._Environment, ConfigJSON)) }
            };

            //Map the task requested to the relevant IQueuerRequest object
            ProcessMapping[SurrogateQueuerProcess.TaskType]();

            this._Request.ReplaceResponses(SurrogateQueuerProcess.Request.Responses);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Builds the QueuerProess from the values provided
        /// </summary>
        private void BuildProcess()
        {
            this.AuthenticateUser();

            //Process the config
            this.ProcessConfig();
        }

        private void AuthenticateUser()
        {
            string BearerToken = this._BearerToken;
            Console.Write(BearerToken);
            //TODO: Create function to authenticate the user against the Auth0 provided and set the Authenticated flag
            this._Authenticated = true;
        }

        /// <summary>
        /// Serializes the QueuerProcess into a JSON string
        /// </summary>
        /// <returns>A JSON string representing the Queuer Process</returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Serializes the QueuerProcess into a BsonDocument
        /// </summary>
        /// <returns>A BsonDocument object representing the Queuer Process</returns>
        public BsonDocument ToBSON()
        {
            return BsonDocument.Parse(this.ToJSON()); 
        }


        /// <summary>
        /// Generates a unique key for the process
        /// </summary>
        /// <returns>A unique 16 character string</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "Key is just randon characters and therefore culture neutral")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Key is just randon characters and therefore culture neutral")]
        private static string GenerateKey()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// Converts the QueuerProcess object into a QueuerProcessSurrogate object
        /// </summary>
        /// <returns>Returns the converted QueuerProcessSurrogate object</returns>
        internal QueuerProcessSurrogate ToQueuerProcessSurrogate()
        {
            return new QueuerProcessSurrogate()
            {
                AppAPIKey = this.AppAPIKey,
                AppUserAPIKey = this.AppUserAPIKey,
                QueueTime = this.QueueTime,
                ExecuteNow = this.ExecuteNow,
                TaskType = this.TaskType,
                Key = this.Key,
                _id = this.ID.ToString(),
                Authenticated = this.Authenticated
            };
        }

        private void ProcessConfig()
        {
            //Parse the JSON into a JOobject
            JObject JSONObject = JObject.Parse(this._JSON);

            //Dictionary to store function mapping
            Dictionary<string, Action> ProcessMapping = new Dictionary<string, Action>
            {
                { "SeedSimulationFromWTT", (() => this._Request = new SeedSimulationFromWTT(this._AppUserAPIKey, this._AppAPIKey, this._Environment, JSONObject["config"].ToString())) }
            };

            //Map the task requested to the relevant IQueuerRequest object
            ProcessMapping[JSONObject["task"].ToString()]();

            //Execute or Queue
            if (this._ExecuteNow)
            {
                Task.Run(() => ExecuteProcess());
            }
            else
            {
                //TODO: Queue the process for later
                this.SaveToDB();
            }
        }

        /// <summary>
        /// Executes the process
        /// </summary>
        private void ExecuteProcess()
        {
            //Execute the process
            Request.Execute();
        }

        /// <summary>
        /// Saves the QueuerProess object to the GroundFrame.MongoDB
        /// </summary>
        public void SaveToDB()
        {

            //Get the groundframeQueuer database

            using GFMongoConnector MongoConnector = Globals.GetGFMongoConnector(this._Environment);
            IMongoDatabase db = MongoConnector.MongoClient.GetDatabase("groundframeQueuer");
            //Get the processQueue collection
            var collection = db.GetCollection<BsonDocument>("processQueue");

            BsonDocument ProcessBSON; //Variable to store the BSON

            //If this _ID is empty then it's a new record
            if (this._ID == ObjectId.Empty)
            {
                //Gnerate a new ID
                this._ID = ObjectId.GenerateNewId();
                ProcessBSON = this.ToBSON();
                //Insert the document
                collection.InsertOne(ProcessBSON);
            }
            else
            {
                var filter = Builders<BsonDocument>.Filter.Eq("key", this.Key);
                ProcessBSON = this.ToBSON();
                collection.ReplaceOne(filter, ProcessBSON);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Filter string will always be culture neutral")]
        private void GetFromDB()
        {
            //BSON document to store the data from the GroundFrame.MongoDB
            BsonDocument Document;

            using (GFMongoConnector MongoConnector = Globals.GetGFMongoConnector(this._Environment))
            {
                IMongoDatabase db = MongoConnector.MongoClient.GetDatabase("groundframeQueuer");
                var collection = db.GetCollection<BsonDocument>("processQueue");

                string filter = string.Format(@"{{ key: '{0}'}}", this._Key);
                Document = collection.Find(filter).FirstOrDefault();
            }

            string JSON = JsonConvert.SerializeObject(BsonTypeMapper.MapToDotNetValue(Document));

            QueuerProcess TempQueuerProcess = JsonConvert.DeserializeObject<QueuerProcess>(JSON, new QueuerProcessConverter(this._Environment));

            this._Request = TempQueuerProcess.Request;
            this._Key = TempQueuerProcess.Key;
            this._AppAPIKey = TempQueuerProcess.AppAPIKey;
            this._AppUserAPIKey = TempQueuerProcess.AppUserAPIKey;
            this._Authenticated = TempQueuerProcess.Authenticated;
            this._ID = TempQueuerProcess.ID;
            this._ExecuteNow = TempQueuerProcess.ExecuteNow;
            this._QueueTime = TempQueuerProcess.QueueTime;
            this._Authenticated = TempQueuerProcess.Authenticated;
        }

        #endregion Methods
    }
}
