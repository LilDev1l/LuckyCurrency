using Newtonsoft.Json;
using System;

namespace LuckyCurrencyTest.Services.Models.LastTrade
{
    class LastTradeData
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("tick_direction")]
        public string Tick_direction { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
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
