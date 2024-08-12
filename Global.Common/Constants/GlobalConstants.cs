
namespace Global.Common.Constants
{
    /// <summary>
    /// Provides common resources.
    /// </summary>
    public static class GlobalConstants
    {
        #region Base signs

        /// <summary>
        /// Syntax for Backslash: '\'
        /// </summary>
        public static string Backslash => @"\";
        /// <summary>
        /// Syntax for Slash: '/'
        /// </summary>
        public static string Slash => "/";
        /// <summary>
        /// Syntax for Opening Brace: '{'
        /// </summary>
        public static string OpeningBrace => "{";
        /// <summary>
        /// Syntax for Closing Brace: '}'
        /// </summary>
        public static string ClosingBrace => "}";
        /// <summary>
        /// Syntax for Open Angle Bracket: &lt;
        /// </summary>
        public static string OAB => "<";
        /// <summary>
        /// Syntax for Close Angle Bracket: '>'
        /// </summary>
        public static string CAB => ">";
        /// <summary>
        /// Syntax for Equals sign: '='
        /// </summary>
        public static string EqualsSign => "=";
        /// <summary>
        /// Syntax for Open Bracket: '['
        /// </summary>
        public static string OpenBracket => "[";
        /// <summary>
        /// Syntax for Closing Bracket: ']'
        /// </summary>
        public static string ClosingBracket => "]";
        /// <summary>
        /// Syntax for Single Quote Mark: '
        /// </summary>
        public static string SingleQuote => "'";
        /// <summary>
        /// Syntax for Double Quote: "
        /// </summary>
        public static string DoubleQuote => "\"";

        #endregion

        /// <summary>
        /// Syntax for Backslash and n: \n
        /// </summary>
        public static string BackslashN => "\n";
        /// <summary>
        /// Syntax for Backslash and r: \r
        /// </summary>
        public static string BackslashR => "\r";

        #region Html base syntax

        /// <summary>
        /// Syntax for 'div'
        /// </summary>
        public static string HtmlDiv => "div";
        /// <summary>
        /// Syntax for 'name'
        /// </summary>
        public static string HtmlName => "name";
        /// <summary>
        /// Syntax for 'id'
        /// </summary>
        public static string HtmlId => "id";

        #endregion

        #region Html combined syntax

        /// <summary>
        /// Html syntax for Open Angle Bracket and 'div': &lt;div
        /// </summary>
        public static string HtmlOABDiv => OAB + HtmlDiv;
        /// <summary>
        /// Html syntax for Open Angle Bracket, Slash, 'div' and Close Angle Bracked: &lt;/div>
        /// </summary>
        public static string HtmlOABSlashDivCAB => OAB + Slash + HtmlDiv + CAB;

        /// <summary>
        /// Html syntax for 'name' and equals sign: name=
        /// </summary>
        public static string HtmlNameEqualsSign => HtmlName + EqualsSign;

        /// <summary>
        /// Html syntax for 'id' and equals sign: id=
        /// </summary>
        public static string HtmlIdEqualsSign => HtmlId + EqualsSign;

        /// <summary>
        /// Html syntax for Open Angle Bracket, 'div', space, 'name', equals sign and single quote: &lt;div name='
        /// </summary>
        public static string HtmlDivNameSingleQuote => $"{HtmlOABDiv} {HtmlNameEqualsSign}{SingleQuote}";

        /// <summary>
        /// Html syntax for Open Angle Bracket, 'div', space, 'id', equals sign and single quote: &lt;div id='
        /// </summary>
        public static string HtmlDivIdSingleQuote => $"{HtmlOABDiv} {HtmlIdEqualsSign}{SingleQuote}";

        #endregion

        #region ContentType

        /// <summary>
        /// Represents the content type "application/xml".
        /// </summary>
        public const string ContentTypeApplicationXml = "application/xml";

        /// <summary>
        /// Represents the content type "application/json".
        /// </summary>
        public const string ContentTypeApplicationJson = "application/json";

        #endregion

        #region Exception

        /// <summary>
        /// Represents the default separator used for formatting exceptions.
        /// </summary>
        public const string DefaultExceptionSeparator = " --> ";

        #endregion
    }
}
