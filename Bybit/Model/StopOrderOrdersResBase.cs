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
    /// Get conditional order list
    /// </summary>
    [DataContract]
    public partial class StopOrderOrdersResBase :  IEquatable<StopOrderOrdersResBase>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopOrderOrdersResBase" /> class.
        /// </summary>
        /// <param name="retCode">retCode.</param>
        /// <param name="retMsg">retMsg.</param>
        /// <param name="extCode">extCode.</param>
        /// <param name="extInfo">extInfo.</param>
        /// <param name="result">result.</param>
        /// <param name="timeNow">timeNow.</param>
        public StopOrderOrdersResBase(decimal? retCode = default(decimal?), string retMsg = default(string), string extCode = default(string), string extInfo = default(string), Object result = default(Object), string timeNow = default(string))
        {
            this.RetCode = retCode;
            this.RetMsg = retMsg;
            this.ExtCode = extCode;
            this.ExtInfo = extInfo;
            this.Result = result;
            this.TimeNow = timeNow;
        }
        
        /// <summary>
        /// Gets or Sets RetCode
        /// </summary>
        [DataMember(Name="ret_code", EmitDefaultValue=false)]
        public decimal? RetCode { get; set; }

        /// <summary>
        /// Gets or Sets RetMsg
        /// </summary>
        [DataMember(Name="ret_msg", EmitDefaultValue=false)]
        public string RetMsg { get; set; }

        /// <summary>
        /// Gets or Sets ExtCode
        /// </summary>
        [DataMember(Name="ext_code", EmitDefaultValue=false)]
        public string ExtCode { get; set; }

        /// <summary>
        /// Gets or Sets ExtInfo
        /// </summary>
        [DataMember(Name="ext_info", EmitDefaultValue=false)]
        public string ExtInfo { get; set; }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name="result", EmitDefaultValue=false)]
        public Object Result { get; set; }

        /// <summary>
        /// Gets or Sets TimeNow
        /// </summary>
        [DataMember(Name="time_now", EmitDefaultValue=false)]
        public string TimeNow { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StopOrderOrdersResBase {\n");
            sb.Append("  RetCode: ").Append(RetCode).Append("\n");
            sb.Append("  RetMsg: ").Append(RetMsg).Append("\n");
            sb.Append("  ExtCode: ").Append(ExtCode).Append("\n");
            sb.Append("  ExtInfo: ").Append(ExtInfo).Append("\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
            sb.Append("  TimeNow: ").Append(TimeNow).Append("\n");
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
            return this.Equals(input as StopOrderOrdersResBase);
        }

        /// <summary>
        /// Returns true if StopOrderOrdersResBase instances are equal
        /// </summary>
        /// <param name="input">Instance of StopOrderOrdersResBase to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StopOrderOrdersResBase input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.RetCode == input.RetCode ||
                    (this.RetCode != null &&
                    this.RetCode.Equals(input.RetCode))
                ) && 
                (
                    this.RetMsg == input.RetMsg ||
                    (this.RetMsg != null &&
                    this.RetMsg.Equals(input.RetMsg))
                ) && 
                (
                    this.ExtCode == input.ExtCode ||
                    (this.ExtCode != null &&
                    this.ExtCode.Equals(input.ExtCode))
                ) && 
                (
                    this.ExtInfo == input.ExtInfo ||
                    (this.ExtInfo != null &&
                    this.ExtInfo.Equals(input.ExtInfo))
                ) && 
                (
                    this.Result == input.Result ||
                    (this.Result != null &&
                    this.Result.Equals(input.Result))
                ) && 
                (
                    this.TimeNow == input.TimeNow ||
                    (this.TimeNow != null &&
                    this.TimeNow.Equals(input.TimeNow))
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
                if (this.RetCode != null)
                    hashCode = hashCode * 59 + this.RetCode.GetHashCode();
                if (this.RetMsg != null)
                    hashCode = hashCode * 59 + this.RetMsg.GetHashCode();
                if (this.ExtCode != null)
                    hashCode = hashCode * 59 + this.ExtCode.GetHashCode();
                if (this.ExtInfo != null)
                    hashCode = hashCode * 59 + this.ExtInfo.GetHashCode();
                if (this.Result != null)
                    hashCode = hashCode * 59 + this.Result.GetHashCode();
                if (this.TimeNow != null)
                    hashCode = hashCode * 59 + this.TimeNow.GetHashCode();
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
