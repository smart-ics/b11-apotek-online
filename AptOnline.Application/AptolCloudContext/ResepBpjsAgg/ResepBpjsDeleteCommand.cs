using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Domain.EmrContext.ResepRsAgg;
using MediatR;


namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
public record ResepBpjsDeleteCommand(string ResepId) : IRequest<ResepBpjsDeleteResponse>, IResepRsKey;

public class ResepBpjsDeleteHandler : IRequestHandler<ResepBpjsDeleteCommand, ResepBpjsDeleteResponse>
{
    private readonly IResepMidwareBuilder _builder;
    private readonly IResepMidwareWriter _writer;
    private readonly IResepBpjsDeleteService _resepBpjsDeleteService;
    private readonly IObatBpjsInsertService _obatBpjsInsertService;
    private readonly IResepMidwareDal _resepMidwareDal;

    public ResepBpjsDeleteHandler(IResepMidwareBuilder builder,
        IResepBpjsDeleteService resepBpjsDeleteService,
        IObatBpjsInsertService obatBpjsInsertService,
        IResepMidwareWriter writer,
        IResepMidwareDal resepMidwareDal)
    {
        _builder = builder;
        _resepBpjsDeleteService = resepBpjsDeleteService;
        _obatBpjsInsertService = obatBpjsInsertService;
        _writer = writer;
        _resepMidwareDal = resepMidwareDal;
    }

    public Task<ResepBpjsDeleteResponse> Handle(ResepBpjsDeleteCommand request, CancellationToken cancellationToken)
    {
        //build
        var resepMidwareDb = _resepMidwareDal.GetData(new ResepRsModel { ResepId = request.ResepId });
        var resepMidware = _builder.Load(resepMidwareDb).Build() ??
            throw new KeyNotFoundException("Resep not found or invalid");
        if(resepMidware.BridgeState.Equals("CREATED"))
            throw new KeyNotFoundException("Status Resep belum CONFIRMED");
        var noApotik = resepMidware.ReffId;
        var noSep = resepMidware.Sep.SepNo;
        var noResep = resepMidware.ResepBpjsNo;
        bool isSuccess = true;
        var deleteNote = string.Empty;
        if (noApotik.Trim().Length > 0)
        {
            var param = new ResepBpjsDeleteParam(noSep, noApotik, noResep);
            var del = _resepBpjsDeleteService.Execute(param);
            if (!del.RespCode.Equals("200"))
            {
                isSuccess = false;
                deleteNote = del.RespMessage;
            }
        }
        _writer.Delete(resepMidware);
        var resp = new ResepBpjsDeleteResponse(request.ResepId, noApotik, isSuccess ? "SUCCESS" : "FAILED", deleteNote);
        return Task.FromResult(resp);
    }
}
