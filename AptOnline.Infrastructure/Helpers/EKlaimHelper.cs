using System.Security.Cryptography;
using System.Text;

namespace AptOnline.Infrastructure.Helpers
{
    public static class EKlaimHelper
    {
        public static string Encrypt(string text, string hexKey)
        {
            byte[] key = Encoding.Default.GetBytes(HexToBytes(hexKey));
            byte[] iv;
            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.GenerateIV();
                iv = aes.IV;

                using ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] plainBytes = Encoding.Default.GetBytes(text);
                encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }

            byte[] signature = new HMACSHA256(key).ComputeHash(encrypted).Take(10).ToArray();
            byte[] combined = signature.Concat(iv).Concat(encrypted).ToArray();
            return Convert.ToBase64String(combined);
        }

        public static string Decrypt(string base64Input, string hexKey)
        {
            byte[] combined = Convert.FromBase64String(base64Input);
            byte[] iv = combined.Skip(10).Take(16).ToArray();
            byte[] encrypted = combined.Skip(26).ToArray();
            byte[] key = Encoding.Default.GetBytes(HexToBytes(hexKey));

            using Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
            return Encoding.Default.GetString(decrypted);
        }

        private static string HexToBytes(string hex)
        {
            hex = hex.Replace("-", "");
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return Encoding.Default.GetString(bytes);
        }
    }
}
