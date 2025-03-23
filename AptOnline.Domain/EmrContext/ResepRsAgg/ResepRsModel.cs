namespace AptOnline.Domain.EmrContext.ResepRsAgg;

public class ResepRsModel
{
    public string ResepId { get; set; }
    public string TglJam { get; set; }
    public string RegId { get; set; }
    public string PasienId { get; set; }
    public string PasienName { get; set; }
    public string LayananId { get; set; }
    public string LayananName { get; set; }
    public string DokterId { get; set; }
    public string DokterName { get; set; }
    public string UrgenitasId { get; set; }
    public string UrgenitasName { get; set; }
    public int Iter { get; set; }
    public IEnumerable<ResepRsItemModel> ListBarang { get; set; }
}