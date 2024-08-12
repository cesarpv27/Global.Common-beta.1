
using System.Collections.ObjectModel;

namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with IDictionary.
    /// </summary>
    public static class IDictionaryExtensions
    {
        #region Constants

        private const string defaultRenamedKeySuffix = "_RenamedKey";
        private const int defaultAddOrRenameKeyMinRetryAttempts = 1;
        private const int defaultAddOrRenameKeyMaxRetryAttempts = 5;

        #endregion
        /// <summary>
        /// Represents the default minimum retry attempts for adding or renaming keys.
        /// </summary>
        public static int DefaultAddOrRenameKeyMinRetryAttempts => defaultAddOrRenameKeyMinRetryAttempts;

        /// <summary>
        /// Represents the default maximum retry attempts for adding or renaming keys.
        /// </summary>
        public static int DefaultAddOrRenameKeyMaxRetryAttempts => defaultAddOrRenameKeyMaxRetryAttempts;

        /// <summary>
        /// Represents the default suffix for renamed keys.
        /// </summary>
        public static string DefaultRenamedKeySuffix => defaultRenamedKeySuffix;

        #region Add methods

        /// <summary>
        /// Adds a new key-value pair to the <see cref="IDictionary{TKey, TValue}"/> if the <paramref name="key"/> does not exist, or updates the existing value if the <paramref name="key"/> already exists.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which the key-value pair will be added or replaced.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="value">The value of the key-value pair.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="key"/> are null.</exception>
        public static void AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            AssertHelper.AssertNotNullOrThrow(key, nameof(key));

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        /// <summary>
        /// Tries to add a new key-value pair to the <see cref="IDictionary{TKey, TValue}"/>. If the <paramref name="key"/> already exists, and <paramref name="updateIfKeyExists"/> is true, the method updates the existing value with the provided one. If <paramref name="updateIfKeyExists"/> is false, the method does not modify the dictionary and returns false.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
        /// <typeparam name="TValue">The type of values in the <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which the key-value pair is to be added or updated.</param>
        /// <param name="key">The key of the key-value pair to add or update.</param>
        /// <param name="value">The value of the key-value pair to add or update.</param>
        /// <param name="updateIfKeyExists">A boolean value indicating whether to update the value if the key already exists.</param>
        /// <returns>True if the key-value pair was successfully added to the dictionary, or updated if <paramref name="updateIfKeyExists"/> is true; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="key"/> are null.</exception>
        public static bool TryAddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value,
            bool updateIfKeyExists)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            AssertHelper.AssertNotNullOrThrow(key, nameof(key));

            if (updateIfKeyExists)
                dictionary.AddOrUpdate(key, value);
            else
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
            else
                return false;

            return true;
        }

        /// <summary>
        /// Adds a new key-value pair to the <see cref="IDictionary{TKey, TValue}"/> if the <paramref name="key"/> does not exist, or generate and rename the new key if the <paramref name="key"/> already exists, using the <paramref name="renamedKeySuffix"/>.
        /// The generation of the new keys will be based on <paramref name="renamedKeySuffix"/> and multi attempts, until the new generated key does not exists in <paramref name="dictionary"/>. The generated keys follow de predefined standard:
        /// <example>
        /// $"{key}{renamedKeySuffix/*First Attempt*/}_{Environment.TickCount/*Second Attempt*/}/*...*/_{Environment.TickCount/*Final Attempt*/}"
        /// </example>
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which the key-value pair will be added or replaced.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="value">The value of the key-value pair.</param>
        /// <param name="renamedKeySuffix">The value used to generate the new keys if <paramref name="key"/> or generated keys already exists in <paramref name="dictionary"/>.The default value is <see cref="DefaultRenamedKeySuffix"/>.</param>
        /// <param name="retryAttempts">The max retry attempts to generate new key. The default value is <see cref="DefaultAddOrRenameKeyMaxRetryAttempts"/>. The min value is <see cref="DefaultAddOrRenameKeyMinRetryAttempts"/>.</param>
        /// <returns>The key of the key-value added if the adding operation was successful; otherwise, the string default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="key"/> are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="retryAttempts"/> is less than <see cref="DefaultAddOrRenameKeyMinRetryAttempts"/>.</exception>
        public static string? AddOrRenameKey<TValue>(
            this IDictionary<string, TValue> dictionary,
            string key,
            TValue value,
            string? renamedKeySuffix = defaultRenamedKeySuffix,
            int retryAttempts = defaultAddOrRenameKeyMaxRetryAttempts)
        {
            dictionary.AddOrRenameKey(
                key,
                value,
                out string? addedKey,
                renamedKeySuffix,
                retryAttempts);

            return addedKey;
        }

        /// <summary>
        /// Adds a new key-value pair to the <see cref="IDictionary{TKey, TValue}"/> if the <paramref name="key"/> does not exist, or generate and rename the new key if the <paramref name="key"/> already exists, using the <paramref name="renamedKeySuffix"/>.
        /// The generation of the new keys will be based on <paramref name="renamedKeySuffix"/> and multi attempts, until the new generated key does not exists in <paramref name="dictionary"/>. The generated keys follow de predefined standard:
        /// <example>
        /// $"{key}{renamedKeySuffix/*First Attempt*/}_{Environment.TickCount/*Second Attempt*/}/*...*/_{Environment.TickCount/*Final Attempt*/}"
        /// </example>
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which the key-value pair will be added or replaced.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="value">The value of the key-value pair.</param>
        /// <param name="addedKey">Represents the name of the key in the key-value pair that was added to the dictionary. This key can either be the original key name or the renamed one, depending on the operation result. If was not possible to add the key-value pair to the dictionary, the value of <paramref name="addedKey"/> will be null. </param>
        /// <param name="renamedKeySuffix">The value used to generate the new keys if <paramref name="key"/> or generated keys already exists in <paramref name="dictionary"/>.The default value is <see cref="DefaultRenamedKeySuffix"/>.</param>
        /// <param name="retryAttempts">The max retry attempts to generate new key. The default value is <see cref="DefaultAddOrRenameKeyMaxRetryAttempts"/>. The min value is <see cref="DefaultAddOrRenameKeyMinRetryAttempts"/>.</param>
        /// <returns>True if the key-value pair was successfully added to the dictionary; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="key"/> are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="retryAttempts"/> is less than <see cref="DefaultAddOrRenameKeyMinRetryAttempts"/>.</exception>
        public static bool AddOrRenameKey<TValue>(
            this IDictionary<string, TValue> dictionary,
            string key,
            TValue value,
            out string? addedKey,
            string? renamedKeySuffix = defaultRenamedKeySuffix,
            int retryAttempts = defaultAddOrRenameKeyMaxRetryAttempts)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            AssertHelper.AssertNotNullOrThrow(key, nameof(key));
            ArgumentOutOfRangeException.ThrowIfLessThan(retryAttempts, 1, nameof(retryAttempts));

            if (string.IsNullOrEmpty(renamedKeySuffix))
                renamedKeySuffix = DefaultRenamedKeySuffix;

            int maxAttempts = retryAttempts + 1;
            do
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, value);
                    addedKey = key;
                    return true;
                }

                key += renamedKeySuffix;
                renamedKeySuffix = $"_{Environment.TickCount}";

            } while (--maxAttempts > 0);

            addedKey = default;
            return false;
        }

        ///<summary>
        /// Adds a new key-value pair to the dictionary. Allows specifying an action if the key already exists.
        /// </summary>
        /// <typeparam name="TValue">The type of values in the <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which the key-value pair is to be added.</param>
        /// <param name="key">The string key of the key-value pair to be added.</param>
        /// <param name="addedKey">Represents the name of the key in the key-value pair that was added to the dictionary. This key can either be the original key name or the renamed one, depending on the operation result. If was not possible to add the key-value pair to the dictionary, the value of <paramref name="addedKey"/> will be null. </param>
        /// <param name="value">The value of the key-value pair to add.</param>
        /// <param name="keyExistAction">An optional action to perform if the key already exists. Default is <see cref="KeyExistAction.TryAdd"/>.</param>
        /// <returns>True if the key-value pair was successfully added to the dictionary; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="key"/> are null.</exception>
        public static bool Add<TValue>(
        this IDictionary<string, TValue> dictionary,
        string key,
        TValue value,
        out string? addedKey,
        KeyExistAction keyExistAction = KeyExistAction.TryAdd)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            AssertHelper.AssertNotNullOrThrow(key, nameof(key));

            switch (keyExistAction)
            {
                case KeyExistAction.TryAdd:
                    if (dictionary.TryAdd(key, value))
                    {
                        addedKey = key;
                        return true;
                    }
                    addedKey = default;
                    return false;
                case KeyExistAction.Update:
                    dictionary.AddOrUpdate(key, value);
                    addedKey = key;
                    return true;
                case KeyExistAction.Rename:
                    addedKey = dictionary.AddOrRenameKey(key, value);
                    return addedKey is not null;
                default:
                    throw new InvalidOperationException(nameof(keyExistAction));
            }
        }

        #endregion

        #region Range

        /// <summary>
        /// Tries to add a range of key-value pairs to the <see cref="IDictionary{TKey, TValue}"/>. 
        /// Supports specifying actions for existing keys. 
        /// If a key already exists in the <paramref name="dictionary"/> and <paramref name="keyExistAction"/> is <see cref="KeyExistAction.Update"/>, the method updates the existing value with the value from the <paramref name="rangeToAdd"/>. 
        /// If a key already exists in the <paramref name="dictionary"/> and <paramref name="keyExistAction"/> is <see cref="KeyExistAction.Rename"/>, the method rename the key and add the new key-value pair to <paramref name="dictionary"/>. 
        /// If <paramref name="keyExistAction"/> is <see cref="KeyExistAction.TryAdd"/> and any key from the <paramref name="rangeToAdd"/> already exists in the <paramref name="dictionary"/>, the method does not modify the <paramref name="dictionary"/> and returns false. 
        /// Returns true if the range was successfully added to the <paramref name="dictionary"/> or updated based on the specified conditions.
        /// </summary>
        /// <typeparam name="TValue">The type of values in the <paramref name="dictionary"/>.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which the range of key-value pairs is to be added or updated.</param>
        /// <param name="rangeToAdd">The range of key-value pairs to add or update.</param>
        /// <param name="keyExistAction">An optional action to perform if keys already exist in the <paramref name="dictionary"/>. Default is <see cref="KeyExistAction.TryAdd"/>.</param>
        /// <returns>True if the range of key-value pairs was successfully added to the dictionary; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> is null.</exception>
        public static bool TryAddRange<TValue>(
            this IDictionary<string, TValue> dictionary,
            IDictionary<string, TValue>? rangeToAdd,
            KeyExistAction keyExistAction = KeyExistAction.TryAdd)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));

            if (rangeToAdd == null)
                return false;

            if (rangeToAdd.Count > 0)
            {
                switch (keyExistAction)
                {
                    case KeyExistAction.TryAdd:
                        return dictionary.TryAddRangeOrUpdate(rangeToAdd, false);
                    case KeyExistAction.Update:
                        return dictionary.TryAddRangeOrUpdate(rangeToAdd, true);
                    case KeyExistAction.Rename:
                        foreach (var keyValue in rangeToAdd)
                            if (!dictionary.Add(keyValue.Key, keyValue.Value, out _, keyExistAction))
                                return false;
                        break;
                    default:
                        throw new InvalidOperationException(nameof(keyExistAction));
                }
            }

            return true;
        }

        /// <summary>
        /// Tries to add a range of key-value pairs to the <see cref="IDictionary{TKey, TValue}"/>. 
        /// If a key already exists in the <paramref name="dictionary"/> and <paramref name="updateIfKeyExists"/> is true, the method updates the existing value with the value from the <paramref name="rangeToAdd"/>. 
        /// If <paramref name="updateIfKeyExists"/> is false and any key from the <paramref name="rangeToAdd"/> already exists in the <paramref name="dictionary"/>, the method does not modify the <paramref name="dictionary"/> and returns false. 
        /// Returns true if the range was successfully added to the <paramref name="dictionary"/> or updated based on the specified conditions.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
        /// <typeparam name="TValue">The type of values in the <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which the range of key-value pairs is to be added or updated.</param>
        /// <param name="rangeToAdd">The range of key-value pairs to add or update in the <paramref name="dictionary"/>.</param>
        /// <param name="updateIfKeyExists">A boolean value indicating whether to update the value if a key from the range already exists in the <paramref name="dictionary"/>.</param>
        /// <returns>True if the range was successfully added to the <paramref name="dictionary"/> or updated based on the specified conditions; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> is null.</exception>
        public static bool TryAddRangeOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue>? rangeToAdd,
            bool updateIfKeyExists)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));

            if (rangeToAdd == null)
                return false;

            if (rangeToAdd.Count > 0)
            {
                if (!updateIfKeyExists)
                    foreach (var keyValue in rangeToAdd)
                        if (dictionary.ContainsKey(keyValue.Key))
                            return false;

                rangeToAdd.ForEach(keyValue =>
                {
                    dictionary.TryAddOrUpdate(keyValue.Key, keyValue.Value, updateIfKeyExists);
                });
            }

            return true;
        }

        /// <summary>
        /// Retrieves a range of key-value pairs from the <see cref="IDictionary{TKey, TValue}"/> based on the specified index and count.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the <paramref name="dictionary"/>.</typeparam>
        /// <typeparam name="TValue">The type of values in the <paramref name="dictionary"/>.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> from which to retrieve the range of key-value pairs.</param>
        /// <param name="index">The zero-based starting index of the range to retrieve.</param>
        /// <param name="count">The number of key-value pairs to retrieve starting from the specified index.</param>
        /// <returns>A new dictionary containing the specified range of key-value pairs.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is less than 0, or <paramref name="count"/> is less than 0, 
        /// or the sum of <paramref name="index"/> and 1 is greater than the number of elements in the dictionary, 
        /// or the number of elements from the dictionary at <paramref name="index"/> to the end is less than <paramref name="count"/>.</exception>
        public static Dictionary<TKey, TValue> GetRange<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            int index,
            int count) where TKey : notnull
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0, nameof(index));
            ArgumentOutOfRangeException.ThrowIfLessThan(count, 0, nameof(count));
            ArgumentOutOfRangeException.ThrowIfLessThan(dictionary.Count, index + 1, nameof(index));
            ArgumentOutOfRangeException.ThrowIfLessThan(dictionary.Count - index, count, nameof(count));

            var ranged = new Dictionary<TKey, TValue>();
            if (count >  0)
            {
                int position = 0;
                int added = 0;
                foreach (var keyValuePair in dictionary)
                    if (position++ >= index)
                    {
                        ranged.Add(keyValuePair.Key, keyValuePair.Value);
                        if (++added >= count)
                            break;
                    }
            }

            return ranged;
        }

        #endregion

        #region Join

        /// <summary>
        /// Joins multiple dictionaries into one, optionally updating existing keys.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the <paramref name="dictionary"/>.</typeparam>
        /// <typeparam name="TValue">The type of values in the <paramref name="dictionary"/>.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to which other <see cref="IDictionary{TKey, TValue}"/> will be joined.</param>
        /// <param name="updateIfKeyExists">A boolean value indicating whether to update existing keys if found in the joined <paramref name="elemsToJoin"/>.</param>
        /// <param name="allowNullInElemsToJoin">A boolean value indicating whether to allow null <see cref="IDictionary{TKey, TValue}"/> in the <paramref name="elemsToJoin"/> parameter.</param>
        /// <param name="elemsToJoin">The <see cref="IDictionary{TKey, TValue}"/> to join into the main dictionary.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="elemsToJoin"/> are null, 
        /// or a null <see cref="IDictionary{TKey, TValue}"/> is encountered in <paramref name="elemsToJoin"/> and <paramref name="allowNullInElemsToJoin"/> is false.</exception>
        public static void JoinMany<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            bool updateIfKeyExists,
            bool allowNullInElemsToJoin,
            params IDictionary<TKey, TValue>[] elemsToJoin)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            ArgumentNullException.ThrowIfNull(elemsToJoin, nameof(elemsToJoin));

            foreach (var dict in elemsToJoin)
            {
                if (dict == null && !allowNullInElemsToJoin)
                    ArgumentNullException.ThrowIfNull(nameof(elemsToJoin), IDictionaryExtensionConstants.NullDictionaryInJoin);
                else
                    dictionary.TryAddRangeOrUpdate(dict, updateIfKeyExists);
            }
        }

        #endregion

        #region TryGetIf

        /// <summary>
        /// Attempts to retrieve values from the dictionary where the keys contain the specified pattern, optionally ignoring certain keys. Ignore case-insensitive. 
        /// If the result of the operation is <c>true</c>, the resulting values will be stored in <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="pattern">The pattern to match keys against.</param>
        /// <param name="values">When this method returns, contains a dictionary of key-value pairs that match the pattern.</param>
        /// <param name="ignoreKeys">An optional array of keys to ignore during the search.</param>
        /// <returns><c>true</c> if one or more keys match the pattern; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="pattern"/> is null.</exception>
        public static bool TryGetIfPatternContainsKey<TValue>(
            this IDictionary<string, TValue> dictionary,
            string pattern,
            out IDictionary<string, TValue> values,
            params string[]? ignoreKeys)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));

            values = new Dictionary<string, TValue>();

            foreach (var keyValue in dictionary)
                if ((ignoreKeys == null || !ignoreKeys.Contains(keyValue.Key)) && pattern.IgnoreCaseContains(keyValue.Key))
                    values.Add(keyValue);

            return values.Count != 0;
        }

        /// <summary>
        /// Attempts to retrieve values from the dictionary where the keys contain the specified pattern. Ignore case-insensitive.
        /// If the result of the operation is <c>true</c>, the resulting values will be stored in <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="pattern">The pattern to match keys against.</param>
        /// <param name="values">When this method returns, contains a dictionary of key-value pairs that match the pattern.</param>
        /// <returns><c>true</c> if one or more keys match the pattern; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="pattern"/> is null.</exception>
        public static bool TryGetIfPatternContainsKey<TValue>(
            this IDictionary<string, TValue> dictionary,
            string pattern,
            out IDictionary<string, TValue> values)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));

            return dictionary.TryGetIfPatternContainsKey(pattern, out values, default);
        }

        /// <summary>
        /// Attempts to retrieve values from the dictionary where the specified pattern contain the key, optionally ignoring certain keys. Ignore case-insensitive. 
        /// If the result of the operation is <c>true</c>, the resulting values will be stored in <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="pattern">The pattern to match keys against.</param>
        /// <param name="values">When this method returns, contains a dictionary of key-value pairs that match the pattern.</param>
        /// <param name="ignoreKeys">An optional array of keys to ignore during the search.</param>
        /// <returns><c>true</c> if one or more keys match the pattern; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="pattern"/> is null.</exception>
        public static bool TryGetIfKeyContainsPattern<TValue>(
            this IDictionary<string, TValue> dictionary,
            string pattern,
            out IDictionary<string, TValue> values,
            params string[]? ignoreKeys)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));

            values = new Dictionary<string, TValue>();

            foreach (var keyValue in dictionary)
                if ((ignoreKeys == null || !ignoreKeys.Contains(keyValue.Key)) && keyValue.Key.IgnoreCaseContains(pattern))
                    values.Add(keyValue);

            return values.Count != 0;
        }

        /// <summary>
        /// Attempts to retrieve values from the dictionary where the specified pattern contain the key. Ignore case-insensitive. 
        /// If the result of the operation is <c>true</c>, the resulting values will be stored in <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="pattern">The pattern to match keys against.</param>
        /// <param name="values">When this method returns, contains a dictionary of key-value pairs that match the pattern.</param>
        /// <returns><c>true</c> if one or more keys match the pattern; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> or <paramref name="pattern"/> is null.</exception>
        public static bool TryGetIfKeyContainsPattern<TValue>(
            this IDictionary<string, TValue> dictionary,
            string pattern,
            out IDictionary<string, TValue> values)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));

            return dictionary.TryGetIfKeyContainsPattern(pattern, out values, default);
        }

        #endregion

        #region ShadowClone

        /// <summary>
        /// Creates a shallow clone of the <paramref name="source"/> dictionary into the target dictionary. 
        /// If the <paramref name="source"/> is null, the dictionary will be initialized as empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The target dictionary to clone into.</param>
        /// <param name="source">The source dictionary to clone from. If null, the target dictionary will be cleared.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> is null.</exception>
        public static void SelfShadowCloneOf<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue>? source)
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));

            dictionary.Clear();

            if (source != null)
                foreach (var keyValuePair in source)
                    dictionary.Add(keyValuePair);
        }

        /// <summary>
        /// Creates a shallow clone of the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to clone.</param>
        /// <returns>A new <see cref="Dictionary{TKey, TValue}"/> containing the same key-value pairs as the original dictionary.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> is null.</exception>
        public static Dictionary<TKey, TValue> ShadowClone<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));

            return new Dictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Creates a read-only shallow clone of the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to clone.</param>
        /// <returns>A new <see cref="ReadOnlyDictionary{TKey, TValue}"/> containing the same key-value pairs as the original dictionary.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> is null.</exception>
        public static ReadOnlyDictionary<TKey, TValue> ReadOnlyShadowClone<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));

            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
        #endregion

        /// <summary>
        /// Skips a specified number of key-value pairs from the beginning of the dictionary and returns the remaining key-value pairs as a new dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to skip elements from.</param>
        /// <param name="countToSkip">The number of key-value pairs to skip.</param>
        /// <returns>A new <see cref="Dictionary{TKey, TValue}"/> containing the remaining key-value pairs after skipping the specified number of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dictionary"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="countToSkip"/> is less than 0 or greater than the number of elements in the dictionary.</exception>
        public static Dictionary<TKey, TValue> SkipFirst<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            int countToSkip) where TKey : notnull
        {
            AssertHelper.AssertNotNullOrThrow(dictionary, nameof(dictionary));
            ArgumentOutOfRangeException.ThrowIfLessThan(countToSkip, 0, nameof(countToSkip));
            ArgumentOutOfRangeException.ThrowIfLessThan(dictionary.Count - countToSkip, 0, nameof(countToSkip));

            if (dictionary.Count == countToSkip)
                return new Dictionary<TKey, TValue>();

            return dictionary.GetRange(countToSkip, dictionary.Count - countToSkip);
        }
    }
}
