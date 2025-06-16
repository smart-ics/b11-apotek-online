using AptOnline.Domain.SepContext.KelasRawatFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.JenisPelayananFeature;

public record JenisPelayananType(string JenisPelayananId, string JenisPelayananName) : IJenisPelayananKey
{
    public static JenisPelayananType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new JenisPelayananType(id, name);
    }

    public static JenisPelayananType Default => new("-", "-");
    public static IJenisPelayananKey Key(string id) => new JenisPelayananType(id, "-");
}