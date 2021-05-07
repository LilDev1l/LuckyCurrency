using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.Position
{
    class PositionData
    {
        public int user_id { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public double size { get; set; }
        public double position_value { get; set; }
        public double entry_price { get; set; }
        public double liq_price { get; set; }
        public double bust_price { get; set; }
        public double leverage { get; set; }
        public bool is_isolated { get; set; }
        public double auto_add_margin { get; set; }
        public double position_margin { get; set; }
        public double occ_closing_fee { get; set; }
        public double realised_pnl { get; set; }
        public double cum_realised_pnl { get; set; }
        public double free_qty { get; set; }
        public string tp_sl_mode { get; set; }
        public double unrealised_pnl { get; set; }
        public int deleverage_indicator { get; set; }
        public int risk_id { get; set; }
    }
}
