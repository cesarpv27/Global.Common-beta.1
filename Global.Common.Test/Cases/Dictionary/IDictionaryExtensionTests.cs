
namespace Global.Common.Test.Cases.Dictionary
{
    public class IDictionaryExtensionTests
    {
        private readonly string _testKey2 = "Test key 2";
        private readonly string _testValue2 = "Test value 2";
        private readonly string _testValue3 = "Test value 3";

        #region AddOrUpdate

        [Fact]
        public void AddOrUpdate_Succeed_Test1()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestValue = _testValue2;

            // Act
            dict.AddOrUpdate(IDictionaryExtensionHelper.TestKey, expectedTestValue);

            // Assert
            Assert.Single(dict);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(expectedTestValue, actualTestValue);
        }

        [Fact]
        public void AddOrUpdate_Succeed_Test2()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKey = _testKey2;
            var expectedTestValue = _testValue2;

            // Act
            dict.AddOrUpdate(expectedTestKey, expectedTestValue);

            // Assert
            Assert.Equal(2, dict.Count);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(IDictionaryExtensionHelper.TestValue, actualTestValue);

            Assert.True(dict.TryGetValue(expectedTestKey, out string? actualTestValue2));
            AssertHelper.AssertNotNullAndEquals(actualTestValue2, expectedTestValue);
        }

        #endregion

        #region AddOrRenameKey

        [Fact]
        public void AddOrRenameKey_Succeed_Test1()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKey = IDictionaryExtensionHelper.TestKey + IDictionaryExtensions.DefaultRenamedKeySuffix;
            var expectedTestValue = _testValue2;

            // Act
            var actualTestKey = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValue);

            // Assert
            Assert.Equal(2, dict.Count);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(IDictionaryExtensionHelper.TestValue, actualTestValue);

            AssertHelper.AssertNotNullAndEquals(expectedTestKey, actualTestKey);
            Assert.True(dict.TryGetValue(actualTestKey!, out string? actualTestValue2));
            AssertHelper.AssertNotNullAndEquals(expectedTestValue, actualTestValue2);
        }

        [Fact]
        public void AddOrRenameKey_Succeed_Test2()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKey = $"{IDictionaryExtensionHelper.TestKey}_2";
            var expectedTestValue = _testValue2;

            // Act
            var actualTestKey = dict.AddOrRenameKey(expectedTestKey, expectedTestValue);

            // Assert
            Assert.Equal(2, dict.Count);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(actualTestValue, IDictionaryExtensionHelper.TestValue);

            AssertHelper.AssertNotNullAndEquals(expectedTestKey, actualTestKey);
            Assert.True(dict.TryGetValue(actualTestKey!, out string? actualTestValue2));
            AssertHelper.AssertNotNullAndEquals(expectedTestValue, actualTestValue2);
        }

        [Fact]
        public void AddOrRenameKey_Succeed_Test3()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKeyRenamed = IDictionaryExtensionHelper.TestKey + IDictionaryExtensions.DefaultRenamedKeySuffix;
            var expectedTestValueOfRenamedKey = _testValue2;
            var expectedTestValueOfRenamedKey2 = _testValue3;

            // Act
            var actualTestKeyRenamed = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey);
            var actualTestKeyRenamed2 = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey2);

            // Assert
            Assert.Equal(3, dict.Count);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(IDictionaryExtensionHelper.TestValue, actualTestValue);

            AssertHelper.AssertNotNullAndEquals(expectedTestKeyRenamed, actualTestKeyRenamed);
            Assert.True(dict.TryGetValue(actualTestKeyRenamed!, out string? actualTestValueOfRenamedKey));
            AssertHelper.AssertNotNullAndEquals(expectedTestValueOfRenamedKey, actualTestValueOfRenamedKey);

            AssertHelper.AssertNotNullNotEmptyNotWhiteSpace(actualTestKeyRenamed2);
            Assert.True(dict.TryGetValue(actualTestKeyRenamed2!, out string? actualTestValueOfRenamedKey2));
            AssertHelper.AssertNotNullAndEquals(expectedTestValueOfRenamedKey2, actualTestValueOfRenamedKey2);
        }
        
        [Fact]
        public void AddOrRenameKey_Succeed_Test4()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKeyRenamed = IDictionaryExtensionHelper.TestKey + IDictionaryExtensions.DefaultRenamedKeySuffix;
            var expectedTestValueOfRenamedKey = _testValue2;
            var expectedTestValueOfRenamedKey2 = _testValue3;

            // Act
            var actualTestKeyRenamed = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey);
            var actualTestKeyRenamed2 = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey2, retryAttempts: 2);

            // Assert
            Assert.Equal(3, dict.Count);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(IDictionaryExtensionHelper.TestValue, actualTestValue);

            AssertHelper.AssertNotNullAndEquals(expectedTestKeyRenamed, actualTestKeyRenamed);
            Assert.True(dict.TryGetValue(actualTestKeyRenamed!, out string? actualTestValueOfRenamedKey));
            AssertHelper.AssertNotNullAndEquals(expectedTestValueOfRenamedKey, actualTestValueOfRenamedKey);

            AssertHelper.AssertNotNull(actualTestKeyRenamed2);
            Assert.Contains(expectedTestKeyRenamed, actualTestKeyRenamed2);
            Assert.True(dict.TryGetValue(actualTestKeyRenamed2!, out string? actualTestValueOfRenamedKey2));
            AssertHelper.AssertNotNullAndEquals(expectedTestValueOfRenamedKey2, actualTestValueOfRenamedKey2);
        }

        [Fact]
        public void AddOrRenameKey_FailedAdd_Test2()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKeyRenamed = IDictionaryExtensionHelper.TestKey + IDictionaryExtensions.DefaultRenamedKeySuffix;
            var expectedTestValueOfRenamedKey = _testValue2;
            var expectedTestValueOfRenamedKey2 = _testValue3;

            // Act
            var actualTestKeyRenamed = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey);
            var actualTestKeyRenamed2 = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey2, retryAttempts: 2);
            var actualTestKeyRenamed3 = dict.AddOrRenameKey(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey2, retryAttempts: 1);

            // Assert
            Assert.Equal(3, dict.Count);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(IDictionaryExtensionHelper.TestValue, actualTestValue);

            AssertHelper.AssertNotNullAndEquals(expectedTestKeyRenamed, actualTestKeyRenamed);
            Assert.True(dict.TryGetValue(actualTestKeyRenamed!, out string? actualTestValueOfRenamedKey));
            AssertHelper.AssertNotNullAndEquals(expectedTestValueOfRenamedKey, actualTestValueOfRenamedKey);

            AssertHelper.AssertNotNullNotEmptyNotWhiteSpace(actualTestKeyRenamed2);
            Assert.True(dict.TryGetValue(actualTestKeyRenamed2!, out string? actualTestValueOfRenamedKey2));
            AssertHelper.AssertNotNullAndEquals(expectedTestValueOfRenamedKey2, actualTestValueOfRenamedKey2);

            Assert.Null(actualTestKeyRenamed3);
        }

        #endregion

        #region Add

        [Fact]
        public void Add_Succeed_Test1_KeyExistActionTryAdd()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKey = _testKey2;
            var expectedTestValue = _testValue2;
            var keyExistAction = KeyExistAction.TryAdd;

            // Act
            var actualAddResult = dict.Add(expectedTestKey, expectedTestValue, out string? actualTestKey, keyExistAction);

            // Assert
            Assert.Equal(2, dict.Count);
            Assert.True(actualAddResult);

            AssertHelper.AssertNotNullAndEquals(expectedTestKey, actualTestKey);
            Assert.True(dict.TryGetValue(expectedTestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(expectedTestValue, actualTestValue);
        }

        [Fact]
        public void Add_Failed_Test1_KeyExistActionTryAdd()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestValue = _testValue2;
            var keyExistAction = KeyExistAction.TryAdd;

            // Act
            var actualAddResult = dict.Add(IDictionaryExtensionHelper.TestKey, expectedTestValue, out string? actualTestKey, keyExistAction);

            // Assert
            Assert.Single(dict);
            Assert.Null(actualTestKey);
            Assert.False(actualAddResult);
        }

        [Fact]
        public void Add_Succeed_Test2_KeyExistActionUpdate()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestValue = _testValue2;
            var keyExistAction = KeyExistAction.Update;

            // Act
            var actualAddResult = dict.Add(IDictionaryExtensionHelper.TestKey, expectedTestValue, out string? actualTestKey, keyExistAction);

            // Assert
            Assert.Single(dict);
            Assert.True(actualAddResult);

            AssertHelper.AssertNotNullAndEquals(IDictionaryExtensionHelper.TestKey, actualTestKey);
            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(expectedTestValue, actualTestValue);
        }

        [Fact]
        public void Add_Succeed_Test3_KeyExistActionRename()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestRenamedKey = IDictionaryExtensionHelper.TestKey + IDictionaryExtensions.DefaultRenamedKeySuffix;
            var expectedTestValueOfRenamedKey = _testValue2;
            var keyExistAction = KeyExistAction.Rename;

            // Act
            var actualAddResult = dict.Add(IDictionaryExtensionHelper.TestKey, expectedTestValueOfRenamedKey, out string? actualTestKey, keyExistAction);

            // Assert
            Assert.Equal(2, dict.Count);
            Assert.True(actualAddResult);

            AssertHelper.AssertNotNullAndEquals(expectedTestRenamedKey, actualTestKey);
            Assert.True(dict.TryGetValue(expectedTestRenamedKey, out string? actualTestValueOfRenamedKey));
            AssertHelper.AssertNotNullAndEquals(expectedTestValueOfRenamedKey, actualTestValueOfRenamedKey);
        }

        #endregion

        #region TryAddOrUpdate

        [Fact]
        public void TryAddOrUpdate_Succeed_Test1()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestKey = _testKey2;
            var expectedTestValue = _testValue2;
            var updateIfKeyExists = false;

            // Act
            var actualTryAddOrUpdateResult = dict.TryAddOrUpdate(expectedTestKey, expectedTestValue, updateIfKeyExists);

            // Assert
            Assert.Equal(2, dict.Count);
            Assert.True(actualTryAddOrUpdateResult);

            Assert.True(dict.TryGetValue(expectedTestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(expectedTestValue, actualTestValue);
        }

        [Fact]
        public void TryAddOrUpdate_Succeed_Test2()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestValue = _testValue2;
            var updateIfKeyExists = true;

            // Act
            var actualTryAddOrUpdateResult = dict.TryAddOrUpdate(IDictionaryExtensionHelper.TestKey, expectedTestValue, updateIfKeyExists);

            // Assert
            Assert.Single(dict);
            Assert.True(actualTryAddOrUpdateResult);

            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(expectedTestValue, actualTestValue);
        }

        [Fact]
        public void TryAddOrUpdate_Failed_Test1()
        {
            // Arrange
            var dict = IDictionaryExtensionHelper.BuildDefaultDictionary();
            var expectedTestValue = _testValue2;
            var updateIfKeyExists = false;

            // Act
            var actualTryAddOrUpdateResult = dict.TryAddOrUpdate(IDictionaryExtensionHelper.TestKey, expectedTestValue, updateIfKeyExists);

            // Assert
            Assert.Single(dict);
            Assert.False(actualTryAddOrUpdateResult);

            Assert.True(dict.TryGetValue(IDictionaryExtensionHelper.TestKey, out string? actualTestValue));
            AssertHelper.AssertNotNullAndEquals(IDictionaryExtensionHelper.TestValue, actualTestValue);
        }

        #endregion
    }
}
