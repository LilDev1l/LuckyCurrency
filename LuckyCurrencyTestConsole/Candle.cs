﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LuckyCurrencyTestConsole
{
    public class Candle
    {
        [JsonProperty("open_time")] 
        public DateTime t { get; set; }
        public double O { get; set; }
        public double H { get; set; }
        public double L { get; set; }
        public double C { get; set; }
        public long V { get; set; }

        public Candle(DateTime t, double O, double H, double L, double C, long V)
        {
            this.t = t;
            this.O = O;
            this.H = H;
            this.L = L;
            this.C = C;
            this.V = V;
        }
    }
}
