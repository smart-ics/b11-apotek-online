using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record KelasRawatValType : StringLookupValueObject<KelasRawatValType>
{
    public KelasRawatValType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "3", // Kelas 3, 
        "2", // Kelas 2, 
        "1", // Kelas 1
    };
}