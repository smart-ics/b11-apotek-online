using MediatR;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public record Covid19StatusListQuery() : IRequest<List<Covid19StatusGetResponse>>;

public record Covid19StatusListResponse(string Covid19StatusId, string Covid19StatusName);

public class Covid19StatusListHandler : IRequestHandler<Covid19StatusListQuery, List<Covid19StatusGetResponse>>
{
    private readonly ICovid19StatusDal _covid19StatusDal;

    public Covid19StatusListHandler(ICovid19StatusDal covid19StatusDal)
    {
        _covid19StatusDal = covid19StatusDal;
    }

    public Task<List<Covid19StatusGetResponse>> Handle(Covid19StatusListQuery request, CancellationToken cancellationToken)
    {
        var result = _covid19StatusDal
            .ListData()
            .Map(x => x.Select(y => new Covid19StatusGetResponse(y.Covid19StatusId, y.Covid19StatusName)).ToList())
            .GetValueOrThrow("Cara Masuk tidak ditemukan");

        return Task.FromResult(result);
    }
}