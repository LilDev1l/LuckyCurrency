using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bybit.Model.LastTrade
{
    public class LastTradeBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public List<LastTradeData> Data { get; set; }
    }
}
