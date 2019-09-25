using System.Linq;
using HyperMap.Mapping;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HyperMap.CodeGen
{
    internal static class MapCodeGen
    {
        internal static NamespaceDeclarationSyntax Create(MapBase map)
        {
            var namespaceSyntax = MapNamespace.Create(map);

            var classSyntax = MapClass.Create(map.SourceType, map.TargetType, map.Mappings,
                map.Mappings.Select(m => m.Converter));

            namespaceSyntax = namespaceSyntax.AddMembers(classSyntax);
            
            return namespaceSyntax;
        }
    }
}
