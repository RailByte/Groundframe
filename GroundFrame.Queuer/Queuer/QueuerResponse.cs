using Newtonsoft.Json;
using System;

namespace GroundFrame.Queuer
{
    public class QueuerResponse
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private string _Environment; //Private variable to store the environment that generated the response
        private QueuerResponseStatus _Status; //Private variable to store the environment
        private DateTime _ResponseDateTime; //Private variable to store the Response Date Time
        private string _Key; //Private variable to store the queuer Key
        private string _ResponseMessage; //Private variable to store the Reponse Message;
        private Exception _Exception; //Private variable to store the exception

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Status of the response status
        /// </summary>
        [JsonProperty("responseStatus")]
        public QueuerResponseStatus Status { get { return this._Status; } }

        /// <summary>
        /// Gets the Status of the response time
        /// </summary>
        [JsonProperty("responseTime")]
        public DateTime ResponseDateTime { get { return this._ResponseDateTime; } }

        /// <summary>
        /// Gets the Status of the response time
        /// </summary>
        [JsonProperty("responseMessage")]
        public string ResponseMessage { get { return this._ResponseMessage; } }

        #endregion Properties

        #region Constructors

        public QueuerResponse(string Key, string Environment, QueuerResponseStatus Status, string ResponseMessage, Exception Ex)
        {
            this._ResponseDateTime = DateTime.UtcNow;
            this._Key = Key;
            this._Status = Status;
            this._Environment = Environment;
            this._ResponseMessage = ResponseMessage;
            this._Exception = Ex;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
