using MediatR;

namespace AptOnline.Application.EKlaimContext.DischargeStatusFeature;

public record DischargeStatusListQuery : IRequest<List<DischargeStatusListResponse>>;

public record DischargeStatusListResponse(string DischargeStatusId, string DischargeStatusName);

public class DischargeStatusListHandler : IRequestHandler<DischargeStatusListQuery, List<DischargeStatusListResponse>>
{
    private readonly IDischargeStatusDal _dischargeStatusDal;

    public DischargeStatusListHandler(IDischargeStatusDal dischargeStatusDal)
    {
        _dischargeStatusDal = dischargeStatusDal;
    }

    public Task<List<DischargeStatusListResponse>> Handle(DischargeStatusListQuery request, CancellationToken cancellationToken)
    {
        var result = _dischargeStatusDal
            .ListData()
            .Map(x => x.Select(y => new DischargeStatusListResponse(y.DischargeStatusId, y.DischargeStatusName)).ToList())
            .GetValueOrThrow("Discharge tidak ditemukan");

        return Task.FromResult(result);
    }
}