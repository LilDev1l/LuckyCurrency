using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTest.Services.Models.OrderBook.OrderBookDelta
{
    class Update
    {
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("side")]
        public string Side { get; set; }
        [JsonProperty("size")]
        public double Size { get; set; }
    }
}
