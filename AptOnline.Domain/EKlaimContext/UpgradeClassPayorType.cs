using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record UpgradeClassPayorType : StringLookupValueObject<UpgradeClassPayorType>
{
    public UpgradeClassPayorType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "peserta",
        "pemberi_kerja",
        "asuransi_tambahan"
    };
}