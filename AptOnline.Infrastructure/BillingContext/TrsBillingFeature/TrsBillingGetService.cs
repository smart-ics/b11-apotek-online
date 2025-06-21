using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.TrsBillingFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.TrsBillingFeature;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.BillingContext.TrsBillingFeature;

public class TrsBillingGetService : ITrsBillingGetService
{
    private readonly IRegGetService _regGetService;
    private readonly IListDataMayBe<TrsBillingBiayaDto, IRegKey> _trsBillingListDal;

    public TrsBillingGetService(IOptions<DatabaseOptions> opt, IRegGetService regGetService, 
        IListDataMayBe<TrsBillingBiayaDto, IRegKey> trsBillingListDal)
    {
        _regGetService = regGetService;
        _trsBillingListDal = trsBillingListDal;
    }

    public MayBe<TrsBillingModel> Execute(IRegKey reg)
    {
        var register = _regGetService.Execute(reg);
        if (register is null) 
            return MayBe<TrsBillingModel>.None;

        var listTrsBillingConcept = _trsBillingListDal.ListData(reg);
        if (!listTrsBillingConcept.HasValue) 
            return MayBe<TrsBillingModel>.None;

        var result = new TrsBillingModel(register.ToRefference());
        foreach (var item in listTrsBillingConcept.Value)
        {
            var reffBiaya = new ReffBiayaType(item.fs_kd_ref_biaya, (ReffBiayaClassEnum)item.fn_modul);
            result.AddBiaya(item.fs_kd_trs, reffBiaya, item.fn_total);
        }
        return MayBe.From(result);
    }
}