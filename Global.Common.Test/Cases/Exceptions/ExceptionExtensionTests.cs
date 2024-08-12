namespace Global.Common.Test.Cases.Exceptions
{
    public class ExceptionExtensionTests
    {
        [Fact]
        public void GetAllStackTracesFromExceptionHierarchy_Test1()
        {
            // Arrange
            Exception exception = ExceptionExtensionHelper.BuildAndAssertExceptionWithInnerException();

            var expectedStackTraces = ExceptionExtensionHelper.GetStackTracesFromExceptionAndInnerExceptionWithDefaultSeparator(exception);

            // Act
            var actualStackTraces = exception.GetAllStackTracesFromExceptionHierarchy();

            // Assert
            AssertHelper.AssertNotNullAndEquals(expectedStackTraces, actualStackTraces);
        }

        [Fact]
        public void GetAllStackTracesFromExceptionHierarchy_Test2()
        {
            // Arrange
            Exception exception = ExceptionExtensionHelper.BuildAndAssertExceptionWithInnerException();
            var customSeparator = ExceptionExtensionHelper.GetCustomSeparatorFunc();

            var expectedStackTraces = ExceptionExtensionHelper.GetStackTracesFromExceptionAndInnerException(
                exception,
                customSeparator,
                customSeparator);

            // Act
            var actualStackTraces = exception.GetAllStackTracesFromExceptionHierarchy(customSeparator);

            // Assert
            AssertHelper.AssertNotNullAndEquals(expectedStackTraces, actualStackTraces);
        }

        [Fact]
        public void GetAllMessagesFromExceptionHierarchy_Test1()
        {
            // Arrange
            Exception exception = ExceptionExtensionHelper.BuildAndAssertExceptionWithInnerException();

            var expectedStackTraces = ExceptionExtensionHelper.GetMessagesFromExceptionAndInnerExceptionWithDefaultSeparator(exception);

            // Act

            var actualStackTraces = exception.GetAllMessagesFromExceptionHierarchy();

            // Assert
            AssertHelper.AssertNotNullAndEquals(expectedStackTraces, actualStackTraces);
        }

        [Fact]
        public void GetAllMessagesFromExceptionHierarchy_Test2()
        {
            // Arrange
            Exception exception = ExceptionExtensionHelper.BuildAndAssertExceptionWithInnerException();
            var customSeparator = ExceptionExtensionHelper.GetCustomSeparatorFunc();

            var expectedStackTraces = ExceptionExtensionHelper.GetMessagesFromExceptionAndInnerException(
                exception,
                customSeparator,
                customSeparator);

            // Act
            var actualStackTraces = exception.GetAllMessagesFromExceptionHierarchy(customSeparator);

            // Assert
            AssertHelper.AssertNotNullAndEquals(actualStackTraces, expectedStackTraces);
        }
    }
}
