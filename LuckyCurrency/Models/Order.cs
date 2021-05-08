using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models
{
    class Order
    {
        public string Order_id { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string Order_type { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public string Order_status { get; set; }
        public double Take_profit { get; set; }
        public double Stop_loss { get; set; }
        public DateTime Create_time { get; set; }

        public Order(string order_id, string symbol, string side, string order_type, double price, double qty, string order_status, double take_profit, double stop_loss, DateTime create_time)
        {
            Order_id = order_id;
            Symbol = symbol;
            Side = side;
            Order_type = order_type;
            Price = price;
            Qty = qty;
            Order_status = order_status;
            Take_profit = take_profit;
            Stop_loss = stop_loss;
            Create_time = create_time;
        }
    }
}
