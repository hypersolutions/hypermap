using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class ConverterFieldInit
    {
        internal static IEnumerable<StatementSyntax> Create(Type converterType, int converterTypeIndex)
        {
            var converterFieldName = IdentifierName($"_converter{converterTypeIndex + 1}");
            var converterTypeName = ConverterType.Create(converterType);
            var arguments = ArgumentList();
            yield return ExpressionStatement(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    converterFieldName,
                    ObjectCreationExpression(converterTypeName).WithArgumentList(arguments)
                        .WithNewKeyword(Token(SyntaxKind.NewKeyword))));
            
            var mappingFactoryField = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                converterFieldName, Token(SyntaxKind.DotToken),
                IdentifierName("MappingFactory"));
            var expression = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, mappingFactoryField,
                Token(SyntaxKind.EqualsToken), IdentifierName("mappingFactory"));
            yield return ExpressionStatement(expression, Token(SyntaxKind.SemicolonToken));
        }
    }
}
