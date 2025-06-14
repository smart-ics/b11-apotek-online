using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record VitalSignType(int Sistole, int Diastole, decimal BodyWeight)
{
    public static VitalSignType Create(int sistole, int diastole, decimal bodyWeight)
    {
        Guard.Against.OutOfRange(sistole, nameof(sistole), 0, 300);
        Guard.Against.OutOfRange(diastole, nameof(diastole), 0, 300);
        Guard.Against.Negative(bodyWeight, nameof(bodyWeight));
        
        return new VitalSignType(sistole, diastole, bodyWeight);
    } 
    public static VitalSignType Default => Create(0, 0, 0m);
}

public class VitalSignTypeTest
{
    [Fact]
    public void UT1_GivenValidValues_ThenOk()
    {
        var actual = VitalSignType.Create(120, 80, 75.5m);
        actual.Sistole.Should().Be(120);
        actual.Diastole.Should().Be(80);
        actual.BodyWeight.Should().Be(75.5m);
    }
    
    [Fact]
    public void UT2_GivenInvalidSistole_ThenThrowEx()
    {
        var actual = () => VitalSignType.Create(301, 80, 75.5m);
        actual.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void UT3_GivenInvalidDiastole_ThenThrowEx()
    {
        var actual = () => VitalSignType.Create(80, 302, 65.5m);
        actual.Should().Throw<ArgumentOutOfRangeException>();
    }

}