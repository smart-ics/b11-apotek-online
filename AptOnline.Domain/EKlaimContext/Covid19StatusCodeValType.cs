using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record Covid19StatusCodeValType : StringLookupValueObject<Covid19StatusCodeValType>
{
    public Covid19StatusCodeValType(string value) : base(value) { }
    protected override string[] ValidValues => new[]
    {
        "1", // ODP (Orang Dalam Pemantauan)
        "2", // PDP (Pasien Dalam Pengawasan)
        "3", // Terkonfirmasi Positif
        "4", //  Suspek
        "5", //  Probabel
    };
}