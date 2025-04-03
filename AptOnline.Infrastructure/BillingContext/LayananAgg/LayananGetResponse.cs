using AptOnline.Domain.BillingContext.LayananAgg;

namespace AptOnline.Infrastructure.BillingContext.LayananAgg;

public class LayananGetResponse
{
    public string status { get; set; }
    public string code { get; set; }
    public LayananGetResponseDto data { get; set; }
}

public class LayananGetResponseDto
{
    public string LayananId {get;set;}
    public string LayananName {get;set;}
    public string LayananBpjsId {get;set;}
    public string LayananBpjsName {get;set;}
    public bool IsActive {get;set;}
}
