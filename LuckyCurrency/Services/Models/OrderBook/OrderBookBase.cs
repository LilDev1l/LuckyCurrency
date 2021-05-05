using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.OrderBook
{
    class OrderBookBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
        [JsonProperty("cross_seq")]
        public long Cross_seq { get; set; }
        [JsonProperty("timestamp_e6")]
        public long Timestamp_e6 { get; set; }
    }
}
