using AptOnline.Application.AptolCloudContext.FaskesAgg;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using MediatR;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

#region COMMAND
public record ResepRsValidateCommand(
    string RegId, string LayananId, 
    List<ResepRsValidateCommandResep> ListResep) 
    : IRequest<IEnumerable<ResepRsValidateResponse>>, IRegKey, ILayananKey;

public record ResepRsValidateCommandResep(
    string Description, 
    List<ResepRsValidateCommandObat> ListItem);

public record ResepRsValidateCommandObat(
    string BrgId, string BrgName, int Qty,
    string Signa, string CaraPakai, int Iter,
    List<ResepRsValidateCommandKomponenRacik> ListKomponenRacik);

public record ResepRsValidateCommandKomponenRacik(
    string BrgId, string BrgName, int Qty,
    string Signa, string CaraPakai);
#endregion

#region RESPONSE
public record ResepRsValidateResponse(
    int NoUrut, string ResepMidwareId);
#endregion

public class ResepRsValidateCommandHandler : 
    IRequestHandler<ResepRsValidateCommand, IEnumerable<ResepRsValidateResponse>>
{
    private readonly IResepMidwareBuilder _builder;
    private readonly IRegGetService _regGetService;
    private readonly ISepGetService _sepGetService;
    private readonly IFaskesGetService _faskesGetService;
    private readonly ILayananGetService _layananGetService;
    

    public ResepRsValidateCommandHandler(IResepMidwareBuilder builder, 
        IRegGetService regGetService, 
        ISepGetService sepGetService, 
        IFaskesGetService faskesGetService, 
        ILayananGetService layananGetService)
    {
        _builder = builder;
        _regGetService = regGetService;
        _sepGetService = sepGetService;
        _faskesGetService = faskesGetService;
        _layananGetService = layananGetService;
    }

    public Task<IEnumerable<ResepRsValidateResponse>> Handle(
        ResepRsValidateCommand request, CancellationToken cancellationToken)
    {
        var reg = _regGetService.Execute(request)
            ?? throw new KeyNotFoundException($"Register {request.RegId} not found");
        var sep = _sepGetService.Execute(reg)
            ?? throw new KeyNotFoundException($"Sep {request.RegId} not found");
        var faskes = _faskesGetService.Execute()
            ?? throw new KeyNotFoundException($"Setting Faskes not found");
        var layanan = _layananGetService.Execute(request)
            ?? throw new KeyNotFoundException($"Layanan {request.LayananId} not found");
        
        var listResepMidware = new List<ResepMidwareModel>();
        foreach (var item in request.ListResep)
        {
            var resepMidware = BuildResepMidware(item, reg, sep, faskes, layanan);
            listResepMidware.Add(resepMidware);
        }

        throw new NotImplementedException();
    }

    private ResepMidwareModel BuildResepMidware(ResepRsValidateCommandResep item, 
        RegModel reg, SepModel sep, FaskesModel faskes, LayananModel layanan)
    {
        var resepMidware = new ResepMidwareModel();
        resepMidware.SetRegister(reg);
        resepMidware.SetSep(sep);
        resepMidware.SetFaskes(faskes);
        resepMidware.SetPoliBpjs(layanan);
            
        throw new NotImplementedException();
    }
}



