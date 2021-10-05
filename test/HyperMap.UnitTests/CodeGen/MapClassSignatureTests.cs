using HyperMap.CodeGen;
using HyperMap.UnitTests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen
{
    public class MapClassSignatureTests
    {
        [Fact]
        public void ForSourceAndTarget_Create_ReturnsClassSignature()
        {
            var sourceType = typeof(User);
            var targetType = typeof(UserView);

            var syntax = MapClassSignature.Create(sourceType, targetType);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                "public class UserToUserViewMapper : " +
                "IMapper<HyperMap.UnitTests.Support.User, HyperMap.UnitTests.Support.UserView>\r\n" +
                "{\r\n" +
                "}");
        }
    }
}
