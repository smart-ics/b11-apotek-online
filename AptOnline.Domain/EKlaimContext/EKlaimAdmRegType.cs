namespace AptOnline.Domain.EKlaimContext;

public record EKlaimAdmRegType
{
    public EKlaimAdmRegType(PeriodeRawatType periodeRawat, CaraMasukValType caraMasukVal, 
        JenisRawatValType jenisRawatVal, KelasRawatInapValType kelasRawatInap, 
        KelasRawatJalanValType kelasRawatJalanVal)
    {
        PeriodeRawat = periodeRawat;
        CaraMasukVal = caraMasukVal;
        JenisRawatVal = jenisRawatVal;
        KelasRawatInap = kelasRawatInap;
        KelasRawatJalanVal = kelasRawatJalanVal;
    }

    public PeriodeRawatType PeriodeRawat { get; init; }
    public CaraMasukValType CaraMasukVal { get; set; }
    public JenisRawatValType JenisRawatVal { get; set; }
    public KelasRawatInapValType KelasRawatInap { get; set; }
    public KelasRawatJalanValType KelasRawatJalanVal { get; set; }

}