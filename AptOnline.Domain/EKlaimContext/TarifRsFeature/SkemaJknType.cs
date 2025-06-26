using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record SkemaJknType(string SkemaTarifJknId, string SkemaTarifJknName) : ISkemaTariJknfKey
{
    public static SkemaJknType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new SkemaJknType(id, name);
    }

    public static SkemaJknType Default => new("-", "-");
    public static ISkemaTariJknfKey Key(string id) => new SkemaJknType(id, "-");
    
    public static SkemaJknType ProsedurNonBedah => new("1", "Prosedur Non Bedah");
    public static SkemaJknType ProsedurBedah => new("2", "Prosedur Bedah");
    public static SkemaJknType Konsultasi => new("3", "Konsultasi");
    public static SkemaJknType TenagaAhli => new("4", "Tenaga Ahli");
    public static SkemaJknType Keperawatan => new("5", "Keperawatan");
    public static SkemaJknType Penunjang => new("6", "Penunjang");
    public static SkemaJknType Radiologi => new("7", "Radiologi");
    public static SkemaJknType Laboratorium => new("8", "Laboratorium");
    public static SkemaJknType PelayananDarah => new("9", "Pelayanan Darah");
    public static SkemaJknType Rehabilitasi => new("10", "Rehabilitasi");
    public static SkemaJknType Kamar => new("11", "Kamar");
    public static SkemaJknType RawatIntensif => new("12", "Rawat Intensif");
    public static SkemaJknType Obat => new("13", "Obat");
    public static SkemaJknType Alkes => new("14", "Alkes");
    public static SkemaJknType Bmhp => new("15", "Bmhp");
    public static SkemaJknType SewaAlat => new("16", "Sewa Alat");
    public static SkemaJknType ObatKronis => new("17", "Obat Kronis");
    public static SkemaJknType ObatKemoterapi => new("18", "Obat Kemoterapi");
    
    public static IEnumerable<SkemaJknType> ListData() => new[]
    {
        ProsedurNonBedah, ProsedurBedah, Konsultasi, TenagaAhli, Keperawatan, Penunjang, 
        Radiologi, Laboratorium, PelayananDarah, Rehabilitasi, Kamar, RawatIntensif, 
        Obat, Alkes, Bmhp, SewaAlat, ObatKronis, ObatKemoterapi
    };
}