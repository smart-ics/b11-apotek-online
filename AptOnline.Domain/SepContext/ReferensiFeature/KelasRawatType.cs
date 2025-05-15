using GuardNet;

namespace AptOnline.Domain.SepContext.ReferensiFeature;

public record KelasRawatType
{
    public KelasRawatType(string code, string name)
    {
        Guard.NotNullOrWhitespace(code, nameof(code));
        Guard.NotNullOrWhitespace(name, nameof(name));

        Code = code;
        Name = name;
    }
    public string Code { get; init; }
    public string Name { get; init; }
    public static KelasRawatType Default => new("-", "-");
}