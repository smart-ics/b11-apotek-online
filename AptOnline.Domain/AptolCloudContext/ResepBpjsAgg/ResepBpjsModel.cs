using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.AptolCloudContext.ResepBpjsAgg
{
    public class ResepBpjsModel
    {
        public ResepBpjsModel(string noApotik, string noSep, string noPeserta, 
            string pasienName, string ppkId, string noResep, string tglResep, 
            string jenisObatId, string tglEntry)
        {
            NoApotik = noApotik;
            NoSep = noSep;
            NoPeserta = noPeserta;
            PasienName = pasienName;
            PpkId = ppkId;
            NoResep = noResep;
            TglResep = tglResep;
            JenisObatId = jenisObatId;
            TglEntry = tglEntry;
        }

        public string NoApotik { get; set; }
        public string NoSep { get; set; }
        public string NoPeserta { get; set; }
        public string PasienName { get; set; }
        public string PpkId { get; set; }
        public string NoResep { get; set; }
        public string TglResep { get; set; }
        public string JenisObatId { get; set; }
        public string TglEntry { get; set; }

    }
}
