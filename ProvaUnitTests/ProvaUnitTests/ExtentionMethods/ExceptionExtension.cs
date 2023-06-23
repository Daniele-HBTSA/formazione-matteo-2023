using ProvaUnitTests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HBTSA.Libraries.ExtensionMethods
{
    public static class ExceptionExtension
    {
        public static List<LogInfo> GetExceptionInfoList(this Exception exception)
        {
            List<LogInfo> result = new List<LogInfo>();

            LogInfo principal = new LogInfo();
            principal.Code = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(exception.Message) == false) principal.Message = exception.Message;
            if (string.IsNullOrWhiteSpace(exception.StackTrace) == false) principal.StackTrace = exception.StackTrace;
            if (string.IsNullOrWhiteSpace(exception.Source) == false) principal.Source = exception.Source;
            if (exception.Data.Count > 0) principal.Data = exception.Data;
            result.Add(principal);
            Exception? exceptionTmp = exception.InnerException;

            while (exceptionTmp != null)
            {
                LogInfo exInternal = new LogInfo();
                exInternal.Code = principal.Code;
                if (string.IsNullOrWhiteSpace(exceptionTmp.Message) == false) exInternal.Message = exceptionTmp.Message;
                if (string.IsNullOrWhiteSpace(exceptionTmp.StackTrace) == false) exInternal.StackTrace = exceptionTmp.StackTrace;
                if (string.IsNullOrWhiteSpace(exceptionTmp.Source) == false) exInternal.Source = exceptionTmp.Source;
                if (exceptionTmp.Data.Count > 0) exInternal.Data = exceptionTmp.Data;
                result.Add(exInternal);
                exceptionTmp = exceptionTmp.InnerException;
            }

            return result;
        }
        
        /// <summary>
        ///  Gets a list of stack frame lines, as strings.
        /// </summary>
        /// <param name="stackTrace">Stack trace string.</param>
        private static List<string> GetStackTraceLines(string stackTrace)
        {
            return stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        /// <summary>
        ///  Gets a list of stack frame lines, as strings, only including those for which line number is known.
        /// </summary>
        /// <param name="fullStackTrace">Full stack trace, including external code.</param>
        private static List<string> GetUserStackTraceLines(string fullStackTrace)
        {
            List<string> outputList = new List<string>();
            Regex regex = new Regex(@"([^\)]*\)) in (.*):line (\d)*$");

            List<string> stackTraceLines = GetStackTraceLines(fullStackTrace);
            foreach (string stackTraceLine in stackTraceLines)
            {
                if (!regex.IsMatch(stackTraceLine))
                {
                    continue;
                }

                outputList.Add(stackTraceLine);
            }

            return outputList;
        }

    }
}