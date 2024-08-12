
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with strings.
    /// </summary>
    public static class StringExtensions
    {
        #region Crypto

        /// <summary>
        /// Decodes the specified string from UTF-8 encoding to a regular string.
        /// </summary>
        /// <remarks>
        /// Convert string values into a bytes, one char at time, then get string from UTF-8 enconding from those bytes
        /// If UTF-8 code unit values have been stored as a sequence of 16-bit code units in a C# string, 
        /// then need to verify that each code unit is within the range of a byte, copy those values into bytes, 
        /// and convert the new UTF-8 byte sequence.
        /// </remarks>
        /// <param name="this">The string encoded in UTF-8 to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string DecodeFromUTF8(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            byte[] utf8Bytes = new byte[@this.Length];
            for (int i = 0; i < @this.Length; ++i)
                utf8Bytes[i] = (byte)@this[i];

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array.
        /// </summary>
        /// <param name="this">The hexadecimal string to convert.</param>
        /// <returns>The byte array representation of the hexadecimal string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static byte[] HexToBytes(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            int strLength = @this.Length;
            byte[] bytes = new byte[strLength / 2];
            for (int i = 0; i < strLength; i += 2)
                bytes[i / 2] = Convert.ToByte(@this.Substring(i, 2), 16);

            return bytes;
        }

        #endregion

        #region Http

        /// <summary>
        /// Tries to convert the specified <paramref name="this"/> string to an <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="this">The <see cref="string"/> to convert.</param>
        /// <returns>The <see cref="HttpStatusCode"/> equivalent of the input string if the conversion succeeded; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static HttpStatusCode? TryConvertToHttpStatusCode(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            var _httpStatusCodes = EnumHelper.GetNameValues<HttpStatusCode>();

            if (_httpStatusCodes.TryGetValue(@this, out HttpStatusCode result))
                return result;

            return null;
        }

        /// <summary>
        /// Tries to convert the specified <paramref name="this"/> string to an <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="this">The <see cref="string"/> to convert.</param>
        /// <param name="httpStatusCode">When this method returns, contains the <see cref="HttpStatusCode"/> equivalent of the input string, if the conversion succeeded, or the default value if the conversion failed.</param>
        /// <returns>True if the conversion succeeded; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static bool TryConvertToHttpStatusCode(
            this string @this,
            out HttpStatusCode httpStatusCode)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            var _httpStatusCodes = EnumHelper.GetNameValues<HttpStatusCode>();

            return _httpStatusCodes.TryGetValue(@this, out httpStatusCode);
        }

        /// <summary>
        /// Performs full HTML decoding on the specified <paramref name="this"/> using <see cref="WebUtility"/>.
        /// </summary>
        /// <param name="this">The string to decode.</param>
        /// <returns>The string after performing full HTML decoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string HtmlUrlDecode(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return WebUtility.HtmlDecode(WebUtility.UrlDecode(@this));
        }

        #endregion

        #region To list

        /// <summary>
        /// Converts a semicolon-separated string into a list of strings.
        /// </summary>
        /// <param name="this">The semicolon-separated string to convert into a list.</param>
        /// <returns>A list of strings containing the substrings of <paramref name="this"/> separated by semicolons.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static List<string> SemicolonSeparetedToList(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.SplitToList(';', false);
        }

        /// <summary>
        /// Splits the specified <paramref name="this"/> string into substrings based on the specified <paramref name="separator"/> and removes leading and trailing white-space characters from each substring.
        /// </summary>
        /// <param name="this">The string to split.</param>
        /// <param name="separator">A character that delimits the substrings in the input string.</param>
        /// <returns>A <see cref="List{T}"/> containing the substrings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static List<string> SplitTrimToList(this string @this, char separator)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return SplitToList(@this, separator, true);
        }

        /// <summary>
        /// Splits the specified <paramref name="this"/> into a list of substrings using the specified <paramref name="separator"/>.
        /// </summary>
        /// <param name="this">The string to split.</param>
        /// <param name="separator">The character used to split the <paramref name="this"/> into substrings.</param>
        /// <param name="trimSubStrings">A boolean value indicating whether to trim whitespace from the resulting substrings.</param>
        /// <returns>A list of substrings obtained by splitting the source string using the specified separator.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static List<string> SplitToList(
            this string @this,
            char separator,
            bool trimSubStrings)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            var splitResult = @this.Split(separator);
            if (trimSubStrings)
                for (int i = 0; i < splitResult.Length; i++)
                    splitResult[i] = splitResult[i].Trim();

            return splitResult.ToList();
        }

        /// <summary>
        /// Splits the specified <paramref name="this"/> into a list of substrings using the specified <paramref name="separator"/>.
        /// </summary>
        /// <param name="this">The string to split.</param>
        /// <param name="separator">An array of strings that serve as separator for splitting the source string.</param>
        /// <param name="stringSplitOptions">Options for removing empty entries from the result.</param>
        /// <param name="trimSubStrings">A boolean value indicating whether to trim whitespace from the resulting substrings.</param>
        /// <returns>A list of substrings obtained by splitting the <paramref name="this"/> using the specified <paramref name="separator"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> or <paramref name="separator"/> are null.</exception>
        public static List<string> SplitToList(
            this string @this,
            string[] separator,
            StringSplitOptions stringSplitOptions,
            bool trimSubStrings)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));
            AssertHelper.AssertNotNullOrThrow(separator, nameof(separator));

            var splitResult = @this.Split(separator, stringSplitOptions);
            if (trimSubStrings)
                for (int i = 0; i < splitResult.Length; i++)
                    splitResult[i] = splitResult[i].Trim();

            return splitResult.ToList();
        }

        #endregion

        /// <summary>
        /// Cleans the specified string by performing various transformations such as <see cref="HtmlUrlDecode"/>, removing line breaks, new lines, tabs, and optionally consecutive whitespaces ('\r', '\n', '\t', optionally consecutive whitespaces).
        /// </summary>
        /// <param name="this">The string to clean.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default value is true.</param>
        /// <returns>The cleaned string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string Clean(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            if (string.IsNullOrEmpty(@this))
                return string.Empty;

            #region Testing

            // Test 1
            //byte[] bytes = Encoding.Default.GetBytes(result);
            //var decodedInput = Encoding.UTF8.GetString(bytes);
            //var decodedInput = System.Web.HttpUtility.UrlDecode(source);
            //decodedInput = System.Web.HttpUtility.HtmlDecode(decodedInput);
            // Test 2
            //UnicodeEncoding destEncoding = new UnicodeEncoding();//new ASCIIEncoding();//Encoding.GetEncoding("iso-8859-1");
            //Encoding sourceEncoding = Encoding.UTF8;
            //byte[] sourceBytes = sourceEncoding.GetBytes(result);
            //byte[] resultBytes = Encoding.Convert(sourceEncoding, destEncoding, sourceBytes);
            //string msg = destEncoding.GetString(resultBytes);

            #endregion

            var result = @this.HtmlUrlDecode();//.FullDecodeFromUTF8();

            result = Regex.Replace(result, @"[\r|\n|\t]", " ").Trim();

            if (removeConsecWhitespaces)
                return RemoveConsecWhitespaces(result);

            return result;
        }

        /// <summary>
        /// Removes consecutive whitespaces from the specified string.
        /// </summary>
        /// <param name="this">The string from which to remove consecutive whitespaces.</param>
        /// <returns>The string with consecutive whitespaces removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string RemoveConsecWhitespaces(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            if (string.IsNullOrEmpty(@this))
                return string.Empty;

            return Regex.Replace(@this, @"\s+", " ");
        }
        
        /// <summary>
        /// Removes consecutive whitespaces from the specified string and converts the result to uppercase.
        /// </summary>
        /// <param name="this">The string from which to remove consecutive whitespaces and convert to uppercase.</param>
        /// <returns>The string with consecutive whitespaces removed and converted to uppercase.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string RemoveConsecWhitespacesToUpper(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.RemoveConsecWhitespaces().ToUpper();
        }
        
        /// <summary>
        /// Removes consecutive whitespaces from the specified string and capitalizes the words in the resulting string.
        /// </summary>
        /// <param name="this">The string from which to remove consecutive whitespaces and capitalize the words.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default value is true.</param>
        /// <returns>The string with consecutive whitespaces removed and words capitalized.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string RemoveConsecWhitespacesCapitalizedCase(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            if (removeConsecWhitespaces)
                return @this.RemoveConsecWhitespaces().CapitalizeCase();

            return @this.CapitalizeCase();
        }

        /// <summary>
        /// Cleans the specified string by removing consecutive whitespaces and optionally converts it to uppercase.
        /// </summary>
        /// <param name="this">The string to clean.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default value is true.</param>
        /// <returns>The cleaned string, optionally converted to uppercase.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CleanToUpper(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.Clean(removeConsecWhitespaces).ToUpper();
        }

        /// <summary>
        /// Checks if the cleaned and normalized string using <see cref="CleanToUpper"/> is equal to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="this">The string to clean and normalize.</param>
        /// <param name="value">The value to compare against the cleaned and normalized source string.</param>
        /// <returns>True if the cleaned and normalized source string is equal to the specified value; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static bool IsCleanedStringEqualTo(this string @this, string value)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.CleanToUpper().Equals(value);
        }

        /// <summary>
        /// Checks if the cleaned, accent-mark-removed, uppercase string contains the specified <paramref name="value"/> (Ignore case-insensitive), using <see cref="CleanRemoveAccentMarkToUpper"/> and <see cref="IgnoreCaseContains(string, string)"/>.
        /// </summary>
        /// <param name="this">The string to clean, remove accent marks from, and convert to uppercase.</param>
        /// <param name="value">The value to check for in the transformed string.</param>
        /// <returns>True if the cleaned, accent-mark-removed, uppercase string contains the specified value (Ignore case-insensitive); otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static bool IsCleanRemoveAccentMarkToUpperIgnoreCaseContains(this string @this, string value)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.CleanRemoveAccentMarkToUpper().IgnoreCaseContains(value);
        }

        /// <summary>
        /// Converts the first character of the string to uppercase and the remaining characters to lowercase.
        /// </summary>
        /// <param name="this">The string to capitalize.</param>
        /// <returns>The capitalized string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CapitalizeCase(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            if (@this.Length == 1)
                return @this.ToUpper();

            return @this[0].ToString(CultureInfo.InvariantCulture).ToUpper() + @this.Substring(1).ToLower();
        }

        /// <summary>
        /// Converts the specified string to title case using the specified culture information.
        /// </summary>
        /// <param name="this">The string to convert to title case.</param>
        /// <param name="cultureInfoName">The name of the culture to use for title case conversion. Default value is "es-ES".</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces before converting to title case. Default is true.</param>
        /// <returns>The string converted to title case.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> or <paramref name="cultureInfoName"/> are null.</exception>
        /// <exception cref="CultureNotFoundException">Thrown if <paramref name="cultureInfoName"/> is not a supported value.</exception>
        public static string TitleCase(
            this string @this, 
            string cultureInfoName = "es-ES",
            bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));
            AssertHelper.AssertNotNullOrThrow(cultureInfoName, nameof(cultureInfoName));

            var resultingStr = @this.Trim();
            if (resultingStr.Length > 1)
            {
                if (removeConsecWhitespaces)
                    resultingStr = RemoveConsecWhitespaces(resultingStr);

                return new CultureInfo(cultureInfoName).TextInfo.ToTitleCase(resultingStr);
            }

            return resultingStr;
        }

        /// <summary>
        /// Removes accent marks from the specified string.
        /// </summary>
        /// <param name="this">The string from which to remove accent marks.</param>
        /// <returns>The string with accent marks removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string RemoveAccentMark(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            var normalizedStr = @this.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedStr)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark || c == 771 || c == 776)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Cleans the specified string by removing consecutive whitespaces and converts the result to capitalized case, using <see cref="Clean"/> and <see cref="RemoveConsecWhitespacesCapitalizedCase"/>.
        /// </summary>
        /// <param name="this">The string to clean.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default value is true.</param>
        /// <returns>The cleaned string converted to capitalized case.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CleanCapitalizedCase(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.Clean(removeConsecWhitespaces).RemoveConsecWhitespacesCapitalizedCase(false);// NOTE: Cleaning whitespaces is not necessary here since it was already done previously.
        }

        /// <summary>
        /// Cleans the specified string by removing consecutive whitespaces and converts the result to title case, using <see cref="Clean"/> and <see cref="TitleCase"/>.
        /// </summary>
        /// <param name="this">The string to clean.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default is true.</param>
        /// <returns>The cleaned source string converted to title case.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CleanTitleCase(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.Clean(removeConsecWhitespaces).TitleCase(removeConsecWhitespaces: false);// NOTE: Cleaning whitespaces is not necessary here since it was already done previously.
        }

        /// <summary>
        /// Capitalizes the specified string and removes accent marks, optionally removing consecutive whitespaces, using <see cref="RemoveConsecWhitespacesCapitalizedCase"/> and <see cref="RemoveAccentMark"/>.
        /// </summary>
        /// <param name="this">The string to capitalize and remove accent marks from.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default value is true.</param>
        /// <returns>The capitalized string with accent marks removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CapitalizeCaseRemoveAccentMark(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.RemoveConsecWhitespacesCapitalizedCase(removeConsecWhitespaces).RemoveAccentMark();
        }

        /// <summary>
        /// Cleans the specified string by converting it to capitalized case and removes accent marks, optionally removing consecutive whitespaces, using <see cref="CleanCapitalizedCase"/> and <see cref="RemoveAccentMark"/>.
        /// </summary>
        /// <param name="this">The source string to clean.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default is true.</param>
        /// <returns>The cleaned source string converted to capitalized case with accent marks removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CleanCapitalizedCaseRemoveAccentMark(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return CleanCapitalizedCase(@this, removeConsecWhitespaces).RemoveAccentMark();
        }

        /// <summary>
        /// Cleans the specified string by removing consecutive whitespaces and removing accent marks, using <see cref="Clean"/> and <see cref="RemoveAccentMark"/>.
        /// </summary>
        /// <param name="this">The string to clean.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default value is true.</param>
        /// <returns>The cleaned string with consecutive whitespaces and accent marks removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CleanRemoveAccentMark(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.Clean(removeConsecWhitespaces).RemoveAccentMark();
        }
        /// <summary>
        /// Cleans the specified string by removing consecutive whitespaces and accent marks, then converts the result to uppercase, using <see cref="CleanRemoveAccentMark"/>.
        /// </summary>
        /// <param name="this">The string to clean.</param>
        /// <param name="removeConsecWhitespaces">A boolean value indicating whether to remove consecutive whitespaces. Default is true.</param>
        /// <returns>The cleaned string with consecutive whitespaces and accent marks removed, converted to uppercase.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string CleanRemoveAccentMarkToUpper(this string @this, bool removeConsecWhitespaces = true)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return CleanRemoveAccentMark(@this, removeConsecWhitespaces).ToUpper();
        }

        /// <summary>
        /// Checks if the specified string contains the specified value (Ignore case-insensitive).
        /// </summary>
        /// <param name="this">The string to check for the value.</param>
        /// <param name="value">The value to check for in the <paramref name="this"/>.</param>
        /// <returns>True if the <paramref name="this"/> contains the specified <paramref name="value"/> (Ignore case-insensitive); otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> or <paramref name="value"/> are null.</exception>
        public static bool IgnoreCaseContains(this string @this, string value)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));
            AssertHelper.AssertNotNullOrThrow(value, nameof(value));

            return @this.IndexOf(value, StringComparison.OrdinalIgnoreCase) != -1;
        }

        /// <summary>
        /// Checks if the specified string is equal to the specified <paramref name="value"/> (Ignore case-insensitive).
        /// </summary>
        /// <param name="this">The string to check for equality.</param>
        /// <param name="value">The value to check for equality with the <paramref name="this"/>.</param>
        /// <returns>True if the <paramref name="this"/> is equal to the specified <paramref name="value"/> (Ignore case-insensitive); otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> or <paramref name="value"/> are null.</exception>
        public static bool IgnoreCaseEquals(this string @this, string value)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));
            AssertHelper.AssertNotNullOrThrow(value, nameof(value));

            return @this.IgnoreCaseContains(value) && @this.Length == value.Length;
        }

        /// <summary>
        /// Retrieves the substring of the <paramref name="this"/> that appears before the specified <paramref name="mark"/>.
        /// </summary>
        /// <param name="this">The string from which to retrieve the substring.</param>
        /// <param name="mark">The character used to determine the boundary of the substring.</param>
        /// <returns>The substring of the <paramref name="this"/> that appears before the specified <paramref name="mark"/>, or the entire <paramref name="this"/> if the <paramref name="mark"/> was not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string GetLeftOfMark(this string @this, char mark)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            int index = @this.IndexOf(mark);
            if (index < 0)
                return @this;
            return @this.Substring(0, index);
        }

        /// <summary>
        /// Retrieves only the alphabetic characters from the specified <paramref name="this"/>.
        /// </summary>
        /// <param name="this">The string from which to retrieve alphabetic characters.</param>
        /// <returns>A string containing only the alphabetic characters from the <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string ExtractOnlyText(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return new String(@this.Where(Char.IsLetter).ToArray());
        }

        /// <summary>
        /// Extracts only the numeric characters from the specified <paramref name="this"/>.
        /// </summary>
        /// <param name="this">The source string from which to extract numeric characters.</param>
        /// <param name="completeToEnd">A boolean value indicating whether to extract numbers until the end of the string.</param>
        /// <returns>A string containing only the numeric characters extracted from the source string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string ExtractNumbers(this string @this, bool completeToEnd)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            var result = "";
            if (!string.IsNullOrWhiteSpace(@this))
            {
                foreach (char ch in @this)
                    if (ch >= '0' && ch <= '9')
                        result += ch;
                    else
                    if (!completeToEnd)
                        return result;
            }
            return result;
        }

        /// <summary>
        /// Matches the specified regular expression <paramref name="pattern"/> against the <paramref name="this"/> and returns the matched substring.
        /// </summary>
        /// <param name="this">The string to match against.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>The matched substring from the <paramref name="this"/> based on the specified regular expression <paramref name="pattern"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> or <paramref name="pattern"/> are null.</exception>
        public static string MatchRegex(this string @this, string pattern)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));
            AssertHelper.AssertNotNullOrThrow(pattern, nameof(pattern));

            return Regex.Match(@this, pattern).Value;
        }

        /// <summary>
        /// Matches the specified regular expression <paramref name="pattern"/> against the <paramref name="this"/> and returns all matched substrings concatenated with the specified <paramref name="matchSeparator"/>.
        /// </summary>
        /// <param name="this">The string to match against.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="matchSeparator">The separator to concatenate the matched substrings. If null or empty, no separator is used.</param>
        /// <returns>A string containing all matched substrings concatenated with the specified <paramref name="matchSeparator"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> or <paramref name="pattern"/> are null.</exception>
        public static string MatchesRegex(
            this string @this, 
            string pattern, 
            string? matchSeparator)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));
            AssertHelper.AssertNotNullOrThrow(pattern, nameof(pattern));

            string result = "";
            Match match = Regex.Match(@this, pattern);//Note: Another way is: var match = Regex.Matches(source, pattern);
            while (match.Success)
            {
                if (!string.IsNullOrEmpty(result))
                    result += matchSeparator;
                result += match.Value;

                match = match.NextMatch();
            }

            return result;
        }

        /// <summary>
        /// Wraps the specified <paramref name="this"/> in single quotation marks.
        /// </summary>
        /// <param name="this">The string to wrap.</param>
        /// <returns>The specified string wrapped in single quotation marks.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string WrapInSingleQuotationMarks(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return $"'{@this}'";
        }

        /// <summary>
        /// Generates a hexadecimal hash code for the specified <paramref name="this"/>.
        /// </summary>
        /// <param name="this">The string for which to generate the hash code.</param>
        /// <returns>A hexadecimal representation of the hash code for the specified <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is null.</exception>
        public static string GenerateHexHash(this string @this)
        {
            AssertHelper.AssertNotNullOrThrow(@this, nameof(@this));

            return @this.GetHashCode().ToString("X");
        }
    }
}
