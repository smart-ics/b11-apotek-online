using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.KelasRawatFeature;

public record KelasRawatType(string KelasRawatId, string KelasRawatName) : IKelasRawatKey
{
    public static KelasRawatType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new KelasRawatType(id, name);
    }

    public static KelasRawatType Default => new("-", "-");
    public static IKelasRawatKey Key(string id) => new KelasRawatType(id, "-");
}