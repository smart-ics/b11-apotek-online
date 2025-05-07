using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public class EKlaimModel
{
    private  EKlaimModel()
    {
        
    }
    public string NomorSep { get; private set; }
    public string NomorKartu { get; private set; }
    public EKlaimPasienType EKlaimPasien { get; private set; }
    public EKlaimRegType RegType { get; private set; }
    public EKlaimIcuType Icu { get; private set; }    


    public YesNoIndikatorValType UpgradeClassIndikatorVal { get; set; }
    public decimal AddPaymentPercentage { get; set; }
    
    public DischargeStatusValType DischargeStatusVal { get; set; }
    
    public TarifRsType TarifRs { get; set; }
    
    public JenazahType Jenazah { get; set; }
    
    public Covid19StatusCodeValType Covid19StatusCodeVal { get; set; }
    public EpisodeValType EpisodesVal { get; set; }
    
    public AksesNaatValType AksesNaatVal { get; set; }
    public YesNoIndikatorValType IsomanIndikatorVal { get; set; }

    public int BayiLahirStatusCode { get; set; }
    
    public YesNoIndikatorValType DializerSingleUse { get; set; }
    public int KantongDarah { get; set; }
    public YesNoIndikatorValType AlteplaseIndikatorVal { get; set; }
    public decimal TarifPoliEksekutif { get; set; }
    public string DokterName { get; set; }
    public KelasTarifInacbgValType KodeTarif { get; set; }
    
    public string PayorId { get; set; }
    public string PayorCode { get; set; }
    public string CobCode { get; set; }
    public string CoderNik { get; set; }
}

