using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.BalanceWebSocket
{
    public class BalanceWSBase
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public List<BalanceWSData> Data { get; set; }
    }
}
