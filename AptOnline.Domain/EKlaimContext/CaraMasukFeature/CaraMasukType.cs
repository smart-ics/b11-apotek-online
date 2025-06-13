using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.CaraMasukFeature;

public record CaraMasukType(string CaraMasukId, string CaraMasukName) : ICaraMasukKey
{
    public static CaraMasukType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new CaraMasukType(id, name);
    }
    public static CaraMasukType Default => new("-", "-");
    public static ICaraMasukKey Key(string id)
        => Default with {CaraMasukId = id};
}