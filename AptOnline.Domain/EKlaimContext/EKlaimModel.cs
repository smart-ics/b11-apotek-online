using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public class EKlaimModel
{
    public string NomorSep { get; set; }
    public string NomorKartu { get; set; }
    public PeriodeRawatType PeriodeRawat { get; init; }
    public CaraMasukType CaraMasuk { get; set; }
    public JenisRawatType JenisRawat { get; set; }
    public KelasRawatInapType KelasRawatInap { get; set; }
    public KelasRawatJalanType KelasRawatJalan { get; set; }
    

    public AdlScoreType AdlSubAcute { get; set; }
    public AdlScoreType AdlChronic { get; set; }
    public YesNoIndikatorType IcuIndikator { get; set; }
    public int IcuLos { get; set; }
    public decimal VentilatorHour { get; set; }
    public VentilatorType Ventilator { get; set; }

    public YesNoIndikatorType UpgradeClassIndikator { get; set; }
    public UpgradeClassType UpgradeClassClass { get; set; }
    public int UpgradeClassLos { get; set; }
    public UpgradeClassPayorType UpgradeClassPayor { get; set; }
    public decimal AddPaymentPercentage { get; set; }

    
    public decimal BirthWeight { get; set; }
    public int Sistole { get; set; }
    public int Diastole { get; set; }
    
    
    public DischartStatusType DischargeStatus { get; set; }
    public TarifRsType TarifRs { get; set; }
    public JenazahType Jenazah { get; set; }
    
    public Covid19StatusCdType Covid19StatusCd { get; set; }
    public NomorKartuTType NomorKartuT { get; set; }
    public EpisodeType Episodes { get; set; }
    

    public AksesNaatType AksesNaat { get; set; }
    public YesNoIndikatorType IsomanIndikator { get; set; }

    public int BayiLahirStatusCode { get; set; }
    
    public YesNoIndikatorType DializerSingleUse { get; set; }
    public int KantongDarah { get; set; }
    public YesNoIndikatorType AlteplaseIndikator { get; set; }
    public decimal TarifPoliEksekutif { get; set; }
    public string DokterName { get; set; }
    public KelasTarifInacbgType KodeTarif { get; set; }
    
    public string PayorId { get; set; }
    public string PayorCode { get; set; }
    public string CobCode { get; set; }
    public string CoderNik { get; set; }

    public ApgarType Apgar { get; private set; }
    public PersalinanType Persalinan { get; private set; }
}

