using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Queuer
{
    public interface IQueuerRequest
    {
        #region Properties

        /// <summary>
        /// Gets the process responses
        /// </summary>
        public List<QueuerResponse> Responses { get; }

        #endregion Properties

        #region Methods

        public Task Execute();

        #endregion Methods
    }
}
