namespace AptOnline.Api.Models
{

    public class ListRefObatBpjsRespDto
    {
        public RefObatResponse response { get; set; }
        public RefObatMetadata metaData { get; set; }
    }

    public class RefObatResponse
    {
        public List<RefObatItem> list { get; set; }
    }

    public class RefObatItem
    {
        public string kode { get; set; }
        public string nama { get; set; }
        public string harga { get; set; }
    }

    public class RefObatMetadata
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}


