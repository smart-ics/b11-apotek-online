using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimPasienType(string PasienId, string PasienName, DateTime TglLahir, 
    GenderValType GenderVal, NomorKartuTValType NomorKartuTVal)
)
{
    public static EKlaimPasienType Default 
        => new EKlaimPasienType(string.Empty, string.Empty, DateTime.MinValue, 
            GenderValType.Default, NomorKartuTValType.Default);
}