using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext;
using Farpu.Domain.Helpers;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public class EklaimModel : IEklaimKey
{
    #region CONSTRUCTORS
    private EklaimModel(
        //
        string eklaimId, DateTime eklaimDate, string nomorSep, string nomorKartu, 
        string coderNik, PasienType pasien,
        //
        NomorKartuTValType nomorKartuT, EKlaimAdmRawatType admRawatType, 
        DischargeStatusType dischargeStatus, string dokterName, 
        //
        EklaimAdmTarifRsType eklaimAdmTarifRs, decimal tarifPoliEksekutif,  
        KelasTarifInacbgValType kodeTarifInacbg, YesNoIndikatorValType upgradeClassIndikator, 
        decimal addPaymentPercentage, EKlaimAdmPaymentType admPayment, 
        //
        EklaimMedVitalSignType vitalSign, EKlaimMedIcuType icu, 
        EKlaimMedCovidType covid, EKlaimMedJenazahType jenazah, 
        EKlaimMedDarahType darah, BayiLahirStatusCodeType bayiLahirStatusCode)
    {
        Guard.Against.NullOrWhiteSpace(eklaimId, nameof(eklaimId), "EKlaim-ID harus terisi");
        Guard.Against.NullOrWhiteSpace(nomorSep, nameof(nomorSep), "Nomor-SEP harus terisi");
        Guard.Against.NullOrWhiteSpace(coderNik, nameof(coderNik), "Coder-Nik harus terisi");
        Guard.Against.NullOrWhiteSpace(dokterName, nameof(dokterName), "Dokter-Name harus terisi");

        Guard.Against.Null(nomorKartuT, nameof(nomorKartuT));
        Guard.Against.Null(admRawatType, nameof(admRawatType));
        Guard.Against.Null(dischargeStatus, nameof(dischargeStatus));

        Guard.Against.Null(eklaimAdmTarifRs, nameof(eklaimAdmTarifRs));
        Guard.Against.Null(kodeTarifInacbg, nameof(kodeTarifInacbg));
        Guard.Against.Null(upgradeClassIndikator, nameof(upgradeClassIndikator));
        Guard.Against.Null(admPayment, nameof(admPayment));

        Guard.Against.Null(vitalSign, nameof(vitalSign));
        Guard.Against.Null(icu, nameof(icu));
        Guard.Against.Null(covid, nameof(covid));
        Guard.Against.Null(jenazah, nameof(jenazah));
        Guard.Against.Null(darah, nameof(darah));
        Guard.Against.Null(bayiLahirStatusCode, nameof(bayiLahirStatusCode));

        //
        EklaimId = eklaimId;
        EklaimDate = eklaimDate;
        NomorSep = nomorSep;
        NomorKartu = nomorKartu;
        CoderNik = coderNik;
        Pasien = pasien;
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

    public static EklaimModel Create(DateTime eklaimDate, SepModel sep, PasienType pasien)
    {
        //Guard.NotNull(sep, nameof(sep), "Sep tidak boleh kosong");
        //Guard.NotNull(pasien, nameof(pasien), "Pasien tidak boleh kosong");

        //var eklaimId = UlidHelper.NewUlid();
        //var eklaimAdmPasien = PasienType.Create(pasien.PasienId, pasien.PasienName, pasien.BirthDate, pasien.Gender);
        //var result = new EklaimModel(
        //    eklaimId, eklaimDate, sep.Sep.SepNo, 
        //    sep.Peserta.NomorKartu, "-", eklaimAdmPasien,

        //    NomorKartuTValType.Default, EKlaimAdmRawatType.Default,
        //    DischargeStatusValType.Default, string.Empty,
        //    EklaimAdmTarifRsType.Default, 0, KelasTarifInacbgValType.Default,
        //    YesNoIndikatorValType.Default, 0, EKlaimAdmPaymentType.Default,
        //    EklaimMedVitalSignType.Default, EKlaimMedIcuType.Default,
        //    EKlaimMedCovidType.Default, EKlaimMedJenazahType.Default,
        //    EKlaimMedDarahType.Default, BayiLahirStatusCodeValType.Default);
        //return result;
        throw new NotImplementedException();
    }
    
    public static EklaimModel Default
        => new EklaimModel(
            UlidHelper.NewUlid(), DateTime.Now, "-", "-", "-", PasienType.Default,
            NomorKartuTValType.Default, EKlaimAdmRawatType.Default,
            DischargeStatusType.Default, "-",
            EklaimAdmTarifRsType.Default, 0, KelasTarifInacbgValType.Default,
            YesNoIndikatorValType.Default, 0, EKlaimAdmPaymentType.Default,
            EklaimMedVitalSignType.Default, EKlaimMedIcuType.Default,
            EKlaimMedCovidType.Default, EKlaimMedJenazahType.Default,
            EKlaimMedDarahType.Default, BayiLahirStatusCodeType.Default);

    public static IEklaimKey Key(string id)
    {
        var result = Default;
        result.EklaimId = id;
        return result;
    } 
    #endregion

    #region PROPERTIES
    //  IDENTITY
    public string EklaimId { get; private set; }
    public DateTime EklaimDate { get; init; }
    public string NomorSep { get; private set; }
    public string NomorKartu { get; private set; }
    public string CoderNik { get; private set; }
    public PasienType Pasien { get; private set; }

    //  ADMINISTRATIF
    public NomorKartuTValType NomorKartuT { get; private set; }
    public EKlaimAdmRawatType AdmRawatType { get; private set; }
    public DischargeStatusType DischargeStatus { get; private set; }
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
    public BayiLahirStatusCodeType BayiLahirStatusCode { get; private set; }
    #endregion

    #region METHODS

    public void SetAdministratif(NomorKartuTValType nomorKartuT, EKlaimAdmRawatType admRawatType,
        DischargeStatusType dischargeStatus, string dokterName)
    {
    }

    #endregion
}

