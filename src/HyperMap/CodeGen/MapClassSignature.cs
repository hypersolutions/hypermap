using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class MapClassSignature
    {
        internal static ClassDeclarationSyntax Create(Type sourceType, Type targetType)
        {
            var className = $"{sourceType.Name}To{targetType.Name}Mapper";
            return 
                ClassDeclaration(className)
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .AddBaseListTypes(SimpleBaseType(ParseTypeName(
                        $"IMapper<{sourceType.FullName}, {targetType.FullName}>")));
        }
    }
}
