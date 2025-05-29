using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

//  NAAT = Nucleic Acid Amplification Test (Tes Infeksi Covid-19)
public record AksesNaatType 
{
    private AksesNaatType(string value)
    {
        Value = value;
    }

    public static AksesNaatType Create(string value)
    {
        var validValues = new[] { "A", "B", "C"};
        Guard.For(() => !validValues.Contains(value), 
            new ArgumentException("Invalid Akses NAAT"));
        return new AksesNaatType(value); 
    }
    public static AksesNaatType Load(string value) => new(value);
    public static AksesNaatType Default => new(string.Empty);
    public string Value { get; init; }
};

public class AksesNaatTypeTest
{
    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C")]
    public void UT1_GivenValidValue_WhenCreate_ThenOk(string value)
    {
        var actual = AksesNaatType.Create(value);
        actual.Value.Should().Be(value);
    }

    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Action act = () => AksesNaatType.Create("D");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid Akses NAAT");
    }

    [Fact]
    public void UT3_GivenEmptyValue_WhenLoad_ThenOk()
    {
        var actual = AksesNaatType.Load("");
        actual.Value.Should().Be("");
    }


}