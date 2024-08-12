
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with IList.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Swaps the elements at the specified <paramref name="sourceIndex"/> and <paramref name="destIndex"/> in the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="list"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> in which to perform the swap.</param>
        /// <param name="sourceIndex">The index of the first element to swap.</param>
        /// <param name="destIndex">The index of the second element to swap.</param>
        /// <returns>True if the swap was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is null.</exception>
        public static bool Swap<T>(this IList<T> list, int sourceIndex, int destIndex)
        {
            AssertHelper.AssertNotNullOrThrow(list, nameof(list));

            if (sourceIndex < 0 || sourceIndex >= destIndex || list.Count <= destIndex)
                return false;

            (list[destIndex], list[sourceIndex])=(list[sourceIndex], list[destIndex]);

            return true;
        }

        /// <summary>
        /// Converts all strings in the specified <paramref name="list"/> to lowercase.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> of strings to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is null.</exception>
        public static void ToLower(this IList<string> list)
        {
            AssertHelper.AssertNotNullOrThrow(list, nameof(list));

            list.TransformAll(str => str.ToLower());
        }

        /// <summary>
        /// Converts all strings in the specified <paramref name="list"/> to uppercase.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> of strings to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is null.</exception>
        public static void ToUpper(this IList<string> list)
        {
            AssertHelper.AssertNotNullOrThrow(list, nameof(list));

            list.TransformAll(str => str.ToUpper());
        }

        /// <summary>
        /// Transforms all elements of the specified <paramref name="list"/> using the provided <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="list"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> to transform.</param>
        /// <param name="action">The <see cref="Func{T, T}"/> to apply to each element in <paramref name="list"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> or <paramref name="action"/> are null.</exception>
        public static void TransformAll<T>(this IList<T> list, Func<T, T> action)
        {
            AssertHelper.AssertNotNullOrThrow(list, nameof(list));
            AssertHelper.AssertNotNullOrThrow(action, nameof(action));

            for (int i = 0; i < list.Count; i++)
                list[i] = action(list[i]);
        }

        /// <summary>
        /// Copies a range of elements from the specified <paramref name="list"/> to the <paramref name="destinationList"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in both <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The source <see cref="IList{T}"/> from which elements will be copied.</param>
        /// <param name="index">The zero-based index in the source <paramref name="list"/> at which copying begins.</param>
        /// <param name="destinationList">The destination <see cref="IList{T}"/> to which elements will be copied.</param>
        /// <param name="count">The number of elements to copy.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> or <paramref name="destinationList"/> are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is less than 0, or <paramref name="index"/> greater than or equal to the count of elements in the source <paramref name="list"/>, 
        /// or <paramref name="count"/> is less than 0, or the range from <paramref name="index"/> to the end of the <paramref name="list"/> is less than <paramref name="count"/>.</exception>
        public static void CopyTo<T>(this IList<T> list, int index, IList<T> destinationList, int count)
        {
            AssertHelper.AssertNotNullOrThrow(list, nameof(list));
            AssertHelper.AssertNotNullOrThrow(destinationList, nameof(destinationList));

            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0, nameof(index));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, list.Count, nameof(index));
            ArgumentOutOfRangeException.ThrowIfLessThan(count, 0, nameof(count));
            ArgumentOutOfRangeException.ThrowIfLessThan(list.Count - index, count, nameof(count));

            var length = index + count;
            for (int i = index; i < length; i++)
                destinationList.Add(list[i]);
        }
    }
}
