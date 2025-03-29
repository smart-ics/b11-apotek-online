namespace AptOnline.Domain.BillingContext.LayananAgg;

public class LayananModel : ILayananKey
{
    public LayananModel()
    {
    }

    public LayananModel(string id)
    {
        LayananId = id;
    }
    public string LayananId { get; set; }
    public string LayananName { get; set; }
    public string SmfId { get; set; }
    public string SmfName { get; set; }
    public string InstalasiId { get; set; }
    public string Instalasi { get; set; }
    public string InstalasiDkId { get; set; }
    public string InstalasiDkName { get; set; }
    public string LayananDkId { get; set; }
    public string LayananDkName { get; set; }
    public string LayananTipeDkId { get; set; }
    public string LayananTipeDkName { get; set; }
    public string LayananBpjsId { get; set; }
    public string LayananBpjsName { get; set; }
    public bool IsActive { get; set; }
}