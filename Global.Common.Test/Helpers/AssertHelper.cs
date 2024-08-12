
namespace Global.Common.Test.Helpers
{
    internal static class AssertHelper
    {
        public static void AssertNotNullAndEquals(string? expected, string? actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            AssertEquals(expected, actual);
        }

        public static void AssertEquals(string? expected, string? actual)
        {
            Assert.Equal(expected, actual);
        }

        public static void AssertNotNull<T>(T? @object)
        {
            Assert.NotNull(@object);
        }

        public static void AssertNotNullNotEmptyNotWhiteSpace(string? value)
        {
            Assert.False(string.IsNullOrEmpty(value));
            Assert.False(string.IsNullOrWhiteSpace(value));
        }
    }
}
