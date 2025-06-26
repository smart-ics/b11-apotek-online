namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimReffBiayaDto
{
    public string EKlaimId { get; set; }
    public int NoUrut { get; set; }
    public string TrsId { get; set; }
    public string ReffBiayaId { get; set; }
    public int ReffClass { get; set; }
    public string KetBiaya { get; set; }
    public decimal Nilai { get; set; }
    public string SkemaTarifJknId { get; set; }
    public string SkemaTarifJknName { get; set; }
}