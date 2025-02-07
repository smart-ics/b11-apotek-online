using AptOnline.Api.Helpers;
using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using AptOnline.Api.Workers;
using Mapster;
using MediatR;
using Newtonsoft.Json;
using Nuna.Lib.DataTypeExtension;
using System;
using System.Text.RegularExpressions;

namespace AptOnline.Api.Usecases
{
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
        public Task<SendResepToAptolCommandResponse> Handle(SendResepToAptolCommand request, 
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
            LogHelper.Log(new LogModel(DateTime.Now, request.PenjualanId, "", "", 0, 0, "Tes"));
            if (resepBpjsReq is null)
                return Task.FromResult(new SendResepToAptolCommandResponse
                {
                    Success = false,
                    PenjualanId = request.PenjualanId,
                    BpjsReffId = string.Empty,
                    Message = "Build resep request failed"
                });
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
                return Task.FromResult(new SendResepToAptolCommandResponse
                {
                    Success = false,
                    PenjualanId = request.PenjualanId,
                    BpjsReffId = string.Empty,
                    Message = "DPHO map not found"
                });

            //SEND
            //---kirim header resep ke bpjs
            var headerResep = _insertResepBpjsService.Execute(resepBpjsReq);
            if (headerResep is null) 
                return Task.FromResult(new SendResepToAptolCommandResponse
                {
                    Success = false,
                    PenjualanId = request.PenjualanId,
                    BpjsReffId = string.Empty,
                    Message = "Send resep header (BPJS) failed"
                });
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
            return Task.FromResult(new SendResepToAptolCommandResponse
            {
                Success = true,
                PenjualanId = request.PenjualanId,
                BpjsReffId = $"{headerResep.response.noApotik}/{headerResep.response.noResep}",
                Message = string.Empty
            });
        } 
    }
}