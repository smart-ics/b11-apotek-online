namespace AptOnline.Infrastructure.BillingContext.DokterAgg;

public class DokterDto
{
    public string status { get; set; }
    public string code { get; set; }
    public DokterData data { get; set; }
}

public class DokterData
{
    public string dokterId { get; set; }
    public string dokterName { get; set; }
    public string nik { get; set; }
    public string phoneNo { get; set; }
    public string email { get; set; }
    public string noSip { get; set; }
    public string dpjpId { get; set; }
    public IEnumerable<DokterLayanan> listLayanan { get; set; }
}

public class DokterLayanan
{
    public string layananId { get; set; }
    public string layananName { get; set; }
    public string smfId { get; set; }
    public string smfName { get; set; }
}