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
            
            var locations = new List<string>();
            locations.Add(Path.Combine(baseLocationPath.FullName, "mscorlib.dll"));
            locations.Add(Path.Combine(baseLocationPath.FullName, "System.Runtime.dll"));
            locations.Add(typeof(object).GetTypeInfo().Assembly.Location);
            locations.Add(Assembly.Load("System.Collections").Location);
            locations.Add(Assembly.Load("netstandard").Location);

            return locations.Distinct();
        }
    }
}
