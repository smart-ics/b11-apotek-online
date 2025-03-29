using AptOnline.Domain.BillingContext.SepAgg;

namespace AptOnline.Infrastructure.BillingContext.SepAgg;

public class SepDto
{
    public string status { get; set; }
    public string code { get; set; }
    public SepModel data { get; set; }
}