using System.Security.Cryptography;
using System.Text;

namespace AptOnline.Infrastructure.Helpers
{
    public static class BpjsHelper
    {
        //---hmac256 encoding
        public static string GenHMAC256(string data, string secretkey)
        {
            // Initialize the keyed hash object using the secret key as the key
            HMACSHA256 hashObject = new(Encoding.UTF8.GetBytes(secretkey));

            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Base 64 Encode
            return Convert.ToBase64String(signature);
        }
        //---unix timestamp
        public static long GetTimeStamp()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
        //---decrypt response (vclaim 1.1+)
        public static string Decrypt(string key, string data)
        {
            string decData = null;
            byte[][] keys = GetHashKeys(key);

            try
            {
                decData = DecryptStringFromBytes_Aes(data, keys[0], keys[1]);
            }
            catch (CryptographicException) { }
            catch (ArgumentNullException) { }
            //---LZString decompresss
            return LzString.DecompressFromEncodedUriComponent(decData);
        }
        private static byte[][] GetHashKeys(string key)
        {
            byte[][] result = new byte[2][];
            Encoding enc = Encoding.UTF8;
            SHA256 sha2 = new SHA256CryptoServiceProvider();
            byte[] rawKey = enc.GetBytes(key);
            byte[] rawIV = enc.GetBytes(key);
            byte[] hashKey = sha2.ComputeHash(rawKey);
            byte[] hashIV = sha2.ComputeHash(rawIV);
            Array.Resize(ref hashIV, 16);
            result[0] = hashKey;
            result[1] = hashIV;
            return result;
        }
        private static string DecryptStringFromBytes_Aes(string cipherTextString, byte[] Key, byte[] IV)
        {
            byte[] cipherText = Convert.FromBase64String(cipherTextString);

            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new(cipherText);
                using CryptoStream csDecrypt =
                        new(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new(csDecrypt);
                plaintext = srDecrypt.ReadToEnd();
            }
            return plaintext;
        }
        
        private static string HexToBin(string input)
        {
            input = input.Replace("-", "");
            byte[] raw = new byte[input.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(input.Substring(i * 2, 2), 16);
            }
            return Encoding.Default.GetString(raw);
        }
        //---SITB------
        //---md5 hash
        public static string CreateMD5(string input)
        {
            using System.Security.Cryptography.MD5 md5 =
            System.Security.Cryptography.MD5.Create();
            byte[] retVal = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
