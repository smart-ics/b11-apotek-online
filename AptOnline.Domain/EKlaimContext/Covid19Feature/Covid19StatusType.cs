using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.Covid19Feature;

public record Covid19StatusType(string Covid19StatusId, string Covid19StatusName) : ICovid19StatusKey
{
    public static Covid19StatusType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new Covid19StatusType(id, name);
    }
    public static Covid19StatusType Default => new("-", "-");
    public static ICovid19StatusKey Key(string id)
        => Default with {Covid19StatusId = id};
}