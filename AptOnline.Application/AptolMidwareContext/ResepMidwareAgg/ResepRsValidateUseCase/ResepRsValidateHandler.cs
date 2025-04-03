using AptOnline.Application.AptolCloudContext.FaskesAgg;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using MediatR;
using Nuna.Lib.TransactionHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;

public class ResepRsValidateHandler :
    IRequestHandler<ResepRsValidateCommand, IEnumerable<ResepRsValidateResponse>>
{
    private readonly IResepMidwareWriter _writer;
    private readonly IRegGetService _regGetService;
    private readonly ISepGetService _sepGetService;
    private readonly IFaskesGetService _faskesGetService;
    private readonly ILayananGetService _layananGetService;
    private readonly IMapDphoGetService _mapDphoGetService;


    public ResepRsValidateHandler(
        IRegGetService regGetService,
        ISepGetService sepGetService,
        IFaskesGetService faskesGetService,
        ILayananGetService layananGetService,
        IMapDphoGetService mapDphoGetService,
        IResepMidwareWriter writer)
    {
        _regGetService = regGetService;
        _sepGetService = sepGetService;
        _faskesGetService = faskesGetService;
        _layananGetService = layananGetService;
        _mapDphoGetService = mapDphoGetService;
        _writer = writer;
    }

    public Task<IEnumerable<ResepRsValidateResponse>> Handle(
        ResepRsValidateCommand request, CancellationToken cancellationToken)
    {
        //  GUARD (header only)
        var reg = _regGetService.Execute(request)
                  ?? throw new KeyNotFoundException($"Register {request.RegId} not found");
        var sep = _sepGetService.Execute(reg)
                  ?? throw new KeyNotFoundException($"Sep {request.RegId} not found");
        var faskes = _faskesGetService.Execute()
                     ?? throw new KeyNotFoundException($"Setting Faskes not found");
        var layanan = _layananGetService.Execute(request)
                      ?? throw new KeyNotFoundException($"Layanan {request.LayananId} not found");

        //  BUILD
        var listResult = new List<ResepRsValidateResponseDto>();
        var noUrutResep = 0;
        foreach (var item in request.ListResep)
        {
            noUrutResep++;
            var createResult = BuildResepMidware(noUrutResep, item, reg, sep, faskes, layanan);
            listResult.Add(createResult);
        }

        //  WRITE
        using var trans = TransHelper.NewScope();
        foreach (var item in listResult.Where(x => x.IsCreated))
        {
            var writeResult = _writer.Save(item.ResepMidware);
            item.ResepMidware.ResepMidwareId = writeResult.ResepMidwareId;
        }
        trans.Complete();
        
        //  RESPONSE
        var response = listResult
            .Select(x => new ResepRsValidateResponse(
                x.NoUrut, 
                x.ResepMidware.ResepMidwareId, 
                x.IsCreated ? "SUCCESS" : "FAILED", 
                x.ValidationNote));
        return Task.FromResult(response);
    }

    private ResepRsValidateResponseDto BuildResepMidware(int noUrut,
        ResepRsValidateCommandResep resep,
        RegModel reg, SepModel sep, FaskesType faskes, LayananModel layanan)
    {
        var resepMidware = CreateResepHeader(reg, sep, faskes, layanan);

        var listValidationNote = new List<string>();
        var itemCount = 0;
        foreach (var itemObat in resep.ListItem)
        {
            resepMidware = AddItemObat(
                itemObat, resepMidware, 
                ref itemCount, ref listValidationNote);
            
            foreach (var itemRacik in itemObat.ListKomponenRacik)
                resepMidware = AddItemRacik(
                    itemRacik, resepMidware, itemObat.Signa, 
                    ref itemCount, ref listValidationNote);
        }

        var listValidationNoteStr = string.Join(", ", listValidationNote);
        if (listValidationNote.Count == itemCount)
            return new ResepRsValidateResponseDto(noUrut, 
                resepMidware, false, listValidationNoteStr);

        return new ResepRsValidateResponseDto(noUrut,
            resepMidware, true, listValidationNoteStr);
    }
    
    private static ResepMidwareModel CreateResepHeader(RegModel reg, SepModel sep, 
        FaskesType faskes, LayananModel layanan)
    {
        var result = new ResepMidwareModel();
        result.SetRegister(reg);
        result.SetSep(sep);
        result.SetFaskes(faskes);
        result.SetPoliBpjs(layanan);
        return result;
    }
    
    private ResepMidwareModel AddItemObat(ResepRsValidateCommandObat itemObat,
        ResepMidwareModel resepMidware,
        ref int itemCount, 
        ref List<string> listValidationNote)
    {
        itemCount++;
        var mapDpho = _mapDphoGetService.Execute(itemObat);
        if (mapDpho is null)
            return resepMidware;
        var resultType = resepMidware.AddObat(mapDpho, itemObat.Signa, itemObat.Qty);
        if (resultType.IsFailed)
            listValidationNote.Add(resultType.ErrorMessage);
        return resepMidware;
    }

    private ResepMidwareModel AddItemRacik(ResepRsValidateCommandKomponenRacik itemRacik,
        ResepMidwareModel resepMidware,
        string signa,
        ref int itemCount,
        ref List<string> listValidationNote)
    {
        itemCount++;
        var mapDpho = _mapDphoGetService.Execute(itemRacik);
        var resultType = resepMidware.AddRacik(mapDpho, signa, itemRacik.Qty);
        if (resultType.IsFailed)
            listValidationNote.Add(resultType.ErrorMessage);
        return resepMidware;
    }
}

