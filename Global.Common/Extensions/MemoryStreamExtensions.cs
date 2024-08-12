
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with <see cref="MemoryStream"/>.
    /// </summary>
    public static class MemoryStreamExtensions
    {
        /// <summary>
        /// Converts the contents of the <paramref name="memoryStream"/> to a string using the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="memoryStream">The memory stream to convert.</param>
        /// <param name="encoding">The character encoding to use (default is UTF-8).</param>
        /// <returns>The string representation of the contents of the <paramref name="memoryStream"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="memoryStream"/> is null.</exception>
        public static string ConvertToString(
            this MemoryStream memoryStream, 
            Encoding? encoding = default)
        {
            AssertHelper.AssertNotNullOrThrow(memoryStream, nameof(memoryStream));

            memoryStream.Flush();
            memoryStream.Position = 0;

            if (encoding == default)
                encoding = Encoding.UTF8;

            return encoding.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// Tries to convert the contents of the <paramref name="memoryStream"/> to a string using the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="memoryStream">The memory stream to convert.</param>
        /// <param name="result">If the conversion succeeded, contains the string representation of the contents of the <paramref name="memoryStream"/>, 
        /// otherwise null.</param>
        /// <param name="encoding">The character encoding to use (default is UTF-8).</param>
        /// <returns><c>true</c> if the conversion succeeded; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="memoryStream"/> is null.</exception>
        public static bool TryConvertToString(
            this MemoryStream memoryStream,
            ref string? result,
            Encoding? encoding = default)
        {
            result = default;
            try
            {
                result = ConvertToString(memoryStream, encoding);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
