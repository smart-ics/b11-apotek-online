using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.ParamSistemFeature;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.RoomChargeFeature;
using AptOnline.Application.BillingContext.TipeLayananDkFeature;
using AptOnline.Application.BillingContext.TrsBillingFeature;
using AptOnline.Application.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Application.EmrContext.AssesmentFeature;
using AptOnline.Application.SepContext.SepFeature;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.ParamSistemFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.RoomChargeFeature;
using AptOnline.Domain.BillingContext.TipeLayananDkFeature;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.Covid19Feature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Domain.EKlaimContext.PelayananDarahFeature;
using AptOnline.Domain.EKlaimContext.UpgradeIndikatorFeature;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
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
    private readonly IKelasTarifRsDal _kelasTarifRsDal;
    private readonly ITrsBillingGetService _trsBillingGetService;
    
    public EKlaimGenClaimDataCommandHandler(ISepDal sepDal, 
        IRegGetService regService, IParamSistemDal paramSistemDal, 
        IRegGetPreviousService regGetPreviousService, 
        ILayananGetService layananGetService, 
        ITipeLayananDkDal tipeLayananDkDal, 
        IEKlaimRepo eklaimRepo, 
        IAssesmentGetService assesmentGetService, 
        IRoomChargeGetService roomChargeGetService, 
        IKelasTarifRsDal kelasTarifRsDal)
    {
        _sepDal = sepDal;
        _regService = regService;
        _paramSistemDal = paramSistemDal;
        _regGetPreviousService = regGetPreviousService;
        _layananGetService = layananGetService;
        _eklaimRepo = eklaimRepo;
        _assesmentGetService = assesmentGetService;
        _roomChargeGetService = roomChargeGetService;
        _kelasTarifRsDal = kelasTarifRsDal;
    }

    public Task<Unit> Handle(EKlaimGenClaimDataCommand request, CancellationToken cancellationToken)
    {
        //  ----SET-ADMINISTRASI-MASUK
        //      1. dpjp
        //      2. cara masuk
        //      3. jenis-rawat
        var regKey = RegType.Key(request.RegId);
        var sep = _sepDal.GetData(regKey)
            .GetValueOrThrow($"SEP utk register '{request.RegId}' tidak ditemukan");
        var dpjp = sep.Dpjp;
        //        
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
        var caraMasuk = CaraMasukType.Create(sep,faskesRs, tipeLayananDk);
        //
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
        var assesment = _assesmentGetService.Execute(regKey)
            .Match(
                onSome: x => x,
                onNone: () => AssesmentModel.Default);
        var roomCharge = _roomChargeGetService.Execute(regKey)
            .Match(
                onSome: x => x,
                onNone: () => RoomChargeModel.Default);
        
        var adlScore = AdlScoreType.Create(assesment);
        var icuIndikator = IcuIndikatorType.Create(roomCharge);
        var covid19 = Covid19Type.Default;
        var pelayananDarah = PelayananDarahType.Default;
        var vitalSign = VitalSignType.Create(assesment);
        
        //  ----BILL-PASIEN
        //      1. Kelas Rawat [done]
        //      2. Kelas Tarif RS [done]
        //      3. Tarif RS  [in-progress]
        //      4. Tarif Poli Eksekutif
        //      5. Indikator Upgrade Kelas
        //      6. Discharge Status
        //      7. Payor
        //      8. Coder
        //      9. Length-Of-Stay
        var reg = _regService.Execute(regKey)
                  ?? throw new ApplicationException($"Register '{regKey}' tidak ditemukan");
        var kelasRawat = KelasJknType.FindKelasTertinggi(roomCharge);
        var kelasTarifRsId = _paramSistemDal.GetData(ParamSistemType.Key("BRI_GROUPER_RSID"))
            .Map(x => x.ParamValue)
            .GetValueOrThrow($"Kelas Tarif RS tidak ditemukan");
        var kelasTarifRs = _kelasTarifRsDal.GetData(KelasTarifRsType.Key(kelasTarifRsId))
            .GetValueOrThrow($"Kelas Tarif RS '{kelasTarifRsId}' tidak ditemukan");
        //
        var upgradeKelasIndikator = UpgradeKelasIndikatorType.Create(sep.KelasHak, roomCharge, 10m);
        
        
        
        throw new AbandonedMutexException();
    }
}

