using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTest.Models
{
    class OrderBook
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }

        public OrderBook(int id, double price, double quantity)
        {
            Id = id;
            Price = price;
            Quantity = quantity;
        }
    }
}
