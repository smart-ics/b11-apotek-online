using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimRepo : IEKlaimRepo
{
    private readonly DatabaseOptions _opt;

    public EKlaimRepo(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }

    public void Insert(EKlaimModel model)
    {
        throw new NotImplementedException();
    }

    public void Update(EKlaimModel model)
    {
        throw new NotImplementedException();
    }

    public void Delete(IEKlaimKey key)
    {
        throw new NotImplementedException();
    }

    public MayBe<EKlaimModel> GetData(IEKlaimKey key)
    {
        throw new NotImplementedException();
    }

    public MayBe<EKlaimModel> GetData(ISepKey key)
    {
        throw new NotImplementedException();
    }

    public MayBe<EKlaimModel> GetData(IRegKey key)
    {
        throw new NotImplementedException();
    }

    public MayBe<IEnumerable<EKlaimModel>> ListData(Periode filter)
    {
        throw new NotImplementedException();
    }
}