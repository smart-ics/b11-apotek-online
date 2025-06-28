using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record AdlScoreType(int SubAcute, int Chronic)
{
    public static AdlScoreType Create(int subAcute, int chronic)
    {
        //  menurut petunjuk, seharusnya score kisaran 12 s/d 60
        //  tetapi tidak dijelaskan skala nilai yg digunakan
        //  SMASS menggunakan Barthel dng skala 0-100. Skala lainnya; Katz, Klein-Bell.
        Guard.Against.OutOfRange(subAcute, nameof(subAcute), 0, 100);
        Guard.Against.OutOfRange(chronic, nameof(chronic), 0, 100);
        return new AdlScoreType(subAcute, chronic);    
    }

    public static AdlScoreType Create(AssesmentModel assesment)
    {
        var adlConcept = assesment.ListAssesmentConcept
            .OrderBy(x => x.AssesmentDate)
            .FirstOrDefault(x => x.Concept.ConceptId == ConceptType.AdlConcept.ConceptId);
        if (adlConcept is null) 
            return AdlScoreType.Default;

        if (adlConcept.Concept.ConceptId != "CC0569")
            throw new ArgumentException("ADL Chronic Concept invalid");
        
        _ = int.TryParse(adlConcept.AssValue, out var chronicValue) ? chronicValue : 0;
        return Create(0, chronicValue);
    }
    public static AdlScoreType Default => new AdlScoreType(0, 0);
};

public class AdlScoreTypeTest
{
    [Theory]
    [InlineData(15, 15, true)]
    [InlineData(-1, 15, false)]
    [InlineData(15, 101, false)]
    public void UT1_ScoreTest(int subAcute, int chronic, bool expected)
    {
        var actual = () => AdlScoreType.Create(subAcute, chronic);
        if (expected)
            actual.Should().NotThrow();
        else
            actual.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void UT2_GivenValidAssesment_ThenReturnAsExpected()
    {
        var assesment = new AssesmentModel(RegType.Default.ToRefference());
        assesment.AddConcept("A", new DateTime(2025, 8, 5), new ConceptType("CC0569", "-", "-"), "20");

        var actual = AdlScoreType.Create(assesment);
        actual.SubAcute.Should().Be(0);
        actual.Chronic.Should().Be(20);
    }
}