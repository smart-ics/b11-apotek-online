using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.FaskesAgg;

public interface IListFaskesService : INunaService<IEnumerable<FaskesModel>, ListFaskesQueryParam>
{
}
public record ListFaskesQueryParam(string JenisFaskes, string Keyword);
