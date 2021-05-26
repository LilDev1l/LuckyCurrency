using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.API_Key
{
    public class API_KeyBase
    {
        public int ret_code { get; set; }
        public string ret_msg { get; set; }
        public string ext_code { get; set; }
        public List<API_KeyData> result { get; set; }
        public object ext_info { get; set; }
        public string time_now { get; set; }
        public int rate_limit_status { get; set; }
        public long rate_limit_reset_ms { get; set; }
        public int rate_limit { get; set; }
    }
}
