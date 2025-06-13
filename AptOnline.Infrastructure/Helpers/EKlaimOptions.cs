namespace AptOnline.Infrastructure.Helpers
{
    public class EKlaimOptions
    {
        public const string SECTION_NAME = "EKlaim";

        public string BaseApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string Debug { get; set; } = "0";
        
    }
}
