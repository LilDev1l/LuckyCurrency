using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models
{
    class Position
    {
        public string Symbol { get; set; }
        public string Side { get; set; }
        public double Size { get; set; }
        public double Position_value { get; set; }
        public double Entry_price { get; set; }
        public double Liq_price { get; set; }
        public double Position_margin { get; set; }
        public double Realised_pnl { get; set; }

        public Position(string symbol, string side, double size, double position_value, double entry_price, double liq_price, double position_margin, double realised_pnl)
        {
            Symbol = symbol;
            Side = side;
            Size = size;
            Position_value = position_value;
            Entry_price = entry_price;
            Liq_price = liq_price;
            Position_margin = position_margin;
            Realised_pnl = realised_pnl;
        }
    }
}
