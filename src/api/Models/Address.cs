using Newtonsoft.Json;
using System;



namespace Titan.UFC.Addresses.API.Models
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
        [JsonIgnore]
        public int AddressID { get; set; }
        /// <summary>
        /// Unique guid to get the address
        /// </summary>
        [JsonProperty("addressUID")]
        public Guid? AddressUID { get; set; }
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
        public string Address1 { get; set; }
        /// <summary>
        /// Addressline 2
        /// </summary>
        [JsonProperty("addressLine2")]
        public string Address2 { get; set; }
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
        public string PinCode { get; set; }
        /// <summary>
        /// State code like "NY"
        /// </summary>
        [JsonProperty("stateProvinceRegion")]
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
        /// <summary>
        /// Validate and update the status
        /// </summary>
        [JsonProperty("isValidated")]
        public byte IsVerified { get; set; }
        /// <summary>
        /// Set address primary or not
        /// </summary>
        [JsonProperty("isPrimary")]
        public bool? IsPrimary { get; set; } = false;
        /// <summary>
        /// Created date of the address
        /// </summary>
        [JsonProperty("createdDate")]
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Updated date of the address
        /// </summary>
        [JsonProperty("updatedDate")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Updated date of the address
        /// </summary>
        [JsonProperty("type")]
        public byte Type { get; set; }

        [JsonIgnore]
        public int StateID { get; set; }

    }

    enum AddressType
    {
        Contact = 1,
        Billing = 2,
        Shipping = 3,
        Location = 4     
    }
}
