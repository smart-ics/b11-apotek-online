using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using MediatR;

namespace AptOnline.Application.SepContext.AssesmentPelayananFeature;

public record AssesmentPelayananGetQuery(string AssesmentPelayananId) : IRequest<AssesmentPelayananGetResponse>;

public record AssesmentPelayananGetResponse(string AssesmentPelayananId, string AssesmentPelayananName);

public class AssesmentPelayananGetHandler : IRequestHandler<AssesmentPelayananGetQuery, AssesmentPelayananGetResponse>
{
    private readonly IAssesmentPelayananDal _assesmentPelayananDal;

    public AssesmentPelayananGetHandler(IAssesmentPelayananDal assesmentPelayananDal)
    {
        _assesmentPelayananDal = assesmentPelayananDal;
    }

    public Task<AssesmentPelayananGetResponse> Handle(AssesmentPelayananGetQuery request, CancellationToken cancellationToken)
    {
        var result = _assesmentPelayananDal
            .GetData(AssesmentPelayananType.Key(request.AssesmentPelayananId))
            .Map(x => new AssesmentPelayananGetResponse(x.AssesmentPelayananId, x.AssesmentPelayananName))
            .GetValueOrThrow($"Assesment Pelayanan '{request.AssesmentPelayananId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}