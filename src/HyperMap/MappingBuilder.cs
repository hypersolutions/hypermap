using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HyperMap.CodeGen;
using HyperMap.CodeGen.Compile;
using HyperMap.Discovery;
using HyperMap.Exceptions;
using HyperMap.Mapping;
using Microsoft.CodeAnalysis;

namespace HyperMap
{
    public sealed class MappingBuilder
    {
        private readonly List<MapBase> _mappings = new List<MapBase>();
        private readonly List<string> _visitedAssemblyLocations = new List<string>();

        private MappingBuilder(IEnumerable<MapBase> mappings, Type assemblyType)
        {
            _visitedAssemblyLocations.Add(assemblyType.Assembly.Location);
            _mappings.AddRange(mappings);
        }
        
        public static MappingBuilder DiscoverIn<TAssemblyType>()
        {
            var discover = new Discover();
            var mappings = discover.FindFrom<TAssemblyType>();
            return new MappingBuilder(mappings, typeof(TAssemblyType));
        }
        
        public MappingBuilder AndDiscoverIn<TAssemblyType>()
        {
            var assemblyType = typeof(TAssemblyType);

            if (_visitedAssemblyLocations.Contains(assemblyType.Assembly.Location))
                throw new ArgumentException($"Already added mappings in assembly {assemblyType.Assembly.FullName}.");
            
            var discover = new Discover();
            var mappings = discover.FindFrom<TAssemblyType>();
            _mappings.AddRange(mappings);
            return this;
        }

        public IMappingFactory BuildFactory(CompilerOptions options = null)
        {
            options = options ?? new CompilerOptions();
            var compiler = new MappingCompiler(options);
            var trees = new List<SyntaxTree>();
            
            foreach (var mapping in _mappings)
            {
                var namespaceSyntax = MapCodeGen.Create(mapping);
            
                var code = namespaceSyntax
                    .NormalizeWhitespace()
                    .ToFullString();
                
                var tree = compiler.ParseText(code);
                trees.Add(tree);
            }

            var compilation = compiler.CreateCompiler(_mappings, trees.ToArray());

            return HandleCompilationResult(compilation);
        }
        
        private static IMappingFactory HandleCompilationResult(Compilation compilation)
        {
            using (var stream = new MemoryStream())
            {
                var emitResult = compilation.Emit(stream);

                if (!emitResult.Success) throw new MappingCompilationException(emitResult.Diagnostics);
                
                stream.Seek(0, SeekOrigin.Begin);
                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return new MappingFactory(Assembly.Load(data));
            }
        }
    }
}
