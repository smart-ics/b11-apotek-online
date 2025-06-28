using Ardalis.GuardClauses;

namespace AptOnline.Domain.EmrContext.AssesmentFeature;

public record ConceptType
{
    public ConceptType( string conceptId, string conceptName, string prompt)
    {
        Guard.Against.NullOrWhiteSpace(conceptId, nameof(conceptId));
        Guard.Against.NullOrWhiteSpace(conceptName, nameof(conceptName));
        Guard.Against.NullOrWhiteSpace(prompt, nameof(prompt));
        ConceptId = conceptId;
        ConceptName = conceptName;
        Prompt = prompt;
    }
    public string ConceptId { get; init; } 
    public string ConceptName { get; init; } 
    public string Prompt { get; init; }
    public static ConceptType Default => new ConceptType("-","-","-");
    public static ConceptType AdlConcept => new ConceptType("CC0569", "-", "-");
}