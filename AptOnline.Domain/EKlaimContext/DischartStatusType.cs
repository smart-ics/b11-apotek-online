using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record DischartStatusType : StringLookupValueObject<DischartStatusType>
{
    public DischartStatusType(string value) : base(value) { }
    protected override string[] ValidValues => new[]
    {
        "1", // Atas persetujuan dokter
        "2", // Dirujuk
        "3", // Atas permintaan sendiri
        "4", // Meninggal
        "5", // Lain-lain
    };
}