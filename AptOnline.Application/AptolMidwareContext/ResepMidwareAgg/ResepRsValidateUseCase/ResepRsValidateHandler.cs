using AptOnline.Application.AptolCloudContext.PpkAgg;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Application.SepContext;
using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.SepContext.SepFeature;
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
            .GetValueOrThrow($"SEP for register {request.RegId} not found");
        if ((DateTime.Now - sep.SepDateTime).Days > 15)
            throw new Exception($"Resep melebihi 15 hari dari SEP No. {sep.SepNo} ({sep.SepDateTime:dd-MM-yyyy})");
        var ppk = _ppkGetService.Execute()
            ?? throw new KeyNotFoundException($"Setting Faskes not found");
        var layanan = sep.JenisPelayananId switch
        {
            "1" => new LayananType("IGD", "IGD", true, new PoliBpjsType("IGD", "IGD")),
            "2" => _layananGetService.Execute(request),
            _ => throw new KeyNotFoundException("Jenis Pelayanan SEP invalid"),
        } ?? throw new KeyNotFoundException($"Layanan {request.LayananId} not found");

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
        var resepMidware = CreateResepHeader(resep, sep, ppk, layanan);
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
        if (itemCount > 0)
            return new ResepRsValidateResponseDto(noUrut,
            resepMidware, true, listValidationNoteStr);

        return new ResepRsValidateResponseDto(noUrut,
            resepMidware, false, listValidationNoteStr);
    }
    
    private ResepMidwareModel CreateResepHeader(ResepRsValidateCommandResep resep, SepType sep, 
        PpkRefference ppk, LayananType layanan)
    {
        int iterasi = resep.ListItem.Any(x => x.Iter > 0) ? 1 : 0;
        var result = new ResepMidwareModel(_dateTime.Now, iterasi, sep, ppk, layanan.PoliBpjs);
        return result;
    }
    
    private ResepMidwareModel AddItemObat(ResepRsValidateCommandObat itemObat,
        ResepMidwareModel resepMidware,
        ref int itemCount, 
        ref List<string> listValidationNote)
    {
        //itemCount++;
        var mapDpho = _mapDphoGetService.Execute(itemObat);
        if (mapDpho is null)
            return resepMidware;
        //if (mapDpho is not null)
        //{
            itemCount++;
            listValidationNote.Add($"'{itemObat.BrgName}' termasuk dalam DPHO");
            //return resepMidware;
        //}
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
        //itemCount++;
        var mapDpho = _mapDphoGetService.Execute(itemRacik);
        if (mapDpho is null)
            return resepMidware;
        //if (mapDpho is not null)
        //{
            itemCount++;
            listValidationNote.Add($"'{itemRacik.BrgName}' termasuk dalam DPHO");
            //return resepMidware;
        //}        
        var resultType = resepMidware.AddRacik(mapDpho, signa, itemRacik.Qty, jenisRacik);
        if (resultType.IsFailed)
            listValidationNote.Add(resultType.ErrorMessage);
        return resepMidware;
    }
}

