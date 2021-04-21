using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTestConsole
{
    class KlineV2Base
    {
        public string topic { get; set; }
        public List<KlineV2Res> data { get; set; }
        public long timestamp_e6 { get; set; }
    }
}
