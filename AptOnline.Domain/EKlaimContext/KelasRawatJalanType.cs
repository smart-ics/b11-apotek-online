using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record KelasRawatJalanType : StringLookupValueObject<KelasRawatJalanType>
{
    public KelasRawatJalanType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "3", // Reguler
        "1", // Eksekutif
    };
}