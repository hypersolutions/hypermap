using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

namespace HyperMap.CodeGen.Compile
{
    public sealed class CompilerOptions
    {
        private const string DefaultNamespace = "HyperMap.Custom.Mappings";
        private string _assemblyName;
        
        public CompilerOptions()
        {
            AssemblyName = DefaultNamespace;
        }
        
        public string AssemblyName 
        { 
            get => !string.IsNullOrWhiteSpace(_assemblyName) ? _assemblyName : DefaultNamespace;
            set => _assemblyName = value; 
        }
        
        public LanguageVersion MaxLanguageVersion => 
            Enum.GetValues(typeof(LanguageVersion)).Cast<LanguageVersion>().Max();
    }
}
