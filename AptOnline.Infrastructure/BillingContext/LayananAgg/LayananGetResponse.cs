using AptOnline.Domain.BillingContext.LayananAgg;

namespace AptOnline.Infrastructure.BillingContext.LayananAgg;

public class LayananGetResponse
{
    public string status { get; set; }
    public string code { get; set; }
    public LayananModel data { get; set; }
}