using AptOnline.Application.SepContext;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext;
using MediatR;
using Nuna.Lib.TransactionHelper;

namespace AptOnline.Application.EKlaimContext;

public record EKlaimCreateCommand(string RegId) : IRequest<EKlaimCreateResponse>;

public record EKlaimCreateResponse(string EKlaimId, string NoSep, PasienType Pasien); 

public class EKlaimCreateHandler : IRequestHandler<EKlaimCreateCommand, EKlaimCreateResponse>
{
    private readonly ISepGetByRegService _sepGetByRegService;
    private readonly IEKlaimRepo _ieKlaimRepo;
    
    public EKlaimCreateHandler(ISepGetByRegService sepGetByRegService, 
        IEKlaimRepo ieKlaimRepo)
    {
        _sepGetByRegService = sepGetByRegService;
        _ieKlaimRepo = ieKlaimRepo;
    }

    public Task<EKlaimCreateResponse> Handle(EKlaimCreateCommand request, CancellationToken cancellationToken)
    {
        //  BUILD
        var sep = _sepGetByRegService.Execute(RegType.Key(request.RegId))
            .GetValueOrThrow($"SEP utk register '{request.RegId}' tidak ditemukan");
        var eKlaim = EKlaimModel.CreateFromSep(sep, DateTime.Now);
        
        //  WRITE
        using var trans = TransHelper.NewScope();
        _ieKlaimRepo.Insert(eKlaim);
        trans.Complete();
        
        //  RESPONSE
        var result = new EKlaimCreateResponse(
            eKlaim.EKlaimId, eKlaim.Sep.SepNo, eKlaim.Pasien);
        return Task.FromResult(result);
    }
}