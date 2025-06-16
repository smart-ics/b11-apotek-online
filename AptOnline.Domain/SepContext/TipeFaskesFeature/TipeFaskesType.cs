using AptOnline.Domain.SepContext.KelasRawatFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.TipeFaskesFeature;

public record TipeFaskesType(string TipeFaskesId, string TipeFaskesName) : ITipeFaskesKey
{
    public static TipeFaskesType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new TipeFaskesType(id, name);
    }

    public static TipeFaskesType Default => new("-", "-");
    public static ITipeFaskesKey Key(string id) => new TipeFaskesType(id, "-");
}