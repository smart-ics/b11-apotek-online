using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ResepBpjsSaveDto
    {
        public string noSep_Kunjungan { get; set; }
        public string noKartu { get; set; }
        public string nama { get; set; }
        public string faskesAsal { get; set; }
        public string noApotik { get; set; }
        public string noResep { get; set; }
        public string tglResep { get; set; }
        public string kdJnsObat { get; set; }
        public string byTagRsp { get; set; }
        public string byVerRsp { get; set; }
        public string tglEntry { get; set; }

        public ResepBpjsModel ToModel()
        {
            var result = new ResepBpjsModel(noApotik, noSep_Kunjungan,
                noKartu, nama, faskesAsal, noResep, tglResep,
                kdJnsObat, tglEntry);
            return result;
        }
    }
}
