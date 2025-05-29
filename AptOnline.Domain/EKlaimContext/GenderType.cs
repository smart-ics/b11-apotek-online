using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record GenderType 
{
    private readonly string[] _validValues = { "1", "2", "" };
    public GenderType(string value)
    {
        Guard.For(() => !_validValues.Contains(value), new ArgumentException("Invalid Gender"));
        Value = value;
    }
    public static GenderType Default => new("");
    public string Value { get; init; }
}

public class GenderTypeTest
{
    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("")]
    public void UT1_GivenValidValue_WhenCreate_ThenOk(string value)
    {
        var actual = new GenderType(value);
        actual.Value.Should().Be(value);
    }
    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Action act = () => new GenderType("3");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid Gender");
    }
}