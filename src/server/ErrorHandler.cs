using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NtFreX.HelloAzureFunctions {
    public class ErrorHandler {
        public static IActionResult Handle(Exception exception, ILogger logger) {
            logger.LogError(FormatException(exception));
            return new BadRequestObjectResult("An unhandled error occurred!");
        }

        private static string FormatException(Exception exception, int tabCount = 0) {
            var tabs = string.Join(string.Empty, Enumerable.Repeat("\t", tabCount));
            var message = $"Message: {exception.Message}";
            message += Environment.NewLine + tabs + $"Source: {exception.Source}";
            message += Environment.NewLine + tabs + $"TargetSite?.Name: {exception.TargetSite?.Name}";
            message += Environment.NewLine + tabs + $"TargetSite?.DeclaringType?.Name: {exception.TargetSite?.DeclaringType?.Name}";
            message += Environment.NewLine + tabs + $"StackTrace: {exception.StackTrace.Replace(Environment.NewLine, Environment.NewLine + tabs + "  ")}";
            if(exception.InnerException != null) {
                message += Environment.NewLine + tabs + $"InnerException:" + Environment.NewLine;
                message += FormatException(exception.InnerException, tabCount + 1);
            }
            return message;
        }
    }
}