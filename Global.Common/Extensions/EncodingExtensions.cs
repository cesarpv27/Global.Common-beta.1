
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with Encoding.
    /// </summary>
    public static class EncodingExtensions
    {
        /// <summary>
        /// Encodes the specified string <paramref name="s"/> with base-64 digits using the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="encoding">The <see cref="Encoding"/> to use for the conversion.</param>
        /// <param name="s">The string to encode.</param>
        /// <returns>The base-64 digits representation of the input string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="encoding"/> or <paramref name="s"/> are null.</exception>
        public static string EncodeToBase64(this Encoding encoding, string s)
        {
            AssertHelper.AssertNotNullOrThrow(encoding, nameof(encoding));
            AssertHelper.AssertNotNullOrThrow(s, nameof(s));

            return Convert.ToBase64String(encoding.GetBytes(s));
        }

        /// <summary>
        /// Decodes the specified base-64 digits encoded string <paramref name="s"/> using the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="encoding">The  <see cref="Encoding"/> to use for the conversion.</param>
        /// <param name="s">The base-64 digits encoded string to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="encoding"/> or <paramref name="s"/> are null.</exception>
        public static string DecodeFromBase64(this Encoding encoding, string s)
        {
            AssertHelper.AssertNotNullOrThrow(encoding, nameof(encoding));
            AssertHelper.AssertNotNullOrThrow(s, nameof(s));

            return encoding.GetString(Convert.FromBase64String(s));
        }
    }
}
