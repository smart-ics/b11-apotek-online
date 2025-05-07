using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record YesNoIndikatorValType : StringLookupValueObject<YesNoIndikatorValType>
{
    public YesNoIndikatorValType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "0", //  No
        "1" //  Yes
    };
}