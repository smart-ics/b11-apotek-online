using AptOnline.Application.PharmacyContext.PenjualanAgg;
using AptOnline.Domain.EmrContext.ResepRsAgg;
using AptOnline.Domain.PharmacyContext.TrsDuAgg;
using MediatR;

namespace AptOnline.Application.EmrContext.ResepRsAgg
{
    public record PenjualanGetDataQuery(string PenjualanId) : IRequest<PenjualanModel>, IPenjualanKey;

    public class PenjualanGetDataHandler : IRequestHandler<PenjualanGetDataQuery, PenjualanModel>
    {
        private readonly IPenjualanGetDataService _penjualanRepository;
        public PenjualanGetDataHandler(IPenjualanGetDataService penjualanRepository)
        {
            _penjualanRepository = penjualanRepository;
        }

        public Task<PenjualanModel> Handle(PenjualanGetDataQuery request, CancellationToken cancellationToken)
        {
            var result = _penjualanRepository.Execute(request);
            return Task.FromResult(result);
        }
    }
}
