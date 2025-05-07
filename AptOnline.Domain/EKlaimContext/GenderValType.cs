using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record GenderValType : StringLookupValueObject<GenderValType>
{
    public GenderValType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new[]
    {
        "1", //  Laki
        "2", //  Perempuan
    };
}