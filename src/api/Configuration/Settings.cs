using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Ufc.Addresses.API.Configuration
{
    public class Settings
    {
        /// <summary>
        /// Number of consecutive failures before the observer becomes unhealthy
        /// </summary>
        public int ObserverFailures { get; set; }
        /// <summary>
        /// Number of consecutive successes before the observer returns to a healthy state
        /// </summary>
        public int ObserverSuccesses { get; set; }
    }
}
