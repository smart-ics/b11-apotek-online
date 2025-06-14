using MediatR;

namespace AptOnline.Application.EKlaimContext.DischargeStatusFeature;

public record DischargeStatusListQuery() : IRequest<List<DischargeStatusGetResponse>>;

public record DischargeStatusListResponse(string DischargeStatusId, string DischargeStatusName);

public class DischargeStatusListHandler : IRequestHandler<DischargeStatusListQuery, List<DischargeStatusGetResponse>>
{
    private readonly IDischargeStatusDal _dischargeStatusDal;

    public DischargeStatusListHandler(IDischargeStatusDal dischargeStatusDal)
    {
        _dischargeStatusDal = dischargeStatusDal;
    }

    public Task<List<DischargeStatusGetResponse>> Handle(DischargeStatusListQuery request, CancellationToken cancellationToken)
    {
        var result = _dischargeStatusDal
            .ListData()
            .Map(x => x.Select(y => new DischargeStatusGetResponse(y.DischargeStatusId, y.DischargeStatusName)).ToList())
            .GetValueOrThrow("Discharge tidak ditemukan");

        return Task.FromResult(result);
    }
}