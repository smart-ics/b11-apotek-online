namespace AptOnline.Api.Models
{

    public class ResepDto
    {
        public string status { get; set; }
        public string code { get; set; }
        public ResepData data { get; set; }
    }

    public class ResepData
    {
        public string resepId { get; set; }
        public string tglJam { get; set; }
        public string regId { get; set; }
        public string pasienId { get; set; }
        public string pasienName { get; set; }
        public string layananId { get; set; }
        public string layananName { get; set; }
        public string dokterId { get; set; }
        public string dokterName { get; set; }
        public string urgenitasId { get; set; }
        public string urgenitasName { get; set; }
        public int iter { get; set; }
        public IEnumerable<ResepItem> listBarang { get; set; }
    }

    public class ResepItem
    {
        public string resepId { get; set; }
        public int noUrut { get; set; }
        public string brgId { get; set; }
        public string brgName { get; set; }
        public int qty { get; set; }
        public string satuanId { get; set; }
        public string satuanName { get; set; }
        public int iter { get; set; }
        public bool isRacik { get; set; }
        public bool isKomponen { get; set; }
        public string etiketJenisObatId { get; set; }
        public string etiketJenisObatName { get; set; }
        public string ketSigna { get; set; }
        public int etiketQty { get; set; }
        public int etiketHariQty { get; set; }
        public string etiketHari { get; set; }
        public string etiketSatuanPakaiId { get; set; }
        public string etiketSatuanPakaiName { get; set; }
        public string etiketCaraPakaiId { get; set; }
        public string etiketCaraPakaiName { get; set; }
        public string etiketNote { get; set; }
        public string etiketDescription { get; set; }
    }

}
