using AptOnline.Domain.Helpers;
using GuardNet;

namespace AptOnline.Domain.BillingContext.DokterAgg;

public record DokterType : IDokterKey
{
    public DokterType(string dokterId, string dokterName)
    {
        Guard.NotNullOrWhitespace(dokterId, nameof(dokterId));
        Guard.NotNullOrWhitespace(dokterName, nameof(dokterName));
        
        DokterId = dokterId;
        DokterName = dokterName;
    }
    public string DokterId { get; } 
    public string DokterName { get; }

    public static DokterType Default => new DokterType(AppConst.DASH, AppConst.DASH);
}