using System.Text.Json.Serialization;
using AptOnline.Infrastructure.AptolCloudContext.DphoAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;

namespace AptOnline.Infrastructure.AptolCloudContext.DphoCloudAgg;

public class DphoCloudListResponse
{
    public DphoCloudListResponseContent response { get; set; }
    public AptolCloudResponseMeta metaData { get; set; }
}

public class DphoCloudListResponseContent
{
    public IEnumerable<DphoCloudDto> list { get; set; }
}
