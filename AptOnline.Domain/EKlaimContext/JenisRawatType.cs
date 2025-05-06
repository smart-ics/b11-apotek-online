using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record JenisRawatType : StringLookupValueObject<JenisRawatType>
{
    public JenisRawatType(string value) : base(value) { }

    protected override string[] ValidValues => new[]
    {
        "1", // rawat inap, 
        "2", // rawat jalan, 
        "3", // rawat igd
    };
}