using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.CurrentBalance
{
    class Result
    {
        [JsonProperty("USDT")]
        public CurrentBalanceData USDT { get; set; }
    }
    class CurrentBalanceData
    {
        [JsonProperty("equity")]
        public double Equity { get; set; }
        [JsonProperty("available_balance")]
        public double Available_balance { get; set; }
        [JsonProperty("used_margin")]
        public int Used_margin { get; set; }
        [JsonProperty("order_margin")]
        public int Order_margin { get; set; }
        [JsonProperty("position_margin")]
        public int Position_margin { get; set; }
        [JsonProperty("occ_closing_fee")]
        public int Occ_closing_fee { get; set; }
        [JsonProperty("occ_funding_fee")]
        public int Occ_funding_fee { get; set; }
        [JsonProperty("wallet_balance")]
        public double Wallet_balance { get; set; }
        [JsonProperty("realised_pnl")]
        public int Realised_pnl { get; set; }
        [JsonProperty("unrealised_pnl")]
        public int Unrealised_pnl { get; set; }
        [JsonProperty("cum_realised_pnl")]
        public int Cum_realised_pnl { get; set; }
        [JsonProperty("given_cash")]
        public int Given_cash { get; set; }
        [JsonProperty("service_cash")]
        public int Service_cash { get; set; }
    }
}
