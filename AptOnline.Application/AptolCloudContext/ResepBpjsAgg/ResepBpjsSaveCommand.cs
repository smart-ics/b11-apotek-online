using AptOnline.Application.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using MediatR;
using Nuna.Lib.DataTypeExtension;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
//06B60QDMMRZ2F741WTTDNC5KT8
public record ResepBpjsSaveCommand(string ResepMidwareId) : IRequest<ResepBpjsModel>, IResepMidwareKey;

public class ResepBpjsSaveHandler : IRequestHandler<ResepBpjsSaveCommand, ResepBpjsModel>
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

    public Task<ResepBpjsModel> Handle(ResepBpjsSaveCommand request, CancellationToken cancellationToken)
    {
        //build
        var resepMidware = _builder.Load(request).Build();
        //send
        var noApotik = resepMidware.ReffId;
        var noSep = resepMidware.Sep.SepNo;
        if (noApotik.Trim().Length == 0)
        {
            var resepBpjs = _resepBpjsSaveService.Execute(resepMidware);
            noSep = resepBpjs.NoSep;
            noApotik = resepBpjs.NoApotik;
        }

        foreach (var item in resepMidware.ListItem)
        {
            var reqParam = new ObatBpjsInsertParam (noSep, noApotik, item);
            var x = _obatBpjsInsertService.Execute( reqParam);
        }
        //response
        throw new NotImplementedException();
    }
}
/*
public class SendResepToAptolCommand : IRequest<SendResepToAptolCommandResponse>
{
    public string PenjualanId { get; set; }
}
public class SendResepToAptolCommandResponse
{
    public bool Success {  get; set; }
    public string? PenjualanId { get; set; }
    public string? BpjsReffId { get; set; }
    public string? Message { get; set; }
}
public class SendResepToAptolCommandHandler : IRequestHandler<SendResepToAptolCommand, 
    SendResepToAptolCommandResponse>
{
    private readonly IInsertResepBpjsService _insertResepBpjsService;
    private readonly IGetDuFarmasiService _getDuFarmasiService;
    private readonly IGetResepFarmasiService _getResepFarmasiService;
    private readonly IInsertObatBpjsService _sendObatSvc;
    private readonly IGetMapDpho _getDphoSvc;
    private readonly IGetSepBillingService _getSepSvc;
    private readonly IGetLayananBillingService _getLayananService;
    private readonly IGetDokterBillingService _getDokterService;
    private readonly IResepRequestBuilder _resepRequestBuilder;
    private readonly IItemNonRacikBuilder _itemNonRacikBuilder;
    private readonly IItemRacikBuilder _itemRacikBuilder;
    private readonly IResepWriter _resepWiter;

    public SendResepToAptolCommandHandler(IInsertResepBpjsService insertResepBpjsService,
        IGetDuFarmasiService getDuFarmasiService,
        IGetResepFarmasiService getResepFarmasiService,
        IInsertObatBpjsService sendObatSvc,
        IGetMapDpho getDphoSvc,
        IGetSepBillingService getSepSvc,
        IGetLayananBillingService getLayananService,
        IGetDokterBillingService getDokterService,
        IResepRequestBuilder resepRequestBuilder,
        IItemNonRacikBuilder itemNonRacikBuilder,
        IItemRacikBuilder itemRacikBuilder,
        IResepWriter resepWiter)
    {
        _insertResepBpjsService = insertResepBpjsService;
        _getDuFarmasiService = getDuFarmasiService;
        _getResepFarmasiService = getResepFarmasiService;
        _sendObatSvc = sendObatSvc;
        _getDphoSvc = getDphoSvc;
        _getSepSvc = getSepSvc;
        _getLayananService = getLayananService;
        _getDokterService = getDokterService;
        _resepRequestBuilder = resepRequestBuilder;
        _itemNonRacikBuilder = itemNonRacikBuilder;
        _itemRacikBuilder = itemRacikBuilder;
        _resepWiter = resepWiter;
    }
    public Task<SendResepToAptolCommandResponse>? Handle(SendResepToAptolCommand request, 
        CancellationToken cancellationToken)
    {
        // BUILD
        var du = _getDuFarmasiService.Execute(request.PenjualanId) ?? new PenjualanDto();
        var duPenjualan = du.data.Adapt<PenjualanData>();
        if (duPenjualan is null) return null;
        var resep = _getResepFarmasiService.Execute(duPenjualan.resepId);
        if (resep is null) return null;
        var sep = _getSepSvc.Execute(du.data.regId);
        if (sep is null) return null;
        var lyn = _getLayananService.Execute(resep.data.layananId);
        var dokter = _getDokterService.Execute(resep.data.dokterId);
        var resepBpjsReq = _resepRequestBuilder.Build(duPenjualan, resep, sep, lyn, dokter);
           
        if (resepBpjsReq is null)
        {
            LogHelper.Log(request.PenjualanId, "", "", "Build resep request failed");
            return Task.FromResult(new SendResepToAptolCommandResponse
            {
                Success = false,
                PenjualanId = request.PenjualanId,
                BpjsReffId = string.Empty,
                Message = "Build resep request failed"
            });
        }
        //
        var listBarangResep = resep.data.listBarang;
        var listBarangJual = duPenjualan.listBarang;
                
        var listDpho = new List<RespMapDpho>(); 
        foreach (var barang in listBarangJual)
        {
            var dpho = _getDphoSvc.Execute(barang.brgId);
            if (dpho is null) continue;
            listDpho.Add(dpho);
        }
        if (listDpho.IsNullOrEmpty())
        {
            LogHelper.Log(request.PenjualanId, "", "", "DPHO map not found");
            return Task.FromResult(new SendResepToAptolCommandResponse
            {
                Success = false,
                PenjualanId = request.PenjualanId,
                BpjsReffId = string.Empty,
                Message = "DPHO map not found"
            });
        }
            
        //SEND
        //---kirim header resep ke bpjs
        var headerResep = _insertResepBpjsService.Execute(resepBpjsReq);
        if (headerResep.response is null)
        {
            LogHelper.Log(request.PenjualanId, "", "", "Send resep header (BPJS) failed");
            return Task.FromResult(new SendResepToAptolCommandResponse
            {
                Success = false,
                PenjualanId = request.PenjualanId,
                BpjsReffId = string.Empty,
                Message = "Send resep header (BPJS) failed"
            });
        }
                
        //apply repo resep
        var resepModel = new ResepModel
        {
            PenjualanId = request.PenjualanId,
            EntryDate = headerResep.response.tglEntry,
            ReffId = headerResep.response.noApotik,
            SepId = headerResep.response.noSep_Kunjungan,
            FaskesAsal = headerResep.response.faskesAsal,
            NoPeserta = headerResep.response.noKartu,
            ResepDate = DateTime.Parse(headerResep.response.tglResep),
            JenisObatId = headerResep.response.kdJnsObat
        };
        _ = _resepWiter.Save(resepModel);
        //---kirim detail obat racik ke bpjs
        var listRacik = _itemRacikBuilder.Build(listBarangJual, headerResep, listBarangResep, listDpho);
        if (listRacik.Any())
        {
            foreach (var item in listRacik)
            {
                var x = _sendObatSvc.ExecuteRacik(item);
                if (x is null || !x.metaData.code.Equals("200")) continue;
                //apply repo item
                var resepItem = new ResepItemModel
                {
                    PenjualanId = request.PenjualanId,
                    BarangId = item.BarangId,
                    DphoId = item.KDOBT,
                    DphoName = item.NMOBAT,
                    IsRacik = "1",
                    RacikId = item.JNSROBT,
                    Signa1 = item.SIGNA1OBT.ToString(),
                    Signa2 = item.SIGNA2OBT.ToString(),
                    Jho = item.JHO.ToString(),
                    Qty = item.JMLOBT.ToString(),
                    Note = item.CatKhsObt
                };
                _resepWiter.SaveItem(resepItem);
            }
        }
        //---kirim detail obat non racik ke bpjs
        var listNonRacik = _itemNonRacikBuilder.Build(listBarangJual, headerResep, listBarangResep, listDpho);
        if (listNonRacik.Any())
        {
            foreach (var item in listNonRacik)
            {
                var x = _sendObatSvc.ExecuteNonRacik(item);
                if (x is null || !x.metaData.code.Equals("200")) continue;
                //apply repo item non racik
                var resepItem = new ResepItemModel
                {
                    PenjualanId = request.PenjualanId,
                    BarangId = item.BarangId,
                    DphoId = item.KDOBT,
                    DphoName = item.NMOBAT,
                    IsRacik = "0",
                    RacikId = "",
                    Signa1 = item.SIGNA1OBT.ToString(),
                    Signa2 = item.SIGNA2OBT.ToString(),
                    Jho = item.JHO.ToString(),
                    Qty = item.JMLOBT.ToString(),
                    Note = item.CatKhsObt
                };
                _resepWiter.SaveItem(resepItem);
            }
        }
        // RETURN
        return Task.FromResult(new SendResepToAptolCommandResponse
        {
            Success = true,
            PenjualanId = request.PenjualanId,
            BpjsReffId = $"{headerResep.response.noApotik}/{headerResep.response.noResep}",
            Message = string.Empty
        });
    } 
}
*/