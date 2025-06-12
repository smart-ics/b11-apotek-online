using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using FluentAssertions;
using MediatR;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;
using static AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsConfirmUseCase.ResepRsConfirmHandler;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsConfirmUseCase;

public record ResepRsConfirmCommand(string ResepMidwareId, string ChartId, string ResepRsId) : 
    IRequest<ResepRsConfirmResponse>, IResepMidwareKey;

public class ResepRsConfirmHandler : IRequestHandler<ResepRsConfirmCommand,ResepRsConfirmResponse>
{
    private readonly IResepMidwareBuilder _builder;
    private readonly ITglJamProvider _tgalJamProvider;
    private readonly IResepMidwareWriter _writer;

    public ResepRsConfirmHandler(IResepMidwareBuilder builder, 
        ITglJamProvider tgalJamProvider, 
        IResepMidwareWriter writer)
    {
        _builder = builder;
        _tgalJamProvider = tgalJamProvider;
        _writer = writer;
    }

    public Task<ResepRsConfirmResponse> Handle(ResepRsConfirmCommand request, CancellationToken cancellationToken)
    {
        var resep = _builder.Load(request).Build()??
            throw new KeyNotFoundException("Resep tidak ditemukan");
        if (resep.BridgeState.Equals("CREATED"))
        {
            resep.Confirm(_tgalJamProvider.Now);
            resep.ChartRefference(request.ChartId, request.ResepRsId);
            _writer.Save(resep);
        }
        var resp = new ResepRsConfirmResponse { ResepMidwareId = request.ResepMidwareId, BridgeState = resep.BridgeState };
        return Task.FromResult(resp);
    }
    public class ResepRsConfirmResponse
    {
        public string ResepMidwareId { get; set; }
        public string BridgeState { get; set; }
    }
}