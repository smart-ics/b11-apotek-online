using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record KondisiJaninType : StringLookupValueObject<KondisiJaninType>
{
    public KondisiJaninType(string value) : base(value) { }
    protected override string[] ValidValues => new[] { "livebirth", "stillbirth", "-" };
}