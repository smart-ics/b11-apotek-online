namespace AptOnline.Domain.EKlaimContext;

public record KondisiJaninType
{
    private KondisiJaninType(string value)
        => Value = value;

    public static KondisiJaninType Create(string value)
    {
        value = value.ToLower();
        string[] validValues = {"livebirth", "stillbirth", "-" };
        if (!validValues.Contains(value))
            throw new ArgumentException("Kondisi Janin must be either 'livebirth', 'stillbirth' or '-'", nameof(value));

        return new KondisiJaninType(value);
    }
    
    public static KondisiJaninType Load(string value) => new KondisiJaninType(value);
    public static KondisiJaninType Default => new KondisiJaninType("-");
    public string Value { get; init; }
}