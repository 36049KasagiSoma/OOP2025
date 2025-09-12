using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColorChecker {
    public class Encryption {
        private Encryption() { }

        private const int SaltSize = 16;
        private const int IvSize = 16;
        private const int KeySize = 32;
        private const int HmacSize = 32;
        private const int Pbkdf2Iterations = 100_000;

        public static string EncryptString(string plainText, string password) {
            if (plainText == null) throw new ArgumentNullException(nameof(plainText));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

            byte[] keyMaterial;
            using (var kdf = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256)) {
                keyMaterial = kdf.GetBytes(KeySize * 2);
            }
            var aesKey = new byte[KeySize];
            var hmacKey = new byte[KeySize];
            Buffer.BlockCopy(keyMaterial, 0, aesKey, 0, KeySize);
            Buffer.BlockCopy(keyMaterial, KeySize, hmacKey, 0, KeySize);

            var iv = new byte[IvSize];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(iv);

            byte[] ciphertext;
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (var aes = Aes.Create()) {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = aesKey;
                aes.IV = iv;
                using (var encryptor = aes.CreateEncryptor()) {
                    ciphertext = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }

            byte[] hmac;
            using (var h = new HMACSHA256(hmacKey)) {
                h.TransformBlock(salt, 0, salt.Length, null, 0);
                h.TransformBlock(iv, 0, iv.Length, null, 0);
                h.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                hmac = h.Hash;
            }

            var outBytes = new byte[SaltSize + IvSize + HmacSize + ciphertext.Length];
            int pos = 0;
            Buffer.BlockCopy(salt, 0, outBytes, pos, SaltSize); pos += SaltSize;
            Buffer.BlockCopy(iv, 0, outBytes, pos, IvSize); pos += IvSize;
            Buffer.BlockCopy(hmac, 0, outBytes, pos, HmacSize); pos += HmacSize;
            Buffer.BlockCopy(ciphertext, 0, outBytes, pos, ciphertext.Length);

            return Convert.ToBase64String(outBytes);
        }

        public static string DecryptString(string encryptedBase64, string password) {
            if (encryptedBase64 == null) throw new ArgumentNullException(nameof(encryptedBase64));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var all = Convert.FromBase64String(encryptedBase64);
            if (all.Length < SaltSize + IvSize + HmacSize) throw new ArgumentException("入力データが短すぎます。");

            int pos = 0;
            var salt = new byte[SaltSize]; Buffer.BlockCopy(all, pos, salt, 0, SaltSize); pos += SaltSize;
            var iv = new byte[IvSize]; Buffer.BlockCopy(all, pos, iv, 0, IvSize); pos += IvSize;
            var hmac = new byte[HmacSize]; Buffer.BlockCopy(all, pos, hmac, 0, HmacSize); pos += HmacSize;
            var ciphertextLen = all.Length - pos;
            var ciphertext = new byte[ciphertextLen]; Buffer.BlockCopy(all, pos, ciphertext, 0, ciphertextLen);
            byte[] keyMaterial;
            using (var kdf = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256)) {
                keyMaterial = kdf.GetBytes(KeySize * 2);
            }
            var aesKey = new byte[KeySize];
            var hmacKey = new byte[KeySize];
            Buffer.BlockCopy(keyMaterial, 0, aesKey, 0, KeySize);
            Buffer.BlockCopy(keyMaterial, KeySize, hmacKey, 0, KeySize);
            byte[] expectedHmac;
            using (var h = new HMACSHA256(hmacKey)) {
                h.TransformBlock(salt, 0, salt.Length, null, 0);
                h.TransformBlock(iv, 0, iv.Length, null, 0);
                h.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                expectedHmac = h.Hash;
            }

            if (!FixedTimeEquals(hmac, expectedHmac))
                throw new CryptographicException("認証に失敗しました（HMAC 不一致）。");
            byte[] plainBytes;
            using (var aes = Aes.Create()) {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = aesKey;
                aes.IV = iv;
                using (var decryptor = aes.CreateDecryptor()) {
                    plainBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                }
            }

            return Encoding.UTF8.GetString(plainBytes);
        }
        private static bool FixedTimeEquals(byte[] a, byte[] b) {
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
