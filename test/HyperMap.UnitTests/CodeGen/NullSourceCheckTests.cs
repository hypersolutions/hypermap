using HyperMap.CodeGen;
using HyperMap.UnitTests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen
{
    public class NullSourceCheckTests
    {
        [Fact]
        public void ForTargetType_Create_ReturnsIfStatementWithDefaultReturn()
        {
            var targetType = typeof(UserView);

            var syntax = NullSourceCheck.Create(targetType);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldContain(
                "if (source == null)\r\n" +
                "    return default(HyperMap.UnitTests.Support.UserView);");
        }
    }
}
