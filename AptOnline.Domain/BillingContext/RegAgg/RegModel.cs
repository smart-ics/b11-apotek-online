using AptOnline.Domain.BillingContext.SepAgg;

namespace AptOnline.Domain.BillingContext.RegAgg;

public class RegModel : IRegKey, ISepKey
{
    public string RegId { get; set; }
    public string RegDate { get; set; }
    public string PasienId { get; set; }
    public string PasienName { get; set; }
    public string SepId { get; set; }
}