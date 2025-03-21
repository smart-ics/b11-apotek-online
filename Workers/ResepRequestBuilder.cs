using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using AptOnline.Infrastructure.BillingContext.DokterAgg;
using AptOnline.Infrastructure.BillingContext.LayananAgg;
using Nuna.Lib.DataTypeExtension;

namespace AptOnline.Api.Workers
{
    public interface IResepRequestBuilder
    {
        InsertResepBpjsReqDto? Build(PenjualanData du,
            ResepDto resep, SepDto sep, LayananDto lyn, DokterDto dokter);
    }
    public class ResepRequestBuilder: IResepRequestBuilder
    {
        public InsertResepBpjsReqDto? Build(PenjualanData du, 
            ResepDto resep, SepDto sep, LayananDto lyn, DokterDto dokter)
        {
            //(1.Obat PRB, 2.Obat Kronis Blm Stabil, 3.Obat Kemoterapi)
            var kodeJenisObat = "2";
            var iter = resep.data.iter > 0 ? "1" : "0";

            var result = new InsertResepBpjsReqDto
            {
                PenjualanId = du.penjualanId,
                TGLPELRSP = resep.data.tglJam,
                REFASALSJP = sep.data.sepNo,
                POLIRSP = lyn.data.layananBpjsId,
                KdDokter = dokter.data.dpjpId,
                KDJNSOBAT = kodeJenisObat,
                TGLSJP = $"{sep.data.sepDateTime.Left(10)} 00:00:00",
                TGLRSP = $"{resep.data.tglJam.Left(10)} 00:00:00",
                NORESEP = du.penjualanId.Substring(4, 5),
                IDUSERSJP = resep.data.dokterId,
                iterasi = iter
            };
            return result;
        }
    }
}
