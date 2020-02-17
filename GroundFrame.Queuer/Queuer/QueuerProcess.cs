using GroundFrame.Queuer.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroundFrame.Classes;
using MongoDB.Driver;

namespace GroundFrame.Queuer
{
    public class QueuerProcess
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private IQueuerRequest _Request; //Private variable to store the request
        private DateTime _StartTime; //Private variable to store the process execution start time
        private string _Key; //Private varialbe to store the unique key for this process
        private string _AppAPIKey; //Private variable to store the application API Key which requested the process
        private string _AppUserAPIKey; //Private variable to store the application user API key of the user who requested the process
        private string _BearerToken; //Private variable to store the bearer token. This will be used to authentiate the user
        private bool _Authenticated = false; //Private variable to indicate whether the user has authenticated
        private string _APIKey; //Private variable to indicate the API key of the application which requested the process
        private DateTimeOffset _QueueTime = DateTimeOffset.UtcNow; //Prviate variable to store the 
        private string _JSON; //Private variable to store the config JSON
        private string _Environment; //Private variable to store the environment
        private bool _ExecuteNow; //Private variable to indicuate whether the process should be executed now

        #endregion Private Variables

        #region Properties

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
        public string AppAPIKey { get { return this._APIKey; } }

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
        public string TaskType {  get { return this._Request.GetType().FullName;  } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a QueuerProess object from the values provided. The Queue date will be current UTC
        /// </summary>
        /// <param name="AppUserAPIKey">The Users API Key</param>
        /// <param name="APIKey">The API Key of the application requesting a proces</param>
        /// <param name="BearerToken">The bearer token so the user can be authenticated</param>
        /// <param name="JSON">The JSON containing the configuration</param>
        public QueuerProcess(string AppUserAPIKey, string APIKey, string BearerToken, string Environment, string JSON, bool ExecuteNow = false)
        {
            this._QueueTime = DateTimeOffset.UtcNow;
            this._ExecuteNow = ExecuteNow;
            this.BuildProcess(AppUserAPIKey, APIKey, BearerToken, Environment, JSON);
        }

        public QueuerProcess(string Key, string Environment)
        {
            this._Key = Key;
            this._Environment = Environment;
            this.GetFromDB();
        }

        [JsonConstructor]
        private QueuerProcess(string AppUserAPIKey, string APIKey, DateTime QueueTime )
        {
            this._APIKey = APIKey;
            this._AppUserAPIKey = AppUserAPIKey;
            this._QueueTime = QueueTime;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Builds the QueuerProess from the values provided
        /// </summary>
        /// <param name="AppUserAPIKey">The Users API Key</param>
        /// <param name="APIKey">The API Key of the application requesting a proces</param>
        /// <param name="BearerToken">The bearer token so the user can be authenticated</param>
        /// <param name="JSON">The JSON containing the configuration</param>
        /// <param name="Environment">The environment where the process should be executed</param>
        private void BuildProcess(string AppUserAPIKey, string APIKey, string BearerToken, string Environment, string JSON)
        {
            this._AppUserAPIKey = AppUserAPIKey;
            this._APIKey = APIKey;
            this._Key = this.GenerateKey();
            this._BearerToken = BearerToken;
            this._JSON = JSON;
            this._Environment = Environment;
            //TODO: Authenticate the user
            this._Authenticated = true;
            //Process the config
            this.ProcessConfig();
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
        private string GenerateKey()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
        }

        private void ProcessConfig()
        {
            //Parse the JSON into a JOobject
            JObject JSONObject = JObject.Parse(this._JSON);

            //Dictionary to store function mapping
            Dictionary<string, Action> ProcessMapping = new Dictionary<string, Action>
            {
                { "SeedSimulationFromWTT", (() => this._Request = new SeedSimulationFromWTT(this._Key, this._AppUserAPIKey, this._AppAPIKey, this._Environment, JSONObject["config"].ToString())) }
            };

            //Mapp the task requested to the relevant IQueuerRequest object
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
            //Set the start time
            this._StartTime = DateTime.UtcNow;
            //Execute the process
            Request.Execute();
        }

        private async void SaveToDB()
        {
            IMongoDatabase db = Globals.GetGFMongoConnector(this._Environment).MongoClient.GetDatabase("groundframeQueuer");
            var collection = db.GetCollection<BsonDocument>("processQueue");
            await collection.InsertOneAsync(this.ToBSON());
        }

        private async void GetFromDB()
        {
            IMongoDatabase db = Globals.GetGFMongoConnector(this._Environment).MongoClient.GetDatabase("groundframeQueuer");
            var collection = db.GetCollection<BsonDocument>("processQueue");

            string filter = string.Format(@"{{ key: '{0}'}}", this._Key);
            BsonDocument Document = await collection.Find(filter).SingleAsync();
            string Test = JsonConvert.SerializeObject(BsonTypeMapper.MapToDotNetValue(Document));
            
            JsonConvert.PopulateObject(Test, this, new JsonSerializerSettings { ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor });
        }

        #endregion Methods
    }
}
