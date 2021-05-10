using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.Symbol
{
    class LeverageFilter
    {
        public double min_leverage { get; set; }
        public double max_leverage { get; set; }
        public double leverage_step { get; set; }
    }
}
