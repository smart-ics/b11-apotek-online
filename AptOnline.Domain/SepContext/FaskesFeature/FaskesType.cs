using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.FaskesFeature;

public record FaskesType : IFaskesKey
{
    private FaskesType(string faskesId, string faskesName, string tipeFaskes)
    {
        Guard.Against.NullOrWhiteSpace(faskesId, nameof(faskesId), "Faskes-Code harus terisi");
        Guard.Against.NullOrWhiteSpace(faskesName, nameof(faskesName), "Faskes-Name harus terisi");

        FaskesId = faskesId;
        FaskesName = faskesName;
        TipeFaskes = tipeFaskes;
    }

    public static FaskesType Default => new FaskesType("-", "-", "-");
    public static IFaskesKey Key(string id)
        => Default with { FaskesId = id };
    
    public string FaskesId { get; init; }
    public string FaskesName { get; init; }
    public string TipeFaskes { get; init; }

    public FaskesRefference ToRefference()
        => new FaskesRefference(FaskesId, FaskesName);
}