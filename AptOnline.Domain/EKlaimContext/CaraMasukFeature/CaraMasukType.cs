using AptOnline.Domain.BillingContext.TipeLayananDkFeature;
using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Domain.SepContext.SkdpFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.CaraMasukFeature;

public record CaraMasukType(string CaraMasukId, string CaraMasukName) : ICaraMasukKey
{
    public static CaraMasukType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new CaraMasukType(id, name);
    }
    
    public static CaraMasukType Default => new("-", "-");
    public static ICaraMasukKey Key(string id)
        => Default with {CaraMasukId = id};

    public static CaraMasukType Create(SepType sep,
        FaskesType faskerRs,
        TipeLayananDkType tipeLayananDk)
    {
        var jnsPlyn = sep.JenisPelayanan.JenisPelayananId;
        var assPlyn = sep.AssesmentPelayanan.AssesmentPelayananId;
        var tipeFas = sep.FaskesPerujuk.TipeFaskes.TipeFaskesId;
        var skdpId = sep.Skdp.SkdpNo;
        var fasRujuk = sep.FaskesPerujuk.FaskesId;
        var fasRs = faskerRs.FaskesId;
        var tipeLynDk = tipeLayananDk.TipeLayananDkId;
        var result = (jnsPlyn, assPlyn, tipeFas, skdpId, fasRujuk, fasRs, tipeLynDk) switch
        {
            ("2", "", "1", "", _, _, _) => RujukanFktp,
            ("2", "", "2", "", _, _, _) => RujukanFkrtl,
            ("2", _, _, not "", var r, var rs, _) when r == rs && r != "" => DariRawatInap,
            ("2", _, _, not "", _, _, _) => DariRawatJalan,
            ("2", not "", _, "", _, _, _) => RujukanSpesialis,
            ("1", _, _, "", _, _, "2") => DariRawatDarurat,
            ("1", _, _, _, _, _, _) => DariRawatJalan,
            _ => Default
        };
        
        return result;
    }
    
    public static CaraMasukType RujukanFktp => new ("gp", "Rujukan FKTP");
    public static CaraMasukType RujukanFkrtl => new ("hosp-trans", "Rujukan FKRTL");
    public static CaraMasukType DariRawatInap => new ("inp", "Dari Rawat Inap");
    public static CaraMasukType DariRawatJalan => new ("outp", "Dari Rawat Jalan");
    public static CaraMasukType RujukanSpesialis => new ("mp", "Rujukan Spesialis");
    public static CaraMasukType DariRawatDarurat => new ("emd", "Dari Rawat Darurat");
}