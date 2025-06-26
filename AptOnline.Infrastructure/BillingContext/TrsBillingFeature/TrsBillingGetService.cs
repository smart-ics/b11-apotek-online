using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.TrsBillingFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
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

    public MayBe<TarifRsModel> Execute(IRegKey reg)
    {
        var register = _regGetService.Execute(reg);
        if (register is null) 
            return MayBe<TarifRsModel>.None;

        var listTrsBillingConcept = _trsBillingListDal.ListData(reg);
        if (!listTrsBillingConcept.HasValue) 
            return MayBe<TarifRsModel>.None;

        var result = new TarifRsModel(register.ToRefference());
        foreach (var item in listTrsBillingConcept.Value)
        {
            var reffBiaya = new ReffBiayaType(item.fs_kd_ref_biaya, (JenisReffBiayaEnum)item.fn_modul);
            result.AddReffBiaya(item.fs_kd_trs, reffBiaya, item.fs_keterangan, item.fn_total);
        }
        return MayBe.From(result);
    }
}