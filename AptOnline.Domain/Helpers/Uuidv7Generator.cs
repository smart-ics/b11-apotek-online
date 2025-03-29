namespace AptOnline.Domain.Helpers;

public static class Uuid7Generator
{
    private static readonly Random Random = new Random();

    public static Guid NewUuid7()
    {
        long unixTimeMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        byte[] uuidBytes = new byte[16];

        // Encode timestamp (first 48 bits)
        uuidBytes[0] = (byte)((unixTimeMs >> 40) & 0xFF);
        uuidBytes[1] = (byte)((unixTimeMs >> 32) & 0xFF);
        uuidBytes[2] = (byte)((unixTimeMs >> 24) & 0xFF);
        uuidBytes[3] = (byte)((unixTimeMs >> 16) & 0xFF);
        uuidBytes[4] = (byte)((unixTimeMs >> 8) & 0xFF);
        uuidBytes[5] = (byte)(unixTimeMs & 0xFF);

        // Set UUID version to 7 (UUIDv7)
        uuidBytes[6] = (byte)(0x70 | ((unixTimeMs >> 12) & 0x0F));

        // Set variant (RFC 4122)
        uuidBytes[8] = (byte)(0x80 | (Random.Next(0, 64)));

        // Fill the rest with randomness
        byte[] randomBytes = new byte[7];
        Random.NextBytes(randomBytes);
        Buffer.BlockCopy(randomBytes, 0, uuidBytes, 7, 7);

        return new Guid(uuidBytes);
    }
}