
namespace Global.Common.Helpers
{
    /// <summary>
    /// Helper class for assertion operations. 
    /// </summary>
    public static class AssertHelper
    {
        #region Generic asserts

        /// <summary>
        /// Checks whether the specified <paramref name="value"/> is not null; otherwise, throws an ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check for null.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown when the specified value is null.</exception>
        public static void AssertNotNullOrThrow<T>(T value, string? paramName, string? message = null)
        {
            if (value is null)
                ThrowArgumentNullException(paramName, message);
        }

        #endregion

        #region String asserts

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <paramref name="argument"/> is null,
        /// or an <see cref="ArgumentException"/> if the specified <paramref name="argument"/> is empty.
        /// </summary>
        /// <param name="argument">The argument to check for null or empty.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if the specified <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified <paramref name="argument"/> is empty.</exception>
        public static void AssertNotNullNotEmptyOrThrow(string? argument, string? paramName, string? message = null)
        {
            AssertNotNullOrThrow(argument, paramName, message);
            if (string.IsNullOrEmpty(argument))
                ThrowArgumentException(paramName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <paramref name="argument"/> is null,
        /// or an <see cref="ArgumentException"/> if the specified <paramref name="argument"/> is empty or consists only of white-space characters.
        /// </summary>
        /// <param name="argument">The argument to check for null or white-space.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if the specified <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified <paramref name="argument"/> is empty or consists only of white-space characters.</exception>
        public static void AssertNotNullNotWhiteSpaceOrThrow(string? argument, string? paramName, string? message = null)
        {
            AssertNotNullNotEmptyOrThrow(argument, paramName, message);
            if (string.IsNullOrWhiteSpace(argument))
                ThrowArgumentException(paramName, message);
        }

        #endregion

        #region Guid asserts

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the specified <paramref name="guid"/> is equal to <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentException">Thrown if the specified <paramref name="guid"/> is equal to <see cref="Guid.Empty"/>.</exception>
        public static void AssertNotEmptyOrThrow(Guid guid, string? paramName, string? message = null)
        {
            if (guid.Equals(Guid.Empty))
                ThrowArgumentException(paramName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <paramref name="guid"/> is null,
        /// or an <see cref="ArgumentException"/> if the specified <paramref name="guid"/> is equal to <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if the specified <paramref name="guid"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified <paramref name="guid"/> is equal to <see cref="Guid.Empty"/>.</exception>
        public static void AssertNotNullNotEmptyOrThrow(Guid? guid, string? paramName, string? message = null)
        {
            AssertNotNullOrThrow(guid, paramName, message);
            AssertNotEmptyOrThrow(guid!.Value, paramName, message);
        }

        #endregion

        #region Common

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/>.</exception>
        public static void ThrowArgumentNullException(string? paramName, string? message = null) =>
            throw new ArgumentNullException(paramName, message);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentException">Throws an <see cref="ArgumentException"/>.</exception>
        public static void ThrowArgumentException(string? paramName, string? message = null) =>
            throw new ArgumentException(paramName, message);

        #endregion
    }
}
