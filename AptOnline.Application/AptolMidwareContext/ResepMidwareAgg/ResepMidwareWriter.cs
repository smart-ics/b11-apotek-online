using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.AutoNumberHelper;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.TransactionHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

public interface IResepMidwareWriter : INunaWriterWithReturn<ResepMidwareModel>
{
    void Delete(ResepMidwareModel model);
}
public class ResepMidwareWriter: IResepMidwareWriter
{
    private readonly IResepMidwareDal _resepMidwareDal;
    private readonly IResepMidwareItemDal _resepMidwareItemDal;
    private readonly INunaCounterBL _counter;

    public ResepMidwareWriter(IResepMidwareDal resepMidwareDal, 
        IResepMidwareItemDal resepMidwareItemDal, INunaCounterBL counter)
    {
        _resepMidwareDal = resepMidwareDal;
        _resepMidwareItemDal = resepMidwareItemDal;
        _counter = counter;
    }

    public ResepMidwareModel Save(ResepMidwareModel model)
    {
        // if (model.ResepMidwareId == string.Empty)
        //     model.ResepMidwareId = _counter.Generate("RSPM", IDFormatEnum.PREFYYMnnnnnC);
        foreach (var item in model.ListItem)
            item.SetId(model.ResepMidwareId);

        using var trans = TransHelper.NewScope();
        
        var resepDb = _resepMidwareDal.GetData(model);
        if (resepDb is null)
            _resepMidwareDal.Insert(model);
        else
            _resepMidwareDal.Update(model);

        _resepMidwareItemDal.Delete(model);
        _resepMidwareItemDal.Insert(model.ListItem);
        trans.Complete();
        return model;
    }
    public void Delete(ResepMidwareModel model)
    {
        using var trans = TransHelper.NewScope();
        _resepMidwareDal.Delete(model);
        _resepMidwareItemDal.Delete(model);
        trans.Complete();
    }
}
