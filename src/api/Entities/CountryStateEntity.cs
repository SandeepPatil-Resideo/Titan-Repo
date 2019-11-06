using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Titan.UFC.Addresses.API.Entities
{
    [Table("CountryState", Schema = "dbo")]    
    public partial class CountryStateEntity
    {
        [Key]
        public int StateId { get; set; }
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
        public string AbbreviatedName { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public bool IsEnabled { get; set; }

    }
}
