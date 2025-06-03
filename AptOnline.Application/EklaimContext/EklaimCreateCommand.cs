using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.EklaimContext;
using AptOnline.Application.SepContext;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.EKlaimContext;
using MediatR;

namespace AptOnline.Application.EKlaimContext;

public record EklaimCreateCommand(string RegId) : IRequest<EKlaimCreateResponse>;

public record EKlaimCreateResponse(string EKlaimId, string NoSep, PasienType Pasien); 

public class EKlaimCreateHandler : IRequestHandler<EklaimCreateCommand, EKlaimCreateResponse>
{
    private readonly ISepGetByRegService _sepGetByRegService;
    private readonly IRegGetService _regGetService;
    private readonly IPasienGetService _pasienGetSerivce;
    private readonly IEklaimRepo _eklaimRepo;
    private readonly IEklaimNewClaimService _eKlaimNewClaimService;
    
    public EKlaimCreateHandler(ISepGetByRegService sepGetByRegService, 
        IRegGetService regGetService, 
        IPasienGetService pasienGetSerivce, 
        IEklaimRepo eklaimRepo, 
        IEklaimNewClaimService eKlaimNewClaimService)
    {
        _sepGetByRegService = sepGetByRegService;
        _regGetService = regGetService;
        _pasienGetSerivce = pasienGetSerivce;
        _eklaimRepo = eklaimRepo;
        _eKlaimNewClaimService = eKlaimNewClaimService;
    }

    public Task<EKlaimCreateResponse> Handle(EklaimCreateCommand request, CancellationToken cancellationToken)
    {
        // var sep = _sepGetByNoSepService.Execute(request.NoSep);
        // var reg = _regGetService.Execute(sep);
        // var pasien = _pasienGetSerivce.Execute(reg);
        //
        // var eklaim = EklaimModel.Create(DateTime.Now, sep, pasien);
        // _eklaimRepo.Insert(eklaim);
        // _eKlaimNewClaimService.Execute(eklaim);    
        //
        // var result = new EKlaimCreateResponse(
        //     eklaim.EklaimId, eklaim.NomorSep, eklaim.Pasien);
        // return Task.FromResult(result);
        throw new NotImplementedException();
    }
}