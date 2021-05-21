using Newtonsoft.Json;
using System;

namespace Bybit.Model.LastTrade
{
    public class LastTradeData
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("tick_direction")]
        public string Tick_direction { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("size")]
        public double Size { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("trade_time_ms")]
        public string Trade_time_ms { get; set; }
        [JsonProperty("side")]
        public string Side { get; set; }
        [JsonProperty("trade_id")]
        public string Trade_id { get; set; }
    }
}
