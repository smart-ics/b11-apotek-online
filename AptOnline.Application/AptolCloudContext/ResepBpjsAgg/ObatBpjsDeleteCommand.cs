using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.EmrContext.ResepRsAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using MediatR;


namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
public record ObatBpjsDeleteCommand(string ResepId, string BarangId) : IRequest<ObatBpjsDeleteResponse>, IResepRsKey;

public class ObatBpjsDeleteHandler : IRequestHandler<ObatBpjsDeleteCommand, ObatBpjsDeleteResponse>
{
    private readonly IResepMidwareBuilder _builder;
    private readonly IResepMidwareWriter _writer;
    private readonly IObatBpjsDeleteService _obatBpjsDeleteService;
    private readonly IResepMidwareDal _resepMidwareDal;

    public ObatBpjsDeleteHandler(IResepMidwareBuilder builder,
        IObatBpjsDeleteService obatBpjsDeleteService,
        IResepMidwareWriter writer,
        IResepMidwareDal resepMidwareDal)
    {
        _builder = builder;
        _obatBpjsDeleteService = obatBpjsDeleteService;
        _writer = writer;
        _resepMidwareDal = resepMidwareDal;
    }

    public Task<ObatBpjsDeleteResponse> Handle(ObatBpjsDeleteCommand request, CancellationToken cancellationToken)
    {
        //build
        var resepMidwareDb = _resepMidwareDal.GetData(new ResepRsModel { ResepId = request.ResepId });
        var resepMidware = _builder.Load(resepMidwareDb).Build() ??
            throw new KeyNotFoundException("Resep not found or invalid");
        if(resepMidware.BridgeState.Equals("CREATED"))
            throw new KeyNotFoundException("Status Resep belum CONFIRMED");
        var noApotik = resepMidware.ReffId;
        var noSep = resepMidware.Sep.SepNo;
        var obat = resepMidware.ListItem.Where(x => x.Brg.BrgId == request.BarangId).FirstOrDefault()??
            throw new KeyNotFoundException("Obat not found or invalid");
        bool isSuccess = true;
        var deleteNote = string.Empty;
        if (noApotik.Trim().Length > 0)
        {
            var param = new ObatBpjsDeleteParam(noSep, noApotik, resepMidware.ResepBpjsNo, obat);
            var del = _obatBpjsDeleteService.Execute(param);
            if (!del.RespCode.Equals("200"))
            {
                isSuccess = false;
                deleteNote = del.RespMessage;
            }
        }
        resepMidware.ListItem.Remove(obat);
        _writer.Save(resepMidware);
        var resp = new ObatBpjsDeleteResponse(request.BarangId, noApotik, isSuccess ? "SUCCESS" : "FAILED", deleteNote);
        return Task.FromResult(resp);
    }
}
