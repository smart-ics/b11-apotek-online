using AptOnline.Domain.EmrContext.ResepRsAgg;
using MediatR;

namespace AptOnline.Application.EmrContext.ResepRsAgg
{
    public record ResepRsGetDataQuery(string ResepId) : IRequest<ResepRsModel>, IResepRsKey;

    public class ResepRsGetDataHandler : IRequestHandler<ResepRsGetDataQuery, ResepRsModel>
    {
        private readonly IResepRsGetDataService _resepRsRepository;
        public ResepRsGetDataHandler(IResepRsGetDataService resepRsRepository)
        {
            _resepRsRepository = resepRsRepository;
        }

        public Task<ResepRsModel> Handle(ResepRsGetDataQuery request, CancellationToken cancellationToken)
        {
            var result = _resepRsRepository.Execute(request);
            return Task.FromResult(result);
        }
    }
}
