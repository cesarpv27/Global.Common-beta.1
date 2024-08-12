
namespace Global.Common.Helpers
{
    /// <summary>
    /// Provides helper methods for working with Reflection.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Attempts to retrieve the name and value <paramref name="properties"/> from the specified <paramref name="obj"/>. If a property was not found, an empty value will be added to the result.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="obj">The object from which to retrieve properties.</param>
        /// <param name="properties">The properties to retrieve from the object.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> containing the found name and value properties of the object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="obj"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="properties"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if no elements are found in <paramref name="properties"/>.</exception>
        public static Dictionary<string, string?> TryGetNameValuePropertiesFrom<T>(T obj, PropertyInfo[] properties)
        {
            AssertHelper.AssertNotNullOrThrow(obj, nameof(obj));
            AssertHelper.AssertNotNullOrThrow(properties, nameof(properties));

            if (properties.Length == 0)
                throw new ArgumentException(HelperConstants.NotPropertiesFound, nameof(properties));

            var nameValueProps = new Dictionary<string, string?>(properties.Length);
            object? propInfoValue;
            foreach (PropertyInfo _prop in properties)
                try
                {
                    propInfoValue = _prop.GetValue(obj);
                    
                    nameValueProps.Add(_prop.Name, propInfoValue != null ? propInfoValue.ToString() : string.Empty);
                }
                catch
                {
                    nameValueProps.Add(_prop.Name, string.Empty);
                }

            return nameValueProps;
        }
    }
}
