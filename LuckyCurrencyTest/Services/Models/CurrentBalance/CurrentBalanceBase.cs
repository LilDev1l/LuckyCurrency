using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTest.Services.Models.CurrentBalance
{
    class CurrentBalanceBase
    {
        [JsonProperty("ret_code")]
        public int Ret_code { get; set; }
        [JsonProperty("ret_msg")]
        public string Ret_msg { get; set; }
        [JsonProperty("ext_code")]
        public string Ext_code { get; set; }
        [JsonProperty("ext_info")]
        public string Ext_info { get; set; }
        [JsonProperty("result")]
        public Result Result { get; set; }
        [JsonProperty("time_now")]
        public string Time_now { get; set; }
        [JsonProperty("rate_limit_status")]
        public int Rate_limit_status { get; set; }
        [JsonProperty("rate_limit_reset_ms")]
        public long Rate_limit_reset_ms { get; set; }
        [JsonProperty("rate_limit")]
        public int Rate_limit { get; set; }
    }
}
