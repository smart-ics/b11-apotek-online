using AptOnline.Application.EKlaimContext.HemodialisaFeature;
using AptOnline.Domain.EKlaimContext.HemodialisaFeature;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.HemodialisaFeature;

public class DializerUsageDal : IDializerUsageDal
{
    private readonly List<DializerUsageType> _datasource = new()
    {
        new DializerUsageType( "1", "Single Use"),
        new DializerUsageType( "2", "Multiple Use"),
    };

    public void Insert(DializerUsageType model)
    {
        throw new NotImplementedException();
    }

    public void Update(DializerUsageType model)
    {
        throw new NotImplementedException();
    }

    public void Delete(IDializerUsageKey key)
    {
        throw new NotImplementedException();
    }

    public MayBe<DializerUsageType> GetData(IDializerUsageKey key)
    {
        var item =  _datasource.FirstOrDefault(x => x.DializerUsageId == key.DializerUsageId);
        return MayBe.From(item);
    }

    public MayBe<IEnumerable<DializerUsageType>> ListData()
    {
        return MayBe.From(_datasource.AsEnumerable());
    }
}