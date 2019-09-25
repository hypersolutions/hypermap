using HyperMap.Mapping;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class MapNamespace
    {
        private const string DefaultNamespace = "HyperMap.Custom";
        
        internal static NamespaceDeclarationSyntax Create(MapBase map)
        {
            var namespaceName = GetNamespaceName(map);
            return NamespaceDeclaration(namespaceName);
        }
        
        private static NameSyntax GetNamespaceName(MapBase map)
        {
            var namespaceText = !string.IsNullOrWhiteSpace(map.Namespace) 
                ? map.Namespace : DefaultNamespace;
            return ParseName(namespaceText);
        }
    }
}
