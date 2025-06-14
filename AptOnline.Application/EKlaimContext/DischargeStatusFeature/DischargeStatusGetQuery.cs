using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.DischargeStatusFeature;

public record DischargeStatusGetQuery(string DischargeStatusId) : IRequest<DischargeStatusGetResponse>;

public record DischargeStatusGetResponse(string DischargeStatusId, string DischargeStatusName);

public class DischargeStatusGet : IRequestHandler<DischargeStatusGetQuery, DischargeStatusGetResponse>
{
    private readonly IDischargeStatusDal _dischargeStatusDal;

    public DischargeStatusGet(IDischargeStatusDal dischargeStatusDal)
    {
        _dischargeStatusDal = dischargeStatusDal;
    }

    public Task<DischargeStatusGetResponse> Handle(DischargeStatusGetQuery request, CancellationToken cancellationToken)
    {
        var result = _dischargeStatusDal
            .GetData(DischargeStatusType.Key(request.DischargeStatusId))
            .Map(x => new DischargeStatusGetResponse(x.DischargeStatusId, x.DischargeStatusName))
            .GetValueOrThrow($"Discharge Status '{request.DischargeStatusId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}