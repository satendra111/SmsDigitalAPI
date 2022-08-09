using Core.Concrete;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public static class SecurityService
    {
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#$%&()*+,-./:;?@[]_";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string GeneratePassword(short passwordLength = -1)
        {
            var specialCharacters = @"!#$%&()*+,-./:;?@[]_";
            var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            passwordLength = (short)((passwordLength == -1) ? 10 : passwordLength);

            var passChars = new char[passwordLength];
            var randomPicker = new Random(new Random().Next(1, int.MaxValue));

            for (var i = 0; i < passwordLength; i++)
            {
                if (i % 2 == 0)
                {
                    passChars[i] = specialCharacters[randomPicker.Next(0, specialCharacters.Length)];
                }
                else
                {
                    passChars[i] = allowedChars[randomPicker.Next(0, allowedChars.Length)];
                }
            }

            return new string(passChars);
        }

        /// <summary>
        /// Computes the hash of the given data using SHA512.
        /// If salt is empty or null, it will not be part of the hash.
        /// </summary>
        /// <param name="dataToHash">Data to hash</param>
        /// <param name="salt">The salt value</param>
        /// <returns>Bytes of hashed data</returns>
        /// <exception cref="SystemException">When failed to compute hash.</exception>
        /// <exception cref="ArgumentNullException">When dataToHash is null.</exception>
        public static byte[] ComputeHash(byte[] dataToHash, byte[] salt)
        {
            #region Validation
            if (dataToHash == null)
            {
                throw new ArgumentNullException(nameof(dataToHash));
            }
            #endregion

            #region Initialization
            byte[] hashedData;
            #endregion

            #region Calculating Hash
            try
            {
                // If salt is null then initialized it with empty array.
                salt = salt ?? new byte[] { };
                // Salt will be used as {salt + data + salt}.
                var tempDataToHash = new byte[(salt.Length * 2) + dataToHash.Length];
                // Appending salt at the beginning.
                Array.Copy(salt, tempDataToHash, salt.Length);
                // Appending data.
                Array.Copy(dataToHash, 0, tempDataToHash, salt.Length, dataToHash.Length);
                // Appending salt at the end.
                Array.Copy(salt, 0, tempDataToHash, dataToHash.Length + salt.Length, salt.Length);

                using (var hashAlgo = new SHA512Managed())
                {
                    hashedData = hashAlgo.ComputeHash(tempDataToHash);
                }
            }
            catch (Exception ex)
            {
                throw new SystemException("Failed to compute the hash", ex);
            }
            #endregion

            #region Return
            return hashedData;
            #endregion
        }

        /// <summary>
        /// Computes the hash of the given data using SHA512.
        /// If salt is empty or null, it will not be part of the hash.
        /// </summary>
        /// <param name="dataToHash">The plain text data</param>
        /// <param name="salt">The salt value</param>
        /// <returns>Base64 String of hashed value</returns>
        /// <exception cref="SystemException">When failed to compute hash.</exception>
        /// <exception cref="ArgumentNullException">When stringToHash is null or empty.</exception>
        public static string ComputeHash(string dataToHash, string salt)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(dataToHash))
            {
                throw new ArgumentNullException(nameof(dataToHash));
            }
            #endregion

            #region Calculating Hash
            try
            {
                return Convert.ToBase64String(ComputeHash(Encoding.UTF8.GetBytes(dataToHash), (string.IsNullOrWhiteSpace(salt) ? null : Encoding.UTF8.GetBytes(salt))));
            }
            catch (Exception ex) when (!(ex is SystemException))
            {
                throw new SystemException(ErrorMessage.FailedToComputeHash, ex);
            }
            #endregion
        }

        /// <summary>
        /// Computes the hash of the given data using SHA512.
        /// If salt is empty or null, it will not be part of the hash.
        /// </summary>
        /// <param name="dataToHash">The plain text data</param>
        /// <param name="salt">The salt value</param>
        /// <returns>Hashed bytes</returns>
        /// <exception cref="SystemException">When failed to compute hash.</exception>
        /// <exception cref="ArgumentNullException">When stringToHash is null or empty.</exception>
        public static byte[] ComputeHashToBytes(string dataToHash, string salt)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(dataToHash))
            {
                throw new ArgumentNullException(nameof(dataToHash), ErrorMessage.ParameterNullOrEmpty);
            }
            #endregion

            #region Calculating Hash
            try
            {
                return ComputeHash(Encoding.UTF8.GetBytes(dataToHash), (string.IsNullOrWhiteSpace(salt) ? null : Encoding.UTF8.GetBytes(salt)));
            }
            catch (Exception ex) when (!(ex is SystemException))
            {
                throw new SystemException(ErrorMessage.FailedToComputeHash, ex);
            }
            #endregion
        }

        /// <summary>
        /// Computes the hash of the given data using SHA512.
        /// If salt is empty or null, it will not be part of the hash.
        /// </summary>
        /// <param name="dataToHash">Data to hash</param>
        /// <param name="salt">The salt value</param>
        /// <returns>Base64 encoded string of hashed data</returns>
        /// <exception cref="SystemException">When failed to compute hash.</exception>
        /// <exception cref="ArgumentNullException">When dataToHash is null.</exception>
        public static string ComputeHashToBase64(byte[] dataToHash, byte[] salt)
        {
            try
            {
                #region Calculating Hash & Return
                return Convert.ToBase64String(ComputeHash(dataToHash, salt));
                #endregion
            }
            catch (Exception ex) when (!(ex is SystemException))
            {
                throw new SystemException(ErrorMessage.FailedToComputeHash, ex);
            }
        }

        /// <summary>
        /// Verify hashes.
        /// </summary>
        /// <param name="originalHash">The Base64String original hash</param>
        /// <param name="hashToVerify">The Base64String hash to verify</param>
        /// <returns>True if verification is positive, false otherwise.</returns>
        /// <exception cref="SystemException">When failed to verify hash.</exception>
        /// <exception cref="ArgumentNullException">When originalHash or hashToVerify is null or empty.</exception>
        public static bool VerifyHash(string originalHash, string hashToVerify)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(originalHash))
            {
                throw new ArgumentNullException(nameof(originalHash), ErrorMessage.ParameterNullOrEmpty);
            }

            if (string.IsNullOrWhiteSpace(hashToVerify))
            {
                throw new ArgumentNullException(nameof(hashToVerify), ErrorMessage.ParameterNullOrEmpty);
            }
            #endregion

            #region Verifying Hash & Return
            try
            {
                return string.Equals(originalHash, hashToVerify);
            }
            catch (Exception ex)
            {
                throw new SystemException(ErrorMessage.FailedToVerifyHash, ex);
            }
            #endregion
        }

        /// <summary>
        /// Verify hashes.
        /// </summary>
        /// <param name="originalHash">The original hash</param>
        /// <param name="hashToVerify">The hash to verify</param>
        /// <returns>True if verification is positive, false otherwise.</returns>
        /// <exception cref="SystemException">When failed to verify hash.</exception>
        /// <exception cref="ArgumentNullException">When originalHash or hashToVerify is null or empty.</exception>
        public static bool VerifyHash(byte[] originalHash, byte[] hashToVerify)
        {
            #region Validation
            if (originalHash == null)
            {
                throw new ArgumentNullException(nameof(originalHash), ErrorMessage.ParameterNullOrEmpty);
            }

            if (hashToVerify == null)
            {
                throw new ArgumentNullException(nameof(hashToVerify), ErrorMessage.ParameterNullOrEmpty);
            }
            #endregion

            #region Verifying Hash & Return
            try
            {
                return originalHash.SequenceEqual(hashToVerify);
            }
            catch (Exception ex)
            {
                throw new SystemException(ErrorMessage.FailedToVerifyHash, ex);
            }
            #endregion
        }

        public static string Decrypt(string cipher, string key, string iv)
        {
            string plainData;
            using var aes = new AesManaged()
            {
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                Key = Encoding.UTF8.GetBytes(key)
            };
            var byteData = Convert.FromBase64String(cipher);
            var decryptor = aes.CreateDecryptor(aes.Key, Encoding.UTF8.GetBytes(iv));
            // Create the streams used for decryption.
            using var ms = new MemoryStream(byteData);
            // Create crypto stream
            var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            try
            {
                using var reader = new StreamReader(cs);
                plainData = reader.ReadToEnd();
                cs = null;
            }
            finally
            {
                cs?.Dispose();
            }
            return plainData;
        }
    }
}
