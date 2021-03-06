using System.Collections.Generic;
using System.Linq;
using HyperMap.CodeGen.Refs;
using HyperMap.Mapping;
using HyperMap.Tests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen.Refs
{
    public class MappingReferencesTests
    {
        [Fact]
        public void MappingReferences_SingleMapping_Get_ReturnsDistinctReferences()
        {
            var mappingReferences = new MappingReferences();
            var mappings = new List<MapBase>(new[] {new UserToUserViewMap()});
            
            var references = mappingReferences.Get(mappings).ToList();
            
            references[0].Display.ShouldContain("HyperMap.dll");
            references[1].Display.ShouldContain("HyperMap.Tests.dll");
        }
    }
}
