
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record DischargeStatusType
{
    private DischargeStatusType(string value) => Value = value;

    public string Value { get; init; }

    public static DischargeStatusType Create(string value)
    {
        /*  CODE TRANSLATION:
         * 
         * 1 = Atas persetujuan dokter
         * 2 = Dirujuk
         * 3 = Atas permintaan sendiri
         * 4 = Meninggal
         * 5 = Lain-lain
         */

        var validValues = new[] { "1", "2", "3", "4", "5" };
        Guard.Against.InvalidInput(value, nameof(value),x => !validValues.Contains(x), "Invalid Discharge Status");
        return new DischargeStatusType(value);
    }
    public static DischargeStatusType Load(string value) => new(value);
    
    public static DischargeStatusType Default => new("");
}

