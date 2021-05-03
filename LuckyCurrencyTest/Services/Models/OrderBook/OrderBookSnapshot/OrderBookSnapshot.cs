using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTest.Services.Models.OrderBook.OrderBookSnapshot
{
    class OrderBookSnapshot
    {
        [JsonProperty("order_book")]
        public List<OrderBookData> Order_book { get; set; }
    }
}
