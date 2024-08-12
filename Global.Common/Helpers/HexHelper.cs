
namespace Global.Common.Helpers
{
    /// <summary>
    /// Provides helper methods for working with hexadecimal strings.
    /// </summary>
    public static class HexHelper
    {
        /// <summary>
        /// Converts a byte array to a hexadecimal <see cref="string"/>.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <returns>The hexadecimal <see cref="string"/> representation of the byte array.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="bytes"/> is null.</exception>
        public static string ConvertByteToHex(byte[] bytes)
        {
            AssertHelper.AssertNotNullOrThrow(bytes, nameof(bytes));

            StringBuilder sb = new(bytes.Length * 2);
            foreach (byte bt in bytes)
                sb.AppendFormat("{0:x2}", bt);

            return sb.ToString();
        }

        /// <summary>
        /// Converts a byte array to a hexadecimal <see cref="string"/> safely.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <returns>The hexadecimal <see cref="string"/> representation of the byte array, or null if the input array is null.</returns>
        public static string? SafeConvertByteToHex(byte[]? bytes)
        {
            if (bytes == null) return default;

            StringBuilder sb = new(bytes.Length * 2);
            foreach (byte bt in bytes)
                sb.AppendFormat("{0:x2}", bt);

            return sb.ToString();
        }

        /// <summary>
        /// Converts a hexadecimal <see cref="string"/> to a byte array.
        /// </summary>
        /// <param name="value">The hexadecimal string to convert.</param>
        /// <returns>The byte array converted from the hexadecimal string.</returns>
        public static byte[] ConvertHexToBytes(string value)
        {
            AssertHelper.AssertNotNullOrThrow(value, nameof(value));

            return value.HexToBytes();
        }
    }
}
