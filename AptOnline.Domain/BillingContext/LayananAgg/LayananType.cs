using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using GuardNet;

namespace AptOnline.Domain.BillingContext.LayananAgg;

public record LayananType : ILayananKey
{
    public LayananType(string id, string name, bool isActive,
        PoliBpjsType poliBpjs)
    {
        Guard.NotNullOrWhitespace(id, nameof(id));
        Guard.NotNullOrWhitespace(name, nameof(name));
        
        LayananId = id;
        LayananName = name;
        IsActive = isActive;

        PoliBpjs = poliBpjs ?? throw new ArgumentNullException(nameof(poliBpjs));
    }
    public string LayananId { get; private set; }
    public string LayananName { get; private set; }
    public PoliBpjsType PoliBpjs { get; private set; }
    public bool IsActive { get; private set; }

    public static LayananType Default => new LayananType("", "", false, PoliBpjsType.Default); 
}

