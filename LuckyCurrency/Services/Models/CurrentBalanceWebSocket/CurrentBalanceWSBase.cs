using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.CurrentBalanceWebSocket
{
    class CurrentBalanceWSBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public List<CurrentBalanceWSData> Data { get; set; }
    }
}
