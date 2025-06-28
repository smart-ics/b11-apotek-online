using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.Helpers;

//  repository pattern
public interface ISaveChange<in T>
{
    void SaveChanges(T model);
}
public interface ILoadEntity<TOut, in TIn>
{
    MayBe<TOut> LoadEntity(TIn key);
}

public interface IDeleteEntity<in T>
{
    void DeleteEntity(T key);
}