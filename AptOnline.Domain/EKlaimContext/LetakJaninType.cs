using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record LetakJaninType : StringLookupValueObject<LetakJaninType>
{
    public LetakJaninType(string value) : base(value) { }
    protected override string[] ValidValues => new []{ "kepala", "sungsang", "lintang", "-" };
}