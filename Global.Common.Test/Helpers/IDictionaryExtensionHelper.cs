
namespace Global.Common.Test.Helpers
{
    internal static class IDictionaryExtensionHelper
    {
        public const string TestKey = "Test key";
        public const string TestValue = "Test value";

        public static IDictionary<string, string> BuildDefaultDictionary(string key = TestKey, string value = TestValue)
        {
            return new Dictionary<string, string> { {  key, value } };
        }
    }
}
