using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Domain.SepContext.SkdpFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;

namespace AptOnline.Infrastructure.SepContext.SepFeature;

public class SepDto
{
    public string SepId { get; set; }
    public string SepDateTime {get;set;}
    public string SepNo {get;set;}
    public string PesertaJaminanId {get;set;}
    
    public string FaskesPerujukId { get; set; }
    public string FaskesPerujukName { get; set; }
    public string TipeFaskesPerujukId { get; set; }
    public string TipeFaskesPerujukName { get; set; }

    public string JenisPelayananId { get; set; }
    public string JenisPelayananName { get; set; }

    public string AssesmentPelayananId { get; set; }
    public string AssesmentPelayananName { get; set; }
    public string SkdpNo { get; set; }
    
    public string RegId {get;set;}
    public string PasienId {get;set;}
    public string PasienName { get; set; }
    public string DpjpId { get; set; }
    public string DpjpName {get;set;}
    public string DpjpLayananId {get;set;}
    public string DpjpLayananName {get;set;}
    public string IsPrb {get;set;}
    public string Prb {get;set;}

    public string KelasPesertaId { get; set; }
    public string KelasPesertaName { get; set; }

    public SepType ToSepType()
    {
        var pasien = PasienType.Create(PasienId, PasienName, new DateTime(3000,1,1), GenderType.Default);
        var pesertaBpjs = PesertaBpjsType.Default with { PesertaBpjsId = PesertaJaminanId };
        var tipeFaskesPerujuk = TipeFaskesType.ListData()
            .FirstOrDefault(x => x.TipeFaskesId == TipeFaskesPerujukId)
            ?? TipeFaskesType.Default;
        var faskesPerujuk = new FaskesType(FaskesPerujukId, FaskesPerujukName, tipeFaskesPerujuk);
        var jenisPelayanan = JenisPelayananType.ListData()
            .FirstOrDefault(x => x.JenisPelayananId == JenisPelayananId)
            ?? JenisPelayananType.Default;
        var assesmentPelayanan = new AssesmentPelayananType(AssesmentPelayananId, AssesmentPelayananName);
        var skdp = new SkdpRefference(SkdpNo);
        var kelasHak = KelasJknType.Get(KelasPesertaId);

        var result = new SepType(SepId, DateTime.Parse(SepDateTime), SepNo, 
            pesertaBpjs.ToRefference(), kelasHak, 
            faskesPerujuk, jenisPelayanan, assesmentPelayanan, skdp,
            new RegType(RegId, DateTime.Parse(SepDateTime), new DateTime(3000,1,1), 
                pasien, JenisRegEnum.RawatJalan, KelasJknType.Default, LayananType.Default.ToRefference()),
            new DokterType(DpjpId, DpjpName),
            new DokterType(DpjpLayananId, DpjpLayananName),
            bool.Parse(IsPrb), Prb.Trim());
        return result;
    }
}