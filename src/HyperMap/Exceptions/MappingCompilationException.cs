using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace HyperMap.Exceptions
{
    public class MappingCompilationException : Exception
    {
        internal MappingCompilationException(IEnumerable<Diagnostic> errors)
        {
            var errorMessage = new StringBuilder();
            
            foreach (var error in errors)
            {
                var issue = $"ID: {error.Id}, Message: {error.GetMessage()}, " +
                            $"Location: {error.Location.GetLineSpan()}, Severity: {error.Severity}";
                errorMessage.AppendLine(issue);
            }

            Message = errorMessage.ToString();
        }

        public override string Message { get; }
    }
}
