using FluentAssertions;
using Xunit;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record GenderType 
{
    private static readonly HashSet<string> ValidValues = new() { "1", "2" };
    
    private GenderType(string value) => Value = value;

    public string Value { get; init; }
    
    public static GenderType Create(string value)
    {
        if (string.IsNullOrEmpty(value)) 
            return Default;
        
        Guard.Against.InvalidInput(value, nameof(value), x => !ValidValues.Contains(x), "Gender invalid");
        
        return new GenderType(value);
    }
    public static GenderType Load(string value) => new(value);    
    public static GenderType Default => new("");
}


public class GenderTypeTest
{
    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("")]
    public void UT1_GivenValidValue_WhenCreate_ThenOk(string value)
    {
        var actual = GenderType.Create(value);
        actual.Value.Should().Be(value);
    }
    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Action act = () => GenderType.Create("3");
        act.Should().Throw<ArgumentException>().WithMessage("Gender invalid");
    }
    
    [Fact]
    public void UT3_GivenEmptyValue_WhenCreate_ThenReturnDefault()
    {
        var actual = GenderType.Create("");
        actual.Should().Be(GenderType.Default);
    }
 }