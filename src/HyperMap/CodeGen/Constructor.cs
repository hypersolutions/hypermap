using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class Constructor
    {
        internal static ClassDeclarationSyntax Create(ClassDeclarationSyntax classSyntax, IEnumerable<Type> converterTypes)
        {
            var parameterTypeName = ParseTypeName(typeof(IMappingFactory).FullName);
            
            var converters = converterTypes.Distinct().ToList();
            
            var blockBits = new List<StatementSyntax>();
            
            for (var i = 0; i < converters.Count; i++)
            {
                var converterFieldName = IdentifierName($"_converter{i + 1}");
                var converterType = converters[i];
                var converterTypeName = ConverterType.Create(converterType);
                var arguments = ArgumentList();
                var newConverterStatement = ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        converterFieldName,
                        ObjectCreationExpression(converterTypeName)
                            .WithArgumentList(arguments)
                            .WithNewKeyword(Token(SyntaxKind.NewKeyword))));
                blockBits.Add(newConverterStatement);

                var mappingFactoryField = MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    converterFieldName,
                    Token(SyntaxKind.DotToken),
                    IdentifierName("MappingFactory"));
                
                var expression = AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression, 
                    mappingFactoryField,
                    Token(SyntaxKind.EqualsToken), 
                    IdentifierName("mappingFactory"));
                blockBits.Add(ExpressionStatement(expression, Token(SyntaxKind.SemicolonToken)));
            }
            
            var constructor = ConstructorDeclaration(classSyntax.Identifier)
                .AddParameterListParameters(Parameter(Identifier("mappingFactory")).WithType(parameterTypeName))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block()
                    .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
                    .AddStatements(blockBits.ToArray())
                    .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken)));
            
            return classSyntax.AddMembers(constructor);
        }
    }
}
