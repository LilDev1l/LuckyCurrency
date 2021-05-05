﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuckyCurrency.Services.Models.LastTrade
{
    class LastTradeBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public List<LastTradeData> Data { get; set; }
    }
}
