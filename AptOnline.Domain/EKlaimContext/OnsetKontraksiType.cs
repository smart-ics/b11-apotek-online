namespace AptOnline.Domain.EKlaimContext;

public record OnsetKontraksiType
{
    private OnsetKontraksiType(string value) => Value = value;

    public static OnsetKontraksiType Create(string value)
    {
        value = value.ToLower();
        string[] validValues = { "spontan", "induksi", "non_spontan_non_induksi", "-" };
        if (!validValues.Contains(value)) 
            throw new ArgumentException("OnsetKontraksi must be either 'spontan', 'induksi' or 'non_spontan_non_induksi'", nameof(value));

        return new OnsetKontraksiType(value);           
    }

    public static OnsetKontraksiType Load(string value)
        => new OnsetKontraksiType(value);

    public static OnsetKontraksiType Default => new OnsetKontraksiType("-");
    public string Value { get; }
}