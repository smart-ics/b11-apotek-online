using AptOnline.Domain.Helpers;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.DokterAgg;

public record DokterType : IDokterKey
{
    public DokterType(string dokterId, string dokterName)
    {
        Guard.Against.NullOrWhiteSpace(dokterId, nameof(dokterId));
        Guard.Against.NullOrWhiteSpace(dokterName, nameof(dokterName));
        
        DokterId = dokterId;
        DokterName = dokterName;
    }
    public string DokterId { get; } 
    public string DokterName { get; }

    public static DokterType Default => new DokterType(AppConst.DASH, AppConst.DASH);
}