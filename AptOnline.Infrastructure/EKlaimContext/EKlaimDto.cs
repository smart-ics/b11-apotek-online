using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.EKlaimContext;

public record EKlaimDto(
    string EKlaimId,
    DateTime EKlaimDate,
    
    string SepId,
    string SepNo,
    string SepDateTime,
    
    string KartuBpjsNo,
    string PasienId,
    string PasienName,
    DateTime BirthDate,
    string Gender)
{
    public EKlaimModel ToModel()
    {
        var sep = new SepRefference(SepId, SepNo, SepDateTime.ToDate(DateFormatEnum.YMD));
        var pasien = PasienType.Load(PasienId, PasienName, BirthDate, GenderType.Create(Gender));
        var pesertaBpjs = new PesertaBpjsRefference(KartuBpjsNo, PasienName);
        var result = EKlaimModel.Load(EKlaimId, EKlaimDate, sep, pasien, pesertaBpjs);
        return result;
    }
};