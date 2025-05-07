using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record BayiLahirStatusCodeValType : StringLookupValueObject<BayiLahirStatusCodeValType>
{
    public BayiLahirStatusCodeValType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new[]
    {
        "1", // tanpa kelainan
        "2" // dengan kelainan
    };
}