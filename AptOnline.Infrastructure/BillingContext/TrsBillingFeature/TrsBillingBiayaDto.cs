namespace AptOnline.Infrastructure.BillingContext.TrsBillingFeature;

public class TrsBillingBiayaDto
{
    public TrsBillingBiayaDto(string fsKdTrs, string fsKdRefBiaya, decimal fnTotal, int fnModul)
    {
        fs_kd_trs = fsKdTrs;
        fs_kd_ref_biaya = fsKdRefBiaya;
        fn_total = fnTotal;
        fn_modul = fnModul;
    }

    public TrsBillingBiayaDto()
    {
    }
    public string fs_kd_trs { get; set; }
    public string fs_kd_ref_biaya{ get; set; }
    public decimal fn_total{ get; set; }
    public int fn_modul { get; set; }
}