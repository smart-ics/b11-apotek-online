using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.AptolCloudContext.DphoAgg;

public class DphoModel : IDphoKey
{
    public string DphoId { get; set; }
    public string DphoName{ get; set; }
    public string Prb { get; set; }
    public string Kronis { get; set; }
    public string Kemo { get; set; }
    public decimal Harga { get; set; }
    public string Restriksi { get; set; }
    public string Generik { get; set; }
    public bool IsAktif { get; set; }
}

public interface IDphoKey
{
    string DphoId { get; }
}
