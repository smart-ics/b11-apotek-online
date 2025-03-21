using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.FaskesAgg;

public interface IListFaskesService : INunaService<IEnumerable<FaskesModel>, ListFaskesQueryParam>
{
}
public class ListFaskesQueryParam
{
    public string JenisFaskes { get; set; }
    public string Keyword { get; set; }
}
