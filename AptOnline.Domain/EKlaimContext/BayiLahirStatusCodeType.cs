using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record BayiLahirStatusCodeType 
{

    private BayiLahirStatusCodeType(string value) => Value = value;

    public static BayiLahirStatusCodeType Create(string value)
    {
        var validValues = new[] { "1", "2" };
        Guard.For(() => !validValues.Contains(value), new ArgumentException("Invalid Status Code Bayi Lahir"));
        return new BayiLahirStatusCodeType(value);
    }
    public static BayiLahirStatusCodeType Load(string value) => new(value);
    public static BayiLahirStatusCodeType Default => new(string.Empty);

    public string Value { get; init; }
}

public class BayuLahirStatusCodeValTypeTest
{
    [Fact]
    public void UT1_GivenValidValue_WhenCreate_ThenOk()
    {
        var actual = BayiLahirStatusCodeType.Create("1");
        actual.Value.Should().Be("1");
    }
    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Action act = () => BayiLahirStatusCodeType.Create("3");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid Status Code Bayi Lahir");
    }

    [Fact]
    public void UT3_GivenInvalidValue_WhenLoad_ThenOk()
    {
        var actual = BayiLahirStatusCodeType.Load("X");
        actual.Value.Should().Be("X");
    }
}