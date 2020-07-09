using System.Linq;
using HyperMap.CodeGen.Refs;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen.Refs
{
    public class CommonReferencesTests
    {
        [Fact]
        public void ForMapping_Get_ReturnsReferences()
        {
            var commonReferences = new CommonReferences();

            var references = commonReferences.Get().ToList();
            
            references[0].Display.ShouldContain("mscorlib.dll");
            references[1].Display.ShouldContain("System.Runtime.dll");
            references[2].Display.ShouldContain("Private.CoreLib.dll");
            references[3].Display.ShouldContain("System.Collections.dll");
            references[4].Display.ShouldContain("netstandard.dll");
        }
    }
}
