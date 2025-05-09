using Nuna.Lib.ValidationHelper;

namespace AptOnline.Domain.EKlaimContext;

public record PeriodeRawatType
{
    private PeriodeRawatType(DateTime tglMasuk, DateTime tglKeluar)
    {
        TglMasuk = tglMasuk;
        TglKeluar = tglKeluar;
    }

    public static PeriodeRawatType Create(DateTime tglMasuk, DateTime tglKeluar)
    {
        if (tglMasuk > tglKeluar)
            throw new ArgumentException("Periode Rawat invalid");

        return new PeriodeRawatType(tglMasuk, tglKeluar);
    }

    public static PeriodeRawatType Load(DateTime tglMasuk, DateTime tglKeluar)
        => new PeriodeRawatType(tglMasuk, tglKeluar);

    public static PeriodeRawatType Default 
        => new PeriodeRawatType(new DateTime(3000, 1, 1), new DateTime(3000, 1, 1));    
    
    public DateTime TglMasuk { get; init; }
    public DateTime TglKeluar { get; init; }
}
