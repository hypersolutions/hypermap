using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HyperMap.Mapping;
using Microsoft.CodeAnalysis;
using TypeInfo = System.Reflection.TypeInfo;

namespace HyperMap.CodeGen.Refs
{
    internal sealed class MappingReferences
    {
        internal IEnumerable<MetadataReference> Get(List<MapBase> mappings)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            
            var sourceTypes = mappings.Select(m => m.SourceType.GetTypeInfo()).ToList();
            var targetTypes = mappings.Select(m => m.TargetType.GetTypeInfo()).ToList();
            var sourcePropertyTypes = sourceTypes.SelectMany(
                m => m.GetProperties(flags).Select(p => p.PropertyType.GetTypeInfo()));
            var targetPropertyTypes = targetTypes.SelectMany(
                m => m.GetProperties(flags).Select(p => p.PropertyType.GetTypeInfo()));
            
            var converterTypes = mappings.SelectMany(m => m.Mappings.Select(p => p.Converter.GetTypeInfo()));
            var locations = GetLocations(
                sourceTypes.Union(sourcePropertyTypes), 
                targetTypes.Union(targetPropertyTypes), 
                converterTypes);
            
            return locations.Select(r => MetadataReference.CreateFromFile(r)).ToList();
        }

        private static IEnumerable<string> GetLocations(
            IEnumerable<TypeInfo> sourceTypes, 
            IEnumerable<TypeInfo> targetTypes,
            IEnumerable<TypeInfo> converterTypes)
        {
            var locations = new List<string>();
            locations.AddRange(converterTypes.Select(t => t.Assembly.Location));
            locations.AddRange(sourceTypes.Select(t => t.Assembly.Location));
            locations.AddRange(targetTypes.Select(t => t.Assembly.Location));
            locations = locations.Distinct().ToList();
            
            return locations;
        }
    }
}
