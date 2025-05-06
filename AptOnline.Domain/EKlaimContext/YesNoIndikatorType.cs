using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record YesNoIndikatorType : StringLookupValueObject<YesNoIndikatorType>
{
    public YesNoIndikatorType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "0", //  No
        "1" //  Yes
    };
}