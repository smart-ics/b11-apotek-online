using AptOnline.Application.EklaimContext;
using AptOnline.Application.SepContext;
using AptOnline.Application.SepContext.SepFeature;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.SepContext.SepFeature;
using MediatR;
using Nuna.Lib.TransactionHelper;

namespace AptOnline.Application.EKlaimContext;

public record EKlaimCreateCommand(string RegId) : IRequest<EKlaimCreateResponse>;

public record EKlaimCreateResponse(string RegId, PasienType Pasien, string NoSep, string EKlaimId); 

public class EKlaimCreateHandler : IRequestHandler<EKlaimCreateCommand, EKlaimCreateResponse>
{
    private readonly ISepDal _sepDal;
    private readonly IEKlaimRepo _eKlaimRepo;
    private readonly IEKlaimNewClaimService _eKlaimNewClaimService;
    
    public EKlaimCreateHandler(ISepDal sepDal, 
        IEKlaimRepo ieKlaimRepo, IEKlaimNewClaimService eKlaimNewClaimService)
    {
        _sepDal = sepDal;
        _eKlaimRepo = ieKlaimRepo;
        _eKlaimNewClaimService = eKlaimNewClaimService;
    }

    public Task<EKlaimCreateResponse> Handle(EKlaimCreateCommand request, CancellationToken cancellationToken)
    {
        //  GUARD
        var sep = _sepDal.GetData(RegType.Key(request.RegId))
            .GetValueOrThrow($"SEP utk register '{request.RegId}' tidak ditemukan");
        var existingEKlaim = _eKlaimRepo.GetData(SepType.Key(sep.SepNo));
        if (existingEKlaim.HasValue)
            throw new ArgumentException($"Register '{request.RegId}' sudah memiliki eKlaim dengan Nomor '{existingEKlaim.Value.EKlaimId}'");

        //  BUILD
        var eKlaim = EKlaimModel.CreateFromSep(sep, DateTime.Now);
        
        //  WRITE
        using var trans = TransHelper.NewScope();
        _eKlaimRepo.Insert(eKlaim);
        _eKlaimNewClaimService.Execute(eKlaim);
        trans.Complete();
        
        //  RESPONSE
        var result = new EKlaimCreateResponse(
            eKlaim.Reg.RegId, eKlaim.Pasien, eKlaim.EKlaimId, eKlaim.Sep.SepNo);
        return Task.FromResult(result);
    }
}