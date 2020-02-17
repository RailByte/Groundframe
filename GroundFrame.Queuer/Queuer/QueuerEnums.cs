using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Queuer
{
    /// <summary>
    /// Enum representing the response statuses a Queuer object can return
    /// </summary>
    public enum QueuerResponseStatus
    {
        /// <summary>
        /// The process is queued but not yet started
        /// </summary>
        Queued = 0,
        /// <summary>
        /// The process is running
        /// </summary>
        Running = 1,
        /// <summary>
        /// The process has completed with success
        /// </summary>
        Success = 2,
        /// <summary>
        /// The process has completed but with warnings. See the response message for details
        /// </summary>
        CompletedWithWarning = 3,
        /// <summary>
        /// The process has failed
        /// </summary>
        Failed = 4,
        /// <summary>
        /// An information event status. This is excluded when getting the process status
        /// </summary>
        Information = 5
    }
}
