namespace AptOnline.Application.AptolCloudContext.LogAgg
{
    public class LogModel
    {
        public LogModel(DateTime logDateTime,
            string resepId, string resepRequest, string resepResponse,
            int itemNonRacikCount, int itemRacikCount, string message)
        {
            LogDateTime = logDateTime;
            ResepId = resepId;
            ResepRequest = resepRequest;
            ResepResponse = resepResponse;
            ItemNonRacikCount = itemNonRacikCount;
            ItemRacikCount = itemRacikCount;
            Message = message;
        }

        public DateTime LogDateTime { get; set; }
        public string ResepId { get; set; }
        public string ResepRequest { get; set; }
        public string ResepResponse { get; set; }
        public int ItemNonRacikCount { get; set; }
        public int ItemRacikCount { get; set; }
        public string Message { get; set; }
    }
}
