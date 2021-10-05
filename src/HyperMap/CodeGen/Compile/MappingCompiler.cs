using System.Collections.Generic;
using System.Linq;
using HyperMap.CodeGen.Refs;
using HyperMap.Mapping;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HyperMap.CodeGen.Compile
{
    internal sealed class MappingCompiler
    {
        private readonly CompilerOptions _options;
        private readonly CommonReferences _commonReferences = new();
        private readonly MappingReferences _mappingReferences = new();
        
        internal MappingCompiler(CompilerOptions options)
        {
            _options = options;
        }

        internal SyntaxTree ParseText(string code)
        {
            var options = new CSharpParseOptions(
                kind: SourceCodeKind.Regular, 
                languageVersion: _options.MaxLanguageVersion);
            return CSharpSyntaxTree.ParseText(code, options);
        }

        internal Compilation CreateCompiler(IEnumerable<MapBase> mappings, params SyntaxTree[] trees)
        {
            var references = _commonReferences.Get().Union(_mappingReferences.Get(mappings.ToList())).ToList();
            
            var options = new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Release,
                allowUnsafe: true);
            
            return CSharpCompilation.Create(_options.AssemblyName, trees, references, options);
        }
    }
}
