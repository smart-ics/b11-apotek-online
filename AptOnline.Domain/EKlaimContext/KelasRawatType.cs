using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record KelasRawatInapType : StringLookupValueObject<KelasRawatInapType>
{
    public KelasRawatInapType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "3", // Kelas 3, 
        "2", // Kelas 2, 
        "1", // Kelas 1
    };
}