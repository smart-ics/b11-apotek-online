namespace AptOnline.Infrastructure.AptolCloudContext.PkpAgg
{

    public class GetSettingPpkBpjsRespDto
    {
        public SettingPpkBpjsResponse response { get; set; }
        public SettingPpkBpjsMetadata metaData { get; set; }
    }

    public class SettingPpkBpjsResponse
    {
        public string kode { get; set; }
        public string namaapoteker { get; set; }
        public string namakepala { get; set; }
        public string jabatankepala { get; set; }
        public string nipkepala { get; set; }
        public string siup { get; set; }
        public string alamat { get; set; }
        public string kota { get; set; }
        public string namaverifikator { get; set; }
        public string nppverifikator { get; set; }
        public string namapetugasapotek { get; set; }
        public string nippetugasapotek { get; set; }
        public string checkstock { get; set; }
    }

    public class SettingPpkBpjsMetadata
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}

