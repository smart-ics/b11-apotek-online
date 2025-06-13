using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Nuna.Lib.ValidationHelper;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace AptOnline.Infrastructure.EKlaimContext;


public class EKlaimRepoDto
{
    public string EKlaimId { get; set; }
    public DateTime EKlaimDate { get; set; }
    
    public string SepId { get; set; }
    public string SepNo { get; set; }
    public string SepDateTime { get; set; }
    
    public string RegId { get; set; }
    public DateTime RegDate { get; set; }
    
    public string KartuBpjsNo { get; set; }
    public string PasienId { get; set; }
    public string PasienName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    
    public EKlaimModel ToModel()
    {
        var sep = new SepRefference(SepId, SepNo, SepDateTime.ToDate(DateFormatEnum.YMD));
        var pasien = PasienType.Load(PasienId, PasienName, BirthDate, GenderType.Create(Gender));
        var pesertaBpjs = new PesertaBpjsRefference(KartuBpjsNo, PasienName);
        var reg = new RegRefference(RegId, RegDate);
        var result = EKlaimModel.Load(EKlaimId, EKlaimDate, reg, sep, pasien, pesertaBpjs);
        return result;
    }
};