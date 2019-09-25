using HyperMap.Mapping;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class TargetPropertyMapFromSource
    {
        internal static ExpressionStatementSyntax Create(MapItem source, MapItem target, int converterTypeIndex)
        {
            var targetProperty = MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName("target"), Token(SyntaxKind.DotToken),
                IdentifierName(target.Name));

            SeparatedSyntaxList<ArgumentSyntax> argumentList;
            
            if (source.Name != null)
            {
                var sourceProperty = Argument(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("source"), Token(SyntaxKind.DotToken),
                        IdentifierName(source.Name)));
                argumentList = SeparatedList(new[] {sourceProperty});
            }
            else
            {
                var sourceObject = Argument(IdentifierName("source"));
                argumentList = SeparatedList(new[] {sourceObject});
            }

            var converterMethod = InvocationExpression(MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName($"_converter{converterTypeIndex + 1}"), Token(SyntaxKind.DotToken),
                    IdentifierName("Convert")) ,
                ArgumentList(argumentList));

            var expression = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, targetProperty,
                Token(SyntaxKind.EqualsToken), converterMethod);

            return ExpressionStatement(expression, Token(SyntaxKind.SemicolonToken));
        }
    }
}