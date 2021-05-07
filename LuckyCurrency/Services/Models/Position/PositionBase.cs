using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuckyCurrency.Services.Models.Position
{
    class PositionBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("data")]
        public List<PositionData> Data { get; set; }
    }
}
