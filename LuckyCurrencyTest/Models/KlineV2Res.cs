using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTest.Models
{
    class KlineV2Res
    {
        public long start { get; set; }
        public long end { get; set; }
        public double open { get; set; }
        public double close { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public int volume { get; set; }
        public double turnover { get; set; }
        public bool confirm { get; set; }
        public long cross_seq { get; set; }
        public long timestamp { get; set; }
    }
}
