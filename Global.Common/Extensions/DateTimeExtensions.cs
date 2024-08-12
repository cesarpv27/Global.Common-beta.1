
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the minimum value for DateTime used in Azure Table Storage.
        /// </summary>
        /// <param name="_">Ignored parameter.</param>
        /// <returns>The minimum DateTime value used in Azure Table Storage (January 1, 1601).</returns>
        public static DateTime MinValueInAzureTable(this DateTime _)
        {
            return new DateTime(1601, 1, 1);
        }
    }
}
