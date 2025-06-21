using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.SkemaTarifJknFeature;

public record SkemaTarifJknType(string SkemaTarifJknId, string SkemaTarifJknName) : ISkemaTariJknfKey
{
    public static SkemaTarifJknType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new SkemaTarifJknType(id, name);
    }

    public static SkemaTarifJknType Default => new("-", "-");
    public static ISkemaTariJknfKey Key(string id) => new SkemaTarifJknType(id, "-");
    
    public static SkemaTarifJknType ProsedurNonBedah => new("1", "Prosedur Non Bedah");
    public static SkemaTarifJknType ProsedurBedah => new("2", "Prosedur Bedah");
    public static SkemaTarifJknType Konsultasi => new("3", "Konsultasi");
    public static SkemaTarifJknType TenagaAhli => new("4", "Tenaga Ahli");
    public static SkemaTarifJknType Keperawatan => new("5", "Keperawatan");
    public static SkemaTarifJknType Penunjang => new("6", "Penunjang");
    public static SkemaTarifJknType Radiologi => new("7", "Radiologi");
    public static SkemaTarifJknType Laboratorium => new("8", "Laboratorium");
    public static SkemaTarifJknType PelayananDarah => new("9", "Pelayanan Darah");
    public static SkemaTarifJknType Rehabilitasi => new("10", "Rehabilitasi");
    public static SkemaTarifJknType Kamar => new("11", "Kamar");
    public static SkemaTarifJknType RawatIntensif => new("12", "Rawat Intensif");
    public static SkemaTarifJknType Obat => new("13", "Obat");
    public static SkemaTarifJknType Alkes => new("14", "Alkes");
    public static SkemaTarifJknType Bmhp => new("15", "Bmhp");
    public static SkemaTarifJknType SewaAlat => new("16", "Sewa Alat");
    public static SkemaTarifJknType ObatKronis => new("17", "Obat Kronis");
    public static SkemaTarifJknType ObatKemoterapi => new("18", "Obat Kemoterapi");
    
    public static IEnumerable<SkemaTarifJknType> ListData() => new[]
    {
        ProsedurNonBedah, ProsedurBedah, Konsultasi, TenagaAhli, Keperawatan, Penunjang, 
        Radiologi, Laboratorium, PelayananDarah, Rehabilitasi, Kamar, RawatIntensif, 
        Obat, Alkes, Bmhp, SewaAlat, ObatKronis, ObatKemoterapi
    };
}