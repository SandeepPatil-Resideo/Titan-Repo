using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Titan.Ufc.Addresses.API.Entities
{
    [Table("Address", Schema = "dbo")]
    public partial class AddressEntity
    {
        [Key]
        public int AddressID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string City { get; set; }

        public int StateID { get; set; }

        public string PinCode { get; set; }

        public string CountryCode { get; set; }

        public byte IsVerified { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AddressUID { get; set; }

        public string ContactName { get; set; }
        public string MailingAddressName { get; set; }

        public string AddressLine3 { get; set; }
    
        public string ContactNumber { get; set; }

        public bool? isPrimary { get; set; }

        public byte Type { get; set; }

        
    }

}
