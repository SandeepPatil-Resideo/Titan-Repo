﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Titan.Ufc.Addresses.API.Entities
{
    [Table("PartnerAddress", Schema = "ADDRESS")]
    public partial class AddressEntity
    {
        [Key]
        public int ID { get; set; }
        public Guid AddressId { get; set; }
        public string ContactName { get; set; }
        public string MailingAddressName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string ZipPostalCode { get; set; }
        public string StateProvinceRegion { get; set; }
        public string CountryCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string ContactNumber { get; set; }
        public bool? IsValidated { get; set; }
        public bool? IsPrimary { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
