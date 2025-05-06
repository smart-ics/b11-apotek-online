namespace AptOnline.Domain.Helpers;

public abstract record StringLookupValueObject<T> where T : StringLookupValueObject<T>
{
    public string Value { get; init; }

    protected StringLookupValueObject(string value)
    {
        Value = value;
    }

    protected abstract string[] ValidValues { get; }

    protected static string Normalize(string input) => input.Trim().ToLowerInvariant();

    public static T Create(string value)
    {
        var normalized = Normalize(value);

        var instance = Activator.CreateInstance(typeof(T), true) as T;
        if (instance == null)
            throw new InvalidOperationException($"Cannot create instance of {typeof(T).Name}");

        if (!instance.ValidValues.Contains(normalized))
            throw new ArgumentException($"Invalid value '{value}' for {typeof(T).Name}");

        return (T)Activator.CreateInstance(typeof(T), normalized)!;
    }

    public static T Load(string value)
    {
        var normalized = Normalize(value);
        return (T)Activator.CreateInstance(typeof(T), normalized)!;
    }

    public static T Default => (T)Activator.CreateInstance(typeof(T), "-")!;
}
