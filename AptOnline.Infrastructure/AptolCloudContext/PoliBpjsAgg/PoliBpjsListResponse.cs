using AptOnline.Infrastructure.AptolCloudContext.Shared;

namespace AptOnline.Infrastructure.AptolCloudContext.PoliBpjsAgg;


public class PoliBpjsListResponse
{
    public ListPoliBpjsResponseContent response { get; set; }
    public AptolCloudResponseMeta metaData { get; set; }
}

public class ListPoliBpjsResponseContent
{
    public List<PoliBpjsDto> list { get; set; }
}
