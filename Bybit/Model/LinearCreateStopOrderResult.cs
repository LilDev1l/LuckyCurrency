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
    /// Create Linear Stop Order
    /// </summary>
    [DataContract]
    public partial class LinearCreateStopOrderResult :  IEquatable<LinearCreateStopOrderResult>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearCreateStopOrderResult" /> class.
        /// </summary>
        /// <param name="stopOrderId">stopOrderId.</param>
        /// <param name="userId">userId.</param>
        /// <param name="side">side.</param>
        /// <param name="symbol">symbol.</param>
        /// <param name="orderType">orderType.</param>
        /// <param name="price">price.</param>
        /// <param name="qty">qty.</param>
        /// <param name="timeInForce">timeInForce.</param>
        /// <param name="orderStatus">orderStatus.</param>
        /// <param name="triggerPrice">triggerPrice.</param>
        /// <param name="orderLinkId">orderLinkId.</param>
        /// <param name="createdAt">createdAt.</param>
        /// <param name="updatedAt">updatedAt.</param>
        /// <param name="takeProfit">takeProfit.</param>
        /// <param name="stopLoss">stopLoss.</param>
        /// <param name="tpTriggerBy">tpTriggerBy.</param>
        /// <param name="slTriggerBy">slTriggerBy.</param>
        public LinearCreateStopOrderResult(string stopOrderId = default(string), long? userId = default(long?), string side = default(string), string symbol = default(string), string orderType = default(string), double? price = default(double?), double? qty = default(double?), string timeInForce = default(string), string orderStatus = default(string), double? triggerPrice = default(double?), string orderLinkId = default(string), string createdAt = default(string), string updatedAt = default(string), double? takeProfit = default(double?), double? stopLoss = default(double?), string tpTriggerBy = default(string), string slTriggerBy = default(string))
        {
            this.StopOrderId = stopOrderId;
            this.UserId = userId;
            this.Side = side;
            this.Symbol = symbol;
            this.OrderType = orderType;
            this.Price = price;
            this.Qty = qty;
            this.TimeInForce = timeInForce;
            this.OrderStatus = orderStatus;
            this.TriggerPrice = triggerPrice;
            this.OrderLinkId = orderLinkId;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.TakeProfit = takeProfit;
            this.StopLoss = stopLoss;
            this.TpTriggerBy = tpTriggerBy;
            this.SlTriggerBy = slTriggerBy;
        }
        
        /// <summary>
        /// Gets or Sets StopOrderId
        /// </summary>
        [DataMember(Name="stop_order_id", EmitDefaultValue=false)]
        public string StopOrderId { get; set; }

        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        [DataMember(Name="user_id", EmitDefaultValue=false)]
        public long? UserId { get; set; }

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
        /// Gets or Sets Qty
        /// </summary>
        [DataMember(Name="qty", EmitDefaultValue=false)]
        public double? Qty { get; set; }

        /// <summary>
        /// Gets or Sets TimeInForce
        /// </summary>
        [DataMember(Name="time_in_force", EmitDefaultValue=false)]
        public string TimeInForce { get; set; }

        /// <summary>
        /// Gets or Sets OrderStatus
        /// </summary>
        [DataMember(Name="order_status", EmitDefaultValue=false)]
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or Sets TriggerPrice
        /// </summary>
        [DataMember(Name="trigger_price", EmitDefaultValue=false)]
        public double? TriggerPrice { get; set; }

        /// <summary>
        /// Gets or Sets OrderLinkId
        /// </summary>
        [DataMember(Name="order_link_id", EmitDefaultValue=false)]
        public string OrderLinkId { get; set; }

        /// <summary>
        /// Gets or Sets CreatedAt
        /// </summary>
        [DataMember(Name="created_at", EmitDefaultValue=false)]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedAt
        /// </summary>
        [DataMember(Name="updated_at", EmitDefaultValue=false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// Gets or Sets TakeProfit
        /// </summary>
        [DataMember(Name="take_profit", EmitDefaultValue=false)]
        public double? TakeProfit { get; set; }

        /// <summary>
        /// Gets or Sets StopLoss
        /// </summary>
        [DataMember(Name="stop_loss", EmitDefaultValue=false)]
        public double? StopLoss { get; set; }

        /// <summary>
        /// Gets or Sets TpTriggerBy
        /// </summary>
        [DataMember(Name="tp_trigger_by", EmitDefaultValue=false)]
        public string TpTriggerBy { get; set; }

        /// <summary>
        /// Gets or Sets SlTriggerBy
        /// </summary>
        [DataMember(Name="sl_trigger_by", EmitDefaultValue=false)]
        public string SlTriggerBy { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LinearCreateStopOrderResult {\n");
            sb.Append("  StopOrderId: ").Append(StopOrderId).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  Side: ").Append(Side).Append("\n");
            sb.Append("  Symbol: ").Append(Symbol).Append("\n");
            sb.Append("  OrderType: ").Append(OrderType).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Qty: ").Append(Qty).Append("\n");
            sb.Append("  TimeInForce: ").Append(TimeInForce).Append("\n");
            sb.Append("  OrderStatus: ").Append(OrderStatus).Append("\n");
            sb.Append("  TriggerPrice: ").Append(TriggerPrice).Append("\n");
            sb.Append("  OrderLinkId: ").Append(OrderLinkId).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
            sb.Append("  TakeProfit: ").Append(TakeProfit).Append("\n");
            sb.Append("  StopLoss: ").Append(StopLoss).Append("\n");
            sb.Append("  TpTriggerBy: ").Append(TpTriggerBy).Append("\n");
            sb.Append("  SlTriggerBy: ").Append(SlTriggerBy).Append("\n");
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
            return this.Equals(input as LinearCreateStopOrderResult);
        }

        /// <summary>
        /// Returns true if LinearCreateStopOrderResult instances are equal
        /// </summary>
        /// <param name="input">Instance of LinearCreateStopOrderResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LinearCreateStopOrderResult input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.StopOrderId == input.StopOrderId ||
                    (this.StopOrderId != null &&
                    this.StopOrderId.Equals(input.StopOrderId))
                ) && 
                (
                    this.UserId == input.UserId ||
                    (this.UserId != null &&
                    this.UserId.Equals(input.UserId))
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
                    this.Qty == input.Qty ||
                    (this.Qty != null &&
                    this.Qty.Equals(input.Qty))
                ) && 
                (
                    this.TimeInForce == input.TimeInForce ||
                    (this.TimeInForce != null &&
                    this.TimeInForce.Equals(input.TimeInForce))
                ) && 
                (
                    this.OrderStatus == input.OrderStatus ||
                    (this.OrderStatus != null &&
                    this.OrderStatus.Equals(input.OrderStatus))
                ) && 
                (
                    this.TriggerPrice == input.TriggerPrice ||
                    (this.TriggerPrice != null &&
                    this.TriggerPrice.Equals(input.TriggerPrice))
                ) && 
                (
                    this.OrderLinkId == input.OrderLinkId ||
                    (this.OrderLinkId != null &&
                    this.OrderLinkId.Equals(input.OrderLinkId))
                ) && 
                (
                    this.CreatedAt == input.CreatedAt ||
                    (this.CreatedAt != null &&
                    this.CreatedAt.Equals(input.CreatedAt))
                ) && 
                (
                    this.UpdatedAt == input.UpdatedAt ||
                    (this.UpdatedAt != null &&
                    this.UpdatedAt.Equals(input.UpdatedAt))
                ) && 
                (
                    this.TakeProfit == input.TakeProfit ||
                    (this.TakeProfit != null &&
                    this.TakeProfit.Equals(input.TakeProfit))
                ) && 
                (
                    this.StopLoss == input.StopLoss ||
                    (this.StopLoss != null &&
                    this.StopLoss.Equals(input.StopLoss))
                ) && 
                (
                    this.TpTriggerBy == input.TpTriggerBy ||
                    (this.TpTriggerBy != null &&
                    this.TpTriggerBy.Equals(input.TpTriggerBy))
                ) && 
                (
                    this.SlTriggerBy == input.SlTriggerBy ||
                    (this.SlTriggerBy != null &&
                    this.SlTriggerBy.Equals(input.SlTriggerBy))
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
                if (this.StopOrderId != null)
                    hashCode = hashCode * 59 + this.StopOrderId.GetHashCode();
                if (this.UserId != null)
                    hashCode = hashCode * 59 + this.UserId.GetHashCode();
                if (this.Side != null)
                    hashCode = hashCode * 59 + this.Side.GetHashCode();
                if (this.Symbol != null)
                    hashCode = hashCode * 59 + this.Symbol.GetHashCode();
                if (this.OrderType != null)
                    hashCode = hashCode * 59 + this.OrderType.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                if (this.Qty != null)
                    hashCode = hashCode * 59 + this.Qty.GetHashCode();
                if (this.TimeInForce != null)
                    hashCode = hashCode * 59 + this.TimeInForce.GetHashCode();
                if (this.OrderStatus != null)
                    hashCode = hashCode * 59 + this.OrderStatus.GetHashCode();
                if (this.TriggerPrice != null)
                    hashCode = hashCode * 59 + this.TriggerPrice.GetHashCode();
                if (this.OrderLinkId != null)
                    hashCode = hashCode * 59 + this.OrderLinkId.GetHashCode();
                if (this.CreatedAt != null)
                    hashCode = hashCode * 59 + this.CreatedAt.GetHashCode();
                if (this.UpdatedAt != null)
                    hashCode = hashCode * 59 + this.UpdatedAt.GetHashCode();
                if (this.TakeProfit != null)
                    hashCode = hashCode * 59 + this.TakeProfit.GetHashCode();
                if (this.StopLoss != null)
                    hashCode = hashCode * 59 + this.StopLoss.GetHashCode();
                if (this.TpTriggerBy != null)
                    hashCode = hashCode * 59 + this.TpTriggerBy.GetHashCode();
                if (this.SlTriggerBy != null)
                    hashCode = hashCode * 59 + this.SlTriggerBy.GetHashCode();
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
