using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record KelasRawatJalanValType : StringLookupValueObject<KelasRawatJalanValType>
{
    public KelasRawatJalanValType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "3", // Reguler
        "1", // Eksekutif
    };
}