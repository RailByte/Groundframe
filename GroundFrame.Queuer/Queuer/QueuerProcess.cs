using GroundFrame.Queuer.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Queuer
{
    public class QueuerProcess
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private IQueuerRequest _Request; //Private variable to store the request
        private DateTime _CreatedOn; //Private variable to store the process creation date time
        private string _Key; //Private varialbe to store the unique key for this process
        private string _AppAPIKey; //Private variable to store the application API Key which requested the process
        private string _AppUserAPIKey; //Private variable to store the application user API key of the user who requested the process
        private string _BearerToken; //Private variable to store the bearer token. This will be used to authentiate the user
        private bool _Authenticated = false; //Private variable to indicate whether the user has authenticated
        private string _APIKey; //Private variable to indicate the API key of the application which requested the process
        private DateTimeOffset _QueueTime = DateTimeOffset.UtcNow; //Prviate variable to store the 
        private string _JSON; //Private variable to store the config JSON

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Request information
        /// </summary>
        public IQueuerRequest Request { get { return this._Request; } }
        
        /// <summary>
        /// Gets the process response
        /// </summary>
        public QueuerResponse Response { get { return this._Request == null ? null : this._Request.Responses == null ? null : this._Request.Responses.Count == 0 ? null : this._Request.Responses[this._Request.Responses.Count - 1];  } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a QueuerProess object from the values provided. The Queue date will be current UTC
        /// </summary>
        /// <param name="AppUserAPIKey">The Users API Key</param>
        /// <param name="APIKey">The API Key of the application requesting a proces</param>
        /// <param name="BearerToken">The bearer token so the user can be authenticated</param>
        /// <param name="JSON">The JSON containing the configuration</param>
        public QueuerProcess(string AppUserAPIKey, string APIKey, string BearerToken, string JSON)
        {
            this._QueueTime = DateTimeOffset.UtcNow;
            this.BuildProcess(AppUserAPIKey, APIKey, BearerToken, JSON);
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
        private void BuildProcess(string AppUserAPIKey, string APIKey, string BearerToken, string JSON)
        {
            this._AppUserAPIKey = AppUserAPIKey;
            this._Key = this.GenerateKey();
            this._BearerToken = BearerToken;
            this._APIKey = APIKey;
            this._JSON = JSON;
            //TODO: Authenticate the user
            this._Authenticated = true;
            //Process the config
            this.ProcessConfig();
        }

        /// <summary>
        /// Generates a unique key for the process
        /// </summary>
        /// <returns>A unique 16 character string</returns>
        private string GenerateKey()
        {
            return new Guid().ToString().Replace("-", string.Empty).ToUpper().Substring(0, 16);
        }

        private void ProcessConfig()
        {
            //Parse the JSON into a JOobject
            JObject JSONObject = JObject.Parse(this._JSON);

            //Dictionary to store function mapping
            Dictionary<string, Action> ProcessMapping = new Dictionary<string, Action>
            {
                { "SeedSimulationFromWTT", (() => this._Request = new SeedSimulationFromWTT(this._AppUserAPIKey, this._AppAPIKey, JSONObject["config"].ToString())) }
            };

            ProcessMapping[JSONObject["task"].ToString()]();
            Task.Run(() => Request.Execute());
        }

        #endregion Methods
    }
}
