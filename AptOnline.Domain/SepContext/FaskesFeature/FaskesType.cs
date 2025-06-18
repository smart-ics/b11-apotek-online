using AptOnline.Domain.SepContext.TipeFaskesFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.FaskesFeature;

public record FaskesType : IFaskesKey
{
    public FaskesType(string faskesId, string faskesName, TipeFaskesType tipeFaskes)
    {
        Guard.Against.NullOrWhiteSpace(faskesId, nameof(faskesId), "Faskes-Code harus terisi");
        Guard.Against.NullOrWhiteSpace(faskesName, nameof(faskesName), "Faskes-Name harus terisi");
        Guard.Against.Null(tipeFaskes, nameof(tipeFaskes), "Tipe Faskes harus terisi");

        FaskesId = faskesId;
        FaskesName = faskesName;
        TipeFaskes = tipeFaskes;
    }

    public static FaskesType Default => new FaskesType("-", "-", TipeFaskesType.Default);
    public static IFaskesKey Key(string id)
        => Default with { FaskesId = id };
    
    public string FaskesId { get; init; }
    public string FaskesName { get; init; }
    public TipeFaskesType TipeFaskes { get; init; }

    public FaskesRefference ToRefference()
        => new FaskesRefference(FaskesId, FaskesName);
}