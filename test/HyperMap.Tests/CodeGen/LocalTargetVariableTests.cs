using HyperMap.CodeGen;
using HyperMap.Tests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
{
    public class LocalTargetVariableTests
    {
        [Fact]
        public void ForTargetType_Create_ReturnsLocalVariable()
        {
            var targetType = typeof(UserView);
            
            var syntax = LocalTargetVariable.Create(targetType);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldContain(
                "var target = new HyperMap.Tests.Support.UserView();");
        }
    }
}
