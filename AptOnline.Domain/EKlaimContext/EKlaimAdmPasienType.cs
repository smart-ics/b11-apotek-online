using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimAdmPasienType
{
    private EKlaimAdmPasienType(string pasienId, string pasienName, DateTime tglLahir, 
        GenderValType gender, NomorKartuTValType nomorKartuTVal)
    {
        Guard.NotNull(gender, nameof(gender), "Gender Tidak boleh kosong");
        Guard.NotNull(nomorKartuTVal, nameof(NomorKartuTValType), "Jenis Nomor Kartu Tidak boleh kosong");
        
        PasienId = pasienId;
        PasienName = pasienName;
        TglLahir = tglLahir;
        Gender = gender;
        NomorKartuT = nomorKartuTVal;
    }

    public static EKlaimAdmPasienType Create(string pasienId, string pasienName, DateTime tglLahir,
        GenderValType gender, NomorKartuTValType nomorKartuT)
    {
        Guard.NotNullOrWhitespace(pasienId, nameof(pasienId), "Pasien ID tidak boleh kosong");
        Guard.NotNullOrWhitespace(pasienName, nameof(pasienName), "Pasien Name tidak boleh kosong");
        
        return new EKlaimAdmPasienType(pasienId, pasienName, tglLahir, gender, nomorKartuT);
    }

    public static EKlaimAdmPasienType Load(string pasienId, string pasienName, DateTime tglLahir,
        GenderValType genderVal, NomorKartuTValType nomorKartuT)
        => new EKlaimAdmPasienType(pasienId, pasienName, tglLahir, genderVal, nomorKartuT);
    
    public static EKlaimAdmPasienType Default 
        => new EKlaimAdmPasienType(string.Empty, string.Empty, DateTime.MinValue, 
            GenderValType.Default, NomorKartuTValType.Default);
    
    public string PasienId { get; init; }
    public string PasienName { get; init; }
    public DateTime TglLahir { get; init; }
    public GenderValType Gender { get; init; }
    public NomorKartuTValType NomorKartuT { get; init; }
}