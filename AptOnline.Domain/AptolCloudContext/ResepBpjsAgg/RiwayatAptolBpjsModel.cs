using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.AptolCloudContext.ResepBpjsAgg
{
    public class RiwayatAptolBpjsModel
    {
        public RiwayatAptolBpjsModel(string pesertaNo, string pesertaName, string dob, 
            IEnumerable<RiwayatAptolBpjsItemModel> history)
        {
            PesertaNo = pesertaNo;
            PesertaName = pesertaName;
            Dob = dob;
            History = history;
        }

        public string PesertaNo { get; internal set; }
        public string PesertaName { get; internal set; }
        public string Dob { get; internal set; }
        public IEnumerable<RiwayatAptolBpjsItemModel> History { get; internal set; }

    }
}
