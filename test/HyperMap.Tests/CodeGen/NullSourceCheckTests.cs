using HyperMap.CodeGen;
using HyperMap.Tests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
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
                "    return default(HyperMap.Tests.Support.UserView);");
        }
    }
}
