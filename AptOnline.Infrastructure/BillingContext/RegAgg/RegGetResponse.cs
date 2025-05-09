namespace AptOnline.Infrastructure.BillingContext.RegAgg;

public class RegGetResponse
{
    public string? status { get; set; }
    public string? code{ get; set; }
    public bool? IsActive { get; set; }
    public RegGetDto? data{ get; set; }
}
