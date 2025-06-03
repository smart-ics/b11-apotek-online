using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
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

    public SepType ToSepType() =>
        new(SepId, DateTime.Parse(SepDateTime),
            SepNo, PesertaJaminanId,
            new RegType(RegId, DateTime.Parse(SepDateTime), PasienId, PasienName),
            new DokterType(DpjpLayananId.Trim().Length > 0 ? DpjpLayananId : DpjpId,
                DpjpLayananId.Trim().Length > 0 ? DpjpLayananName : DpjpName),
            bool.Parse(IsPrb), Prb.Trim(), JnsPelayananKode);
}