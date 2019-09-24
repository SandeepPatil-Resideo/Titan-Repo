using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanTemplate.titanaddressapi.Models.Common
{
    /// <summary>
    /// Phone number model 
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// Type(e.g work/home)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Country code like "UAS"
        /// </summary>
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
        /// <summary>
        /// phone number belongs to that address
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string Number { get; set; }
        /// <summary>
        /// verify the phone number
        /// </summary>
        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }
    }
} 

