using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Resources;

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

        /// <summary>
        /// Gets the exception message in the case of an error
        /// </summary>
        [JsonProperty("responseExceptionMessage")]
        public string ResponseExceptionMessage { get { return this._ResponseException == null ? string.Empty : this.ResponseException.BuildExceptionMessage(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new QueuerReponse object from the supplied argument
        /// </summary>
        /// <param name="Status">The status of the response</param>
        /// <param name="ResponseKey">The repsonse message key. A list of response keys and messages can be found in Resource\QueuerResources.resx</param>
        /// <param name="Ex">The exception which caused the failure in the case of a failure responses</param>
        public QueuerResponse(QueuerResponseStatus Status, string ResponseKey, Exception Ex)
        {
            this._ResponseDateTime = DateTime.UtcNow;
            this._Status = Status;
            this._ResponseMessage = Status == QueuerResponseStatus.DebugMesssage || Status == QueuerResponseStatus.Failed ? ResponseKey : GetResponseMessage(ResponseKey);
            this._ResponseException = Ex;
        }

        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Only used by Newtonsoft.JSON deserializer")]
        private QueuerResponse(QueuerResponseStatus Status, string ResponseKey, DateTime ResponseDateTime)
        {
            this._ResponseDateTime = ResponseDateTime;
            this._Status = Status;
            this._ResponseMessage = Status == QueuerResponseStatus.DebugMesssage || Status == QueuerResponseStatus.Failed ? ResponseKey : GetResponseMessage(ResponseKey);
        }

        /// <summary>
        /// Gets the translated Response message from the Response Key
        /// </summary>
        /// <param name="ResponseKey"></param>
        /// <returns></returns>
        private string GetResponseMessage(string ResponseKey)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.QueuerResources", Assembly.GetExecutingAssembly());
            return ExceptionMessageResources.GetString(ResponseKey, Globals.UserSettings.GetCultureInfo());
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
