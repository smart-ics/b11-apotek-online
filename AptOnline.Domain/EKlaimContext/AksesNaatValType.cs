using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record AksesNaatValType : StringLookupValueObject<AksesNaatValType>
{
    public AksesNaatValType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new [] { "A", "B", "C"};