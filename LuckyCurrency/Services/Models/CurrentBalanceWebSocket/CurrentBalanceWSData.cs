using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.CurrentBalanceWebSocket
{
    class CurrentBalanceWSData
    {
        [JsonProperty("wallet_balance")]
        public double Wallet_balance { get; set; }
        [JsonProperty("available_balance")]
        public double Available_balance { get; set; }
    }
}
