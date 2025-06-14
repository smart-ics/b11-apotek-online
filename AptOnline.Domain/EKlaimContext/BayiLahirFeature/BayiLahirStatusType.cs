using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.BayiLahirFeature;

public record BayiLahirStatusType(string BayiLahirStatusId, string BayiLahirStatusName) : IBayiLahirStatusKey
{
    public static BayiLahirStatusType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new BayiLahirStatusType(id, name);
    }
    public static BayiLahirStatusType Default => new("-", "-");
    public static IBayiLahirStatusKey Key(string id)
        => Default with {BayiLahirStatusId = id};
}