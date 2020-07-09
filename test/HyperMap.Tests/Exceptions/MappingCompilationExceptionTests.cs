using System.Collections.Generic;
using HyperMap.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Exceptions
{
    public class MappingCompilationExceptionTests
    {
        [Fact]
        public void WithSingleError_Ctor_SetsMessage()
        {
            var diagnostic = CreateDiagnostic(
                "1234", "Test Error", "Missing semicolon", "Compilation", @"c:\temp\file.cs");
            var errors = new List<Diagnostic> {diagnostic};
            var exception = new MappingCompilationException(errors);

            var message = exception.Message;
            
            message.ShouldBe(
                "ID: 1234, Message: Missing semicolon, Location: c:\\temp\\file.cs: (0,0)-(5,3), Severity: Error\r\n");
        }
        
        [Fact]
        public void WithMultipleErrors_Ctor_SetsMessage()
        {
            var diagnostic1 = CreateDiagnostic(
                "1234", "Test Error", "Missing semicolon", "Compilation", @"c:\temp\file.cs");
            var diagnostic2 = CreateDiagnostic(
                "5678", "Test Error", "Missing dot", "Compilation", @"c:\temp\file.cs");
            var errors = new List<Diagnostic> {diagnostic1, diagnostic2};
            var exception = new MappingCompilationException(errors);

            var message = exception.Message;
            
            message.ShouldBe(
                "ID: 1234, Message: Missing semicolon, Location: c:\\temp\\file.cs: (0,0)-(5,3), Severity: Error\r\n" +
                "ID: 5678, Message: Missing dot, Location: c:\\temp\\file.cs: (0,0)-(5,3), Severity: Error\r\n");
        }

        private static Diagnostic CreateDiagnostic(
            string id, 
            string title,
            string message,
            string category,
            string file)
        {
            var descriptor = new DiagnosticDescriptor(
                id, 
                title, 
                message, 
                category,
                DiagnosticSeverity.Error, 
                true);
            var location = Location.Create(
                file, 
                TextSpan.FromBounds(10, 20),
                new LinePositionSpan(LinePosition.Zero, new LinePosition(5, 3)));
            return Diagnostic.Create(descriptor, location, DiagnosticSeverity.Error);
        }
    }
}
