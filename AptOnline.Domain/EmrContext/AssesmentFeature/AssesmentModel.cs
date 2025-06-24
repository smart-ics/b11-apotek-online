using Ardalis.GuardClauses;
using AptOnline.Domain.BillingContext.RegAgg;

namespace AptOnline.Domain.EmrContext.AssesmentFeature;

public class AssesmentModel
{
    private readonly List<AssesmentConceptType> _listAssesmentConcept;
    public AssesmentModel(RegRefference reg)
    {
        Guard.Against.Null(reg, nameof(reg));
        Reg = reg;
        _listAssesmentConcept = new List<AssesmentConceptType>();
    }
    public RegRefference Reg { get; init; }
    public IEnumerable<AssesmentConceptType> ListAssesmentConcept => _listAssesmentConcept;
    public static AssesmentModel Default => new(RegType.Default.ToRefference()); 
        
    public void AddConcept(string assesmentId, DateTime assesmentDate, ConceptType concept, string assValue)
    {
        Guard.Against.NullOrWhiteSpace(assesmentId, nameof(assesmentId));
        Guard.Against.Null(assesmentDate, nameof(assesmentDate));
        Guard.Against.Null(concept, nameof(concept));
        
        var newItem = new AssesmentConceptType(assesmentId, assesmentDate, concept, assValue);
        _listAssesmentConcept.Add(newItem);
    }
}