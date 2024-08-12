
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with IEnumerable.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Executes the specified <paramref name="action"/> for each element in the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> collection.</param>
        /// <param name="action">The action to perform on each element.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="enumerable"/> or <paramref name="action"/> are null.</exception>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            AssertHelper.AssertNotNullOrThrow(enumerable, nameof(enumerable));
            AssertHelper.AssertNotNullOrThrow(action, nameof(action));

            foreach (var item in enumerable)
                action(item);
        }

        /// <summary>
        /// Concatenates all the elements of the <paramref name="enumerable"/> into a single string, using the specified <paramref name="separator"/> between each element.
        /// </summary>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> collection of strings to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <returns>A string that consists of the elements of the <paramref name="enumerable"/> delimited by the specified <paramref name="separator"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="enumerable"/> or <paramref name="separator"/> are null.</exception>
        public static string? JoinAll(this IEnumerable<string> enumerable, string separator)
        {
            AssertHelper.AssertNotNullOrThrow(enumerable, nameof(enumerable));
            AssertHelper.AssertNotNullOrThrow(separator, nameof(separator));

            string? result = default;

            int index = 0;
            int countMinusOne = enumerable.Count() - 1;
            foreach (var item in enumerable)
            {
                result += item;
                if (index++ < countMinusOne)
                    result += separator;
            }

            return result;
        }
    }
}
