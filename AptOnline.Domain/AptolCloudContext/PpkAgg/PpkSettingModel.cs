using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.AptolCloudContext.PpkAgg
{
    public class PpkSettingModel
    {
        public string PpkId { get; set; }
        public string NamaApoteker { get; set; }
        public string NamaKepala { get; set; }
        public string JabatanKepala { get; set; }
        public string NipKepala { get; set; }
        public string Siup { get; set; }
        public string Alamat { get; set; }
        public string Kota { get; set; }
        public string NamaVerifikator { get; set; }
        public string NppVerifikator { get; set; }
        public string NamaPetugasApotek { get; set; }
        public string NipPetugasApotek { get; set; }
        public string CheckStock { get; set; }
    }
}
