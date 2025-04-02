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

public class ResepRsValidateCommandHandler :
    IRequestHandler<ResepRsValidateCommand, IEnumerable<ResepRsValidateResponse>>
{
    private readonly IResepMidwareWriter _writer;
    private readonly IRegGetService _regGetService;
    private readonly ISepGetService _sepGetService;
    private readonly IFaskesGetService _faskesGetService;
    private readonly ILayananGetService _layananGetService;
    private readonly IMapDphoGetService _mapDphoGetService;


    public ResepRsValidateCommandHandler(
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
        var listResult = new List<ResepMidwareResultDto>();
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

    private ResepMidwareResultDto BuildResepMidware(int noUrut,
        ResepRsValidateCommandResep resep,
        RegModel reg, SepModel sep, FaskesModel faskes, LayananModel layanan)
    {
        var resepMidware = new ResepMidwareModel();
        resepMidware.SetRegister(reg);
        resepMidware.SetSep(sep);
        resepMidware.SetFaskes(faskes);
        resepMidware.SetPoliBpjs(layanan);
        var listValidationNote = new List<string>();
        var itemCount = 0;
        foreach (var item in resep.ListItem)
        {
            itemCount++;
            var mapDpho = _mapDphoGetService.Execute(item);
            if (mapDpho is null)
                continue;
            var result = resepMidware.AddObat(mapDpho, item.Signa, item.Qty);
            if (result.IsFailed)
            {
                listValidationNote.Add(result.ErrorMessage);
                continue;
            }

            foreach (var itemRacik in item.ListKomponenRacik)
            {
                itemCount++;
                mapDpho = _mapDphoGetService.Execute(itemRacik);
                result = resepMidware.AddRacik(mapDpho, item.Signa, itemRacik.Qty);
                if (result.IsFailed)
                    listValidationNote.Add(result.ErrorMessage);
            }
        }

        var listValidationNoteStr = string.Join(", ", listValidationNote);
        if (listValidationNote.Count == itemCount)
            return new ResepMidwareResultDto(noUrut, 
                resepMidware, false, listValidationNoteStr);
        else
            return new ResepMidwareResultDto(noUrut,
                resepMidware, true, listValidationNoteStr);
    }
}
public record ResepMidwareResultDto(
    int NoUrut,
    ResepMidwareModel ResepMidware,
    bool IsCreated,
    string ValidationNote);