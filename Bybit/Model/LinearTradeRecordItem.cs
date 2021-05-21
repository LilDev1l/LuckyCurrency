/* 
 * Bybit API
 *
 * ## REST API for the Bybit Exchange. Base URI: [https://api.bybit.com]  
 *
 * OpenAPI spec version: 0.2.10
 * Contact: support@bybit.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Bybit.Client.SwaggerDateConverter;

namespace Bybit.Model
{
    /// <summary>
    /// LinearTradeRecordItem
    /// </summary>
    [DataContract]
    public partial class LinearTradeRecordItem :  IEquatable<LinearTradeRecordItem>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearTradeRecordItem" /> class.
        /// </summary>
        /// <param name="closedSize">closedSize.</param>
        /// <param name="execFee">execFee.</param>
        /// <param name="execId">execId.</param>
        /// <param name="execPrice">execPrice.</param>
        /// <param name="execQty">execQty.</param>
        /// <param name="execType">execType.</param>
        /// <param name="execValue">execValue.</param>
        /// <param name="feeRate">feeRate.</param>
        /// <param name="lastLiquidityInd">lastLiquidityInd.</param>
        /// <param name="leavesQty">leavesQty.</param>
        /// <param name="orderId">orderId.</param>
        /// <param name="orderLinkId">orderLinkId.</param>
        /// <param name="orderPrice">orderPrice.</param>
        /// <param name="orderQty">orderQty.</param>
        /// <param name="orderType">orderType.</param>
        /// <param name="price">price.</param>
        /// <param name="side">side.</param>
        /// <param name="symbol">symbol.</param>
        /// <param name="tradeTime">tradeTime.</param>
        /// <param name="tradeTimeMs">tradeTimeMs.</param>
        public LinearTradeRecordItem(double? closedSize = default(double?), double? execFee = default(double?), string execId = default(string), double? execPrice = default(double?), double? execQty = default(double?), string execType = default(string), double? execValue = default(double?), double? feeRate = default(double?), string lastLiquidityInd = default(string), double? leavesQty = default(double?), string orderId = default(string), string orderLinkId = default(string), double? orderPrice = default(double?), double? orderQty = default(double?), string orderType = default(string), double? price = default(double?), string side = default(string), string symbol = default(string), long? tradeTime = default(long?), long? tradeTimeMs = default(long?))
        {
            this.ClosedSize = closedSize;
            this.ExecFee = execFee;
            this.ExecId = execId;
            this.ExecPrice = execPrice;
            this.ExecQty = execQty;
            this.ExecType = execType;
            this.ExecValue = execValue;
            this.FeeRate = feeRate;
            this.LastLiquidityInd = lastLiquidityInd;
            this.LeavesQty = leavesQty;
            this.OrderId = orderId;
            this.OrderLinkId = orderLinkId;
            this.OrderPrice = orderPrice;
            this.OrderQty = orderQty;
            this.OrderType = orderType;
            this.Price = price;
            this.Side = side;
            this.Symbol = symbol;
            this.TradeTime = tradeTime;
            this.TradeTimeMs = tradeTimeMs;
        }
        
        /// <summary>
        /// Gets or Sets ClosedSize
        /// </summary>
        [DataMember(Name="closed_size", EmitDefaultValue=false)]
        public double? ClosedSize { get; set; }

        /// <summary>
        /// Gets or Sets ExecFee
        /// </summary>
        [DataMember(Name="exec_fee", EmitDefaultValue=false)]
        public double? ExecFee { get; set; }

        /// <summary>
        /// Gets or Sets ExecId
        /// </summary>
        [DataMember(Name="exec_id", EmitDefaultValue=false)]
        public string ExecId { get; set; }

        /// <summary>
        /// Gets or Sets ExecPrice
        /// </summary>
        [DataMember(Name="exec_price", EmitDefaultValue=false)]
        public double? ExecPrice { get; set; }

        /// <summary>
        /// Gets or Sets ExecQty
        /// </summary>
        [DataMember(Name="exec_qty", EmitDefaultValue=false)]
        public double? ExecQty { get; set; }

        /// <summary>
        /// Gets or Sets ExecType
        /// </summary>
        [DataMember(Name="exec_type", EmitDefaultValue=false)]
        public string ExecType { get; set; }

        /// <summary>
        /// Gets or Sets ExecValue
        /// </summary>
        [DataMember(Name="exec_value", EmitDefaultValue=false)]
        public double? ExecValue { get; set; }

        /// <summary>
        /// Gets or Sets FeeRate
        /// </summary>
        [DataMember(Name="fee_rate", EmitDefaultValue=false)]
        public double? FeeRate { get; set; }

        /// <summary>
        /// Gets or Sets LastLiquidityInd
        /// </summary>
        [DataMember(Name="last_liquidity_ind", EmitDefaultValue=false)]
        public string LastLiquidityInd { get; set; }

        /// <summary>
        /// Gets or Sets LeavesQty
        /// </summary>
        [DataMember(Name="leaves_qty", EmitDefaultValue=false)]
        public double? LeavesQty { get; set; }

        /// <summary>
        /// Gets or Sets OrderId
        /// </summary>
        [DataMember(Name="order_id", EmitDefaultValue=false)]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or Sets OrderLinkId
        /// </summary>
        [DataMember(Name="order_link_id", EmitDefaultValue=false)]
        public string OrderLinkId { get; set; }

        /// <summary>
        /// Gets or Sets OrderPrice
        /// </summary>
        [DataMember(Name="order_price", EmitDefaultValue=false)]
        public double? OrderPrice { get; set; }

        /// <summary>
        /// Gets or Sets OrderQty
        /// </summary>
        [DataMember(Name="order_qty", EmitDefaultValue=false)]
        public double? OrderQty { get; set; }

        /// <summary>
        /// Gets or Sets OrderType
        /// </summary>
        [DataMember(Name="order_type", EmitDefaultValue=false)]
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name="price", EmitDefaultValue=false)]
        public double? Price { get; set; }

        /// <summary>
        /// Gets or Sets Side
        /// </summary>
        [DataMember(Name="side", EmitDefaultValue=false)]
        public string Side { get; set; }

        /// <summary>
        /// Gets or Sets Symbol
        /// </summary>
        [DataMember(Name="symbol", EmitDefaultValue=false)]
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or Sets TradeTime
        /// </summary>
        [DataMember(Name="trade_time", EmitDefaultValue=false)]
        public long? TradeTime { get; set; }

        /// <summary>
        /// Gets or Sets TradeTimeMs
        /// </summary>
        [DataMember(Name="trade_time_ms", EmitDefaultValue=false)]
        public long? TradeTimeMs { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LinearTradeRecordItem {\n");
            sb.Append("  ClosedSize: ").Append(ClosedSize).Append("\n");
            sb.Append("  ExecFee: ").Append(ExecFee).Append("\n");
            sb.Append("  ExecId: ").Append(ExecId).Append("\n");
            sb.Append("  ExecPrice: ").Append(ExecPrice).Append("\n");
            sb.Append("  ExecQty: ").Append(ExecQty).Append("\n");
            sb.Append("  ExecType: ").Append(ExecType).Append("\n");
            sb.Append("  ExecValue: ").Append(ExecValue).Append("\n");
            sb.Append("  FeeRate: ").Append(FeeRate).Append("\n");
            sb.Append("  LastLiquidityInd: ").Append(LastLiquidityInd).Append("\n");
            sb.Append("  LeavesQty: ").Append(LeavesQty).Append("\n");
            sb.Append("  OrderId: ").Append(OrderId).Append("\n");
            sb.Append("  OrderLinkId: ").Append(OrderLinkId).Append("\n");
            sb.Append("  OrderPrice: ").Append(OrderPrice).Append("\n");
            sb.Append("  OrderQty: ").Append(OrderQty).Append("\n");
            sb.Append("  OrderType: ").Append(OrderType).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Side: ").Append(Side).Append("\n");
            sb.Append("  Symbol: ").Append(Symbol).Append("\n");
            sb.Append("  TradeTime: ").Append(TradeTime).Append("\n");
            sb.Append("  TradeTimeMs: ").Append(TradeTimeMs).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as LinearTradeRecordItem);
        }

        /// <summary>
        /// Returns true if LinearTradeRecordItem instances are equal
        /// </summary>
        /// <param name="input">Instance of LinearTradeRecordItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LinearTradeRecordItem input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ClosedSize == input.ClosedSize ||
                    (this.ClosedSize != null &&
                    this.ClosedSize.Equals(input.ClosedSize))
                ) && 
                (
                    this.ExecFee == input.ExecFee ||
                    (this.ExecFee != null &&
                    this.ExecFee.Equals(input.ExecFee))
                ) && 
                (
                    this.ExecId == input.ExecId ||
                    (this.ExecId != null &&
                    this.ExecId.Equals(input.ExecId))
                ) && 
                (
                    this.ExecPrice == input.ExecPrice ||
                    (this.ExecPrice != null &&
                    this.ExecPrice.Equals(input.ExecPrice))
                ) && 
                (
                    this.ExecQty == input.ExecQty ||
                    (this.ExecQty != null &&
                    this.ExecQty.Equals(input.ExecQty))
                ) && 
                (
                    this.ExecType == input.ExecType ||
                    (this.ExecType != null &&
                    this.ExecType.Equals(input.ExecType))
                ) && 
                (
                    this.ExecValue == input.ExecValue ||
                    (this.ExecValue != null &&
                    this.ExecValue.Equals(input.ExecValue))
                ) && 
                (
                    this.FeeRate == input.FeeRate ||
                    (this.FeeRate != null &&
                    this.FeeRate.Equals(input.FeeRate))
                ) && 
                (
                    this.LastLiquidityInd == input.LastLiquidityInd ||
                    (this.LastLiquidityInd != null &&
                    this.LastLiquidityInd.Equals(input.LastLiquidityInd))
                ) && 
                (
                    this.LeavesQty == input.LeavesQty ||
                    (this.LeavesQty != null &&
                    this.LeavesQty.Equals(input.LeavesQty))
                ) && 
                (
                    this.OrderId == input.OrderId ||
                    (this.OrderId != null &&
                    this.OrderId.Equals(input.OrderId))
                ) && 
                (
                    this.OrderLinkId == input.OrderLinkId ||
                    (this.OrderLinkId != null &&
                    this.OrderLinkId.Equals(input.OrderLinkId))
                ) && 
                (
                    this.OrderPrice == input.OrderPrice ||
                    (this.OrderPrice != null &&
                    this.OrderPrice.Equals(input.OrderPrice))
                ) && 
                (
                    this.OrderQty == input.OrderQty ||
                    (this.OrderQty != null &&
                    this.OrderQty.Equals(input.OrderQty))
                ) && 
                (
                    this.OrderType == input.OrderType ||
                    (this.OrderType != null &&
                    this.OrderType.Equals(input.OrderType))
                ) && 
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
                ) && 
                (
                    this.Side == input.Side ||
                    (this.Side != null &&
                    this.Side.Equals(input.Side))
                ) && 
                (
                    this.Symbol == input.Symbol ||
                    (this.Symbol != null &&
                    this.Symbol.Equals(input.Symbol))
                ) && 
                (
                    this.TradeTime == input.TradeTime ||
                    (this.TradeTime != null &&
                    this.TradeTime.Equals(input.TradeTime))
                ) && 
                (
                    this.TradeTimeMs == input.TradeTimeMs ||
                    (this.TradeTimeMs != null &&
                    this.TradeTimeMs.Equals(input.TradeTimeMs))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.ClosedSize != null)
                    hashCode = hashCode * 59 + this.ClosedSize.GetHashCode();
                if (this.ExecFee != null)
                    hashCode = hashCode * 59 + this.ExecFee.GetHashCode();
                if (this.ExecId != null)
                    hashCode = hashCode * 59 + this.ExecId.GetHashCode();
                if (this.ExecPrice != null)
                    hashCode = hashCode * 59 + this.ExecPrice.GetHashCode();
                if (this.ExecQty != null)
                    hashCode = hashCode * 59 + this.ExecQty.GetHashCode();
                if (this.ExecType != null)
                    hashCode = hashCode * 59 + this.ExecType.GetHashCode();
                if (this.ExecValue != null)
                    hashCode = hashCode * 59 + this.ExecValue.GetHashCode();
                if (this.FeeRate != null)
                    hashCode = hashCode * 59 + this.FeeRate.GetHashCode();
                if (this.LastLiquidityInd != null)
                    hashCode = hashCode * 59 + this.LastLiquidityInd.GetHashCode();
                if (this.LeavesQty != null)
                    hashCode = hashCode * 59 + this.LeavesQty.GetHashCode();
                if (this.OrderId != null)
                    hashCode = hashCode * 59 + this.OrderId.GetHashCode();
                if (this.OrderLinkId != null)
                    hashCode = hashCode * 59 + this.OrderLinkId.GetHashCode();
                if (this.OrderPrice != null)
                    hashCode = hashCode * 59 + this.OrderPrice.GetHashCode();
                if (this.OrderQty != null)
                    hashCode = hashCode * 59 + this.OrderQty.GetHashCode();
                if (this.OrderType != null)
                    hashCode = hashCode * 59 + this.OrderType.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                if (this.Side != null)
                    hashCode = hashCode * 59 + this.Side.GetHashCode();
                if (this.Symbol != null)
                    hashCode = hashCode * 59 + this.Symbol.GetHashCode();
                if (this.TradeTime != null)
                    hashCode = hashCode * 59 + this.TradeTime.GetHashCode();
                if (this.TradeTimeMs != null)
                    hashCode = hashCode * 59 + this.TradeTimeMs.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
