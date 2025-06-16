using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.JenisRawatFeature;

public record JenisRawatType(string JenisRawatId, string JenisRawatName) : IJenisRawatKey
{
    public static JenisRawatType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new JenisRawatType(id, name);
    }

    public static JenisRawatType Default => new("-", "-");
    public static IJenisRawatKey Key(string id) => new JenisRawatType(id, "-");
}