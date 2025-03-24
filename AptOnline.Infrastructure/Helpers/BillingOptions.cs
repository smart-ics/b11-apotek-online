namespace AptOnline.Infrastructure.Helpers
{
    public class BillingOptions
    {
        public const string SECTION_NAME = "Billing";

        public string BaseApiUrl { get; set; } = "";
        public string ConsId { get; set; } = "";
        public string SecretKey { get; set; } = "";
    }
}
