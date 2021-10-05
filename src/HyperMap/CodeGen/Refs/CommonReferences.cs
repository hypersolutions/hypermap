using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace HyperMap.CodeGen.Refs
{
    internal sealed class CommonReferences
    {
        internal IEnumerable<MetadataReference> Get()
        {
            var locations = GetLocations();
            
            return locations.Select(l => MetadataReference.CreateFromFile(l)).ToList();
        }

        private static IEnumerable<string> GetLocations()
        {
            var baseLocation = typeof(Enumerable).GetTypeInfo().Assembly.Location;
            var baseLocationPath = Directory.GetParent(baseLocation);
            
            var locations = new List<string>
            {
                Path.Combine(baseLocationPath!.FullName, "mscorlib.dll"),
                Path.Combine(baseLocationPath.FullName, "System.Runtime.dll"),
                typeof(object).GetTypeInfo().Assembly.Location,
                Assembly.Load("System.Collections").Location,
                Assembly.Load("netstandard").Location
            };

            return locations.Distinct();
        }
    }
}
