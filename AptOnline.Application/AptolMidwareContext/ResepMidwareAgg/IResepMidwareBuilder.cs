using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

public interface  IResepMidwareBuilder : INunaBuilder<ResepMidwareModel>
{
    IResepMidwareBuilder Load(IResepMidwareKey key);
}

public class ResepMidwareBuilder : IResepMidwareBuilder
{
    private readonly IResepMidwareDal _resepMidwareDal;
    private readonly IResepMidwareItemDal _resepMidwareItemDal;
    private ResepMidwareModel _agg;

    public ResepMidwareBuilder(IResepMidwareDal resepMidwareDal, 
        IResepMidwareItemDal resepMidwareItemDal)
    {
        _resepMidwareDal = resepMidwareDal;
        _resepMidwareItemDal = resepMidwareItemDal;
    }

    public ResepMidwareModel Build()
    {
        return _agg;
    }

    public IResepMidwareBuilder Load(IResepMidwareKey key)
    {
        _agg = _resepMidwareDal.GetData(key);
        if(_agg is not null)
            _agg.ListItem = _resepMidwareItemDal.ListData(key)?.ToList() 
                ?? new List<ResepMidwareItemModel>();
        return this;
    }
}