namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimMedisDto
{
    public string EKlaimId { get; set; }
    public int AdlSubAcuteScore { get; set; }
    public int AdlChronicScore { get; set; }
    public int IcuFlag { get; set; }
    public int IcuLos { get; set; }
    public string IcuDescription { get; set; }
    public string Covid19StatusId { get; set; }
    public string Covid19StatusName { get; set; }
    public string Covid19TipeNoKartuId { get; set; }
    public string Covid19TipeNoKartuName { get; set; }
    public bool IsPemulasaranJenazah { get; set; }
    public bool IsKantongJenazah { get; set; }
    public bool IsPetiJenazah { get; set; }
    public bool IsPlastikErat { get; set; }
    public bool IsDesinfeksiJenazah { get; set; }
    public bool IsMobilJenazah { get; set; }
    public bool IsDesinfektanMobilJenazah { get; set; }
    public bool IsIsoman { get; set; }
    public string Episodes { get; set; }
    public string AksesNaat { get; set; }
    public string DializerUsageId { get; set; }
    public string DializerUsageName { get; set; }
    public int JumKantongDarah { get; set; }
    public bool AlteplaseIndikator { get; set; }
    public int Sistole { get; set; }
    public int Diastole { get; set; }
    public decimal BodyWeight { get; set; }
    public string TbIndikatorId { get; set; }
    public string TbIndikatorName { get; set; }    
}