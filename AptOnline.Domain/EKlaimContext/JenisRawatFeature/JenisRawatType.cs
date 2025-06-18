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
    
    public static JenisRawatType RawatInap => new("1", "Rawat Inap");
    public static JenisRawatType RawatJalan => new("2", "Rawat Jalan");
    public static JenisRawatType RawatDarurat => new("3", "Rawat Darurat");
    
    public static IEnumerable<JenisRawatType> ListData() => new List<JenisRawatType> { RawatInap, RawatJalan, RawatDarurat };
}