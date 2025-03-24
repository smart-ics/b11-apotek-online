namespace AptOnline.Domain.EmrContext.ResepRsAgg;

public class ResepRsItemModel : IResepRsKey
{
    public string ResepId { get; set; }
    public int NoUrut { get; set; }
    public string BrgId { get; set; }
    public string BrgName { get; set; }
    public int Qty { get; set; }
    public string SatuanId { get; set; }
    public string SatuanName { get; set; }
    public int Iter { get; set; }
    public bool IsRacik { get; set; }
    public bool IsKomponen { get; set; }
    public string EtiketJenisObatId { get; set; }
    public string EtiketJenisObatName { get; set; }
    public string KetSigna { get; set; }
    public int EtiketQty { get; set; }
    public int EtiketHariQty { get; set; }
    public string EtiketHari { get; set; }
    public string EtiketSatuanPakaiId { get; set; }
    public string EtiketSatuanPakaiName { get; set; }
    public string EtiketCaraPakaiId { get; set; }
    public string EtiketCaraPakaiName { get; set; }
    public string EtiketNote { get; set; }
    public string EtiketDescription { get; set; }
}

public interface IResepRsKey
{
    public string ResepId { get; }
}