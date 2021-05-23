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
    /// LinearSetMarginResult
    /// </summary>
    [DataContract]
    public partial class LinearSetMarginResult :  IEquatable<LinearSetMarginResult>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearSetMarginResult" /> class.
        /// </summary>
        /// <param name="positionListResult">positionListResult.</param>
        /// <param name="availableBalance">availableBalance.</param>
        /// <param name="walletBalance">walletBalance.</param>
        public LinearSetMarginResult(Object positionListResult = default(Object), double? availableBalance = default(double?), double? walletBalance = default(double?))
        {
            this.PositionListResult = positionListResult;
            this.AvailableBalance = availableBalance;
            this.WalletBalance = walletBalance;
        }
        
        /// <summary>
        /// Gets or Sets PositionListResult
        /// </summary>
        [DataMember(Name="PositionListResult", EmitDefaultValue=false)]
        public Object PositionListResult { get; set; }

        /// <summary>
        /// Gets or Sets AvailableBalance
        /// </summary>
        [DataMember(Name="available_balance", EmitDefaultValue=false)]
        public double? AvailableBalance { get; set; }

        /// <summary>
        /// Gets or Sets WalletBalance
        /// </summary>
        [DataMember(Name="wallet_balance", EmitDefaultValue=false)]
        public double? WalletBalance { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LinearSetMarginResult {\n");
            sb.Append("  PositionListResult: ").Append(PositionListResult).Append("\n");
            sb.Append("  AvailableBalance: ").Append(AvailableBalance).Append("\n");
            sb.Append("  WalletBalance: ").Append(WalletBalance).Append("\n");
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
            return this.Equals(input as LinearSetMarginResult);
        }

        /// <summary>
        /// Returns true if LinearSetMarginResult instances are equal
        /// </summary>
        /// <param name="input">Instance of LinearSetMarginResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LinearSetMarginResult input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.PositionListResult == input.PositionListResult ||
                    (this.PositionListResult != null &&
                    this.PositionListResult.Equals(input.PositionListResult))
                ) && 
                (
                    this.AvailableBalance == input.AvailableBalance ||
                    (this.AvailableBalance != null &&
                    this.AvailableBalance.Equals(input.AvailableBalance))
                ) && 
                (
                    this.WalletBalance == input.WalletBalance ||
                    (this.WalletBalance != null &&
                    this.WalletBalance.Equals(input.WalletBalance))
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
                if (this.PositionListResult != null)
                    hashCode = hashCode * 59 + this.PositionListResult.GetHashCode();
                if (this.AvailableBalance != null)
                    hashCode = hashCode * 59 + this.AvailableBalance.GetHashCode();
                if (this.WalletBalance != null)
                    hashCode = hashCode * 59 + this.WalletBalance.GetHashCode();
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