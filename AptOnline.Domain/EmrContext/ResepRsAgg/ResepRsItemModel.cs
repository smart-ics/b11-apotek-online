namespace AptOnline.Domain.EmrContext.ResepRsAgg;

public class ResepRsItemModel
{
    //public ResepItemModel(string penjualanId, 
    //    string orderNo, string isRacik, string racikId, 
    //    string barangId, string dphoId, string dphoName, 
    //    string signa1, string signa2, string jho, 
    //    string qty, string note)
    //{
    //    PenjualanId = penjualanId;
    //    OrderNo = orderNo;
    //    IsRacik = isRacik;
    //    RacikId = racikId;
    //    BarangId = barangId;
    //    DphoId = dphoId;
    //    DphoName = dphoName;
    //    Signa1 = signa1;
    //    Signa2 = signa2;
    //    Jho = jho;
    //    Qty = qty;
    //    Note = note;
    //}

    public string PenjualanId { get; set; }  
    public string OrderNo { get; set; }
    public string IsRacik { get; set; }
    public string RacikId { get; set; }
    public string BarangId { get; set; }
    public string DphoId { get; set; }
    public string DphoName { get; set; }
    public string Signa1 { get; set; }
    public string Signa2 { get; set; }
    public string Jho { get; set; }
    public string Qty { get; set; }
    public string Note { get; set; }
}
