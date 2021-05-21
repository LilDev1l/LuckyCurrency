using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.ServerTime2
{
    public class ServerTimeData
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
        public object Result { get; set; }
        [JsonProperty("time_now")]
        public double Time_now { get; set; }
    }
}
