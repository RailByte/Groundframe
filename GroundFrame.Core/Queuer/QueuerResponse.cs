using Newtonsoft.Json;
using System;

namespace GroundFrame.Core.Queuer
{
    /// <summary>
    /// A class represnting a single Queuer Response object
    /// </summary>
    public class QueuerResponse
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly QueuerResponseStatus _Status; //Private variable to store the environment
        private readonly DateTime _ResponseDateTime; //Private variable to store the Response Date Time
        private readonly string _ResponseMessage; //Private variable to store the Reponse Message;
        private readonly Exception _ResponseException; //Private variable to store the exception

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

        /// <summary>
        /// Gets the exception message in the case of an error
        /// </summary>
        [JsonIgnore]
        public Exception ResponseException { get { return this._ResponseException; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new QueuerReponse object from the supplied argument
        /// </summary>
        /// <param name="Status">The status of the response</param>
        /// <param name="ResponseMessage">The repsonse message</param>
        /// <param name="Ex">The exception which caused the failure in the case of a failure response</param>
        public QueuerResponse(QueuerResponseStatus Status, string ResponseMessage, Exception Ex)
        {
            this._ResponseDateTime = DateTime.UtcNow;
            this._Status = Status;
            this._ResponseMessage = ResponseMessage;
            this._ResponseException = Ex;
        }

        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Only used by Newtonsoft.JSON deserializer")]
        private QueuerResponse(QueuerResponseStatus Status, string ResponseMessage, DateTime ResponseDateTime)
        {
            this._ResponseDateTime = ResponseDateTime;
            this._Status = Status;
            this._ResponseMessage = ResponseMessage;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
