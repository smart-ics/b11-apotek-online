using Ardalis.GuardClauses;

namespace AptOnline.Domain.EmrContext.AssesmentFeature;

public record AssesmentConceptType
{
    public AssesmentConceptType(string assesmentId, DateTime assesmentDate, 
        ConceptType concept, string assValue)
    {
        Guard.Against.NullOrWhiteSpace(assesmentId, nameof(assesmentId));
        Guard.Against.Null(assesmentDate, nameof(assesmentDate));
        Guard.Against.Null(concept, nameof(concept));
        Guard.Against.NullOrWhiteSpace(assValue, nameof(assValue));
        
        AssesmentId = assesmentId;
        AssesmentDate = assesmentDate;
        Concept = concept;
        AssValue = assValue;
    }
    public string AssesmentId { get; init; }
    public DateTime AssesmentDate { get; init; } 
    public ConceptType Concept { get; init; }
    public string AssValue { get; init; }
    public static AssesmentConceptType Default => new AssesmentConceptType(string.Empty, DateTime.MinValue, ConceptType.Default, string.Empty);
}