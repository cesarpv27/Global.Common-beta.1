namespace Global.Common.Test.Cases.Constants
{
    public class ConstantTests
    {
        private const string Backslash = @"\";
        private const string Slash = "/";
        private const string OpeningBrace = "{";
        private const string ClosingBrace = "}";
        private const string OAB = "<";
        private const string CAB = ">";
        private const string EqualsSign = "=";
        private const string OpenBracket = "[";
        private const string ClosingBracket = "]";
        private const string SingleQuote = "'";
        private const string DoubleQuote = "\"";
        private const string BackslashN = "\n";
        private const string BackslashR = "\r";
        private const string HtmlDiv = "div";
        private const string HtmlName = "name";
        private const string HtmlId = "id";
        private const string HtmlOABDiv = "<div";
        private const string HtmlOABSlashDivCAB = "</div>";
        private const string HtmlNameEqualsSign = "name=";
        private const string HtmlIdEqualsSign = "id=";
        private const string HtmlDivNameSingleQuote = "<div name='";
        private const string HtmlDivIdSingleQuote = "<div id='";

        [Fact]
        public void Test1()
        {
            // Arrange

            // Act

            // Assert
            AssertHelper.AssertNotNullAndEquals(Backslash, GlobalConstants.Backslash);
            AssertHelper.AssertNotNullAndEquals(Slash, GlobalConstants.Slash);
            AssertHelper.AssertNotNullAndEquals(OpeningBrace, GlobalConstants.OpeningBrace);
            AssertHelper.AssertNotNullAndEquals(ClosingBrace, GlobalConstants.ClosingBrace);
            AssertHelper.AssertNotNullAndEquals(OAB, GlobalConstants.OAB);
            AssertHelper.AssertNotNullAndEquals(CAB, GlobalConstants.CAB);
            AssertHelper.AssertNotNullAndEquals(EqualsSign, GlobalConstants.EqualsSign);
            AssertHelper.AssertNotNullAndEquals(OpenBracket, GlobalConstants.OpenBracket);
            AssertHelper.AssertNotNullAndEquals(ClosingBracket, GlobalConstants.ClosingBracket);
            AssertHelper.AssertNotNullAndEquals(SingleQuote, GlobalConstants.SingleQuote);
            AssertHelper.AssertNotNullAndEquals(DoubleQuote, GlobalConstants.DoubleQuote);
            AssertHelper.AssertNotNullAndEquals(BackslashN, GlobalConstants.BackslashN);
            AssertHelper.AssertNotNullAndEquals(BackslashR, GlobalConstants.BackslashR);
            AssertHelper.AssertNotNullAndEquals(HtmlDiv, GlobalConstants.HtmlDiv);
            AssertHelper.AssertNotNullAndEquals(HtmlName, GlobalConstants.HtmlName);
            AssertHelper.AssertNotNullAndEquals(HtmlId, GlobalConstants.HtmlId);
            AssertHelper.AssertNotNullAndEquals(HtmlOABDiv, GlobalConstants.HtmlOABDiv);
            AssertHelper.AssertNotNullAndEquals(HtmlOABSlashDivCAB, GlobalConstants.HtmlOABSlashDivCAB);
            AssertHelper.AssertNotNullAndEquals(HtmlNameEqualsSign, GlobalConstants.HtmlNameEqualsSign);
            AssertHelper.AssertNotNullAndEquals(HtmlIdEqualsSign, GlobalConstants.HtmlIdEqualsSign);
            AssertHelper.AssertNotNullAndEquals(HtmlDivNameSingleQuote, GlobalConstants.HtmlDivNameSingleQuote);
            AssertHelper.AssertNotNullAndEquals(HtmlDivIdSingleQuote, GlobalConstants.HtmlDivIdSingleQuote);
        }
    }
}
