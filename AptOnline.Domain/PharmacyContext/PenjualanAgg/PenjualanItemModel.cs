namespace AptOnline.Domain.PharmacyContext.TrsDuAgg;

public class PenjualanItemModel : IPenjualanKey
{
    public string PenjualanId { get; set; }
    public string PenjualanItemId { get; set; }
    public int NoUrut { get; set; }
    public string BrgId { get; set; }
    public string BrgName { get; set; }
    public int Qty { get; set; }
    public string SatuanId { get; set; }
    public string SatuanName { get; set; }
    public bool IsRacik { get; set; }
    public bool IsKomponenRacik { get; set; }
    public string RacikId { get; set; }
    public int Harga { get; set; }
    public int Diskon { get; set; }
    public int Embalase { get; set; }
    public int Tax { get; set; }
    public int Total { get; set; }
    public string BrgType { get; set; }
    public string Etiket { get; set; }
    public int EtiketQty { get; set; }
    public string EtiketQtyStr { get; set; }
    public int EtiketHari { get; set; }
    public string EtiketHariStr { get; set; }
    public string EtiketSatuanPakaiId { get; set; }
    public string EtiketSatuanPakaiName { get; set; }
    public string EtiketCaraPakaiId { get; set; }
    public string EtiketCaraPakaiName { get; set; }
    public string EtiketNote { get; set; }
}