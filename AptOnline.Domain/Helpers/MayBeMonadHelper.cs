namespace AptOnline.Domain.Helpers;

public readonly struct MayBe<T>
{
    private readonly T _value;

    public bool HasValue { get; }

    public T Value => HasValue
        ? _value
        : throw new InvalidOperationException("No value present.");

    private MayBe(T value)
    {
        _value = value;
        HasValue = true;
    }

    public static MayBe<T> Some(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return new MayBe<T>(value);
    }

    public static MayBe<T> None => new MayBe<T>();

    public MayBe<TResult> Map<TResult>(Func<T, TResult> mapper)
    {
        return HasValue ? MayBe<TResult>.Some(mapper(_value)) : MayBe<TResult>.None;
    }

    public MayBe<TResult> Bind<TResult>(Func<T, MayBe<TResult>> binder)
    {
        return HasValue ? binder(_value) : MayBe<TResult>.None;
    }

    public T GetValueOrDefault(T defaultValue = default!)
    {
        return HasValue ? _value : defaultValue!;
    }

    public T GetValueOrThrow(string errorMessage)
    {
        return HasValue ? _value : throw new KeyNotFoundException(errorMessage);
    }

    public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone)
    {
        return HasValue ? onSome(_value) : onNone();
    }

    public void Match(Action<T> onSome, Action onNone)
    {
        if (HasValue)
            onSome(_value);
        else
            onNone();
    }
    
    public override string ToString() => HasValue ? $"Some({_value})" : "None";
}

public static class MayBe
{
    public static MayBe<T> From<T>(T? value) where T : class =>
        value is null ? MayBe<T>.None : MayBe<T>.Some(value);
}