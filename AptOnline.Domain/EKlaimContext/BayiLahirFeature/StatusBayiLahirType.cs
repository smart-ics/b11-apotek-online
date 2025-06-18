using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.BayiLahirFeature;

public record StatusBayiLahirType(string StatusBayiLahirId, string StatusBayiLahirName) : IStatusBayiLahirKey
{
    public static StatusBayiLahirType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new StatusBayiLahirType(id, name);
    }
    public static StatusBayiLahirType Default => new("-", "-");
    public static IStatusBayiLahirKey Key(string id)
        => Default with {StatusBayiLahirId = id};
    
    public static StatusBayiLahirType TanpaKelainan => new("1", "Tanpa Kelainan");
    public static StatusBayiLahirType DenganKelainan => new("2", "Dengan Kelainan");
    public static IEnumerable<StatusBayiLahirType> ListData() => new[] {TanpaKelainan, DenganKelainan};
}