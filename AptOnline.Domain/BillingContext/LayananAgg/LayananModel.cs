using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;

namespace AptOnline.Domain.BillingContext.LayananAgg;

public class LayananModel : ILayananKey
{
    public LayananModel(string id, string name, bool isActive)
    {
        if (id == string.Empty ^ name == string.Empty)
            throw new ArgumentException("Layanan is invalid");
        LayananId = id;
        LayananName = name;
        IsActive = isActive;
        PoliBpjs = PoliBpjsType.Default;
    }
    public string LayananId { get; private set; }
    public string LayananName { get; private set; }
    public PoliBpjsType PoliBpjs { get; private set; }
    public bool IsActive { get; private set; }

    public static LayananModel Default => new LayananModel("", "", false); 
    public void SetPoliBpjs(PoliBpjsType poliBpjs) => PoliBpjs = poliBpjs;
}

