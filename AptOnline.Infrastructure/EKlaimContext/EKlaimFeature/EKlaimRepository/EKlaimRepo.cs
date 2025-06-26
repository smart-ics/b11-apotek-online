using AptOnline.Application.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.TransactionHelper;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimRepo : IEKlaimRepo
{
    private readonly IEKlaimDal _eklaimDal;
    private readonly IEKlaimMedisDal _eklaimMedisDal;
    private readonly IEKlaimReffBiayaDal _eklaimReffBiayaDal;

    public EKlaimRepo(IEKlaimDal eklaimDal, 
        IEKlaimMedisDal eklaimMedisDal, 
        IEKlaimReffBiayaDal eklaimReffBIayaDal)
    {
        _eklaimDal = eklaimDal;
        _eklaimMedisDal = eklaimMedisDal;
        _eklaimReffBiayaDal = eklaimReffBIayaDal;
    }

    public void SaveChanges(EKlaimModel model)
    {
        using var trans = TransHelper.NewScope();
        var eklaimDb = _eklaimDal.GetData(model);
        if (eklaimDb.HasValue)
            _eklaimDal.Update(eklaimDb.Value);
        
        var eklaimMedisDb = _eklaimMedisDal.GetData(model);
        if (eklaimMedisDb.HasValue)
            _eklaimMedisDal.Update(eklaimMedisDb.Value);
        
        _eklaimReffBiayaDal.Delete(model);
        var listReffBiaya = EKlaimReffBiayaDto.Create(model);
        _eklaimReffBiayaDal.Insert(listReffBiaya);
        trans.Complete();
    }

    public void DeleteEntity(IEKlaimKey key)
    {
        using var trans = TransHelper.NewScope();
        _eklaimDal.Delete(key);
        _eklaimMedisDal.Delete(key);
        _eklaimReffBiayaDal.Delete(key);
        trans.Complete();
    }

    public MayBe<EKlaimModel> LoadEntity(IEKlaimKey key)
    {
        var eklaimDb = _eklaimDal.GetData(key)
            .GetValueOrThrow("EKlaim tidak ditemukan");
        var result = eklaimDb.ToModel();

        var eklaimMedisDb = _eklaimMedisDal.GetData(key);
        if (eklaimMedisDb.HasValue)
            result = eklaimMedisDb.Value.ToModel(result);
        
        var eklaimReffBiayaDb = _eklaimReffBiayaDal.ListData(key);
        if (!eklaimReffBiayaDb.HasValue) 
            return MayBe.From(result);
        
        var listReffBiaya = eklaimReffBiayaDb.Value
            .Select(x => x.ToModel())
            .ToList();            
        result.AttachTarifRs(TarifRsModel.Create(listReffBiaya));

        return MayBe.From(result);
    }

    public MayBe<EKlaimModel> LoadEntity(IRegKey key)
    {
        var eklaimDb = _eklaimDal.GetData(key)
            .GetValueOrThrow("EKlaim tidak ditemukan");
        var result = eklaimDb.ToModel();

        var eklaimMedisDb = _eklaimMedisDal.GetData(result);
        if (eklaimMedisDb.HasValue)
            result = eklaimMedisDb.Value.ToModel(result);
        
        var eklaimReffBiayaDb = _eklaimReffBiayaDal.ListData(result);
        if (!eklaimReffBiayaDb.HasValue) 
            return MayBe.From(result);
        
        var listReffBiaya = eklaimReffBiayaDb.Value
            .Select(x => x.ToModel())
            .ToList();            
        result.AttachTarifRs(TarifRsModel.Create(listReffBiaya));

        return MayBe.From(result);
    }
    
}