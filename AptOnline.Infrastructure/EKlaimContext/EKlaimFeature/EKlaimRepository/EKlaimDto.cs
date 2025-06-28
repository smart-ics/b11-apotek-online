using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Domain.EKlaimContext.PayorFeature;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
using AptOnline.Domain.EKlaimContext.UpgradeIndikatorFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimDto
{
    public EKlaimDto()
    {
    }

    public EKlaimDto(EKlaimModel model)
    {
        EKlaimId = model.EKlaimId;
        EKlaimDate = model.EKlaimDate;
        SepId = model.Sep.SepId;
        SepNo = model.Sep.SepNo;
        SepDate = model.Sep.SepDate;
        KartuBpjsNo = model.PesertaBpjs.PesertaBpjsNo;
        
        RegId = model.Reg.RegId;
        RegDate = model.Reg.RegDate;
        PasienId = model.Pasien.PasienId;
        PasienName = model.Pasien.PasienName;
        BirthDate = model.Pasien.BirthDate;
        Gender = model.Pasien.Gender.Value;
        DpjpId = model.Dpjp.DokterId;
        DpjpName = model.Dpjp.DokterName;
        
        CaraMasukId = model.CaraMasuk.CaraMasukId;
        CaraMasukName = model.CaraMasuk.CaraMasukName;
        JenisRawatId = model.JenisRawat.JenisRawatId;
        JenisRawatName = model.JenisRawat.JenisRawatName;
        
        KelasJknId = model.KelasJkn.KelasJknId;
        KelasJknName = model.KelasJkn.KelasJknName;
        KelasJknValue = model.KelasJkn.KelasValue;
        KelasTarifRsId = model.KelasTarifRs.KelasTarifRsId;
        KelasTarifRsName = model.KelasTarifRs.KelasTarifRsName;
        TarifPoliEksekutif = model.TarifPoliEksekutif;
        UpgradeIndikator = model.UpgradeKelasIndikator.UpgradeIndikator;
        AddPaymentProcentage = model.UpgradeKelasIndikator.AddPaymentProsentage;
        
        DischargeStatusId = model.DischargeStatus.DischargeStatusId;
        DischargeStatusName = model.DischargeStatus.DischargeStatusName;
        PayorId = model.Payor.PayorId;
        PayorName = model.Payor.PayorName;
        CoderPegId = model.Coder.PegId;
        CoderPegName = model.Coder.PegName;
        CoderNik = model.Coder.Nik;
        Los = model.LengthOfStay;
        
    }
    public string EKlaimId { get;set;} 
    public DateTime EKlaimDate { get;set;} 
    public string SepId { get; set; }
    public string SepNo { get;set;} 
    public DateTime SepDate { get; set; }
    public string KartuBpjsNo { get;set;} 
    public string RegId { get;set;} 
    public DateTime  RegDate { get;set;} 
    public string PasienId { get;set;} 
    public string PasienName { get;set;} 
    public DateTime BirthDate { get;set;} 
    public string Gender { get;set;} 
    public string DpjpId { get;set;} 
    public string DpjpName { get;set;} 
    public string CaraMasukId { get;set;} 
    public string CaraMasukName { get;set;}
    public string JenisRawatId { get; set; }
    public string JenisRawatName { get; set; }
    public string KelasJknId { get;set;} 
    public string KelasJknName { get;set;} 
    public int KelasJknValue { get;set;} 
    public string KelasTarifRsId { get;set;} 
    public string KelasTarifRsName { get; set; }
    public decimal TarifPoliEksekutif { get;set;} 
    public int UpgradeIndikator { get;set;} 
    public decimal AddPaymentProcentage { get;set;} 
    public string DischargeStatusId { get;set;}  
    public string DischargeStatusName { get;set;} 
    public string PayorId { get;set;} 
    public string PayorName { get;set;} 
    public string CoderPegId { get;set;} 
    public string CoderPegName { get;set;} 
    public string CoderNik { get;set;} 
    public int Los { get;set;}

    public EKlaimModel ToModel()
    {
        var reg = new RegRefference(RegId, RegDate);
        var sep = new SepRefference(SepId, SepNo, SepDate);
        var pasien = PasienType.Create(PasienId, PasienName, BirthDate, GenderType.Create(Gender));
        var pesertaBpjs = new PesertaBpjsRefference(KartuBpjsNo, PasienName);

        var result = new EKlaimModel(EKlaimId, EKlaimDate,reg, sep, pasien, pesertaBpjs);
        
        var dpjp = new DokterType(DpjpId, DpjpName);
        var caraMasuk = new CaraMasukType(CaraMasukId, CaraMasukName);
        var jenisRawat = new JenisRawatType(JenisRawatId, JenisRawatName);
        result.SetAdministrasiMasuk(dpjp, caraMasuk, jenisRawat);
        
        var kelasJkn = new KelasJknType(KelasJknId, KelasJknName, KelasJknValue);
        var kelasTarifRs = new KelasTarifRsType(KelasTarifRsId, KelasTarifRsName);
        var tarifRs = TarifRsModel.Default;
        var upgradeIndikator = new UpgradeKelasIndikatorType(UpgradeIndikator, AddPaymentProcentage);
        var dischargeStatus = new DischargeStatusType(DischargeStatusId, DischargeStatusName);
        var payor = new PayorType(PayorId, PayorName, PayorName);
        var coder = new PegType(
            CoderPegId == "" ? "-" : CoderPegId, 
            CoderPegName == "" ? "-" : CoderPegName, 
            CoderNik);
        result.SetBillPasien(kelasJkn, kelasTarifRs, tarifRs, TarifPoliEksekutif,
            upgradeIndikator, dischargeStatus, payor, coder, Los);
        
        return result;
    }
}