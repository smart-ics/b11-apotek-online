using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

//  NAAT = Nucleic Acid Amplification Test (Tes Infeksi Covid-19)
public record AksesNaatType
{
    private static readonly string[] ValidValues = { "A", "B", "C" };
    private AksesNaatType(string value) => Value = value;

    public string Value { get; init; }

    public static AksesNaatType Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Default;

        Guard.Against.InvalidInput(value, nameof(value), x => !ValidValues.Contains(x), "Akses NAAT invalid");

        return new AksesNaatType(value);
    }

    public static AksesNaatType Load(string value) => new(value);
    public static AksesNaatType Default => new(string.Empty);
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
        act.Should().Throw<ArgumentException>().WithMessage("Akses NAAT invalid");
    }

    [Fact]
    public void UT3_GivenEmptyValue_WhenCreate_ThenReturnDefault()
    {
        var actual = AksesNaatType.Load("");
        actual.Should().Be(AksesNaatType.Default);
    }
}