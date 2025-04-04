using AptOnline.Domain.AptolCloudContext.PpkAgg;

namespace AptOnline.Infrastructure.AptolCloudContext.PpkAgg;

public class PpkGetDto
{
    public string Kode { get; set; }
    public string Nama { get; set; }
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

    public PpkType ToType()
        => new PpkType(Kode, Nama, Siup, Alamat, Kota,
            new KepalaType(NamaKepala, JabatanKepala, NipKepala),
            new VerifikatorType(NamaVerifikator, NppVerifikator),
            new ApotekType(NamaPetugasApotek, NipPetugasApotek, NamaApoteker, CheckStock));
}