using System.Text.Json;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.ParamSistemFeature;
using AptOnline.Application.BillingContext.PegFeature;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.RoomChargeFeature;
using AptOnline.Application.BillingContext.TipeLayananDkFeature;
using AptOnline.Application.BillingContext.TrsBillingFeature;
using AptOnline.Application.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Application.EmrContext.AssesmentFeature;
using AptOnline.Application.SepContext.SepFeature;
using AptOnline.Domain.BillingContext.ParamSistemFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.RoomChargeFeature;
using AptOnline.Domain.BillingContext.TipeLayananDkFeature;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.Covid19Feature;
using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Domain.EKlaimContext.PayorFeature;
using AptOnline.Domain.EKlaimContext.PelayananDarahFeature;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
using AptOnline.Domain.EKlaimContext.TbIndikatorFeature;
using AptOnline.Domain.EKlaimContext.UpgradeIndikatorFeature;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.EKlaimFeature;

public record EKlaimGenClaimDataCommand(string RegId, string CoderNik) : IRequest, IRegKey;

public class EKlaimGenClaimDataCommandHandler : IRequestHandler<EKlaimGenClaimDataCommand, Unit>
{
    private readonly ISepDal _sepDal;
    private readonly IParamSistemDal _paramSistemDal;
    private readonly IRegGetPreviousService _regGetPreviousService;
    private readonly ILayananGetService _layananGetService;
    private readonly IEKlaimRepo _eklaimRepo;
    private readonly IAssesmentGetService _assesmentGetService;
    private readonly IRoomChargeGetService _roomChargeGetService;
    private readonly IKelasTarifRsDal _kelasTarifRsDal;
    private readonly IPegDal _pegDal;
    private readonly ITrsBillingGetService _trsBillingGetService;
    private readonly IMapSkemaJknDal _mapSkemaJknDal;
    
    public EKlaimGenClaimDataCommandHandler(ISepDal sepDal, 
        IRegGetService regService, IParamSistemDal paramSistemDal, 
        IRegGetPreviousService regGetPreviousService, 
        ILayananGetService layananGetService, 
        ITipeLayananDkDal tipeLayananDkDal, 
        IEKlaimRepo eklaimRepo, 
        IAssesmentGetService assesmentGetService, 
        IRoomChargeGetService roomChargeGetService, 
        IKelasTarifRsDal kelasTarifRsDal, IPegDal pegDal, 
        ITrsBillingGetService trsBillingGetService, 
        IMapSkemaJknDal mapSkemaJknDal)
    {
        _sepDal = sepDal;
        _paramSistemDal = paramSistemDal;
        _regGetPreviousService = regGetPreviousService;
        _layananGetService = layananGetService;
        _eklaimRepo = eklaimRepo;
        _assesmentGetService = assesmentGetService;
        _roomChargeGetService = roomChargeGetService;
        _kelasTarifRsDal = kelasTarifRsDal;
        _pegDal = pegDal;
        _trsBillingGetService = trsBillingGetService;
        _mapSkemaJknDal = mapSkemaJknDal;
    }

    public Task<Unit> Handle(EKlaimGenClaimDataCommand request, CancellationToken cancellationToken)
    {
        var regKey = RegType.Key(request.RegId);
        var sep = _sepDal.GetData(regKey)
            .GetValueOrThrow($"SEP utk register '{request.RegId}' tidak ditemukan");
        var roomCharge = _roomChargeGetService.Execute(regKey)
            .Match(
                onSome: x => x,
                onNone: () => RoomChargeModel.Default);
        
        var eklaim = _eklaimRepo.GetData(regKey)
            .GetValueOrThrow($"eKlaim utk register '{request.RegId}' tidak ditemukan");
        eklaim = SetAdministrasiMasuk(eklaim, sep, regKey);
        eklaim = SetMedisPasien(eklaim, regKey, roomCharge);
        eklaim = SetBillPasien(eklaim, regKey, roomCharge, sep, request.CoderNik);

        //_eklaimRepo.SaveChanges();
        var eklaimJson = JsonSerializer.Serialize(eklaim, new JsonSerializerOptions());
        throw new NotImplementedException();
    }

    private EKlaimModel SetAdministrasiMasuk(EKlaimModel eklaim, SepType sep, IRegKey regKey)
    {
        var dpjp = sep.Dpjp;
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
        eklaim.SetAdministrasiMasuk(dpjp, caraMasuk, jenisRawat);

        return eklaim;
    }
    private EKlaimModel SetMedisPasien(EKlaimModel eklaim, IRegKey regKey, RoomChargeModel roomCharge)
    {
        var assesment = _assesmentGetService.Execute(regKey)
            .Match(
                onSome: x => x,
                onNone: () => AssesmentModel.Default);
        var adlScore = AdlScoreType.Create(assesment);
        var icuIndikator = IcuIndikatorType.Create(roomCharge);
        var covid19 = Covid19Type.Default;
        var pelayananDarah = PelayananDarahType.Default;
        var vitalSign = VitalSignType.Create(assesment);

        eklaim.SetMedisPasien(adlScore, icuIndikator, 
            covid19, pelayananDarah, vitalSign, 
            TbIndikatorType.Default);

        return eklaim;
    }
    private EKlaimModel SetBillPasien(EKlaimModel eklaim, IRegKey regKey, RoomChargeModel roomCharge,
        SepType sep, string coderNik)
    {
        var kelasRawat = KelasJknType.FindKelasTertinggi(roomCharge);
        var kelasTarifRsId = _paramSistemDal.GetData(ParamSistemType.Key("BRI_GROUPER_RSID"))
            .Map(x => x.ParamValue)
            .GetValueOrThrow($"Kelas Tarif RS tidak ditemukan");
        var kelasTarifRs = _kelasTarifRsDal.GetData(KelasTarifRsType.Key(kelasTarifRsId))
            .GetValueOrThrow($"Kelas Tarif RS '{kelasTarifRsId}' tidak ditemukan");

        var prosenUpgradeVip = _paramSistemDal
            .GetData(ParamSistemType.Key("BRI_GROUPER_PAYMNT_PCT"))
            .Map(x => decimal.Parse(x.ParamValue));
        var upgradeKelasIndikator = UpgradeKelasIndikatorType
            .Create(sep.KelasHak, roomCharge, prosenUpgradeVip.Value);

        var tarifRs = _trsBillingGetService.Execute(regKey)
            .Match(onSome: x => x, onNone: () => TarifRsModel.Default);
        tarifRs.GenerateSkemaJkn(_mapSkemaJknDal);
        
        var dischargeStatus = DischargeStatusType.Default;
        var payor = PayorType.Create("3", "JKN", "JKN");
        var coder = PegType.Default with { Nik = coderNik };
        
        eklaim.SetBillPasien(kelasRawat, kelasTarifRs, tarifRs, 0, 
            upgradeKelasIndikator, dischargeStatus, payor, coder, 1);
        
        
        return eklaim;
    }
}

