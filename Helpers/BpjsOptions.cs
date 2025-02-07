namespace AptOnline.Api.Helpers
{
    public class BpjsOptions
    {
        public const string SECTION_NAME = "Bpjs";

        public string BaseApiUrl { get; set; }
        public string ConsId { get; set; }
        public string SecretKey { get; set; }
        public string UserKey { get; set; }
    }
}
