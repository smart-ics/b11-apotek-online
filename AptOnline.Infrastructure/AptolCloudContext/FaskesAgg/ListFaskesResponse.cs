using AptOnline.Infrastructure.AptolCloudContext.Shared;

namespace AptOnline.Infrastructure.AptolCloudContext.FaskesAgg
{
    public class ListFaskesResponse
    {
        public ListFaskesResponseContent response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }

    public class ListFaskesResponseContent
    {
        public List<FaskesDto> list { get; set; }
    }

}
