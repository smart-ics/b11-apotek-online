using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record LokasiSpesimenType : StringLookupValueObject<LokasiSpesimenType>
{
    public LokasiSpesimenType(string value) : base(value) { }
    protected override string[] ValidValues => new[] { "vena", "tumit", "-" };
}