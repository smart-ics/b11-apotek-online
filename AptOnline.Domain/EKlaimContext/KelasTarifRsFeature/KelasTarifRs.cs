using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;

public record KelasTarifRsType(string KelasTarifRsId, string KelasTarifRsName) : IKelasTarifRsKey
{
    public static KelasTarifRsType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new KelasTarifRsType(id, name);
    }
    public static KelasTarifRsType Default => new("-", "-");
    public static IKelasTarifRsKey Key(string id)
        => Default with {KelasTarifRsId = id};
}