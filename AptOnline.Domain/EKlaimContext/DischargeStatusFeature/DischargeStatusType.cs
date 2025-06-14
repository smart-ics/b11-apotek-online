using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.DischargeStatusFeature;

public record DischargeStatusType(string DischargeStatusId, string DischargeStatusName) : IDischargeStatusKey
{
    public static DischargeStatusType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new DischargeStatusType(id, name);
    }
    public static DischargeStatusType Default => new("-", "-");
    public static IDischargeStatusKey Key(string id)
        => Default with {DischargeStatusId = id};
}