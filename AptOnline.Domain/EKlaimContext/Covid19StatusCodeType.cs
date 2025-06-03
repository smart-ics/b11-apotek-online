using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record Covid19StatusCodeType
{
    private Covid19StatusCodeType(string value) => Value = value;
    public string Value { get; init; }

    public static Covid19StatusCodeType Create(string value)
    {
        /*  Code Translation:
            1 = ODP (Orang Dalam Pemantauan)
            2 = PDP (Pasien Dalam Pengawasan)
            3 = Terkonfirmasi Positif
            4 = Suspek
            5 = Probabel */
        var validValue = new[] { "1", "2", "3", "4", "5" };
        Guard.Against.InvalidInput(value, nameof(value), x => !validValue.Contains(x),
            "Covid-19 Status Code invalid");
        return new Covid19StatusCodeType(value);
    }

    public static Covid19StatusCodeType Load(string value) => new(value);
    public static Covid19StatusCodeType Default => new("");
}

public class Covid19StatusCodeValTypeTest
{
    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    public void UT1_GivenValidValue_WhenCreate_ThenOk(string value)
    {
        var actual = Covid19StatusCodeType.Create(value);
        actual.Value.Should().Be(value);
    }
    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Action act = () => Covid19StatusCodeType.Create("6");
        act.Should().Throw<ArgumentException>().WithMessage("Covid-19 Status Code invalid");
    }
    [Fact]
    public void UT3_GivenEmptyValue_WhenLoad_ThenThrowException()
    {
        var act = () => Covid19StatusCodeType.Load("");
        act.Should().Throw<ArgumentException>();    
    }
}