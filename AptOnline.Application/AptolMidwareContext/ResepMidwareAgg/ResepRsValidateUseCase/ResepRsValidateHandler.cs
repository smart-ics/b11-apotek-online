using AptOnline.Application.AptolCloudContext.PpkAgg;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using MediatR;
using Nuna.Lib.TransactionHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;

public class ResepRsValidateHandler :
    IRequestHandler<ResepRsValidateCommand, IEnumerable<ResepRsValidateResponse>>
{
    private readonly IResepMidwareWriter _writer;
    private readonly ISepGetByRegService _sepGetByRegService;
    private readonly IPpkGetService _ppkGetService;
    private readonly ILayananGetService _layananGetService;
    private readonly IMapDphoGetService _mapDphoGetService;
    private readonly ITglJamProvider _dateTime;


    public ResepRsValidateHandler(
        ISepGetByRegService sepGetByRegService,
        IPpkGetService ppkGetService,
        ILayananGetService layananGetService, 
        IMapDphoGetService mapDphoGetService, 
        IResepMidwareWriter writer, 
        ITglJamProvider dateTime)
    {
        _sepGetByRegService = sepGetByRegService;
        _ppkGetService = ppkGetService;
        _layananGetService = layananGetService;
        _mapDphoGetService = mapDphoGetService;
        _writer = writer;
        _dateTime = dateTime;
    }

    public Task<IEnumerable<ResepRsValidateResponse>> Handle(
        ResepRsValidateCommand request, CancellationToken cancellationToken)
    {
        //  GUARD (header only)
        var sep = _sepGetByRegService.Execute(request)
            ?? throw new KeyNotFoundException($"SEP for register {request.RegId} not found");
        var ppk = _ppkGetService.Execute()
            ?? throw new KeyNotFoundException($"Setting Faskes not found");
        var layanan = _layananGetService.Execute(request)
            ?? throw new KeyNotFoundException($"Layanan {request.LayananId} not found");

        //  BUILD
        var listResult = new List<ResepRsValidateResponseDto>();
        var noUrutResep = 0;
        foreach (var item in request.ListResep)
        {
            noUrutResep++;
            var createResult = BuildResepMidware(noUrutResep, item, sep, ppk.ToRefference(), layanan);
            listResult.Add(createResult);
        }

        //  WRITE
        using var trans = TransHelper.NewScope();
        foreach (var item in listResult.Where(x => x.IsCreated))
        {
            _ = _writer.Save(item.ResepMidware);
            //item.ResepMidware.ResepMidwareId = writeResult.ResepMidwareId;
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
        SepType sep, PpkRefference ppk, LayananType layanan)
    {
        var resepMidware = CreateResepHeader(sep, ppk, layanan);

        var listValidationNote = new List<string>();
        var itemCount = 0;
        foreach (var itemObat in resep.ListItem)
        {
            if (itemObat.ListKomponenRacik.Count == 0)
                resepMidware = AddItemObat(
                    itemObat, resepMidware, 
                    ref itemCount, ref listValidationNote);
            else            
                foreach (var itemRacik in itemObat.ListKomponenRacik)
                    resepMidware = AddItemRacik(
                        itemRacik, resepMidware, itemObat.Signa, itemObat.BrgId, 
                        ref itemCount, ref listValidationNote);
        }

        var listValidationNoteStr = string.Join("\n", listValidationNote);
        if (listValidationNote.Count == itemCount)
            return new ResepRsValidateResponseDto(noUrut, 
                resepMidware, false, listValidationNoteStr);

        return new ResepRsValidateResponseDto(noUrut,
            resepMidware, true, listValidationNoteStr);
    }
    
    private ResepMidwareModel CreateResepHeader(SepType sep, 
        PpkRefference ppk, LayananType layanan)
    {
        var result = new ResepMidwareModel(_dateTime.Now, 1, sep, ppk, layanan.PoliBpjs);
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
        {
            listValidationNote.Add($"'{itemObat.BrgName}' tidak masuk dalam daftar DPHO");
            return resepMidware;
        }
        var resultType = resepMidware.AddObat(mapDpho, itemObat.Signa, itemObat.Qty);
        if (resultType.IsFailed)
            listValidationNote.Add(resultType.ErrorMessage);
        return resepMidware;
    }

    private ResepMidwareModel AddItemRacik(ResepRsValidateCommandKomponenRacik itemRacik,
        ResepMidwareModel resepMidware,
        string signa,
        string jenisRacik,
        ref int itemCount,
        ref List<string> listValidationNote)
    {
        itemCount++;
        var mapDpho = _mapDphoGetService.Execute(itemRacik);
        var resultType = resepMidware.AddRacik(mapDpho, signa, itemRacik.Qty, jenisRacik);
        if (resultType.IsFailed)
            listValidationNote.Add(resultType.ErrorMessage);
        return resepMidware;
    }
}

