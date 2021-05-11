using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.Symbol
{
    class LotSizeFilter
    {
        public double max_trading_qty { get; set; }
        public double min_trading_qty { get; set; }
        public double qty_step { get; set; }
        public int qty_scale { get; set; }

        public void GetQtyScale()
        {
            string temp = qty_step.ToString();
            if (temp.Contains('.'))
            {
                qty_scale = temp.Length - 2;
            }
            else
            {
                qty_scale = temp.Length - 1;
            }
            
        }
    }
}
