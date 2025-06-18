using AptOnline.Domain.EKlaimContext.Covid19Feature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.TipeLayananDkFeature;

public record TipeLayananDkType(string TipeLayananDkId, string TipeLayananDkName) : ITipeLayananDkKey
{
    public static TipeLayananDkType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new TipeLayananDkType(id, name);
    }
    public static TipeLayananDkType Default => new("-", "-");
    public static ITipeLayananDkKey Key(string id)
        => Default with {TipeLayananDkId = id};
}