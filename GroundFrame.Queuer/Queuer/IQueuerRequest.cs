using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        [JsonProperty("responses")]
        public List<QueuerResponse> Responses { get; }

        /// <summary>
        /// Gets the request configuration JSON
        /// </summary>
        [JsonProperty("config")]
        public JObject Config { get; }

        #endregion Properties

        #region Methods

        public Task Execute();

        #endregion Methods
    }
}
