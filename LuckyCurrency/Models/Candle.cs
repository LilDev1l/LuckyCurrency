using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LuckyCurrency.Models
{
    public class Candle : FancyCandles.ICandle
    {
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

        public override string ToString()
        {
            return $"Candle: [t: {t}; O: {O}; H: {H}; L: {L}; C: {C}; V: {V}]";
        }
    }
}
