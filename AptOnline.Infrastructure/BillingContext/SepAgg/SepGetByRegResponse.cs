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
    public string RegId {get;set;}
    public string PasienId {get;set;}
    public string PasienName {get;set;}
    public string DpjpId {get;set;}
    public string DpjpName {get;set;}
    public string IsPrb {get;set;}
    public string Prb {get;set;}
    
    public SepType ToSepType() => 
        new SepType(SepId, DateTime.Parse(SepDateTime), 
            SepNo, PesertaJaminanId, 
            RegType.Load(RegId, DateTime.Parse(SepDateTime), new DateTime(3000, 1, 1), PasienId, PasienName, JenisRegEnum.Unknown), 
            new DokterType(DpjpId, DpjpName), 
            bool.Parse(IsPrb), Prb.Trim());
}