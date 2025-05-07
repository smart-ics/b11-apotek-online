using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record JenisRawatValType : StringLookupValueObject<JenisRawatValType>
{
    public JenisRawatValType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "1", // rawat inap, 
        "2", // rawat jalan, 
        "3", // rawat igd
    };
}