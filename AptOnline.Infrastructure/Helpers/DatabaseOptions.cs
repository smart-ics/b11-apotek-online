namespace AptOnline.Infrastructure.Helpers
{
    public class DatabaseOptions
    {
        public const string SECTION_NAME = "Database";

        public string Server { get; set; }
        public string DbName { get; set; }
        public string Log { get; set; }
    }
}
