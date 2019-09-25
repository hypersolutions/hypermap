using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class ReturnTarget
    {
        internal static ReturnStatementSyntax Create()
        {
            return ReturnStatement(IdentifierName("target"));
        }
    }
}
