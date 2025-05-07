using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public class EKlaimModel
{
    public EKlaimModel(string eklaimId, DateTime eklaimDate, 
        string nomorSep, string nomorKartu, string coderNik, 
        EKlaimAdmPasienType eKlaimAdmPasien, EKlaimAdmRegType admRegType, DischargeStatusValType dischargeStatusVal, 
        string dokterName, EklaimAdmTarifRsType eklaimAdmTarifRs, decimal tarifPoliEksekutif, 
        KelasTarifInacbgValType kodeTarifInacbg, YesNoIndikatorValType upgradeClassIndikatorVal, 
        decimal addPaymentPercentage, EKlaimAdmPaymentType admPayment, 
        EklaimMedVitalSignType vitalSign, EKlaimMedIcuType icu, 
        EKlaimMedCovidType covid, EKlaimMedJenazahType jenazah, 
        EKlaimMedDarahType darah, BayiLahirStatusCodeValType bayiLahirStatusCodeVal)
    {
        EklaimId = eklaimId;
        EklaimDate = eklaimDate;
        NomorSep = nomorSep;
        NomorKartu = nomorKartu;
        CoderNik = coderNik;
        EKlaimAdmPasien = eKlaimAdmPasien;
        AdmRegType = admRegType;
        DischargeStatusVal = dischargeStatusVal;
        DokterName = dokterName;
        EklaimAdmTarifRs = eklaimAdmTarifRs;
        TarifPoliEksekutif = tarifPoliEksekutif;
        KodeTarifInacbg = kodeTarifInacbg;
        UpgradeClassIndikatorVal = upgradeClassIndikatorVal;
        AddPaymentPercentage = addPaymentPercentage;
        AdmPayment = admPayment;
        VitalSign = vitalSign;
        Icu = icu;
        Covid = covid;
        Jenazah = jenazah;
        Darah = darah;
        BayiLahirStatusCodeVal = bayiLahirStatusCodeVal;
    }

    public string EklaimId { get; init; }
    public DateTime EklaimDate { get; init; }
    
    public string NomorSep { get; private set; }
    public string NomorKartu { get; private set; }
    public string CoderNik { get; private set; }

    //  ADMINISTRATIF
    public EKlaimAdmPasienType EKlaimAdmPasien { get; private set; }
    public EKlaimAdmRegType AdmRegType { get; private set; }
    public DischargeStatusValType DischargeStatusVal { get; private set; }
    public string DokterName { get; private set; }

    //  TARIF
    public EklaimAdmTarifRsType EklaimAdmTarifRs { get; private set; }
    public decimal TarifPoliEksekutif { get; private set; }
    public KelasTarifInacbgValType KodeTarifInacbg { get; private set; }
    public YesNoIndikatorValType UpgradeClassIndikatorVal { get; private set; }
    public decimal AddPaymentPercentage { get; private set; }

    //  PAYMENT
    public EKlaimAdmPaymentType AdmPayment { get; private set; }

    //  MEDICAL CONTENT
    public EklaimMedVitalSignType VitalSign { get; private set; }
    public EKlaimMedIcuType Icu { get; private set; }
    public EKlaimMedCovidType Covid { get; set; }
    public EKlaimMedJenazahType Jenazah { get; private set; }
    public EKlaimMedDarahType Darah { get; set; }
    public BayiLahirStatusCodeValType BayiLahirStatusCodeVal { get; private set; }
}

