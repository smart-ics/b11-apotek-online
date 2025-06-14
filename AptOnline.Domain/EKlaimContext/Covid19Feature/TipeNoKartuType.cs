using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.Covid19Feature;

public record TipeNoKartuType(string TipeNoKartuId, string TipeNoKartuName) : ITipeNoKartuKey
{
    public static TipeNoKartuType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new TipeNoKartuType(id, name);
    }
    public static TipeNoKartuType Default => new("-", "-");
    public static ITipeNoKartuKey Key(string id)
        => Default with {TipeNoKartuId = id};
}