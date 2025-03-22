namespace AptOnline.Infrastructure.AptolMidwareContext.PenjualanAgg
{

    public class PenjualanDto
    {
        public string status { get; set; }
        public string code { get; set; }
        public PenjualanData data { get; set; }
    }

    public class PenjualanData
    {
        public string penjualanId { get; set; }
        public string tglJamTrs { get; set; }
        public string resepId { get; set; }
        public string regId { get; set; }
        public string pasienId { get; set; }
        public string pasienName { get; set; }
        public string layananId { get; set; }
        public string layananName { get; set; }
        public string dokterId { get; set; }
        public string dokterName { get; set; }
        public string noDokumen { get; set; }
        public int subTotal { get; set; }
        public int totalEmbalase { get; set; }
        public int totalTax { get; set; }
        public int totalDiskon { get; set; }
        public int diskonLain { get; set; }
        public int biayaLain { get; set; }
        public int grandTotal { get; set; }
        public IEnumerable<Listbarang> listBarang { get; set; }
        public IEnumerable<ListbarangEd> listBarangEd { get; set; }
    }

    public class Listbarang
    {
        public string penjualanId { get; set; }
        public string penjualanItemId { get; set; }
        public int noUrut { get; set; }
        public string brgId { get; set; }
        public string brgName { get; set; }
        public int qty { get; set; }
        public string satuanId { get; set; }
        public string satuanName { get; set; }
        public bool isRacik { get; set; }
        public bool isKomponenRacik { get; set; }
        public string racikId { get; set; }
        public int harga { get; set; }
        public int diskon { get; set; }
        public int embalase { get; set; }
        public int tax { get; set; }
        public int total { get; set; }
        public string brgType { get; set; }
        public string etiket { get; set; }
        public int etiketQty { get; set; }
        public string etiketQtyStr { get; set; }
        public int etiketHari { get; set; }
        public string etiketHariStr { get; set; }
        public string etiketSatuanPakaiId { get; set; }
        public string etiketSatuanPakaiName { get; set; }
        public string etiketCaraPakaiId { get; set; }
        public string etiketCaraPakaiName { get; set; }
        public string etiketNote { get; set; }
    }

    public class ListbarangEd
    {
        public string penjualanId { get; set; }
        public string penjualanItemId { get; set; }
        public string penjualanItemEdId { get; set; }
        public int noUrut { get; set; }
        public string brgId { get; set; }
        public int qty { get; set; }
        public string satuanId { get; set; }
        public string expiredDate { get; set; }
        public string batch { get; set; }
        public string hpp { get; set; }
    }

}
