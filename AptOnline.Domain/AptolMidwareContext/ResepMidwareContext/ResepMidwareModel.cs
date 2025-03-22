namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareModel
{
    public string ResepId { get; set; }
    public string ResepAptolId { get; set; }
    public DateTime ResepDate { get; set; }
    public DateTime ResepPelDate { get; set; }

    public string SepId { get; set; }
    public DateTime SepDate { get; set; }
    public string SepUserId { get; set; }

    public string PoliBpjsId { get; set; }
    public string PoliBpjsName { get; set; }
    public string KodeJenisObat { get; set; }

    public string DokterId { get; set; }
    public int Iterasi { get; set; }
    public List<AbstractResepMidwareItemModel> ListItem { get; set; }
}

public abstract class AbstractResepMidwareItemModel
{
    public string ResepId { get; set; }

    public string ObatBpjsId { get; set; }
    public string ObatBpjsName { get; set; }

    public int Signa1 { get; set; }
    public int Signa2 { get; set; }
    public int Jumlah { get; set; }
    public int Jho { get; set; }

    public string Catatan { get; set; }
}

public class ResepMidwareNonRacikModel : AbstractResepMidwareItemModel
{
}

public class  ResepMidwareRacikModel : AbstractResepMidwareItemModel
{
    public string JenisRacikan { get; set; }
}
