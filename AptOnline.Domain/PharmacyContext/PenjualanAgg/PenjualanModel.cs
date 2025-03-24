using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.PharmacyContext.TrsDuAgg
{
    public class PenjualanModel : IPenjualanKey
    {
        public string PenjualanId { get; set; }
        public string TglJamTrs { get; set; }
        public string ResepId { get; set; }
        public string RegId { get; set; }
        public string PasienId { get; set; }
        public string PasienName { get; set; }
        public string LayananId { get; set; }
        public string LayananName { get; set; }
        public string DokterId { get; set; }
        public string DokterName { get; set; }
        public string NoDokumen { get; set; }
        public int SubTotal { get; set; }
        public int TotalEmbalase { get; set; }
        public int TotalTax { get; set; }
        public int TotalDiskon { get; set; }
        public int DiskonLain { get; set; }
        public int BiayaLain { get; set; }
        public int GrandTotal { get; set; }
        public IEnumerable<PenjualanItemModel> ListBarang { get; set; }
    }


}
