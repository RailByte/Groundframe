using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Core
{
    /// <summary>
    /// A helper class to manage GroundFrame security
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// Checks whether the user is authenticated against the OAuth service
        /// </summary>
        /// <param name="AppUserAPIKey">The users OAuth API key</param>
        /// <param name="BearerToken">The bearer token issued by the OAuth provider</param>
        /// <returns></returns>
        public static bool IsAuthenticated(string AppUserAPIKey, string BearerToken)
        {
            //TODO: Write code
            return true;
        }
    }
}
