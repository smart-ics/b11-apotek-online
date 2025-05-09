using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimAdmRawatType
{
    private EKlaimAdmRawatType(PeriodeRawatType periodeRawat, CaraMasukValType caraMasuk, 
        JenisRawatValType jenisRawat, KelasRawatValType kelasRawat)
    {
        Guard.NotNull(periodeRawat, nameof(periodeRawat), "Periode Rawat Tidak boleh kosong");
        Guard.NotNull(caraMasuk, nameof(caraMasuk), "Cara Masuk Tidak boleh kosong");
        Guard.NotNull(jenisRawat, nameof(jenisRawat), "Jenis Rawat Tidak boleh kosong");
        Guard.NotNull(kelasRawat, nameof(kelasRawat), "Kelas Rawat Jalan Tidak boleh kosong");
        
        PeriodeRawat = periodeRawat;
        CaraMasuk = caraMasuk;
        JenisRawat = jenisRawat;
        KelasRawat = kelasRawat;
    }

    public static EKlaimAdmRawatType Create(PeriodeRawatType periodeRawat, CaraMasukValType caraMasuk, 
        JenisRawatValType jenisRawat, KelasRawatValType kelasRawat)
    {
        Guard.NotNull(jenisRawat, nameof(jenisRawat), "Jenis Rawat Tidak boleh kosong");
        Guard.NotNull(kelasRawat, nameof(kelasRawat), "Kelas Rawat Jalan Tidak boleh kosong");

        if (jenisRawat.Value == "2")
            if (kelasRawat.Value == "2")
                throw new ArgumentException("KelasRawat Pasien R.Jalan adalah 1 (Reguler) atau 3 (Eksekutif)");
        
        return new EKlaimAdmRawatType(periodeRawat, caraMasuk, jenisRawat, kelasRawat);
    }
    
    public static EKlaimAdmRawatType Load(PeriodeRawatType periodeRawat, CaraMasukValType caraMasuk, 
        JenisRawatValType jenisRawat, KelasRawatValType kelasRawat)
        => new EKlaimAdmRawatType(periodeRawat, caraMasuk, jenisRawat, kelasRawat);
    
    public static EKlaimAdmRawatType Default
        => new EKlaimAdmRawatType(PeriodeRawatType.Default, CaraMasukValType.Default, 
            JenisRawatValType.Default, KelasRawatValType.Default);
    
    

    public PeriodeRawatType PeriodeRawat { get; init; }
    public CaraMasukValType CaraMasuk { get; set; }
    public JenisRawatValType JenisRawat { get; set; }
    public KelasRawatValType KelasRawat { get; set; }
}