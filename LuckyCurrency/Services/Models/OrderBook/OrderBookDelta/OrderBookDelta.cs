using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.OrderBook.OrderBookDelta
{
    class OrderBookDelta
    {
        [JsonProperty("delete")]
        public List<Delete> Delete { get; set; }
        [JsonProperty("update")]
        public List<Update> Update { get; set; }
        [JsonProperty("insert")]
        public List<Insert> Insert { get; set; }
        [JsonProperty("transactTimeE6")]
        public int TransactTimeE6 { get; set; }
    }
}
