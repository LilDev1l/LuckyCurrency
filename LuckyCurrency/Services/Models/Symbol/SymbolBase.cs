using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.Symbol
{
    class SymbolBase
    {
        public int ret_code { get; set; }
        public string ret_msg { get; set; }
        public string ext_code { get; set; }
        public string ext_info { get; set; }
        public List<SymbolData> result { get; set; }
        public string time_now { get; set; }
    }
}
