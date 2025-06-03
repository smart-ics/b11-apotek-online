using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record AdlScoreType // Activity Daily Living Score
{
    private AdlScoreType(int value) => Value = value;
    
    public int Value { get; init; }
    
    public static AdlScoreType Create(int value)
    {
        if (value is < 12 or > 60)
            throw new ArgumentException("ADL Score invalid. Harus berada antara 12 s/d 60");
        return new AdlScoreType(value);
    }
    public static AdlScoreType Load(int value) => new(value);
    public static AdlScoreType Default => new(60);
}

public class AdlScoreTypeTest
{
    [Fact]
    public void UT1_GivenValidValue_WhenCreate_ThenOk()
    {
        var actual = AdlScoreType.Create(30);
        actual.Value.Should().Be(30);
    }

    [Fact]
    public void UT2_GivenInvalidValue_WhenCreate_ThenThrowException()
    {
        Assert.Throws<ArgumentException>(() => AdlScoreType.Create(11));
    }
}