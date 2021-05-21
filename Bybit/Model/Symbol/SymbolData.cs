using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.Symbol
{
    public class SymbolData
    {
        public string name { get; set; }
        public string alias { get; set; }
        public string status { get; set; }
        public string base_currency { get; set; }
        public string quote_currency { get; set; }
        public int price_scale { get; set; }
        public double taker_fee { get; set; }
        public double maker_fee { get; set; }
        public LeverageFilter leverage_filter { get; set; }
        public PriceFilter price_filter { get; set; }
        public LotSizeFilter lot_size_filter { get; set; }
    }
}
