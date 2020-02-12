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
        private string _Key; //Private variable to store the key of the process
        private string _UserName; //Private variable to store the username of the user who requested the process

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

        public QueuerProcess(string UserName, string BearerToken, string JSON)
        {
            this._UserName = UserName;
            this._Key = this.GenerateKey();
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
