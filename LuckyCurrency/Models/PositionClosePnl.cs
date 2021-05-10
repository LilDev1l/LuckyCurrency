using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models
{
    class PositionClosePnl
    {
        public string Symbol { get; set; }
        public string Side { get; set; }
        public double Qty { get; set; }
        public double Avg_entry_price { get; set; }
        public double Avg_exit_price { get; set; }
        public double Closed_pnl { get; set; }
        public string Exec_type { get; set; }
        public DateTime Created_at { get; set; }

        public PositionClosePnl(string symbol, string side, double qty, double avg_entry_price, double avg_exit_price, double closed_pnl, string exec_type, DateTime created_at)
        {
            Symbol = symbol;
            Side = side;
            Qty = qty;
            Avg_entry_price = avg_entry_price;
            Avg_exit_price = avg_exit_price;
            Closed_pnl = closed_pnl;
            Exec_type = exec_type;
            Created_at = created_at;
        }
    }
}
