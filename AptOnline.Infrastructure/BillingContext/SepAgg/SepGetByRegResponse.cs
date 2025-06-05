using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.ReferensiFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Nuna.Lib.DataTypeExtension;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.BillingContext.SepAgg;

public class SepGetByRegResponse
{
    public string status { get; set; }
    public string code { get; set; }
    public SepGetByRegResponseData data { get; set; }
}

public class SepGetByRegResponseData
{
    public string SepId { get; set; }
    public string SepDateTime {get;set;}
    public string SepNo {get;set;}
    public string PesertaJaminanId {get;set;}
    public string JnsPelayananKode { get; set; }
    public string RegId {get;set;}
    public string PasienId {get;set;}
    public string PasienName { get; set; }
    public string DpjpId { get; set; }
    public string DpjpName {get;set;}
    public string DpjpLayananId {get;set;}
    public string DpjpLayananName {get;set;}
    public string IsPrb {get;set;}
    public string Prb {get;set;}

    public SepType ToSepType()
    {
        var pasien = PasienType.Create(PasienId, PasienName, new DateTime(3000,1,1), GenderType.Default);
        var pesertaBpjs = PesertaBpjsType.Default with { NomorPeserta = PesertaJaminanId };
        var result = new SepType(SepId, DateTime.Parse(SepDateTime),
            SepNo, pesertaBpjs,
            RegType.Create(RegId, DateTime.Parse(SepDateTime), DateTime.Parse(SepDateTime), 
                pasien, JenisRegEnum.RawatJalan, KelasRawatType.Default),
            new DokterType(DpjpLayananId.Trim().Length > 0 ? DpjpLayananId : DpjpId,
                DpjpLayananId.Trim().Length > 0 ? DpjpLayananName : DpjpName),
            bool.Parse(IsPrb), Prb.Trim(), JnsPelayananKode);
        return result;
    }
}