using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TbIndikatorFeature;

public record TbIndikatorType(string TbIndikatorId, string TbIndikatorName) : ITbIndikatorKey
{
    public static TbIndikatorType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new TbIndikatorType(id, name);
    }
    public static TbIndikatorType Default => new("-", "-");
    public static ITbIndikatorKey Key(string id)
        => Default with {TbIndikatorId = id};
    
    public static TbIndikatorType Ya => new("1", "Ya");
    public static TbIndikatorType Tidak => new("0", "Tidak");
    public static IEnumerable<TbIndikatorType> ListData() => new[] {Ya, Tidak};
    
}