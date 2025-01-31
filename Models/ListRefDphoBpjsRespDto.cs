namespace AptOnline.Api.Models
{
    public class ListRefDphoBpjsRespDto
    {
        public RefDphoResp response { get; set; }
        public RefDphoMeta metaData { get; set; }
    }

    public class RefDphoResp
    {
        public IEnumerable<RefDphoItem> list { get; set; }
    }

    public class RefDphoItem
    {
        public string kodeobat { get; set; }
        public string namaobat { get; set; }
        public string prb { get; set; }
        public string kronis { get; set; }
        public string kemo { get; set; }
        public string harga { get; set; }
        public string restriksi { get; set; }
        public string generik { get; set; }
        public object aktif { get; set; }
    }

    public class RefDphoMeta
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}


