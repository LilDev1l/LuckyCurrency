using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuckyCurrencyTest.Services.Models.LinearKline
{
    class LinearKlineBase
    {
        [JsonProperty("ret_code")]
        public decimal? RetCode { get; set; }
        [JsonProperty("ret_msg")]
        public string RetMsg { get; set; }
        [JsonProperty("ext_code")]
        public string ExtCode { get; set; }
        [JsonProperty("ext_info")]
        public string ExtInfo { get; set; }
        [JsonProperty("result")]
        public List<LinearKlineData> Result { get; set; }
        [JsonProperty("time_now")]
        public string TimeNow { get; set; }
    }
}
