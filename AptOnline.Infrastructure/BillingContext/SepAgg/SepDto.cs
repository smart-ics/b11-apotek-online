namespace AptOnline.Infrastructure.BillingContext.SepAgg
{

    public class SepDto
    {
        public string status { get; set; }
        public string code { get; set; }
        public SepData data { get; set; }
    }

    public class SepData
    {
        public string sepId { get; set; }
        public string sepDateTime { get; set; }
        public string regId { get; set; }
        public string pasienId { get; set; }
        public string pasienName { get; set; }
        public string pesertaJaminanId { get; set; }
        public string sepNo { get; set; }
        public string telpNo { get; set; }
        public string jnsPesertaName { get; set; }
        public string kelasPesertaName { get; set; }
        public string kelasRawatKode { get; set; }
        public string jenisKelaminName { get; set; }
        public string layananName { get; set; }
        public bool isPoliEksekutif { get; set; }
        public bool isKatarak { get; set; }
        public string dpjpLayananId { get; set; }
        public string dpjpLayananName { get; set; }
        public string dpjpId { get; set; }
        public string dpjpName { get; set; }
        public string tujuanKunjunganId { get; set; }
        public object flagPrecodureId { get; set; }
        public string penunjangId { get; set; }
        public string assPelayananId { get; set; }
        public string skdpNo { get; set; }
        public string rujukanNo { get; set; }
        public string rujukanDate { get; set; }
        public string rujukanTime { get; set; }
        public string rujukanDiagCode { get; set; }
        public string rujukanDiagName { get; set; }
        public string rujukanProviderId { get; set; }
        public string rujukanProviderName { get; set; }
        public string jnsPelayananKode { get; set; }
        public string provider { get; set; }
        public bool isCob { get; set; }
        public string cobName { get; set; }
        public string cobNo { get; set; }
        public string note { get; set; }
        public string lakaId { get; set; }
        public string lakaDate { get; set; }
        public string propLakaId { get; set; }
        public string propLakaName { get; set; }
        public string kabLakaId { get; set; }
        public string kabLakaName { get; set; }
        public string kecLakaId { get; set; }
        public string kecLakaName { get; set; }
        public string keteranganLaka { get; set; }
        public string penjaminLakaId { get; set; }
        public bool isSuplesi { get; set; }
        public string sepSuplesiNo { get; set; }
        public bool isPrb { get; set; }
        public string prb { get; set; }
        public string subSpesialis { get; set; }
        public string pembiayaanId { get; set; }
        public string penanggungJawab { get; set; }
        public string pulangDate { get; set; }
        public string statusPulangId { get; set; }
        public string meninggalDate { get; set; }
        public string suratKematianNo { get; set; }
        public string lpManualNo { get; set; }
        public string rujukanInternalNo { get; set; }
    }
}
