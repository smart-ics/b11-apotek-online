using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using Ardalis.GuardClauses;
using FluentAssertions;
using Nuna.Lib.PatternHelper;
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

    public static VitalSignType Create(AssesmentModel assesment)
    {
        var sistole = GetIntAssValue(assesment, "CC0001");
        var diastole = GetIntAssValue(assesment, "CC0002");
        var bodyWeight = GetIntAssValue(assesment, "CC0007");
        return new VitalSignType(sistole, diastole, bodyWeight);
    }
    
    private static int GetIntAssValue(AssesmentModel assesment, string conceptId) 
        => MayBe
            .From(assesment.ListAssesmentConcept
                .OrderByDescending(x => x.AssesmentDate)
                .FirstOrDefault(x => x.Concept.ConceptId == conceptId))
            .Match(
                onSome: x => int.Parse(x.AssValue),
                onNone: () => 0);
    
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
    
    [Fact]
    public void UT4_GivenValidAssesment_WhenCreate_ThenReturnAsExpected()
    {
        var assesment = new AssesmentModel(RegType.Default.ToRefference());
        assesment.AddConcept("A", new DateTime(2025, 8, 5), new ConceptType("CC0001", "-", "-"), "90");
        assesment.AddConcept("A", new DateTime(2025, 8, 5), new ConceptType("CC0002", "-", "-"), "140");
        assesment.AddConcept("A", new DateTime(2025, 8, 5), new ConceptType("CC0007", "-", "-"), "88");
        
        var actual = VitalSignType.Create(assesment);
        actual.Sistole.Should().Be(90);
        actual.Diastole.Should().Be(140);
        actual.BodyWeight.Should().Be(88m);
    }
    [Fact]
    public void UT5_GivenNoBodyWeguhtAssesment_WhenCreate_ThenReturnBodyWeight0()
    {
        var assesment = new AssesmentModel(RegType.Default.ToRefference());
        assesment.AddConcept("A", new DateTime(2025, 8, 5), new ConceptType("CC0001", "-", "-"), "90");
        assesment.AddConcept("A", new DateTime(2025, 8, 5), new ConceptType("CC0002", "-", "-"), "140");
        
        var actual = VitalSignType.Create(assesment);
        actual.Sistole.Should().Be(90);
        actual.Diastole.Should().Be(140);
        actual.BodyWeight.Should().Be(0);
    }
    [Fact]
    public void UT6_GivenNoAssesment_WhenCreate_ThenReturnAll0()
    {
        var assesment = new AssesmentModel(RegType.Default.ToRefference());
        
        var actual = VitalSignType.Create(assesment);
        actual.Sistole.Should().Be(0);
        actual.Diastole.Should().Be(0);
        actual.BodyWeight.Should().Be(0);
    }

}