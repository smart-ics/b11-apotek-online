using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimAdmPasienType(string PasienId, string PasienName, DateTime TglLahir, 
    GenderValType GenderVal, NomorKartuTValType NomorKartuTVal)
{
    public static EKlaimAdmPasienType Default 
        => new EKlaimAdmPasienType(string.Empty, string.Empty, DateTime.MinValue, 
            GenderValType.Default, NomorKartuTValType.Default);
}