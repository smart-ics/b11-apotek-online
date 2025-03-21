namespace AptOnline.Api.Models
{

    public class ListRefPoliBpjsRespDto
    {
        public RefPoliResponse response { get; set; }
        public RefPoliMetadata metaData { get; set; }
    }

    public class RefPoliResponse
    {
        public List<PoliBpjs> list { get; set; }
    }

    public class PoliBpjs
    {
        public string kode { get; set; }
        public string nama { get; set; }
    }

    public class RefPoliMetadata
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}
