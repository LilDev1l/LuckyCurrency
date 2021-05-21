using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bybit.Model.LinearKlineWS
{
    public class LinearKlineWSBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public List<LinearKlineWSData> Data { get; set; }
        [JsonProperty("timestamp_e6")]
        public long Timestamp_e6 { get; set; }
    }
}