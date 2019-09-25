using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HyperMap.CodeGen
{
    internal static class ConverterType
    {
        internal static TypeSyntax Create(Type converterType)
        {
            var safeConverterTypeName = GenerateSafeConverterType(converterType);
            return ParseTypeName(safeConverterTypeName);
        }
        
        private static string GenerateSafeConverterType(Type converterType)
        {
            var safeConverterType = new StringBuilder();
            var converterTypeName = converterType.Name.Replace("`1", string.Empty).Replace("`2", string.Empty);
            safeConverterType.Append($"{converterType.Namespace}.{converterTypeName}");
            var genericArgs = converterType.GetGenericArguments();
            
            if (genericArgs.Any())
            {
                var args = string.Join(",", genericArgs.Select(a => a.FullName));                
                safeConverterType.Append($"<{args}>");
            }

            return safeConverterType.ToString();
        }
    }
}
