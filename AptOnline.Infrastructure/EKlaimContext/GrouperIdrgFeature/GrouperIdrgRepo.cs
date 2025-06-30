using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;
using AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.TransactionHelper;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgRepo : IGrouperIdrgRepo
{
    private readonly IGrouperIdrgDal _grouperIdrgDal;
    private readonly IGrouperIdrgDiagDal _grouperIdrgDiagDal;
    private readonly IGrouperIdrgProsDal _grouperIdrgProsDal;
    private readonly IEKlaimDal _eKlaimDal;

    public GrouperIdrgRepo(IGrouperIdrgDal grouperIdrgDal, 
        IGrouperIdrgDiagDal grouperIdrgDiagDal, 
        IGrouperIdrgProsDal grouperIdrgProsDal, 
        IEKlaimDal eKlaimDal)
    {
        _grouperIdrgDal = grouperIdrgDal;
        _grouperIdrgDiagDal = grouperIdrgDiagDal;
        _grouperIdrgProsDal = grouperIdrgProsDal;
        _eKlaimDal = eKlaimDal;
    }

    public void SaveChanges(GrouperIdrgModel model)
    {
        using var trans = TransHelper.NewScope();

        var grouperIdrgDto = new GrouperIdrgDto(model);
        _grouperIdrgDal.GetData(model)
            .Match(
                onSome: _ => _grouperIdrgDal.Update(grouperIdrgDto),
                onNone: () => _grouperIdrgDal.Insert(grouperIdrgDto)
            );

        _grouperIdrgDiagDal.Delete(model);
        var listDiag = model.ListDiagnosa
            .Select(x => new GrouperIdrgDiagDto(model.EKlaimId, x)).ToList();
        _grouperIdrgDiagDal.Insert(listDiag);

        _grouperIdrgProsDal.Delete(model);
        var listProsedur = model.ListProsedur
            .Select(x => new GrouperIdrgProsDto(model.EKlaimId, x)).ToList();
        _grouperIdrgProsDal.Insert(listProsedur);
        
        trans.Complete();
    }

    public void DeleteEntity(IEKlaimKey key)
    {
        using var trans = TransHelper.NewScope();
        _grouperIdrgDal.Delete(key);
        _grouperIdrgDiagDal.Delete(key);
        _grouperIdrgProsDal.Delete(key);
        trans.Complete();
    }

    public MayBe<GrouperIdrgModel> LoadEntity(IEKlaimKey key)
    {
        var grouperIdrgDto = _grouperIdrgDal.GetData(key)
            .GetValueOrThrow("GrouperIdrg tidak ditemukan");

        var listDiagDto = _grouperIdrgDiagDal.ListData(key)
            .Match(
                onSome: x => x,
                onNone: () => new List<GrouperIdrgDiagDto>()
            );
        
        var listProsDto = _grouperIdrgProsDal.ListData(key)
            .Match(
                onSome: x => x,
                onNone: () => new List<GrouperIdrgProsDto>()
                );
        var result = grouperIdrgDto.ToModel(
            listDiagDto.Select(x => x.ToModel()).ToList(), 
            listProsDto.Select(x => x.ToModel()).ToList());
        
        return MayBe.From(result);
    }

    public MayBe<GrouperIdrgModel> LoadEntity(IRegKey key)
    {
        var eklaimDto = _eKlaimDal.GetData(key)
            .GetValueOrThrow("EKlaim tidak ditemukan");
        return LoadEntity(EKlaimModel.Key(eklaimDto.EKlaimId));
    }
}