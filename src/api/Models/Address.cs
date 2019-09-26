using Newtonsoft.Json;
using System;



namespace TitanTemplate.titanaddressapi.Models
{
    /// <summary>
    /// Address model
    /// </summary>
    
    public class Address
    {
        /// <summary>
        /// Primary key of the user address
        /// </summary>
        [JsonProperty("addressId")]        
        public int AddressId { get; set; }
        /// <summary>
        /// Unique guid to get the address
        /// </summary>
        [JsonProperty("uniqueAddressId")]
        public Guid UniqueAddressId { get; set; } = new Guid();
        /// <summary>
        /// contactName
        /// </summary>
        [JsonProperty("contactName")]
        public string Name { get; set; }
        /// <summary>
        /// contactNumber
        /// </summary>
        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; }
        /// <summary>
        /// Addressline 1 
        /// </summary>
        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }
        /// <summary>
        /// Addressline 2
        /// </summary>
        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }
        /// <summary>
        /// Addressline 3
        /// </summary>
        [JsonProperty("addressLine3")]
        public string AddressLine3 { get; set; }
        /// <summary>
        /// City
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }
        /// <summary>
        /// Zipcode 
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        /// <summary>
        /// State code like "NY"
        /// </summary>
        [JsonProperty("stateCode")]
        public string StateCode { get; set; }
        /// <summary>
        /// Country code like "USA"
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }
     
    }
}
