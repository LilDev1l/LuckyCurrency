using Newtonsoft.Json;

namespace LuckyCurrency.Services.Models.LinearKlineWS
{
    public class LinearKlineWSData
    {
        [JsonProperty("start")]
        public int Start { get; set; }
        [JsonProperty("end")]
        public int End { get; set; }
        [JsonProperty("open")]
        public double Open { get; set; }
        [JsonProperty("close")]
        public double Close { get; set; }
        [JsonProperty("high")]
        public double High { get; set; }
        [JsonProperty("low")]
        public double Low { get; set; }
        [JsonProperty("volume")]
        public string Volume { get; set; }
        [JsonProperty("turnover")]
        public string Turnover { get; set; }
        [JsonProperty("confirm")]
        public bool Confirm { get; set; }
        [JsonProperty("cross_seq")]
        public long Cross_seq { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}