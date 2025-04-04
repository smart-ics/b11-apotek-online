using AptOnline.Infrastructure.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;

namespace AptOnline.Infrastructure.AptolCloudContext.PpkAgg;

public class PpkGetResponse
{
    public PpkGetDto response { get; set; }
    public AptolCloudResponseMeta metaData { get; set; }
}
