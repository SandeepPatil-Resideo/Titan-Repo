using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanTemplate.titanaddressapi.Models.Common
{
    /// <summary>
    /// Email Model belongs to that address
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Type like primary/alternate
        /// </summary>
        [JsonProperty("countryCode")]
        public int Type { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        [JsonProperty("email")]
        public string Address { get; set; }
        /// <summary>
        /// Set true once the email address is verified
        /// </summary>
        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }
    }
}
