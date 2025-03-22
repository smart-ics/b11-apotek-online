namespace AptOnline.Infrastructure.AptolCloudContext.ObatBpjsAgg;


public class ListObatBpjsResponse
{
    public ListObatBpjsResponseContent response { get; set; }
    public AptolCloudResponseMeta metaData { get; set; }
}

public class ListObatBpjsResponseContent
{
    public List<ObatBpjsDto> list { get; set; }
}

