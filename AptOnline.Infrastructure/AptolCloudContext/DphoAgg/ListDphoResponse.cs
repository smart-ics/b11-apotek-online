using AptOnline.Infrastructure.AptolCloudContext.Shared;

namespace AptOnline.Infrastructure.AptolCloudContext.DphoAgg;

public class ListDphoResponse
{
    public ListDphoResponseContent response { get; set; }
    public AptolCloudResponseMeta metaData { get; set; }
}

public class ListDphoResponseContent
{
    public IEnumerable<DphoDto> list { get; set; }
}
