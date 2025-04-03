using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.Helpers;
using GuardNet;

namespace AptOnline.Domain.BillingContext.RegAgg;

public record RegType : IRegKey
{
    public RegType(string regId, DateTime regDate, string pasienId, 
        string pasienName)
    {
        Guard.NotNullOrWhitespace(regId, nameof(regId));
        Guard.NotNullOrWhitespace(pasienId, nameof(pasienId));
        Guard.NotNullOrWhitespace(pasienName, nameof(pasienName));
        
        RegId = regId;
        RegDate = regDate;
        PasienId = pasienId;
        PasienName = pasienName;
    }

    public string RegId { get; }
    public DateTime RegDate { get; }
    public string PasienId { get; }
    public string PasienName { get; }

    public static RegType Default => new RegType(
        AppConst.DASH, AppConst.DEF_DATE,
        AppConst.DASH, AppConst.DASH);
}