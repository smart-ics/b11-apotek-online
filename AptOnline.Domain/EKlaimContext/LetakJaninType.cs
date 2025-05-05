namespace AptOnline.Domain.EKlaimContext;

public record LetakJaninType
{
    private LetakJaninType(string value) => Value = value;

    public static LetakJaninType Create(string value)
    {
        value = value.ToLower();
        string[] validValue = { "kepala", "sungsang", "lintang", "-" };
        if (!validValue.Contains(value))
            throw new ArgumentException("Letak Janin must be either 'kepala', 'sungsang' or 'lintang'", nameof(value));

        return new LetakJaninType(value);
    }

    public static LetakJaninType Load(string value) => new LetakJaninType(value);

    public static LetakJaninType Default => new LetakJaninType("-");
    public string Value { get; init; }
}