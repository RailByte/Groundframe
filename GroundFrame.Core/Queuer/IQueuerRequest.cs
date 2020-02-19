using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Core.Queuer
{
    /// <summary>
    /// Querer Request interface to ensure all queuer tasks conform to the same structure
    /// </summary>
    public interface IQueuerRequest
    {
        #region Properties

        /// <summary>
        /// Gets the process responses
        /// </summary>
        [JsonProperty("responses")]
        public List<QueuerResponse> Responses { get; }

        /// <summary>
        /// Gets the request configuration JSON
        /// </summary>
        [JsonProperty("config")]
        public JObject Config { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Task to execute the process
        /// </summary>
        /// <returns></returns>
        public Task Execute();

        /// <summary>
        /// Replaces all the responses attached to the process with the supplied list of responses
        /// </summary>
        /// <param name="Responses"></param>
        public void ReplaceResponses(List<QueuerResponse> Responses);

        #endregion Methods
    }
}
