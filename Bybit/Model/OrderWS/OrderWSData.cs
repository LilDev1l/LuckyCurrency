using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.OrderWS
{
    public class OrderWSData
    {
        public string order_id { get; set; }
        public string order_link_id { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public string order_type { get; set; }
        public double price { get; set; }
        public double qty { get; set; }
        public double leaves_qty { get; set; }
        public double last_exec_price { get; set; }
        public double cum_exec_qty { get; set; }
        public double cum_exec_value { get; set; }
        public double cum_exec_fee { get; set; }
        public string time_in_force { get; set; }
        public string create_type { get; set; }
        public string cancel_type { get; set; }
        public string order_status { get; set; }
        public double take_profit { get; set; }
        public double stop_loss { get; set; }
        public double trailing_stop { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public bool reduce_only { get; set; }
        public bool close_on_trigger { get; set; }
    }
}
