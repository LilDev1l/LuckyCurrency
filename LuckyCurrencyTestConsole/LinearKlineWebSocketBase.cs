using System.Collections.Generic;
using Newtonsoft.Json;

namespace LuckyCurrencyTestConsole
{
    public class LinearKlineWebSocketBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public List<LinearKlineWebSocket> Data { get; set; }
        [JsonProperty("timestamp_e6")]
        public long Timestamp_e6 { get; set; }
    }
}