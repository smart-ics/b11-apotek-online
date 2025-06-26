namespace AptOnline.Domain.Helpers;

//  repository pattern
public interface ISaveChange<T>
{
    void SaveChanges(T model);
}
public interface ILoadData<out TOut, in TIn>
{
    TOut LoadData(TIn key);
}
