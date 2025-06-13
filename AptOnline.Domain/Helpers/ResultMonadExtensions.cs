namespace AptOnline.Domain.Helpers;

public static class ResultExtensions
{
    // Monadic Bind: chains operations that return Result<T>
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func)
    {
        if (result.IsFailure)
            return Result<TOut>.Failure(result.Error);

        return func(result.Value);
    }

    // Map: transforms value if success
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
    {
        if (result.IsFailure)
            return Result<TOut>.Failure(result.Error);

        return Result<TOut>.Success(func(result.Value));
    }

    // Run side effect if success
    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value);

        return result;
    }

    // Run side effect if failure
    public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
    {
        if (result.IsFailure)
            action(result.Error);

        return result;
    }
}