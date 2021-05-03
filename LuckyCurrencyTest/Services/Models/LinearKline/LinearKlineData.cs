using Newtonsoft.Json;

namespace LuckyCurrencyTest.Services.Models.LinearKline
{
    class LinearKlineData
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("start_at")]
        public long? StartAt { get; set; }
        [JsonProperty("period")]
        public string Period { get; set; }
        [JsonProperty("open_time")]
        public long? OpenTime { get; set; }
        [JsonProperty("open")]
        public double? Open { get; set; }
        [JsonProperty("low")]
        public double? Low { get; set; }
        [JsonProperty("interval")]
        public string Interval { get; set; }
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("high")]
        public double? High { get; set; }
        [JsonProperty("close")]
        public double? Close { get; set; }
        [JsonProperty("turnover")]
        public double? Turnover { get; set; }
        [JsonProperty("volume")]
        public double? Volume { get; set; }
    }
}
