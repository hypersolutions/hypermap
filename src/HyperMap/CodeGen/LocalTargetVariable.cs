using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class LocalTargetVariable
    {
        internal static LocalDeclarationStatementSyntax Create(Type targetType)
        {
            var targetTypeName = ParseTypeName(targetType.FullName);
            return LocalDeclarationStatement(VariableDeclaration(ParseTypeName("var"))
                .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("target"))
                    .WithInitializer(
                        EqualsValueClause(
                            ObjectCreationExpression(targetTypeName, ArgumentList(), null)
                                .WithNewKeyword(Token(SyntaxKind.NewKeyword)))))));
        }
    }
}