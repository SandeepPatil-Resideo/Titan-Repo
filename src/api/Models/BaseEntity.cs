using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanTemplate.titanaddressapi.Models
{
    /// <summary>
    /// Base entity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// User who create the resource
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Date of created resorce
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// User who modified the  resource
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Date of modified resource
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}
