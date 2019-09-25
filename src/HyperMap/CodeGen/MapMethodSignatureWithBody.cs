using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class MapMethodSignatureWithBody
    {
        internal static MethodDeclarationSyntax Create(
            Type sourceType,
            Type targetType,
            IEnumerable<StatementSyntax> blockBits)
        {
            var parameterTypeName = ParseTypeName(sourceType.FullName);
            var targetTypeName = ParseTypeName(targetType.FullName);
            
            return MethodDeclaration(targetTypeName, "Map")
                .AddParameterListParameters(Parameter(Identifier("source")).WithType(parameterTypeName))
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .WithBody(Block(blockBits));
        }
    }
}
