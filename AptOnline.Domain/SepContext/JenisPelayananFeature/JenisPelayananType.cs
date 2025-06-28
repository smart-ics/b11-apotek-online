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

    public static JenisPelayananType RawatInap => new("1", "Rawat Inap");
    public static JenisPelayananType RawatJalan => new("2", "Rawat Jalan");
    public static IEnumerable<JenisPelayananType> ListData() => new List<JenisPelayananType> { RawatInap, RawatJalan };
}