namespace AptOnline.Application.Helpers;

public static class MaybeMonadExtensions
{
    public static MayBe<T> ToMaybe<T>(this T? value)
    {
        return value is null ? MayBe<T>.None : MayBe<T>.Some(value);
    }

    public static MayBe<T> Where<T>(this MayBe<T> mayBe, Func<T, bool> predicate)
    {
        return mayBe.HasValue && predicate(mayBe.Value) ? mayBe : MayBe<T>.None;
    }
    public static MayBe<T> ThrowIfSome<T>(this MayBe<T> mayBe, Func<T, Exception> exceptionFactory)
    {
        if (mayBe.HasValue)
            throw exceptionFactory(mayBe.Value);

        return mayBe;
    }
 
}