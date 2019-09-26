using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TitanTemplate.titanaddressapi.Entities
{
    [Table("Address", Schema = "Address")]
    public partial class AddressEntity
    {
        
        public AddressEntity()
        {
            Address = new HashSet<AddressEntity>();
        }
        [Key]
        public int AddressId { get; set; }
        public Guid Uuid { get; set; }
        public string ContactName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ContactNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual ICollection<AddressEntity> Address { get; set; }
    }
}
