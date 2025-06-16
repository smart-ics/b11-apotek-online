using AptOnline.Domain.SepContext.JenisPelayananFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.AssesmentPelayananFeature;

public record AssesmentPelayananType(string AssesmentPelayananId, string AssesmentPelayananName) : IAssesmentPelayananKey
{
    public static AssesmentPelayananType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new AssesmentPelayananType(id, name);
    }

    public static AssesmentPelayananType Default => new("-", "-");
    public static IAssesmentPelayananKey Key(string id) => new AssesmentPelayananType(id, "-");
}