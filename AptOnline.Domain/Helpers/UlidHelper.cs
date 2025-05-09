namespace Farpu.Domain.Helpers;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public struct UlidHelper
{
    private static readonly char[] EncodingChars = "0123456789ABCDEFGHJKMNPQRSTVWXYZ".ToCharArray();
    private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

    public static string NewUlid()
    {
        var unixTimeMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var bytes = new byte[16];

        // Encode timestamp
        bytes[0] = (byte)((unixTimeMs >> 40) & 0xFF);
        bytes[1] = (byte)((unixTimeMs >> 32) & 0xFF);
        bytes[2] = (byte)((unixTimeMs >> 24) & 0xFF);
        bytes[3] = (byte)((unixTimeMs >> 16) & 0xFF);
        bytes[4] = (byte)((unixTimeMs >> 8) & 0xFF);
        bytes[5] = (byte)(unixTimeMs & 0xFF);

        // Generate random part
        Rng.GetBytes(bytes, 6, 10);

        // Convert to Base32
        var sb = new StringBuilder(26);
        for (var i = 0; i < 26; i++)
        {
            var idx = (i * 5) / 8;
            var shift = (i * 5) % 8;
            var value = (bytes[idx] << 8) | (idx + 1 < bytes.Length ? bytes[idx + 1] : 0);
            sb.Append(EncodingChars[(value >> (11 - shift)) & 0x1F]);
        }

        return sb.ToString();
    }
}