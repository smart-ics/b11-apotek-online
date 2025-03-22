namespace AptOnline.Infrastructure.AptolCloudContext.PoliBpjsAgg;


public class ListPoliBpjsResponse
{
    public ListPoliBpjsResponseContent response { get; set; }
    public AptolCloudResponseMeta metaData { get; set; }
}

public class ListPoliBpjsResponseContent
{
    public List<PoliBpjsDto> list { get; set; }
}
