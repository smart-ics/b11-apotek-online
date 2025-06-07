using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.AptolCloudContext.ResepBpjsAgg
{
    public class ResepBpjsItemModel
    {
        public ResepBpjsItemModel(string obatBpjsId, string obatBpjsName,
            bool isRacik, decimal signa1, decimal signa2, decimal hari,
            decimal permintaan, decimal qty, decimal harga)
        {
            ObatBpjsId = obatBpjsId;
            ObatBpjsName = obatBpjsName;
            IsRacik = isRacik;
            Signa1 = signa1;
            Signa2 = signa2;
            Hari = hari;
            Permintaan = permintaan;
            Qty = qty;
            Harga = harga;
        }

        public string ObatBpjsId { get; internal set; }
        public string ObatBpjsName { get; internal set; }
        public bool IsRacik { get; internal set; }
        public decimal Signa1 { get; internal set; }
        public decimal Signa2 { get; internal set; }
        public decimal Hari { get; internal set; }
        public decimal Permintaan { get; internal set; }
        public decimal Qty { get; internal set; }
        public decimal Harga { get; internal set; }
    }
}
