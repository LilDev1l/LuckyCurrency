using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.Symbol
{
    class PriceFilter
    {
        public double min_price { get; set; }
        public double max_price { get; set; }
        public double tick_size { get; set; }
    }
}
