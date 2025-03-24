using AptOnline.Domain.EmrContext.ResepRsAgg;
using AptOnline.Domain.PharmacyContext.TrsDuAgg;

namespace AptOnline.Infrastructure.PharmacyContext.PenjualanAgg;

public class PenjualanDto
{
    public string status { get; set; }
    public string code { get; set; }
    public PenjualanModel data { get; set; }

}