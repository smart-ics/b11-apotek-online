using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record BayiLahirStatusCodeType 
{
    /*  Status Code Bayi Lahir
     *      1 = Livebirth (hidup
     *      2 = Stillbirth (meninggal) */
    private static readonly HashSet<string> ValidValues = new() { "1", "2" };
    private BayiLahirStatusCodeType(string value) => Value = value;

    public string Value { get; init; }

    public static BayiLahirStatusCodeType Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Default;
        
        Guard.Against.InvalidInput(value, nameof(value), x => !ValidValues.Contains(x),
            "Status Code Bayi Lahir invalid");
        
        return new BayiLahirStatusCodeType(value);
    }
    public static BayiLahirStatusCodeType Load(string value) => new(value);
    public static BayiLahirStatusCodeType Default => new(string.Empty);
}

public class BayuLahirStatusCodeValTypeTest
{
    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    public void UT1_GivenValidValue_WhenCreate_ThenOk(string value)
    {
        var actual = BayiLahirStatusCodeType.Create(value);
        actual.Value.Should().Be(value);
    }
    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Action act = () => BayiLahirStatusCodeType.Create("3");
        act.Should().Throw<ArgumentException>().WithMessage("Status Code Bayi Lahir invalid");
    }

    [Fact]
    public void UT3_GivenEmptyValue_WhenCreate_ThenOkReturnDefault()
    {
        var actual = BayiLahirStatusCodeType.Load("");
        actual.Should().Be(BayiLahirStatusCodeType.Default);
    }
}