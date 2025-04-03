using AptOnline.Domain.PharmacyContext.MapDphoAgg;
// ReSharper disable InconsistentNaming

namespace AptOnline.Infrastructure.PharmacyContext.MapDphoAgg;

public class MapDphoGetResponse
{
    public string status { get; set; }
    public string code { get; set; }
    public MapDphoDto data { get; set; }
}

public record MapDphoDto(string brgId, string brgName, string dphoId, string dphoName);
