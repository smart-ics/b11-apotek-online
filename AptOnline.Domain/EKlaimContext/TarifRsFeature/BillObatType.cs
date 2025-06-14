using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record BillObatType(
    decimal Obat,
    decimal ObatKronis,
    decimal ObatKemoterapi,
    decimal Alkes,
    decimal Bhp,
    decimal SewaAlat)
{
    public static BillObatType Create(decimal obat, decimal obatKronis, decimal obatKemoterapi, decimal alkes,
        decimal bhp, decimal sewaAlat)
    {
        Guard.Against.Negative(obat, nameof(obat));
        Guard.Against.Negative(obatKronis, nameof(obatKronis));
        Guard.Against.Negative(obatKemoterapi, nameof(obatKemoterapi));
        Guard.Against.Negative(alkes, nameof(alkes));
        Guard.Against.Negative(bhp, nameof(bhp));
        Guard.Against.Negative(sewaAlat, nameof(sewaAlat));
        return new BillObatType(obat, obatKronis, obatKemoterapi, alkes, bhp, sewaAlat);
    }

    public static BillObatType Default => new BillObatType(0, 0, 0, 0, 0, 0);
}