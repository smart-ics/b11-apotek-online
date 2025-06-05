using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.SepFeature;

public record SepType : ISepKey
{
    public SepType(string sepId, DateTime sepDateTime, string sepNo, 
        PesertaBpjsType pesertaBpjs, RegType reg, DokterType dpjp, 
        bool isPrb, string prb, string jnsPelayananId)
    {
        Guard.Against.NullOrWhiteSpace(sepId, nameof(sepId));
        Guard.Against.Null(pesertaBpjs, nameof(pesertaBpjs));
        Guard.Against.Null(reg, nameof(reg));
        Guard.Against.Null(dpjp, nameof(dpjp));
        
        SepId = sepId;
        SepDateTime = sepDateTime;
        SepNo = sepNo;
        PesertaBpjs = pesertaBpjs;
        Reg = reg;
        Dpjp = dpjp;
        IsPrb = isPrb;
        Prb = prb;
        JenisPelayananId = jnsPelayananId;
    }

    public string SepId { get; private set; }
    public DateTime SepDateTime { get; private set; }
    public string SepNo { get; private set; }
    public PesertaBpjsType PesertaBpjs { get; private set; }

    public string JenisPelayananId { get; set; }
    
    public RegType Reg { get; private set; }
    public DokterType Dpjp { get; private set; }
    
    public bool IsPrb { get; private set; }
    public string Prb { get; private set; }
    
    public static SepType Default => new SepType(
        AppConst.DASH, AppConst.DEF_DATE, AppConst.DASH, 
        PesertaBpjsType.Default, RegType.Default, DokterType.Default, false, 
        AppConst.DASH, AppConst.DASH);
}