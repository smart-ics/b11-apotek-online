using AptOnline.Domain.EKlaimContext.Covid19Feature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public record Covid19StatusGetQuery(string Covid19StatusId) : IRequest<Covid19StatusGetResponse>;

public record Covid19StatusGetResponse(string Covid19StatusId, string Covid19StatusName);

public class Covid19StatusGet : IRequestHandler<Covid19StatusGetQuery, Covid19StatusGetResponse>
{
    private readonly ICovid19StatusDal _covid19StatusDal;

    public Covid19StatusGet(ICovid19StatusDal covid19StatusDal)
    {
        _covid19StatusDal = covid19StatusDal;
    }

    public Task<Covid19StatusGetResponse> Handle(Covid19StatusGetQuery request, CancellationToken cancellationToken)
    {
        var result = _covid19StatusDal
            .GetData(Covid19StatusType.Key(request.Covid19StatusId))
            .Map(x => new Covid19StatusGetResponse(x.Covid19StatusId, x.Covid19StatusName))
            .GetValueOrThrow($"Status Covid-19 '{request.Covid19StatusId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}