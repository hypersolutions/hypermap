using HyperMap.CodeGen;
using HyperMap.UnitTests.Support;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen
{
    public class MapMethodSignatureWithBodyTests
    {
        [Fact]
        public void WithBody_Create_ReturnsMethod()
        {
            var sourceType = typeof(User);
            var targetType = typeof(UserView);
            var localTargetSyntax = LocalTargetVariable.Create(targetType);
            var returnSyntax = ReturnTarget.Create();
            var body = new StatementSyntax[] {localTargetSyntax, returnSyntax};
            
            var syntax = MapMethodSignatureWithBody.Create(sourceType, targetType, body);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                "public HyperMap.UnitTests.Support.UserView Map(HyperMap.UnitTests.Support.User source)\r\n" +
                "{\r\n" +
                "    var target = new HyperMap.UnitTests.Support.UserView();\r\n" +
                "    return target;\r\n" +
                "}");
        }
    }
}