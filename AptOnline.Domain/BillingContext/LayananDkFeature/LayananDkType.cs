using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.LayananDkFeature;

public record LayananDkType(string LayananDkId, string LayananDkName) : ILayananDkKey
{
    public static LayananDkType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new LayananDkType(id, name);
    }
    public static LayananDkType Default => new("-", "-");
    public static ILayananDkKey Key(string id)
        => Default with {LayananDkId = id};
    
    public static LayananDkType Icu => new("23", "ICU");
}