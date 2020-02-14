using System;

namespace GroundFrame.Queuer
{
    public class QueuerResponse
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        public QueuerResponseStatus Status { get; set; }

        #endregion Properties

        #region Constructors

        public QueuerResponse()
        {
            this.Status = QueuerResponseStatus.Queued;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
