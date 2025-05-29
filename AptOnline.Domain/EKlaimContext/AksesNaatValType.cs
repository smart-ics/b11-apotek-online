using AptOnline.Domain.Helpers;
using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

//  NAAT = Nucleic Acid Amplification Test (Tes Infeksi Covid-19)
public record AksesNaatType 
{
    private readonly string[] _validValues = { "A", "B", "C", "" };
    public AksesNaatType(string value)
    {
        Guard.For(() => !_validValues.Contains(value), 
            new ArgumentException("Invalid Akses NAAT"));
        Value = value;
    }
    public static AksesNaatType Default => new("");
    public string Value { get; init; }
};

public class AksesNaatTypeTest
{
    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C")]
    [InlineData("")]
    public void UT1_GivenValidValue_WhenCreate_ThenOk(string value)
    {
        var actual = new AksesNaatType(value);
        actual.Value.Should().Be(value);
    }
    
    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Action act = () => new AksesNaatType("D");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid Akses NAAT");
    }
}