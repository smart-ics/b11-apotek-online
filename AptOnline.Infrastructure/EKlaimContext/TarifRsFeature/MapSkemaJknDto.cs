using AptOnline.Domain.EKlaimContext.TarifRsFeature;

namespace AptOnline.Infrastructure.EKlaimContext.TarifRsFeature;

public class MapSkemaJknDto
{
    public string fs_kd_tarif { get; set; }
    public string fs_kd_grup_tarif {get;set;}
    public int flag { get; set; }
    
    public MapSkemaJknType ToModel()
    {
        var reffBiaya = new ReffBiayaType(fs_kd_tarif, (JenisReffBiayaEnum) flag);
        var skemaJkn = fs_kd_grup_tarif switch
        {
            "1" => SkemaJknType.ProsedurNonBedah,
            "2" => SkemaJknType.ProsedurBedah,
            "3" => SkemaJknType.Konsultasi,
            "4" => SkemaJknType.TenagaAhli,
            "5" => SkemaJknType.Keperawatan,
            "6" => SkemaJknType.Penunjang,
            "7" => SkemaJknType.Radiologi,
            "8" => SkemaJknType.Laboratorium,
            "9" => SkemaJknType.PelayananDarah,
            "10" => SkemaJknType.Rehabilitasi,
            "11" => SkemaJknType.Kamar,
            "12" => SkemaJknType.RawatIntensif,
            "13" => SkemaJknType.Obat,
            "14" => SkemaJknType.Alkes,
            "15" => SkemaJknType.Bmhp,
            "16" => SkemaJknType.SewaAlat,
            "17" => SkemaJknType.ObatKronis,
            "18" => SkemaJknType.ObatKemoterapi,
            _ => throw new ArgumentOutOfRangeException(nameof(fs_kd_grup_tarif), fs_kd_grup_tarif, null)
        };

        return new MapSkemaJknType(reffBiaya, skemaJkn);
    }
}