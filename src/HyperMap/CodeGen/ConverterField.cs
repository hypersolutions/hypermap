using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class ConverterField
    {
        internal static FieldDeclarationSyntax Create(Type converterType, int converterTypeIndex)
        {
            var converterTypeName = ConverterType.Create(converterType);
            var variableDeclaration = CreateVariable(converterTypeName, converterTypeIndex);
            return CreateField(variableDeclaration);
        }
        
        private static VariableDeclarationSyntax CreateVariable(TypeSyntax converterTypeName, int converterTypeIndex)
        {
            var variableDeclarator = VariableDeclarator($"_converter{converterTypeIndex + 1}");
            return VariableDeclaration(converterTypeName).AddVariables(variableDeclarator);
        }
        
        private static FieldDeclarationSyntax CreateField(VariableDeclarationSyntax variable)
        {
            return FieldDeclaration(variable)
                .AddModifiers(Token(SyntaxKind.PrivateKeyword))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword));
        }
    }
}
