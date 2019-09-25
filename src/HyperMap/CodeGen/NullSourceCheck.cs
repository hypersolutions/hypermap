using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class NullSourceCheck
    {
        internal static IfStatementSyntax Create(Type targetType)
        {
            var targetTypeName = ParseTypeName(targetType.FullName);
            var defaultType = ReturnStatement(DefaultExpression(targetTypeName));
            return IfStatement(
                BinaryExpression(
                    SyntaxKind.EqualsExpression, 
                    IdentifierName(Identifier("source")), 
                    IdentifierName("null")),
                defaultType);
        }
    }
}