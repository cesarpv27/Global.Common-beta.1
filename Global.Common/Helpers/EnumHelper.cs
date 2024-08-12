
namespace Global.Common.Helpers
{
    /// <summary>
    /// Provides helper methods for working with enums.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieves a <see cref="Dictionary{K, V}"/> containing the names and values of the constants in the enumeration of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <returns>A dictionary containing the names and values of the constants in the enumeration.</returns>
        /// <exception cref="ArgumentException">Thrown if <typeparamref name="T"/> is not an enumeration type.</exception>
        public static Dictionary<string, T> GetNameValues<T>() where T : Enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(EnumExtensionConstants.IsNotValidEnum(typeof(T).Name), typeof(T).Name);

            var names = Enum.GetNames(typeof(T));
            var _dictionary = new Dictionary<string, T>(names.Length);

            foreach (var _name in names)
                _dictionary.Add(_name, (T)Enum.Parse(typeof(T), _name));

            return _dictionary;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="value"/> is defined in the enumeration of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="value">The value to check for presence in the enumeration.</param>
        /// <returns>True if the specified value is defined in the enumeration; otherwise, false.</returns>
        public static bool Contains<T>(int value) where T : Enum
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>
        /// Converts the specified <paramref name="httpStatusCode"/> to an integer.
        /// </summary>
        /// <param name="httpStatusCode">The HttpStatusCode to convert.</param>
        /// <returns>The integer representation of the HttpStatusCode.</returns>
        public static int ConvertToInt(HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode;
        }

        /// <summary>
        /// Gets the name and value representation of the specified enum <paramref name="enum"/>.
        /// </summary>
        /// <typeparam name="T">The enum type, where T is <see cref="Enum"/>.</typeparam>
        /// <param name="enum">The enum value.</param>
        /// <returns>A string representing the name and value of the enum.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="enum"/> is null.</exception>
        public static string GetNameValue<T>(T @enum) where T : Enum
        {
            AssertHelper.AssertNotNullOrThrow(@enum, nameof(@enum));

            return $"{nameof(T)}:{@enum}";
        }
    }
}
