using AptOnline.Application.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;

namespace AptOnline.Infrastructure.AptolCloudContext.FaskesAgg;

public class FaskesGetService : IFaskesGetService
{
    public FaskesModel Execute()
    {
        return new FaskesModel("0176B004", "RS Sehat Sejahtera");
    }
}