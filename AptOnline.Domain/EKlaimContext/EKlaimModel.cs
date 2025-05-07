using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public class EKlaimModel
{
    private EKlaimModel()
    {

    }

    public string NomorSep { get; private set; }
    public string NomorKartu { get; private set; }
    public string CoderNik { get; set; }

    //  ADMINISTRATIF
    public EKlaimAdmPasienType EKlaimAdmPasien { get; private set; }
    public EKlaimAdmRegType AdmRegType { get; private set; }
    public DischargeStatusValType DischargeStatusVal { get; private set; }
    public string DokterName { get; set; }

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
    public BayiLahirStatusCodeValType BayiLahirStatusCodeVal { get; set; }
}

