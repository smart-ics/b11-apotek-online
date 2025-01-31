namespace AptOnline.Api.Infrastructures
{
    public class AptOnlineOption
    {
        public const string SECTION_NAME = "AptOnline";

        public string BaseApiUrl { get; set; }
        public string ConsId { get; set; }
        public string SecretKey { get; set; }
        public string UserKey { get; set; }
    }
}
