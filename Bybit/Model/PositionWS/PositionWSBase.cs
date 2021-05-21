using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bybit.Model.PositionWS
{
    public class PositionWSBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("data")]
        public List<PositionWSData> Data { get; set; }
    }
}
