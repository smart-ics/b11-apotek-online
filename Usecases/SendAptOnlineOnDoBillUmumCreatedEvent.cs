using MassTransit;
using MediatR;
using MyHospital.MsgContract.Pharmacy.SalesEvents;
using AptOnline.Api.Models;
using Mapster;
using System.Text.RegularExpressions;
using Nuna.Lib.DataTypeExtension;
using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Workers;
using AptOnline.Api.Helpers;

namespace AptOnline.Api.Usecases;
public class SendMsgOnDoBillUmumCreatedEvent : IConsumer<DoBillUmumCreatedNotifEvent>
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

    public SendMsgOnDoBillUmumCreatedEvent(IInsertResepBpjsService insertResepBpjsService, 
        IGetDuFarmasiService getDuFarmasiService, 
        IGetResepFarmasiService getResepFarmasiService, 
        IInsertObatBpjsService sendObatSvc, 
        IGetMapDpho getDphoSvc, 
        IGetSepBillingService getSepSvc, 
        IGetLayananBillingService getLayananService, 
        IGetDokterBillingService getDokterService, 
        IResepRequestBuilder resepRequestBuilder, 
        IItemNonRacikBuilder itemNonRacikBuilder, 
        IItemRacikBuilder itemRacikBuilder)
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
    }

    public Task Consume(ConsumeContext<DoBillUmumCreatedNotifEvent> context)
    {
        // BUILD
        var msg = context.Message;
        var id = msg.DoBillUmumId;
        var du = _getDuFarmasiService.Execute(id) ?? new PenjualanDto();
        var duPenjualan = du.data.Adapt<PenjualanData>();
        if (duPenjualan is null)
        {
            LogHelper.Log(new LogModel(DateTime.Now, id, "", "", 0, 0, 
                "DU tidak ditemukan"));
            return Task.CompletedTask;
        }
        var resep = _getResepFarmasiService.Execute(duPenjualan.resepId);
        if (resep is null)
        {
            LogHelper.Log(new LogModel(DateTime.Now, id, "", "", 0, 0, 
                "Resep tidak ditemukan"));
            return Task.CompletedTask;
        }
        var sep = _getSepSvc.Execute(du.data.regId);
        if (sep is null)
        {
            LogHelper.Log(new LogModel(DateTime.Now, id, "", "", 0, 0, 
                "Sep tidak ditemukan"));
            return Task.CompletedTask;
        }
        var lyn = _getLayananService.Execute(resep.data.layananId);
        var dokter = _getDokterService.Execute(resep.data.dokterId);
        var resepBpjsReq = _resepRequestBuilder.Build(duPenjualan, resep, sep, lyn, dokter);
        if (resepBpjsReq is null)
        {
            LogHelper.Log(new LogModel(DateTime.Now, id, "", "", 0, 0, 
                "Gagal generate Resep BPJS Request"));
            return Task.CompletedTask;
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
            LogHelper.Log(new LogModel(DateTime.Now, id, "", "", 0, 0, 
                "Map DPHO tidak ditemukan"));
            return Task.CompletedTask;
        }

        //SEND
        //---kirim header resep ke bpjs
        var headerResep = _insertResepBpjsService.Execute(resepBpjsReq);
        if (headerResep is null)
        {
            LogHelper.Log(new LogModel(DateTime.Now, id, resepBpjsReq.ToString(), "", 0, 0, 
                "Request Resep Header BPJS gagal"));
            return Task.CompletedTask;
        }
        //---kirim detail obat racik ke bpjs
        var listRacik = _itemRacikBuilder.Build(listBarangJual, headerResep, listBarangResep, listDpho);
        if (listRacik.Any())
        {
            foreach (var item in listRacik)
            {
                _sendObatSvc.ExecuteRacik(item);
            }
        }
        //---kirim detail obat non racik ke bpjs
        var listNonRacik = _itemNonRacikBuilder.Build(listBarangJual, headerResep, listBarangResep, listDpho);
        if (listNonRacik.Any())
        {
            foreach (var item in listNonRacik)
            {
                _sendObatSvc.ExecuteNonRacik(item);
            }
        }
        // RETURN
        return Task.CompletedTask;
    }
}
