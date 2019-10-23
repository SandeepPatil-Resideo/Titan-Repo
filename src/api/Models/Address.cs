using Newtonsoft.Json;
using System;



namespace Titan.Ufc.Addresses.API.Models
{
    /// <summary>
    /// Address model
    /// </summary>
    
    public class Address
    {
        /// <summary>
        /// Primary key of the user address
        /// </summary>
        [JsonProperty("id")]   
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Unique guid to get the address
        /// </summary>
        [JsonProperty("addressId")]
        public Guid AddressId { get; set; }
        /// <summary>
        /// contactName
        /// </summary>
        [JsonProperty("contactName")]
        public string ContactName { get; set; }

        /// <summary>
        /// contactName
        /// </summary>
        [JsonProperty("mailingAddressName")]
        public string MailingAddressName { get; set; }

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
        [JsonProperty("zipPostalCode")]
        public string ZipPostalCode { get; set; }
        /// <summary>
        /// State code like "NY"
        /// </summary>
        [JsonProperty("stateProvinceRegion")]
        public string StateProvinceRegion { get; set; }
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
        /// <summary>
        /// Validate and update the status
        /// </summary>
        [JsonProperty("isValidated")]
        public bool? IsValidated { get; set; }
        /// <summary>
        /// set address is primary or not
        /// </summary>
        [JsonProperty("isPrimary")]
        public bool? IsPrimary { get; set; }
    }
}
