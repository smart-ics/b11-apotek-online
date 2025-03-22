using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using Nuna.Lib.CleanArchHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Application.AptolCloudContext.PoliBpjsAgg
{
    public  interface IListPoliBpjsService : INunaService<IEnumerable<PoliBpjsModel>, string>
    {
    }
}
