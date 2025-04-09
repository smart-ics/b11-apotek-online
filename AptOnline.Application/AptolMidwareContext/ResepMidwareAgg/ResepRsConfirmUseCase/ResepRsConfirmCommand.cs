using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using FluentAssertions;
using MediatR;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsConfirmUseCase;

public record ResepRsConfirmCommand(string ResepMidwareId, string ChartId, string ResepRsId) : IRequest, IResepMidwareKey;

public class ResepRsConfirmHandler : IRequestHandler<ResepRsConfirmCommand>
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

    public Task<Unit> Handle(ResepRsConfirmCommand request, CancellationToken cancellationToken)
    {
        var resep = _builder.Load(request).Build();
        resep.Confirm(_tgalJamProvider.Now);
        resep.ChartRefference(request.ChartId, request.ResepRsId);
        _writer.Save(resep);
        return Task.FromResult(Unit.Value);
    }
}