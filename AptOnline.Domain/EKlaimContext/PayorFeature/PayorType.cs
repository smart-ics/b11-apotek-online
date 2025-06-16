using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.PayorFeature;

public record PayorType(string PayorId, string PayorName, string Code) : IPayorKey
{
    public static PayorType Create(string id, string name, string code)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(code, nameof(code));
        return new PayorType(id, name, code);
    }
    public static PayorType Default => new("-", "-", "_");
    public static IPayorKey Key(string id)
        => Default with {PayorId = id};
}