using AptOnline.Domain.EmrContext.ResepRsAgg;

namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareModel : IResepMidwareKey
{
    public string ResepMidwareId { get; set; }
    public string ReffId { get; set; }
    public DateTime ResepMidwareDate { get; set; }
    public DateTime EntryDate { get; set; }

    public string SepId { get; set; }
    public DateTime SepDate { get; set; }
    public string NoPeserta { get; set; }

    public string FaskesId { get; set; }
    public string FaskesName { get; set; }
    public string PoliBpjsId { get; set; }
    public string PoliBpjsName { get; set; }

    public string JenisObatId { get; set; }

    public string DokterId { get; set; }
    public string DokterName { get; set; }
    public int Iterasi { get; set; }
    public List<ResepMidwareItemModel> ListItem { get; set; }
    
    
    public void ImportItems(ResepRsModel model)
    {
        throw new NotImplementedException();
    }
}