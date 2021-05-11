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
        public int round { get; set; }

        public void GetRound()
        {
            string temp = tick_size.ToString();
            if (temp.Contains('5'))
            {
                round = temp.Length - 3;
            }
            else
            {
                round = temp.Length - 2;
            }

        }
    }
}
