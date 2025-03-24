namespace AptOnline.Infrastructure.BillingContext.LayananAgg
{
    public class LayananDto
    {
        public string status { get; set; }
        public string code { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string layananId { get; set; }
        public string layananName { get; set; }
        public string smfId { get; set; }
        public string smfName { get; set; }
        public string instalasiId { get; set; }
        public string instalasi { get; set; }
        public string instalasiDkId { get; set; }
        public string instalasiDkName { get; set; }
        public string layananDkId { get; set; }
        public string layananDkName { get; set; }
        public string layananTipeDkId { get; set; }
        public string layananTipeDkName { get; set; }
        public string layananBpjsId { get; set; }
        public string layananBpjsName { get; set; }
        public bool isActive { get; set; }
    }
}
