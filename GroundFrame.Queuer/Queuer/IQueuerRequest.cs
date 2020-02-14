using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Queuer
{
    public interface IQueuerRequest
    {
        #region Properties

        /// <summary>
        /// Gets the process response
        /// </summary>
        public QueuerResponse Response { get; }

        #endregion Properties
    }
}
