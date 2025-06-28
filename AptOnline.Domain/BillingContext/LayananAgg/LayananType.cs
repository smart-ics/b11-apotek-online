using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.BillingContext.TipeLayananDkFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.LayananAgg;

public record LayananType : ILayananKey
{
    public LayananType(string id, string name, bool isActive,
        PoliBpjsType poliBpjs, TipeLayananDkType tipeLayananDk)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.Null(poliBpjs, nameof(poliBpjs));
        Guard.Against.Null(tipeLayananDk, nameof(tipeLayananDk));
        
        LayananId = id;
        LayananName = name;
        IsActive = isActive;

        PoliBpjs = poliBpjs ?? throw new ArgumentNullException(nameof(poliBpjs));
        TipeLayananDk = tipeLayananDk;
    }
    public string LayananId { get; private set; }
    public string LayananName { get; private set; }
    public PoliBpjsType PoliBpjs { get; private set; }
    public bool IsActive { get; private set; }
    public TipeLayananDkType TipeLayananDk { get; private set; }

    public static LayananType Default => new LayananType("-", "-", false,
        PoliBpjsType.Default, TipeLayananDkType.Default);
    public LayananRefference ToRefference() => new LayananRefference(LayananId, LayananName);
}

