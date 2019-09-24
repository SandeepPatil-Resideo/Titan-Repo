using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitanTemplate.titanaddressapi.Models.Common;

namespace TitanTemplate.titanaddressapi.Models
{
    /// <summary>
    /// Address model
    /// </summary>
    [Table("Address", Schema = "dbo")]
    public class Address
    {
        /// <summary>
        /// Primary key of the user address
        /// </summary>
        [JsonProperty("addressId")]
        [Key]
        public int AddressId { get; set; }
        /// <summary>
        /// Unique guid to get the address
        /// </summary>
        [JsonProperty("uniqueAddressId")]
        public Guid UniqueAddressId { get; set; } = new Guid();
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
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
        /// <summary>
        /// Emails
        /// </summary>
        [JsonProperty("primaryEmail")]
        public string PrimaryEmail { get; set; }
        /// <summary>
        /// Emails
        /// </summary>
        [JsonProperty("secondaryEmail")]
        public string SecondaryEmail { get; set; }
        /// <summary>
        /// mobile number belongs to the address
        /// </summary>
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
        /// <summary>
        /// Telephone number belongs to the address
        /// </summary>
        [JsonProperty("telephoneNumber")]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Fax belongs to the address
        /// </summary>
        [JsonProperty("fax")]
        public string Fax { get; set; }
    }
}
