using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record AdlScoreType(int SubAcute, int Chronic)
{
    public static AdlScoreType Create(int subAcute, int chronic)
    {
        Guard.Against.OutOfRange(subAcute, nameof(subAcute), 12, 60);
        Guard.Against.OutOfRange(chronic, nameof(chronic), 12, 60);
        return new AdlScoreType(subAcute, chronic);    
    }

    public static AdlScoreType Default => new AdlScoreType(60, 60);
};

public class AdlScoreTypeTest
{
    [Theory]
    [InlineData(15, 15, true)]
    [InlineData(10, 15, false)]
    [InlineData(15, 65, false)]
    public void UT1_ScoreTest(int subAcute, int chronic, bool expected)
    {
        var actual = () => AdlScoreType.Create(subAcute, chronic);
        if (expected)
            actual.Should().NotThrow();
        else
            actual.Should().Throw<ArgumentOutOfRangeException>();
    }
}