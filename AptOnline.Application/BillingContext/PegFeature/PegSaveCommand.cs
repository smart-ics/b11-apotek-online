using AptOnline.Domain.BillingContext.PegFeature;
using MediatR;

namespace AptOnline.Application.BillingContext.PegFeature;

public record PegSaveCommand(string PegName, string Nik) : IRequest<PegSaveResponse>;

public record PegSaveResponse(string PegId, string PegName, string Nik);

public class PegSaveHandler : IRequestHandler<PegSaveCommand, PegSaveResponse>
{
    private readonly IPegDal _pegDal;
    
    public PegSaveHandler(IPegDal pegDal)
    {
        _pegDal = pegDal;
    }

    public Task<PegSaveResponse> Handle(PegSaveCommand request, CancellationToken cancellationToken)
    {
        var existingPeg  = _pegDal.GetData(request.Nik);
        var peg = existingPeg.HasValue 
            ? new PegType(existingPeg.Value.PegId, request.PegName, request.Nik)  
            : PegType.Create(request.PegName, request.Nik);
        
        if (existingPeg.HasValue)
            _pegDal.Update(peg);
        else
            _pegDal.Insert(peg);

        var result = new PegSaveResponse(peg.PegId, peg.PegName, peg.Nik);
        return Task.FromResult(result);
    }
}