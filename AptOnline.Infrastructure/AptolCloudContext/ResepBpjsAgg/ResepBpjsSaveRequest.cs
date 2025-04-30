using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ResepBpjsSaveRequest
    {
        public string PenjualanId { get; set; } //untuk save resep
        public string TGLSJP { get; set; } //tgl sep
        public string REFASALSJP { get; set; } //nosep (api sep)
        public string POLIRSP { get; set; } //poli asal resep
        public string KDJNSOBAT { get; set; } //item obat -> mapping dhpo + sep prb
        public string NORESEP { get; set; } //kp
        public string IDUSERSJP { get; set; } //user sep/resp
        public string TGLRSP { get; set; } //
        public string TGLPELRSP { get; set; }
        public string KdDokter { get; set; } //dokter resep (mapping)
        public string iterasi { get; set; } //resep
    }
}
