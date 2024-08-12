
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with Exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Retrieves all messages from the <paramref name="exception"/> hierarchy, including the message of <paramref name="exception"/> and all inner exceptions, concatenated with the separator built using <paramref name="buildSeparatorFunc"/>.
        /// </summary>
        /// <param name="exception">The exception from which to retrieve the messages.</param>
        /// <param name="buildSeparatorFunc">The function used to build the separator between stack traces. By default, a <see cref="GetDefaultSeparatorFunc"/> will be used.</param>
        /// <returns>A string containing all messages retrieved from the <paramref name="exception"/> hierarchy, concatenated with the separator built using <paramref name="buildSeparatorFunc"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static string GetAllMessagesFromExceptionHierarchy(
            this Exception exception,
            Func<Exception, string>? buildSeparatorFunc = default)
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));

            return exception.GetFromExceptionHierarchy(
                exception => exception.Message,
                buildSeparatorFunc);
        }

        /// <summary>
        /// Retrieves all stack traces from the <paramref name="exception"/> hierarchy, including the stack trace of <paramref name="exception"/> and all inner exceptions, concatenated with the separator built using <paramref name="buildSeparatorFunc"/>.
        /// </summary>
        /// <param name="exception">The exception from which stack traces will be retrieved.</param>
        /// <param name="buildSeparatorFunc">The function used to build the separator between stack traces. By default, a <see cref="GetDefaultSeparatorFunc"/> will be used.</param>
        /// <returns>A string containing all stack traces retrieved from the <paramref name="exception"/> hierarchy, concatenated with the separator built using <paramref name="buildSeparatorFunc"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static string? GetAllStackTracesFromExceptionHierarchy(
            this Exception exception,
            Func<Exception, string>? buildSeparatorFunc = default)
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));

            return exception.GetFromExceptionHierarchy(
                exception => exception.StackTrace,
                buildSeparatorFunc);
        }

        /// <summary>
        /// Retrieves information from the <paramref name="exception"/> hierarchy using the provided <paramref name="getFromExceptionFunc"/> to extract information from <paramref name="exception"/> and all inner exceptions, concatenated with the separator built using <paramref name="buildSeparatorFunc"/>.
        /// </summary>
        /// <param name="exception">The exception from which information will be retrieved.</param>
        /// <param name="getFromExceptionFunc">The function used to extract information from each exception in the hierarchy.</param>
        /// <param name="buildSeparatorFunc">The function used to build the separator between extracted information. By default, a <see cref="GetDefaultSeparatorFunc"/> will be used.</param>
        /// <returns>A string containing information retrieved from the <paramref name="exception"/> hierarchy, concatenated with the separator built using <paramref name="buildSeparatorFunc"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> or <paramref name="getFromExceptionFunc"/> are null.</exception>
        public static string GetFromExceptionHierarchy(
            this Exception exception,
            Func<Exception, string?> getFromExceptionFunc,
            Func<Exception, string>? buildSeparatorFunc = default)
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));
            AssertHelper.AssertNotNullOrThrow(getFromExceptionFunc, nameof(getFromExceptionFunc));

            var buildSeparator = buildSeparatorFunc != default ?
                buildSeparatorFunc :
                exception.GetDefaultSeparatorFunc();

            var value = string.Empty;

            Exception? e = exception;
            while (e != default)
            {
                value += buildSeparator(e) + getFromExceptionFunc(e);
                e = e.InnerException;
            }

            return value;
        }

        /// <summary>
        /// Retrives the default function used to build the separator between extracted information, using the pattern:
        /// <example>
        /// e => $" ({e.GetType().Name}) {<see cref="GlobalConstants.DefaultExceptionSeparator"/>}"
        /// </example>
        /// </summary>
        /// <param name="_">Ignored parameter.</param>
        /// <returns>The default function used to build the separator between extracted information</returns>
        public static Func<Exception, string> GetDefaultSeparatorFunc(this Exception _)
        {
            return e => $" ({e.GetType().Name}) {GlobalConstants.DefaultExceptionSeparator}";
        }

        #region Threading

        /// <summary>
        /// Creates an <see cref="ExceptionDispatchInfo"/> object that represents the <see cref="Exception.InnerException"/> of <paramref name="exception"/> or the specified <paramref name="exception"/> at the current point in code.
        /// Then throws the exception that's represented by the current <see cref="ExceptionDispatchInfo"/> object, after restoring the state that was saved when the exception was captured.
        /// </summary>
        /// <param name="exception">The exception to prepare.</param>
        /// <returns>Will never return. Just return a value to work around a badly-designed API (ExceptionDispatchInfo.Throw).</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        /// <exception cref="Exception">Throws the exception that's represented by the current <see cref="ExceptionDispatchInfo"/> object</exception>
        public static Exception PrepareForReThrow<TEx>(this TEx exception) where TEx : Exception
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));

            if (exception.InnerException == null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();

                // The code cannot ever get here. We just return a value to work around a badly-designed API (ExceptionDispatchInfo.Throw):
                //  https://connect.microsoft.com/VisualStudio/feedback/details/689516/exceptiondispatchinfo-api-modifications
                return exception;
            }

            ExceptionDispatchInfo.Capture(exception.InnerException).Throw();

            // The code cannot ever get here. We just return a value to work around a badly-designed API (ExceptionDispatchInfo.Throw):
            //  https://connect.microsoft.com/VisualStudio/feedback/details/689516/exceptiondispatchinfo-api-modifications
            return exception.InnerException;
        }

        #endregion
    }
}
