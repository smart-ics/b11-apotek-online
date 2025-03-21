namespace AptOnline.Api.Models
{

    public class ListRefFaskesBpjsRespDto
    {
        public RefFaskesResponse response { get; set; }
        public RefFaskesMetadata metaData { get; set; }
    }

    public class RefFaskesResponse
    {
        public List<Faskes> list { get; set; }
    }

    public class Faskes
    {
        public string kode { get; set; }
        public string nama { get; set; }
    }

    public class RefFaskesMetadata
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}
