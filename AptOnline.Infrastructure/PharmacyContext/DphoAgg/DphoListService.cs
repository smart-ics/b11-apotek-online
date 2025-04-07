using AptOnline.Application.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;

namespace AptOnline.Infrastructure.PharmacyContext.DphoAgg;

//  TODO: (to bung @we) Implement list Dpho dari farpu-api 
public class DphoListService : IDphoListService
{
    private readonly FarmasiOptions _opt;

    public DphoListService(IOptions<FarmasiOptions> opt)
    {
        _opt = opt.Value;
    }

    public DphoType Execute(IEnumerable<IBrgKey> req)
    {
        throw new NotImplementedException();
    }
}