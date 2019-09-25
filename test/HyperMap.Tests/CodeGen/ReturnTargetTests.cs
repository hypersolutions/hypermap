using HyperMap.CodeGen;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
{
    public class ReturnTargetTests
    {
        [Fact]
        public void ReturnTarget_Target_Create_ReturnsReturnTarget()
        {
            var syntax = ReturnTarget.Create();
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe("return target;");
        }
    }
}
