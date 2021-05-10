using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.PositionClosePnl
{
    class PositionClosePnlBase
    {
        public int ret_code { get; set; }
        public string ret_msg { get; set; }
        public string ext_code { get; set; }
        public string ext_info { get; set; }
        public PositionClosePnlPage result { get; set; }
        public string time_now { get; set; }
        public int rate_limit_status { get; set; }
        public long rate_limit_reset_ms { get; set; }
        public int rate_limit { get; set; }
    }
}
