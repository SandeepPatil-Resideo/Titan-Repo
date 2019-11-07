using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Titan.UFC.Addresses.API.Entities
{
    [Table("vw_States", Schema = "dbo")]    
    public partial class CountryStateEntity
    {
        
        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// State name
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// State code
        /// </summary>
        [Key]
        public string StateCode { get; set; }
       

    }
}
