
namespace Global.Common.Test.Helpers
{
    internal static class ExceptionExtensionHelper
    {
        public static string? GetStackTracesFromExceptionAndInnerExceptionWithDefaultSeparator(Exception exception)
        {
            return GetStackTracesFromExceptionAndInnerException(
                exception, 
                e => e.StackTrace,
                exception.GetDefaultSeparatorFunc(),
                exception.InnerException!.GetDefaultSeparatorFunc());
        }

        public static string? GetStackTracesFromExceptionAndInnerException(
            Exception exception,
            Func<Exception, string> buildSeparatorFuncForException,
            Func<Exception, string> buildSeparatorFuncForInnerException)
        {
            return GetStackTracesFromExceptionAndInnerException(
                exception,
                e => e.StackTrace,
                buildSeparatorFuncForException,
                buildSeparatorFuncForInnerException);
        }

        public static string? GetMessagesFromExceptionAndInnerExceptionWithDefaultSeparator(Exception exception)
        {
            return GetStackTracesFromExceptionAndInnerException(
                exception,
                e => e.Message,
                exception.GetDefaultSeparatorFunc(),
                exception.InnerException!.GetDefaultSeparatorFunc());
        }

        public static string? GetMessagesFromExceptionAndInnerException(
            Exception exception,
            Func<Exception, string> buildSeparatorFuncForException,
            Func<Exception, string> buildSeparatorFuncForInnerException)
        {
            return GetStackTracesFromExceptionAndInnerException(
                exception,
                e => e.Message,
                buildSeparatorFuncForException,
                buildSeparatorFuncForInnerException);
        }

        public static string? GetStackTracesFromExceptionAndInnerException(
            Exception exception,
            Func<Exception, string?> getFromExceptionFunc,
            Func<Exception, string> buildSeparatorFuncForException,
            Func<Exception, string> buildSeparatorFuncForInnerException)
        {
            return buildSeparatorFuncForException(exception) + getFromExceptionFunc(exception)
                + buildSeparatorFuncForInnerException(exception.InnerException!) + getFromExceptionFunc(exception.InnerException!);
        }

        public static Exception BuildAndAssertExceptionWithInnerException()
        {
            var exception = BuildExceptionWithInnerException();

            AssertHelper.AssertNotNull(exception);
            AssertHelper.AssertNotNull(exception!.InnerException);

            AssertHelper.AssertNotNullNotEmptyNotWhiteSpace(exception!.StackTrace);
            AssertHelper.AssertNotNullNotEmptyNotWhiteSpace(exception!.InnerException!.StackTrace);

            return exception;
        }

        private static Exception BuildExceptionWithInnerException()
        {
            return BuidException(BuidInnerException()); ;
        }

        private static Exception BuidInnerException()
        {
            try
            {
                throw new InvalidCastException(InnerExceptionMessage);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        private static Exception BuidException(Exception innerException)
        {
            try
            {
                throw new InvalidOperationException(ExceptionMessage, innerException);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static Func<Exception, string> GetCustomSeparatorFunc()
        {
            return e => e.GetType().Name;
        } 

        private static string ExceptionMessage => $"Exception test message. Exception type: {typeof(InvalidOperationException)}.";
        private static string InnerExceptionMessage => $"InnerException test message. InnerException type: {typeof(InvalidCastException)}.";
    }
}
