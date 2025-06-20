using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.ParamSistemFeature;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.RoomChargeFeature;
using AptOnline.Application.BillingContext.TipeLayananDkFeature;
using AptOnline.Application.EmrContext.AssesmentFeature;
using AptOnline.Application.SepContext.SepFeature;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.ParamSistemFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.TipeLayananDkFeature;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.Covid19Feature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using AptOnline.Domain.EKlaimContext.PelayananDarahFeature;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.EKlaimFeature;

public record EKlaimGenClaimDataCommand(string RegId) : IRequest, IRegKey;

public class EKlaimGenClaimDataCommandHandler : IRequestHandler<EKlaimGenClaimDataCommand, Unit>
{
    private readonly ISepDal _sepDal;
    private readonly IRegGetService _regService;
    private readonly IParamSistemDal _paramSistemDal;
    private readonly IRegGetPreviousService _regGetPreviousService;
    private readonly ILayananGetService _layananGetService;
    private readonly IEKlaimRepo _eklaimRepo;
    private readonly IAssesmentGetService _assesmentGetService;
    private readonly IRoomChargeGetService _roomChargeGetService;

    public EKlaimGenClaimDataCommandHandler(ISepDal sepDal, 
        IRegGetService regService, IParamSistemDal paramSistemDal, 
        IRegGetPreviousService regGetPreviousService, 
        ILayananGetService layananGetService, 
        ITipeLayananDkDal tipeLayananDkDal, 
        IEKlaimRepo eklaimRepo, 
        IAssesmentGetService assesmentGetService, 
        IRoomChargeGetService roomChargeGetService)
    {
        _sepDal = sepDal;
        _regService = regService;
        _paramSistemDal = paramSistemDal;
        _regGetPreviousService = regGetPreviousService;
        _layananGetService = layananGetService;
        _eklaimRepo = eklaimRepo;
        _assesmentGetService = assesmentGetService;
        _roomChargeGetService = roomChargeGetService;
    }

    public Task<Unit> Handle(EKlaimGenClaimDataCommand request, CancellationToken cancellationToken)
    {
        //  ----SET-ADMINISTRASI-MASUK
        //      1. dpjp
        var regKey = RegType.Key(request.RegId);
        var sep = _sepDal.GetData(regKey)
            .GetValueOrThrow($"SEP utk register '{request.RegId}' tidak ditemukan");
        var dpjp = sep.Dpjp;
        
        //      2. cara masuk
        var faskesRs = _paramSistemDal.GetData(ParamSistemType.Key("BRI_BPJS_PPKRS"))
            .Map(x => new FaskesType(x.ParamValue, "-", TipeFaskesType.FaskesRs))
            .GetValueOrThrow($"Param sistem 'FASKES RS' tidak ditemukan");
        var regPrevMaybe = _regGetPreviousService.Execute(regKey);
        var regPrev = regPrevMaybe.HasValue 
            ? regPrevMaybe.Value 
            : RegType.Default;
        var layanan = _layananGetService.Execute(regPrev.Layanan);
        var tipeLayananDk = layanan is null 
            ? TipeLayananDkType.Default
            : layanan.TipeLayananDk;
        var caraMasuk = CaraMasukType.Resolve(sep.JenisPelayanan, 
            sep.AssesmentPelayanan, sep.FaskesPerujuk.TipeFaskes, 
            sep.Skdp, sep.FaskesPerujuk, faskesRs, tipeLayananDk);
        
        //      3. jenis-rawat
        var jenisRawat = JenisRawatType.Create(sep.JenisPelayanan);
        var eklaim = _eklaimRepo.GetData(regKey)
            .GetValueOrThrow($"eKlaim utk register '{request.RegId}' tidak ditemukan");
        eklaim.SetAdministrasiMasuk(dpjp, caraMasuk, jenisRawat);
        
        //  ----SET-MEDIS-PASIEN
        //      1. adl-score
        //      2. icu-indikator
        //      3. covid-19 ***
        //      4. pelayanan-darah ***
        //      5. vital-sign
        //      6. pasien-tb
        var reg = _regService.Execute(regKey)
                  ?? throw new ApplicationException($"Register '{regKey}' tidak ditemukan");
        var assesment = _assesmentGetService.Execute(regKey)
            .Match(
                onNone: () => AssesmentModel.Default,
                onSome: x => x);
        var adlScore = AdlScoreType.Create(assesment);
        
        var icuIndikator = _roomChargeGetService.Execute(regKey)
            .Match(
                onSome:  IcuIndikatorType.Create,
                onNone: () => IcuIndikatorType.Default);
        
        var covid19 = Covid19Type.Default;
        var pelayananDarah = PelayananDarahType.Default;

        var vitalSign = VitalSignType.Create(assesment);
        //var pasienTb = PasienTbType.Default;
        throw new AbandonedMutexException();
    }
}

