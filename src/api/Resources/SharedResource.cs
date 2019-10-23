using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Titan.Ufc.Addresses.API.Resources
{
    public interface ISharedResource
    {
        LocalizedString this[string name] { get; }
    }

    /// <summary>
    /// SharedResource for localisation
    /// </summary>
    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        LocalizedString ISharedResource.this[string name] => _localizer[name];
    }
}
