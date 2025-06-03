using AptOnline.Domain.PharmacyContext.MapDphoAgg;
// ReSharper disable InconsistentNaming

namespace AptOnline.Infrastructure.PharmacyContext.MapDphoAgg;

public class MapDphoGetResponse
{
    public string status { get; set; }
    public string code { get; set; }
    public MapDphoDto data { get; set; }
}

public class MapDphoDto
{
    public MapDphoDto(string brgId, string brgName, string dphoId, string dphoName)
    {
        BrgId = brgId;
        BrgName = brgName;
        DphoId = dphoId;
        DphoName = dphoName;
    }

    public MapDphoDto()
    {
    }
    public string BrgId { get; set; } 
    public string BrgName { get; set; }
    public string DphoId { get; set; }
    public string DphoName  { get; set; }
}
