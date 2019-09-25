using System;
using System.Collections.Generic;
using System.Linq;
using HyperMap.Mapping;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HyperMap.CodeGen
{
    internal static class MapClass
    {
        internal static ClassDeclarationSyntax Create(
            Type sourceType, 
            Type targetType, 
            IEnumerable<MapInfo> mappings,
            IEnumerable<Type> converterTypes)
        {
            var converterTypeList = converterTypes.Distinct().ToList();
            var classSyntax = MapClassSignature.Create(sourceType, targetType);
            classSyntax = Constructor.Create(classSyntax, converterTypeList);

            for (var i = 0; i < converterTypeList.Count; i++)
            {
                var field = ConverterField.Create(converterTypeList[i], i);
                classSyntax = classSyntax.AddMembers(field);
            }
            
            var blockBits = new List<StatementSyntax>();
            var ifStatement = NullSourceCheck.Create(targetType);
            var targetVariable = LocalTargetVariable.Create(targetType);
            
            blockBits.Add(ifStatement);
            blockBits.Add(targetVariable);
            
            blockBits.AddRange((
                from m in mappings 
                let c = converterTypeList.First(c => c == m.Converter) 
                let i = converterTypeList.IndexOf(c)
                select TargetPropertyMapFromSource.Create(m.Source, m.Target, i)));

            blockBits.Add(ReturnTarget.Create());
            
            var methodDeclaration = MapMethodSignatureWithBody.Create(sourceType, targetType, blockBits);

            classSyntax = classSyntax.AddMembers(methodDeclaration);
            
            return classSyntax;
        }
    }
}
