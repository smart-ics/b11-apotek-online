using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record AksesNaatType : StringLookupValueObject<AksesNaatType>
{
    public AksesNaatType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new [] { "A", "B", "C"};