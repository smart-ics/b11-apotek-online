using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.AptolCloudContext.ResepBpjsAgg
{
    public class RiwayatAptolBpjsItemModel
    {
        public RiwayatAptolBpjsItemModel(string sepNo, string resepDate, string reffId, 
            string obatBpjsId, string obatBpjsName, string qty)
        {
            SepNo = sepNo;
            ResepDate = resepDate;
            ReffId = reffId;
            ObatBpjsId = obatBpjsId;
            ObatBpjsName = obatBpjsName;
            Qty = qty;
        }

        public string SepNo { get; internal set; }
        public string ResepDate { get; internal set; }
        public string ReffId { get; internal set; }
        public string ObatBpjsId { get; internal set; }
        public string ObatBpjsName { get; internal set; }
        public string Qty { get; internal set; }
    }
}
