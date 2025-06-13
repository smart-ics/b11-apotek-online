using AptOnline.Domain.Helpers;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.Helpers;

public interface ISaveWriter<in T>
{
    void Save(T entity);
}
public interface IDeleteWriter<in T>
{
    void Delete(T key);
}
public interface IGetDataReader<T, in TKey>
{
    MayBe<T> GetData(TKey key);
}
