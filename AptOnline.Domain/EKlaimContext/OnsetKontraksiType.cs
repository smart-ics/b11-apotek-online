using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record OnsetKontraksiType : StringLookupValueObject<OnsetKontraksiType>
{
    public OnsetKontraksiType(string value) : base(value) { }
    protected override string[] ValidValues => new[] { "spontan", "induksi", "non_spontan_non_induksi", "-" };
}
