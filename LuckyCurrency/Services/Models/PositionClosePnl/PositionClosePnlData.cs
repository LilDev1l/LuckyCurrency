using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.PositionClosePnl
{
    class PositionClosePnlData
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string symbol { get; set; }
        public string order_id { get; set; }
        public string side { get; set; }
        public double qty { get; set; }
        public double order_price { get; set; }
        public string order_type { get; set; }
        public string exec_type { get; set; }
        public double closed_size { get; set; }
        public double cum_entry_value { get; set; }
        public double avg_entry_price { get; set; }
        public double cum_exit_value { get; set; }
        public double avg_exit_price { get; set; }
        public double closed_pnl { get; set; }
        public int fill_count { get; set; }
        public double leverage { get; set; }
        public long created_at { get; set; }
    }
}
