
namespace Global.Common.Helpers
{
    /// <summary>
    /// Provides helper methods for working with crypto.
    /// </summary>
    public static class AesCryptoHelper
    {
        #region Encryption

        /// <summary>
        /// Encrypts the specified <paramref name="plainText"/> using the provided <paramref name="privateKey"/>.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="privateKey">The private key used for encryption.</param>
        /// <returns>The encrypted text.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="plainText"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="privateKey"/> is null or empty.</exception>
        public static string? Encrypt(string plainText, string privateKey)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(plainText, nameof(plainText));
            AssertHelper.AssertNotNullNotEmptyOrThrow(privateKey, nameof(privateKey));

            byte[] passwordBytes = Encoding.UTF8.GetBytes(privateKey);
            byte[] aesKey = SHA256.HashData(passwordBytes);
            byte[] aesIV = MD5.HashData(passwordBytes);

            return EncryptToString_Aes(plainText, aesKey, aesIV, CipherMode.CBC, PaddingMode.PKCS7);
        }

        private static string? EncryptToString_Aes(
            string plainText,
            byte[] aesKey,
            byte[] aesIV,
            CipherMode mode,
            PaddingMode paddingMode)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(plainText, nameof(plainText));
            AssertHelper.AssertNotNullOrThrow(aesKey, nameof(aesKey));
            AssertHelper.AssertNotNullOrThrow(aesIV, nameof(aesIV));

            return HexHelper.SafeConvertByteToHex(EncryptToBytes_Aes(plainText, aesKey, aesIV, mode, paddingMode));
        }

        private static byte[]? EncryptToBytes_Aes(
            string plainText,
            byte[] aesKey,
            byte[] aesIV,
            CipherMode mode,
            PaddingMode paddingMode)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(plainText, nameof(plainText));
            AssertHelper.AssertNotNullOrThrow(aesKey, nameof(aesKey));
            AssertHelper.AssertNotNullOrThrow(aesIV, nameof(aesIV));

            ArgumentOutOfRangeException.ThrowIfZero(aesKey.Length, nameof(aesKey));
            ArgumentOutOfRangeException.ThrowIfZero(aesIV.Length, nameof(aesIV));

            using (var _aes = Aes.Create())
            {
                InitializeAesAlg(_aes, aesKey, aesIV, mode, paddingMode);

                ICryptoTransform encryptor = CreateEncryptor(_aes);

                return ExecuteCrypto(encryptor, CryptoStreamMode.Write, plainText) as byte[];
            }
        }

        private static ICryptoTransform CreateEncryptor(Aes _aes)
        {
            return _aes.CreateEncryptor(_aes.Key, _aes.IV);
        }

        private static MemoryStream CreateMemoryStream(byte[]? buffer = default)
        {
            if (buffer == null)
                return new MemoryStream();

            return new MemoryStream(buffer);
        }

        #endregion

        #region Decryption

        /// <summary>
        /// Decrypts the specified <paramref name="cipherText"/> using the provided <paramref name="privateKey"/>.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="privateKey">The private key used for decryption.</param>
        /// <returns>The decrypted text, or null if the input cipher text or private key is null or empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="cipherText"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="privateKey"/> is null or empty.</exception>
        public static string? Decrypt(string cipherText, string privateKey)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(cipherText, nameof(cipherText));
            AssertHelper.AssertNotNullNotEmptyOrThrow(privateKey, nameof(privateKey));

            byte[] passwordBytes = Encoding.UTF8.GetBytes(privateKey);
            byte[] aesKey = SHA256.HashData(passwordBytes);
            byte[] aesIV = MD5.HashData(passwordBytes);

            return DecryptFromString_Aes(cipherText, aesKey, aesIV, CipherMode.CBC, PaddingMode.PKCS7);
        }

        private static string? DecryptFromString_Aes(
            string cipherText,
            byte[] aesKey,
            byte[] aesIV,
            CipherMode mode,
            PaddingMode paddingMode)
        {
            return DecryptFromBytes_Aes(HexHelper.ConvertHexToBytes(cipherText), aesKey, aesIV, mode, paddingMode);
        }

        private static string? DecryptFromBytes_Aes(
            byte[] cipherText,
            byte[] aesKey,
            byte[] aesIV,
            CipherMode mode,
            PaddingMode paddingMode)
        {
            AssertHelper.AssertNotNullOrThrow(cipherText, nameof(cipherText));
            AssertHelper.AssertNotNullOrThrow(aesKey, nameof(aesKey));
            AssertHelper.AssertNotNullOrThrow(aesIV, nameof(aesIV));

            ArgumentOutOfRangeException.ThrowIfZero(cipherText.Length, nameof(aesKey));
            ArgumentOutOfRangeException.ThrowIfZero(aesKey.Length, nameof(aesKey));
            ArgumentOutOfRangeException.ThrowIfZero(aesIV.Length, nameof(aesIV));

            using (var _aes = Aes.Create())
            {
                InitializeAesAlg(_aes, aesKey, aesIV, mode, paddingMode);

                ICryptoTransform decryptor = CreateDecryptor(_aes);

                return ExecuteCrypto(decryptor, CryptoStreamMode.Read, cipherText: cipherText) as string;
            }
        }

        private static ICryptoTransform CreateDecryptor(Aes _aes)
        {
            return _aes.CreateDecryptor(_aes.Key, _aes.IV);
        }

        #endregion

        #region Common

        private static void InitializeAesAlg(
            Aes _aes,
            byte[] aesKey,
            byte[] aesIV,
            CipherMode mode,
            PaddingMode paddingMode)
        {
            _aes.Key = aesKey;
            _aes.IV = aesIV;
            _aes.Mode = mode;
            _aes.Padding = paddingMode;
        }

        private static object ExecuteCrypto(
            ICryptoTransform cryptoTransform,
            CryptoStreamMode csMode,
            string? plainText = null, 
            byte[]? cipherText = null)
        {
            using (var memStream = CreateMemoryStream(cipherText))
            {
                using (var _cryptoStream = new CryptoStream(memStream, cryptoTransform, csMode))
                {
                    switch (csMode)
                    {
                        case CryptoStreamMode.Read:
                            using (var _streamReader = new StreamReader(_cryptoStream))
                                return _streamReader.ReadToEnd();
                        case CryptoStreamMode.Write:
                            using (var _streamWriter = new StreamWriter(_cryptoStream))
                                _streamWriter.Write(plainText);
                            return memStream.ToArray();
                        default:
                            throw new InvalidOperationException(nameof(csMode));
                    }
                }
            }
        }

        #endregion
    }
}
