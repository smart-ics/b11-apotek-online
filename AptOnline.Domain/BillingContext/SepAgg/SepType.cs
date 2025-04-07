using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.Helpers;
using GuardNet;

namespace AptOnline.Domain.BillingContext.SepAgg;

public record SepType : ISepKey
{
    public SepType(string sepId, DateTime sepDateTime, string sepNo, 
        string pesertaJaminanId, RegType reg, DokterType dpjp, 
        bool isPrb, string prb)
    {
        Guard.NotNullOrWhitespace(sepId, nameof(sepId));
        Guard.NotNullOrWhitespace(pesertaJaminanId, nameof(pesertaJaminanId));
        Guard.NotNull(reg, nameof(reg));
        Guard.NotNull(dpjp, nameof(dpjp));
        
        SepId = sepId;
        SepDateTime = sepDateTime;
        SepNo = sepNo;
        NoPeserta = pesertaJaminanId;
        Reg = reg;
        Dpjp = dpjp;
        IsPrb = isPrb;
        Prb = prb;
    }

    public string SepId { get; private set; }
    public DateTime SepDateTime { get; private set; }
    public string SepNo { get; private set; }
    public string NoPeserta { get; private set; }
    
    public RegType Reg { get; private set; }
    public DokterType Dpjp { get; private set; }
    public bool IsPrb { get; private set; }
    public string Prb { get; private set; }
    
    public static SepType Default => new SepType(
        AppConst.DASH, AppConst.DEF_DATE, AppConst.DASH, 
        AppConst.DASH, RegType.Default, DokterType.Default, false, 
        AppConst.DASH);
}