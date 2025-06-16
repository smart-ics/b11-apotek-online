using AptOnline.Application.SepContext.JenisPelayananFeature;
using MediatR;

namespace AptOnline.Application.SepContext.AssesmentPelayananFeature;

public record AssesmentPelayananListQuery : IRequest<List<AssesmentPelayananListResponse>>;

public record AssesmentPelayananListResponse(string AssesmentPelayananId, string AssesmentPelayananName);

public class AssesmentPelayananListHandler : IRequestHandler<AssesmentPelayananListQuery, List<AssesmentPelayananListResponse>>
{
    private readonly IAssesmentPelayananDal _assesmentPelayananDal;

    public AssesmentPelayananListHandler(IAssesmentPelayananDal assesmentPelayananDal)
    {
        _assesmentPelayananDal = assesmentPelayananDal;
    }

    public Task<List<AssesmentPelayananListResponse>> Handle(AssesmentPelayananListQuery request, CancellationToken cancellationToken)
    {
        var result = _assesmentPelayananDal
            .ListData()
            .Map(x => x.Select(y => new AssesmentPelayananListResponse(y.AssesmentPelayananId, y.AssesmentPelayananName)).ToList())
            .GetValueOrThrow("Assesment Pelayanan tidak ditemukan");

        return Task.FromResult(result);
    }
}