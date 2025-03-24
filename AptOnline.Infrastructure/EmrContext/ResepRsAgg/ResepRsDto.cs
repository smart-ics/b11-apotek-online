using AptOnline.Domain.EmrContext.ResepRsAgg;

namespace AptOnline.Infrastructure.EmrContext.ResepRsAgg;

public class ResepRsDto
{
    public string status { get; set; }
    public string code { get; set; }
    public ResepRsModel data { get; set; }
    
}