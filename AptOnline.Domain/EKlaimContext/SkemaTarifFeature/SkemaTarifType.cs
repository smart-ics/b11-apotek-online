using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.SkemaTarifFeature;

public record SkemaTarifType(string SkemaTarifId, string SkemaTarifJknName) : ISkemaTarifKey
{
    public static SkemaTarifType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new SkemaTarifType(id, name);
    }

    public static SkemaTarifType Default => new("-", "-");
    public static ISkemaTarifKey Key(string id) => new SkemaTarifType(id, "-");
    
    public static SkemaTarifType ProsedurNonBedah => new("1", "Prosedur Non Bedah");
    public static SkemaTarifType ProsedurBedah => new("2", "Prosedur Bedah");
    public static SkemaTarifType Konsultasi => new("3", "Konsultasi");
    public static SkemaTarifType TenagaAhli => new("4", "Tenaga Ahli");
    public static SkemaTarifType Keperawatan => new("5", "Keperawatan");
    public static SkemaTarifType Penunjang => new("6", "Penunjang");
    public static SkemaTarifType Radiologi => new("7", "Radiologi");
    public static SkemaTarifType Laboratorium => new("8", "Laboratorium");
    public static SkemaTarifType PelayananDarah => new("9", "Pelayanan Darah");
    public static SkemaTarifType Rehabilitasi => new("10", "Rehabilitasi");
    public static SkemaTarifType Kamar => new("11", "Kamar");
    public static SkemaTarifType RawatIntensif => new("12", "Rawat Intensif");
    public static SkemaTarifType Obat => new("13", "Obat");
    public static SkemaTarifType Alkes => new("14", "Alkes");
    public static SkemaTarifType Bmhp => new("15", "Bmhp");
    public static SkemaTarifType SewaAlat => new("16", "Sewa Alat");
    public static SkemaTarifType ObatKronis => new("17", "Obat Kronis");
    public static SkemaTarifType ObatKemoterapi => new("18", "Obat Kemoterapi");
    
    public static IEnumerable<SkemaTarifType> ListData() => new[]
    {
        ProsedurNonBedah, ProsedurBedah, Konsultasi, TenagaAhli, Keperawatan, Penunjang, 
        Radiologi, Laboratorium, PelayananDarah, Rehabilitasi, Kamar, RawatIntensif, 
        Obat, Alkes, Bmhp, SewaAlat, ObatKronis, ObatKemoterapi
    };
}