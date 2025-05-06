namespace AptOnline.Domain.EKlaimContext;

public record TarifRsType
{
    public TarifRsType(TarifRsJasaMedisType jasaMedis, TarifRsPelayananType pelayanan, TarifRsObatType obat)
    {
        JasaMedis = jasaMedis;
        Pelayanan = pelayanan;
        Obat = obat;
    }

    public static TarifRsType Default => new TarifRsType(
        TarifRsJasaMedisType.Default,
        TarifRsPelayananType.Default,
        TarifRsObatType.Default);
    public TarifRsJasaMedisType JasaMedis { get; init; }
    public TarifRsPelayananType Pelayanan { get; init; }
    public TarifRsObatType Obat { get; init; }
}

public record TarifRsJasaMedisType
{
    public TarifRsJasaMedisType(decimal prosedurNonBedah, decimal prosedurBedah, 
        decimal konsultasi, decimal tenagaAhli, decimal keperawatan)
    {
        ProsedurNonBedah = prosedurNonBedah;
        ProsedurBedah = prosedurBedah;
        Konsultasi = konsultasi;
        TenagaAhli = tenagaAhli;
        Keperawatan = keperawatan;
    }

    public static TarifRsJasaMedisType Default => new TarifRsJasaMedisType(0, 0, 0, 0, 0);

    public decimal ProsedurNonBedah { get; init; }
    public decimal ProsedurBedah { get; init; }
    public decimal Konsultasi { get; init; }
    public decimal TenagaAhli { get; init; }
    public decimal Keperawatan { get; init; }
}

public record TarifRsPelayananType
{
    public TarifRsPelayananType(decimal penunjang, decimal radiologi, decimal laboratorium, decimal pelayananDarah, decimal rehabilitasi, decimal rawatIntensif)
    {
        Penunjang = penunjang;
        Radiologi = radiologi;
        Laboratorium = laboratorium;
        PelayananDarah = pelayananDarah;
        Rehabilitasi = rehabilitasi;
        RawatIntensif = rawatIntensif;
    }

    public static TarifRsPelayananType Default 
        => new TarifRsPelayananType(0, 0, 0, 0, 0, 0);

    public decimal Penunjang { get; init; }
    public decimal Radiologi { get; init; }
    public decimal Laboratorium { get; init; }
    public decimal PelayananDarah { get; init; }
    public decimal Rehabilitasi { get; init; }
    public decimal RawatIntensif { get; init; }
}

public record TarifRsObatType
{
    public TarifRsObatType(decimal obat, decimal obatKronis, decimal obatKemoterapi, decimal alkes, decimal bmhp, decimal sewaAlat)
    {
        Obat = obat;
        ObatKronis = obatKronis;
        ObatKemoterapi = obatKemoterapi;
        Alkes = alkes;
        Bmhp = bmhp;
        SewaAlat = sewaAlat;
    }

    public static TarifRsObatType Default => new TarifRsObatType(0, 0, 0, 0, 0, 0);

    public decimal Obat { get; init; }
    public decimal ObatKronis { get; set; }
    public decimal ObatKemoterapi { get; init; }
    public decimal Alkes { get; init; }
    public decimal Bmhp { get; init; }
    public decimal SewaAlat { get; init; }
}