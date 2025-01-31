using MassTransit;
using MediatR;
using MyHospital.MsgContract.Pharmacy.SalesEvents;
using AptOnline.Api.Services;
using AptOnline.Api.Models;
using Mapster;
using System.Text.RegularExpressions;
using Nuna.Lib.DataTypeExtension;

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
   

    private InsertResepBpjsReqDto _ResepBpjs;
    private PenjualanData _duPenjualan;
    private ResepDto _resep;

    public SendMsgOnDoBillUmumCreatedEvent(IInsertResepBpjsService insertResepBpjsService,
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

    public Task Consume(ConsumeContext<DoBillUmumCreatedNotifEvent> context)
    {
        // BUILD
        var msg = context.Message;
        var id = msg.DoBillUmumId;
        _ResepBpjs = BuildResepRequest(id);
        if(_ResepBpjs is null) return Task.CompletedTask;

        //
        var listBarangResep = _resep.data.listBarang;
        var listBarangJual = _duPenjualan.listBarang;

        var listDpho = new List<RespMapDpho>(); //listBarangJual.Select(x => _getDphoSvc.Execute(x.brgId));
        foreach(var barang in listBarangJual)
        {
            var dpho = _getDphoSvc.Execute(barang.brgId);
            if(dpho is null) continue;
            listDpho.Add(dpho);
        }
        if (listDpho.IsNullOrEmpty()) return Task.CompletedTask;
        //
        var headerResep = _insertResepBpjsService.Execute(_ResepBpjs);

        var listRacik = BuildListRacik(listBarangJual, headerResep, listBarangResep);
        var listNonRacik = BuildListNonRacik(listBarangJual, headerResep, listBarangResep);

        if (listRacik.Any())
        {
            foreach(var item in listRacik)
            {
                _sendObatSvc.ExecuteRacik(item);
            }
        }

        if(listNonRacik.Any())
        {
            foreach( var item in listNonRacik)
            {
                _sendObatSvc.ExecuteNonRacik(item);
            }
        }

        // RETURN
        return Task.FromResult(Unit.Value);
    }

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
            POLIRSP =  lyn.data.layananBpjsId,
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
    private List<InsertObatRacikReqDto> BuildListRacik(IEnumerable<Listbarang> listBrg,
        InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep)
    {
        var listObat = listBrg
            .Where(x => x.isRacik == true)
            .Where(x => x.racikId.Trim().Length > 0)?
            .ToList() ?? new List<Listbarang>();

        var listDpho = listObat.Select(x => _getDphoSvc.Execute(x.brgId));

        var result = (
            from c in listObat
            join d in listDpho on c.brgId equals d.BrgId into g
            from dataDpho in g.DefaultIfEmpty()
            join e in listBrgResep on c.brgId equals e.brgId into h
            from dataResep in h.DefaultIfEmpty()
            select new InsertObatRacikReqDto
            {
                NOSJP = header.response.noApotik,
                NORESEP = header.response.noResep,
                JNSROBT = header.response.kdJnsObat,
                KDOBT = dataDpho.DphoId,
                NMOBAT = dataDpho.DphoName,
                SIGNA1OBT = Convert.ToInt16(string.IsNullOrEmpty(GenSigna(dataResep.etiketDescription).Signa1) ? "0" : GenSigna(dataResep.etiketDescription).Signa1),
                SIGNA2OBT = Convert.ToInt16(string.IsNullOrEmpty(GenSigna(dataResep.etiketDescription).Signa2) ? "0" : GenSigna(dataResep.etiketDescription).Signa2),
                PERMINTAAN = dataResep.qty,
                JMLOBT = c.qty,
                JHO = Convert.ToInt16(dataResep?.etiketHariQty ?? 0) *
                  Convert.ToInt16(string.IsNullOrEmpty(dataResep?.etiketHari) ? "1" : dataResep.etiketHari),
                CatKhsObt = ""
            })?.ToList() ?? new List<InsertObatRacikReqDto>();

        return result;
    }

   private string GetJenisObat(IEnumerable<Listbarang> listbarangs)
    {
        return string.Empty;
    }

    private List<InsertObatNonRacikReqDto> BuildListNonRacik(IEnumerable<Listbarang> listBrg,
        InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep)
    {
        var listObat = listBrg
            .Where(x => x.isRacik == false)
            .Where(x => x.racikId.Trim().Length == 0)?
            .ToList() ?? new List<Listbarang>();

        var listDpho = listObat.Select(x => _getDphoSvc.Execute(x.brgId));

        var result = (
            from c in listObat
            join d in listDpho on c.brgId equals d.BrgId into g
            from dataDpho in g.DefaultIfEmpty()
            join e in listBrgResep on c.brgId equals e.brgId into h
            from dataResep in h.DefaultIfEmpty()
            select new InsertObatNonRacikReqDto
            {
                NOSJP = header.response.noApotik,
                NORESEP = header.response.noResep,
                KDOBT = dataDpho.DphoId,
                NMOBAT = dataDpho.DphoName,
                SIGNA1OBT = Convert.ToInt16(string.IsNullOrEmpty(GenSigna(dataResep.etiketDescription).Signa1) ? "0" : GenSigna(dataResep.etiketDescription).Signa1),
                SIGNA2OBT = Convert.ToInt16(string.IsNullOrEmpty(GenSigna(dataResep.etiketDescription).Signa2) ? "0" : GenSigna(dataResep.etiketDescription).Signa2),
                JMLOBT = c.qty,
                JHO = Convert.ToInt16(dataResep?.etiketHariQty ?? 0) *
                  Convert.ToInt16(string.IsNullOrEmpty(dataResep?.etiketHari) ? "1" : dataResep.etiketHari),
                CatKhsObt = ""
            }).ToList() ?? new List<InsertObatNonRacikReqDto>();
        return result;
    }

    private (string Signa1, string Signa2) GenSigna(string signa)
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

}