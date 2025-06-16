using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.PegFeature;

public record PegType(string PegId, string PegName, string Nik) : IPegKey
{
    public static PegType Create(string name, string nik)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(nik, nameof(nik));
        
        var id = Ulid.NewUlid().ToString();
        return new PegType(id, name, nik);
    }
    public static PegType Default => new("-", "-", "-");
    public static IPegKey Key(string id)
        => Default with {PegId = id};
}