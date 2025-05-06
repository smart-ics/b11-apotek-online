using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record UpgradeClassType : StringLookupValueObject<UpgradeClassType>
{
    public UpgradeClassType(string value) : base(value){ }
    protected override string[] ValidValues => new[]
    {
        "kelas_1", // naik ke kelas 1
        "kelas_2", // naik ke kelas 2
        "vip", // naik ke kelas vip
        "vvip", // naik ke kelas vvip
    };
}