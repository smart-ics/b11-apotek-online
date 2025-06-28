using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SkdpFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.SepFeature;

public record SepType : ISepKey
{
    public SepType(string sepId, DateTime sepDateTime, string sepNo, 
        PesertaBpjsRefference pesertaBpjs, KelasJknType kelasHak, FaskesType faskesPerujuk,
        JenisPelayananType jenisPelayanan, AssesmentPelayananType assesmentPelayanan,
        SkdpRefference skdp, RegType reg, DokterType dpjp, DokterType dpjpLayanan, 
        bool isPrb, string prb)
    {
        Guard.Against.NullOrWhiteSpace(sepId, nameof(sepId));
        Guard.Against.Null(pesertaBpjs, nameof(pesertaBpjs));
        Guard.Against.Null(reg, nameof(reg));
        Guard.Against.Null(dpjp, nameof(dpjp));
        
        Guard.Against.Null(dpjpLayanan, nameof(dpjpLayanan));
        Guard.Against.Null(faskesPerujuk, nameof(faskesPerujuk));
        Guard.Against.Null(jenisPelayanan, nameof(jenisPelayanan));
        Guard.Against.Null(assesmentPelayanan, nameof(assesmentPelayanan));
        Guard.Against.Null(kelasHak, nameof(kelasHak));

        Guard.Against.Null(skdp, nameof(skdp));
        
        SepId = sepId;
        SepDateTime = sepDateTime;
        SepNo = sepNo;
        
        PesertaBpjs = pesertaBpjs;
        KelasHak = kelasHak;
        FaskesPerujuk = faskesPerujuk;
        JenisPelayanan = jenisPelayanan;
        AssesmentPelayanan = assesmentPelayanan;
        Skdp = skdp;

        Reg = reg;
        Dpjp = dpjp;
        DpjpLayanan = dpjpLayanan;
        IsPrb = isPrb;
        Prb = prb;
    }

    public string SepId { get; private set; }
    public DateTime SepDateTime { get; private set; }
    public string SepNo { get; private set; }
    
    public PesertaBpjsRefference PesertaBpjs { get; private set; }
    public KelasJknType KelasHak { get; private set; }
    public FaskesType FaskesPerujuk { get; private set; }
    public JenisPelayananType JenisPelayanan { get; private set; }
    public AssesmentPelayananType AssesmentPelayanan { get; private set; }
    public SkdpRefference Skdp { get; private set; }
    public RegType Reg { get; private set; }
    public DokterType Dpjp { get; private set; }
    public DokterType DpjpLayanan { get; private set; }
    
    public bool IsPrb { get; private set; }
    public string Prb { get; private set; }
    
    public static SepType Default 
        => new SepType(AppConst.DASH, AppConst.DEF_DATE, AppConst.DASH, 
        PesertaBpjsType.Default.ToRefference(), KelasJknType.Default, FaskesType.Default, 
        JenisPelayananType.Default, AssesmentPelayananType.Default, 
        SkdpRefference.Default, RegType.Default, DokterType.Default, 
        DokterType.Default, false, AppConst.DASH);
    
    public static ISepKey Key(string sepId) 
        => SepType.Default with { SepId = sepId }; 

    public SepRefference ToRefference()
        => new SepRefference(SepId, SepNo, SepDateTime);
}