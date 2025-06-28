using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using MediatR;


namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
public record ResepBpjsSaveCommand(string ResepMidwareId) : IRequest<ResepBpjsSaveResponse>, IResepMidwareKey;

public class ResepBpjsSaveHandler : IRequestHandler<ResepBpjsSaveCommand, ResepBpjsSaveResponse>
{
    private readonly IResepMidwareBuilder _builder;
    private readonly IResepMidwareWriter _writer;
    private readonly IResepBpjsSaveService _resepBpjsSaveService;
    private readonly IObatBpjsInsertService _obatBpjsInsertService;

    public ResepBpjsSaveHandler(IResepMidwareBuilder builder,
        IResepBpjsSaveService resepBpjsSaveService,
        IObatBpjsInsertService obatBpjsInsertService,
        IResepMidwareWriter writer)
    {
        _builder = builder;
        _resepBpjsSaveService = resepBpjsSaveService;
        _obatBpjsInsertService = obatBpjsInsertService;
        _writer = writer;
    }

    public Task<ResepBpjsSaveResponse> Handle(ResepBpjsSaveCommand request, CancellationToken cancellationToken)
    {
        //build
        var resepMidware = _builder.Load(request).Build() ??
            throw new KeyNotFoundException("Resep not found or invalid");
        //jika sep tsb sudah pernah kirim header ke bpjs, ambil no apotik
        if(resepMidware.BridgeState.Equals("CREATED"))
            throw new KeyNotFoundException("Status Resep belum CONFIRMED");
        var noApotik = resepMidware.ReffId;
        var noSep = resepMidware.Sep.SepNo;
        var noResep = resepMidware.ResepBpjsNo;
        //jika belum pernah kirim header ke bpjs
        if (noApotik.Trim().Length == 0)
        {
            var resepBpjs = _resepBpjsSaveService.Execute(resepMidware);
            noSep = resepBpjs.NoSep;

            noApotik = resepBpjs.NoApotik;
            resepMidware.Sent(noApotik, DateTime.Now);
        }
        
        var saveNote = string.Empty;
        var saveResult = string.Empty;
        bool isLanjut = true;
        var errMsg = string.Empty;
        foreach (var item in resepMidware.ListItem)
        {
            if (item.IsUploaded) 
                continue;
            var reqParam = new ObatBpjsInsertParam (noSep, noApotik, noResep, item);
            var x = _obatBpjsInsertService.Execute(reqParam);
            switch (x.RespCode)
            {
                case "200":
                    item.SetUploaded();
                    if (saveResult.Equals(string.Empty)) saveResult = "SUCCESS";
                    break;
                case "201":
                    saveResult = "FAILED";
                    item.SetNote(x.RespMessage);
                    saveNote += $"{x.ObatId}: {x.RespMessage}{Environment.NewLine}";
                    break;
                //case "404":
                //    throw new KeyNotFoundException($"{request.ResepMidwareId} - {x.RespMessage}");
                default:
                    isLanjut = false;
                    errMsg = $"Insert Obat BPJS\n({x.RespCode}) {x.RespMessage}";
                    break;                  
            }
            if(!isLanjut) 
                break;
        }
        _writer.Save(resepMidware);
        if (!isLanjut) 
            throw new Exception(errMsg);
        //response
        var resp = new ResepBpjsSaveResponse(request.ResepMidwareId, resepMidware.ResepRsId, 
            noApotik, saveResult, saveNote);
        return Task.FromResult(resp);
    }
}
