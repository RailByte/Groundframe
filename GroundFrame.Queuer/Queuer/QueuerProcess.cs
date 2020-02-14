using System;
using System.Collections.Generic;
using System.Text;

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
        private bool _Authenticated; //Private variable to indicate whether the user has authenticated
        private string _APIKey; //Private variable to indicate the API key of the application which requested the process

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Request information
        /// </summary>
        public IQueuerRequest Request { get { return this._Request; } }
        
        /// <summary>
        /// Gets the process response
        /// </summary>
        public QueuerResponse Response { get { return this._Request.Response; } }

        #endregion Properties

        #region Constructors

        public QueuerProcess(string AppUserAPIKey, string BearerToken, string APIKey, string JSON)
        {
            this._AppUserAPIKey = AppUserAPIKey;
            this._Key = this.GenerateKey();
            this._BearerToken = BearerToken;
            this._APIKey = APIKey;

            //TODO: Authenticate the user before queuing.
            this._Authenticated = true;


        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Generates a unique key for the process
        /// </summary>
        /// <returns>A unique 16 character string</returns>
        private string GenerateKey()
        {
            return new Guid().ToString().Replace("-", string.Empty).ToUpper().Substring(0, 16);
        }

        #endregion Methods
    }
}
