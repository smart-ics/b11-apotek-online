using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record IcuIndikatorType(int IcuFlag, int Los)
{
    public static IcuIndikatorType Create(int los)
    {
        Guard.Against.OutOfRange(los, nameof(los), 0, 90);
        var flag = los == 0 ? 0 : 1;
        return new IcuIndikatorType(flag, los);
    }
    public string Description => IcuFlag switch
    {
        0 => "Non ICU",
        1 => "ICU",
        _ => throw new ArgumentOutOfRangeException(nameof(IcuFlag), IcuFlag, null)
    };
}

public class IcuIndikatorTypeTest
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(3, 1)]
    public void UT1_GivenValidLos_ThenIcuFlagAsExpected(int los, int expectedFlag)
    {
        var actual = IcuIndikatorType.Create(los);
        actual.IcuFlag.Should().Be(expectedFlag);
    }

    [Fact]
    public void UT1_GivenInvalidLos_ThenThrowEx()
    {
        var actual = () => IcuIndikatorType.Create(92);
        actual.Should().Throw<ArgumentOutOfRangeException>();
    }
}