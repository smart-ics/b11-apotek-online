using AptOnline.Domain.PharmacyContext.MapDphoAgg;

namespace AptOnline.Infrastructure.PharmacyContext.MapDphoAgg;

public class MapDphoGetResponse
{
    public string status { get; set; }
    public string code { get; set; }
    public MapDphoModel data { get; set; }
}
