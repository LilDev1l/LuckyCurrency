using Newtonsoft.Json;

namespace LuckyCurrency.Services.Models.Position
{
    class PositionData
    {
        [JsonProperty("user_id")]
        public int User_id { get; set; }
        [JsonProperty("symbol")]
        public string size { get; set; }
        [JsonProperty("topic")]
        public int Size { get; set; }
        [JsonProperty("side")]
        public string Side { get; set; }
        [JsonProperty("position_value")]
        public string Position_value { get; set; }
        [JsonProperty("entry_price")]
        public string Entry_price { get; set; }
        [JsonProperty("liq_price")]
        public string Liq_price { get; set; }
        [JsonProperty("bust_price")]
        public string Bust_price { get; set; }
        [JsonProperty("leverage")]
        public string Leverage { get; set; }
        [JsonProperty("order_margin")]
        public string Order_margin { get; set; }
        [JsonProperty("position_margin")]
        public string Position_margin { get; set; }
        [JsonProperty("available_balance")]
        public string Available_balance { get; set; }
        [JsonProperty("take_profit")]
        public string Take_profit { get; set; }
        [JsonProperty("tp_trigger_by")]
        public string Tp_trigger_by { get; set; }
        [JsonProperty("stop_loss")]
        public string Stop_loss { get; set; }
        [JsonProperty("sl_trigger_by")]
        public string Sl_trigger_by { get; set; }
        [JsonProperty("realised_pnl")]
        public string Realised_pnl { get; set; }
        [JsonProperty("trailing_stop")]
        public string Trailing_stop { get; set; }
        [JsonProperty("trailing_active")]
        public string Trailing_active { get; set; }
        [JsonProperty("wallet_balance")]
        public string Wallet_balance { get; set; }
        [JsonProperty("risk_id")]
        public int Risk_id { get; set; }
        [JsonProperty("occ_closing_fee")]
        public string Occ_closing_fee { get; set; }
        [JsonProperty("occ_funding_fee")]
        public string Occ_funding_fee { get; set; }
        [JsonProperty("auto_add_margin")]
        public int Auto_add_margin { get; set; }
        [JsonProperty("cum_realised_pnl")]
        public string Cum_realised_pnl { get; set; }
        [JsonProperty("position_status")]
        public string Position_status { get; set; }
        [JsonProperty("position_seq")]
        public int Position_seq { get; set; }
    }
}
