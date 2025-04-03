using AptOnline.Application.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;

namespace AptOnline.Infrastructure.AptolCloudContext.FaskesAgg;

public class FaskesGetService : IFaskesGetService
{
    public FaskesType Execute()
    {
        return new FaskesType("0176B004", "RS Sehat Sejahtera");
    }
}