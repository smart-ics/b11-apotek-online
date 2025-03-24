using AptOnline.Domain.AptolCloudContext.ObatBpjsAgg;
using Nuna.Lib.CleanArchHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Application.AptolCloudContext.ObatBpjsAgg
{
    public interface IListObatBpjsService : INunaService<IEnumerable<ObatBpjsModel>, ListObatBpjsServiceParam>
    {
    }
    public record ListObatBpjsServiceParam(string KodeJenisObat, string TglResep, string Keyword);
}
