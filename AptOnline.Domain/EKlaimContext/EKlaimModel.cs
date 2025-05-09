using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using Farpu.Domain.Helpers;
using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public class EKlaimModel
{
    #region CONSTRUCTORS
    private EKlaimModel(
        //
        string eklaimId, DateTime eklaimDate, string nomorSep, string nomorKartu, 
        string coderNik,  EKlaimAdmPasienType eKlaimAdmPasien,
        //
        NomorKartuTValType nomorKartuT, EKlaimAdmRawatType admRawatType, 
        DischargeStatusValType dischargeStatus, string dokterName, 
        //
        EklaimAdmTarifRsType eklaimAdmTarifRs, decimal tarifPoliEksekutif,  
        KelasTarifInacbgValType kodeTarifInacbg, YesNoIndikatorValType upgradeClassIndikator, 
        decimal addPaymentPercentage, EKlaimAdmPaymentType admPayment, 
        //
        EklaimMedVitalSignType vitalSign, EKlaimMedIcuType icu, 
        EKlaimMedCovidType covid, EKlaimMedJenazahType jenazah, 
        EKlaimMedDarahType darah, BayiLahirStatusCodeValType bayiLahirStatusCode)
    {
        Guard.NotNullOrWhitespace(eklaimId, nameof(eklaimId), "EKlaim-ID harus terisi");
        Guard.NotNullOrWhitespace(nomorSep, nameof(nomorSep), "Nomor-SEP harus terisi");
        Guard.NotNullOrWhitespace(coderNik, nameof(coderNik), "Coder-Nik harus terisi");
        Guard.NotNullOrWhitespace(dokterName, nameof(dokterName), "Dokter-Name harus terisi");

        Guard.NotNull(nomorKartuT, nameof(nomorKartuT));
        Guard.NotNull(admRawatType, nameof(admRawatType));
        Guard.NotNull(dischargeStatus, nameof(dischargeStatus));

        Guard.NotNull(eklaimAdmTarifRs, nameof(eklaimAdmTarifRs));
        Guard.NotNull(kodeTarifInacbg, nameof(kodeTarifInacbg));
        Guard.NotNull(upgradeClassIndikator, nameof(upgradeClassIndikator));
        Guard.NotNull(admPayment, nameof(admPayment));

        Guard.NotNull(vitalSign, nameof(vitalSign));
        Guard.NotNull(icu, nameof(icu));
        Guard.NotNull(covid, nameof(covid));
        Guard.NotNull(jenazah, nameof(jenazah));
        Guard.NotNull(darah, nameof(darah));
        Guard.NotNull(bayiLahirStatusCode, nameof(bayiLahirStatusCode));

        //
        EklaimId = eklaimId;
        EklaimDate = eklaimDate;
        NomorSep = nomorSep;
        NomorKartu = nomorKartu;
        CoderNik = coderNik;
        EKlaimAdmPasien = eKlaimAdmPasien;
        //
        NomorKartuT = nomorKartuT;
        AdmRawatType = admRawatType;
        DischargeStatus = dischargeStatus;
        DokterName = dokterName;
        //
        EklaimAdmTarifRs = eklaimAdmTarifRs;
        TarifPoliEksekutif = tarifPoliEksekutif;
        KelasTarifInacbg = kodeTarifInacbg;
        UpgradeClassIndikator = upgradeClassIndikator;
        AddPaymentPercentage = addPaymentPercentage;
        AdmPayment = admPayment;
        //
        VitalSign = vitalSign;
        Icu = icu;
        Covid = covid;
        Jenazah = jenazah;
        Darah = darah;
        BayiLahirStatusCode = bayiLahirStatusCode;
    }

    public static EKlaimModel Create(DateTime eklaimDate)
    {
        
        var eklaimId = UlidHelper.NewUlid();
        var result = new EKlaimModel(
            eklaimId, eklaimDate, "-", "-", "-", EKlaimAdmPasienType.Default,
            NomorKartuTValType.Default, EKlaimAdmRawatType.Default,
            DischargeStatusValType.Default, string.Empty,

            EklaimAdmTarifRsType.Default, 0, KelasTarifInacbgValType.Default,
            YesNoIndikatorValType.Default, 0, EKlaimAdmPaymentType.Default,

            EklaimMedVitalSignType.Default, EKlaimMedIcuType.Default,
            EKlaimMedCovidType.Default, EKlaimMedJenazahType.Default,
            EKlaimMedDarahType.Default, BayiLahirStatusCodeValType.Default);
        return result;
    }
    
    public static EKlaimModel Default
        => new EKlaimModel(
            UlidHelper.NewUlid(), DateTime.Now, "-", "-", "-", EKlaimAdmPasienType.Default,
            NomorKartuTValType.Default, EKlaimAdmRawatType.Default,
            DischargeStatusValType.Default, "-",
            EklaimAdmTarifRsType.Default, 0, KelasTarifInacbgValType.Default,
            YesNoIndikatorValType.Default, 0, EKlaimAdmPaymentType.Default,
            EklaimMedVitalSignType.Default, EKlaimMedIcuType.Default,
            EKlaimMedCovidType.Default, EKlaimMedJenazahType.Default,
            EKlaimMedDarahType.Default, BayiLahirStatusCodeValType.Default);
    #endregion

    #region PROPERTIES
    //  IDENTITY
    public string EklaimId { get; init; }
    public DateTime EklaimDate { get; init; }
    public string NomorSep { get; private set; }
    public string NomorKartu { get; private set; }
    public string CoderNik { get; private set; }
    public EKlaimAdmPasienType EKlaimAdmPasien { get; private set; }

    //  ADMINISTRATIF
    public NomorKartuTValType NomorKartuT { get; private set; }
    public EKlaimAdmRawatType AdmRawatType { get; private set; }
    public DischargeStatusValType DischargeStatus { get; private set; }
    public string DokterName { get; private set; }

    //  TARIF-PAYMENT
    public EklaimAdmTarifRsType EklaimAdmTarifRs { get; private set; }
    public decimal TarifPoliEksekutif { get; private set; }
    public KelasTarifInacbgValType KelasTarifInacbg { get; private set; }
    public YesNoIndikatorValType UpgradeClassIndikator { get; private set; }
    public decimal AddPaymentPercentage { get; private set; }
    public EKlaimAdmPaymentType AdmPayment { get; private set; }

    //  MEDICAL
    public EklaimMedVitalSignType VitalSign { get; private set; }
    public EKlaimMedIcuType Icu { get; private set; }
    public EKlaimMedCovidType Covid { get; set; }
    public EKlaimMedJenazahType Jenazah { get; private set; }
    public EKlaimMedDarahType Darah { get; private set; }
    public BayiLahirStatusCodeValType BayiLahirStatusCode { get; private set; }
    #endregion

    #region METHODS
    public void MapFromSep(SepType sep)
    {
        Guard.NotNull(sep, nameof(sep), "Sep tidak boleh kosong");
        
        NomorSep = sep.SepId;
        NomorKartu = sep.NoPeserta;
        DokterName = sep.Dpjp.DokterName;
    }

    public void MapFromPasien(PasienType pasien)
    {
        Guard.NotNull(pasien, nameof(pasien), "Pasien tidak boleh kosong");

        var eklaimPasien = EKlaimAdmPasienType.Create(pasien.PasienId, pasien.PasienName,
            pasien.BirthDate, pasien.Gender);
        EKlaimAdmPasien = eklaimPasien;
    }

    public void MapFromReg(RegType reg)
    {
        Guard.NotNull(reg, nameof(reg), "Reg tidak boleh kosong");

        //  TODO: set 'cara-masuk'
        //      terpending, bikin fitur get SEP dulu
        //var jenisRawat = reg.JenisReg switch
        //{
        //    JenisRegEnum.RawatJalan => throw new NotImplementedException(),
        //    JenisRegEnum.RawatInap => throw new NotImplementedException(),
        //    JenisRegEnum.External => throw new NotImplementedException(),
        //    JenisRegEnum.RawatDarurat => throw new NotImplementedException(),
        //    JenisRegEnum.Meninggal => throw new NotImplementedException(),
        //    JenisRegEnum.ExternalInap => throw new NotImplementedException(),
        //    JenisRegEnum.Unknown => throw new NotImplementedException(),
        //}

        //var admRawat = EKlaimAdmRawatType.Create(
        //    PeriodeRawatType.Load(reg.RegDate, ),
        //    CaraMasukValType.Default),
        //    JenisRawatValType.Create(reg.JenisRawat),
        //    KelasRawatValType.Create(reg.KelasRawat));

    }
    #endregion

}

