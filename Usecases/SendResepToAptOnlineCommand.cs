using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
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

        private InsertResepBpjsReqDto _ResepBpjs;
        private PenjualanData _duPenjualan;
        private ResepDto _resep;
        public SendResepToAptolCommandHandler(IInsertResepBpjsService insertResepBpjsService,
        IGetDuFarmasiService getDuFarmasiService,
        IGetResepFarmasiService getResepFarmasiService,
        IInsertObatBpjsService sendObatSvc,
        IGetMapDpho getDphoSvc,
        IGetSepBillingService getSepSvc,
        IGetLayananBillingService getLayananService,
        IGetDokterBillingService getDokterService)
        {
            _insertResepBpjsService = insertResepBpjsService;
            _getDuFarmasiService = getDuFarmasiService;
            _getResepFarmasiService = getResepFarmasiService;
            _sendObatSvc = sendObatSvc;
            _getDphoSvc = getDphoSvc;
            _getSepSvc = getSepSvc;
            _getLayananService = getLayananService;
            _getDokterService = getDokterService;
        }
        public Task<SendResepToAptolCommandResponse> Handle(SendResepToAptolCommand request, 
            CancellationToken cancellationToken)
        {
            // BUILD
            _ResepBpjs = BuildResepRequest(request.PenjualanId);
            if (_ResepBpjs is null)
                return Task.FromResult(new SendResepToAptolCommandResponse
                {
                    Success = false,
                    PenjualanId = request.PenjualanId,
                    BpjsReffId = string.Empty,
                    Message = "Build resep request failed"
                });

            //
            var listBarangResep = _resep.data.listBarang;
            var listBarangJual = _duPenjualan.listBarang;

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
            var headerResep = _insertResepBpjsService.Execute(_ResepBpjs);
            if (headerResep is null) 
                return Task.FromResult(new SendResepToAptolCommandResponse
                {
                    Success = false,
                    PenjualanId = request.PenjualanId,
                    BpjsReffId = string.Empty,
                    Message = "Send resep header (BPJS) failed"
                });
            //---kirim detail obat racik ke bpjs
            var listRacik = BuildListRacik(listBarangJual, headerResep, listBarangResep, listDpho);
            if (listRacik.Any())
            {
                foreach (var item in listRacik)
                {
                    _sendObatSvc.ExecuteRacik(item);
                }
            }
            //---kirim detail obat non racik ke bpjs
            var listNonRacik = BuildListNonRacik(listBarangJual, headerResep, listBarangResep, listDpho);
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


        #region Builder
        private InsertResepBpjsReqDto? BuildResepRequest(string id)
        {
            var du = _getDuFarmasiService.Execute(id) ?? new PenjualanDto();
            _duPenjualan = du.data.Adapt<PenjualanData>();
            if (_duPenjualan is null) return null;
            _resep = _getResepFarmasiService.Execute(_duPenjualan.resepId);
            if (_resep is null) return null;
            //(1.Obat PRB, 2.Obat Kronis Blm Stabil, 3.Obat Kemoterapi)
            var kodeJenisObat = "2";
            var iter = _resep.data.iter > 0 ? "1" : "0";
            var sep = _getSepSvc.Execute(du.data.regId);
            //var lyn = 
            var lyn = _getLayananService.Execute(_resep.data.layananId);
            var dokter = _getDokterService.Execute(_resep.data.dokterId);
            if (sep is null) return null;
            var result = new InsertResepBpjsReqDto
            {
                TGLPELRSP = _resep.data.tglJam,
                REFASALSJP = sep.data.sepNo,
                POLIRSP = lyn.data.layananBpjsId,
                KdDokter = dokter.data.dpjpId,
                KDJNSOBAT = kodeJenisObat,
                TGLSJP = $"{sep.data.sepDateTime.Left(10)} 00:00:00",
                TGLRSP = $"{_resep.data.tglJam.Left(10)} 00:00:00",
                NORESEP = _duPenjualan.penjualanId.Substring(4, 5),
                IDUSERSJP = _resep.data.dokterId,
                iterasi = iter
            };
            return result;
        }
        private static List<InsertObatRacikReqDto> BuildListRacik(IEnumerable<Listbarang> listBrg,
        InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep, 
        IEnumerable<RespMapDpho> listDpho)
        {
            //ambil header raciknya dulu
            var listRacik = listBrg
                .Where(x => x.isRacik == true)
                .Where(x => x.racikId.Trim().Length == 0)?.ToList();
            if (listRacik is null) return null;

            List<Listbarang> listObat = new();
            List<InsertObatRacikReqDto> listDto = new();
            foreach (var racik in listRacik)
            {
                //ambil item raciknya
                listObat.AddRange(listBrg
                    .Where(x => x.racikId == racik.brgId).ToList());
                //tambahkan ke dto
                foreach(var obat in listObat)
                {
                    var dpho = listDpho.Where(x => x.BrgId == obat.brgId).FirstOrDefault();
                    if (dpho is null) continue;
                    var resep = listBrgResep.Where(x => x.brgId == obat.brgId).FirstOrDefault();
                    int signa1 = racik.etiketQty;
                    int signa2 = racik.etiketHari;
                    int jho = (int)Math.Ceiling((double)(obat.qty / (signa1 * signa2)));
                    listDto.Add(new InsertObatRacikReqDto
                    {
                        NOSJP = header.response.noApotik,
                        NORESEP = header.response.noResep,
                        JNSROBT = obat.racikId,
                        KDOBT = dpho.DphoId,
                        NMOBAT = dpho.DphoName,
                        SIGNA1OBT = signa1,
                        SIGNA2OBT = signa2,
                        PERMINTAAN = resep is not null ? resep.qty : obat.qty,
                        JMLOBT = obat.qty,
                        JHO = jho,
                        CatKhsObt = $"Obat Racikan {racik.brgId}"
                    });
                }
            }
            return listDto;
        }

        private static List<InsertObatNonRacikReqDto> BuildListNonRacik(IEnumerable<Listbarang> listBrg,
        InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep, 
        IEnumerable<RespMapDpho> listDpho)
        {
            var listObat = listBrg
                .Where(x => x.isRacik == false)
                .Where(x => x.racikId.Trim().Length == 0)?
                .ToList() ?? new List<Listbarang>();
            List<InsertObatNonRacikReqDto> listDto = new();
            foreach (var item in listObat)
            {
                var dpho = listDpho.Where(x => x.BrgId == item.brgId).FirstOrDefault();
                if (dpho is null) continue;
                var resep = listBrgResep.Where(x => x.brgId == item.brgId).FirstOrDefault();
                var signa1 = item.etiketQty; //resep.etiketDescription
                var signa2 = item.etiketHari;
                int jho = (int)Math.Ceiling((double)(item.qty / (signa1 * signa2)));
                listDto.Add(new InsertObatNonRacikReqDto
                {
                    NOSJP = header.response.noApotik,
                    NORESEP = header.response.noResep,
                    KDOBT = dpho.DphoId,
                    NMOBAT = dpho.DphoName,
                    SIGNA1OBT = signa1,
                    SIGNA2OBT = signa2,
                    JMLOBT = item.qty,
                    JHO = jho,
                    CatKhsObt = ""
                });
            }
            return listDto;
            //var result = (
            //    from c in listObat
            //    join d in listDpho on c.brgId equals d.BrgId into g
            //    from dataDpho in g.DefaultIfEmpty()
            //    join e in listBrgResep on c.brgId equals e.brgId into h
            //    from dataResep in h.DefaultIfEmpty()
            //    select new InsertObatNonRacikReqDto
            //    {
            //        NOSJP = header.response.noApotik,
            //        NORESEP = header.response.noResep,
            //        KDOBT = dataDpho.DphoId,
            //        NMOBAT = dataDpho.DphoName,
            //        SIGNA1OBT = c. 1,//Convert.ToInt16(string.IsNullOrEmpty(GenSigna(dataResep.etiketDescription).Signa1) ? "0" : GenSigna(dataResep.etiketDescription).Signa1),
            //        SIGNA2OBT = 1,//Convert.ToInt16(string.IsNullOrEmpty(GenSigna(dataResep.etiketDescription).Signa2) ? "0" : GenSigna(dataResep.etiketDescription).Signa2),
            //        JMLOBT = c.qty,
            //        JHO = 5,
            //        CatKhsObt = "test"
            //    }).ToList() ?? new List<InsertObatNonRacikReqDto>();
            //return result;
        }
        #endregion
        #region Private Function
        private static (string Signa1, string Signa2) GenSigna(string signa)
        {
            var signa1 = "";
            var signa2 = "";

            if (!string.IsNullOrWhiteSpace(signa))
            {
                var matches = Regex.Matches(signa, @"\d+")
                                   .Cast<Match>()
                                   .Take(2)
                                   .Select(m => m.Value)
                                   .ToArray();

                if (matches.Length > 0) signa1 = matches[0];
                if (matches.Length > 1) signa2 = matches[1];
            }

            return (signa1, signa2);
        }
        #endregion
    }
}