using HyperMap.CodeGen;
using HyperMap.Tests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
{
    public class MapClassSignatureTests
    {
        [Fact]
        public void MapClassSignature_ForSourceAndTarget_Create_ReturnsClassSignature()
        {
            var sourceType = typeof(User);
            var targetType = typeof(UserView);

            var syntax = MapClassSignature.Create(sourceType, targetType);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                "public class UserToUserViewMapper : " +
                "IMapper<HyperMap.Tests.Support.User, HyperMap.Tests.Support.UserView>\r\n" +
                "{\r\n" +
                "}");
        }
    }
}
