using Newtonsoft.Json;

namespace Bybit.Model.PositionWS
{
    public class PositionWSData
    {
        [JsonProperty("user_id")]
        public string user_id { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("size")]
        public double Size { get; set; }
        [JsonProperty("side")]
        public string Side { get; set; }
        [JsonProperty("position_value")]
        public double Position_value { get; set; }
        [JsonProperty("entry_price")]
        public double Entry_price { get; set; }
        [JsonProperty("liq_price")]
        public double Liq_price { get; set; }
        [JsonProperty("bust_price")]
        public double Bust_price { get; set; }
        [JsonProperty("leverage")]
        public double Leverage { get; set; }
        [JsonProperty("order_margin")]
        public double Order_margin { get; set; }
        [JsonProperty("position_margin")]
        public double Position_margin { get; set; }
        [JsonProperty("occ_closing_fee")]
        public double Occ_closing_fee { get; set; }
        [JsonProperty("take_profit")]
        public double Take_profit { get; set; }
        [JsonProperty("tp_trigger_by")]
        public string Tp_trigger_by { get; set; }
        [JsonProperty("stop_loss")]
        public double Stop_loss { get; set; }
        [JsonProperty("sl_trigger_by")]
        public string Sl_trigger_by { get; set; }
        [JsonProperty("trailing_stop")]
        public double Trailing_stop { get; set; }
        [JsonProperty("realised_pnl")]
        public double Realised_pnl { get; set; }
        [JsonProperty("auto_add_margin")]
        public string Auto_add_margin { get; set; }
        [JsonProperty("cum_realised_pnl")]
        public double Cum_realised_pnl { get; set; }
        [JsonProperty("position_status")]
        public string Position_status { get; set; }
        [JsonProperty("position_id")]
        public string Position_id { get; set; }
        [JsonProperty("position_seq")]
        public string Position_seq { get; set; }
        [JsonProperty("adl_rank_indicator")]
        public string Adl_rank_indicator { get; set; }
        [JsonProperty("free_qty")]
        public double Free_qty { get; set; }
        [JsonProperty("tp_sl_mode")]
        public string Tp_sl_mode { get; set; }
    }
}
