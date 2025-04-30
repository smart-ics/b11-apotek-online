using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.DataAccessHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg
{
    public interface IObatBpjsInsertService : IRequestResponseService<ObatBpjsInsertParam, object>
    {
    }

    public record ObatBpjsInsertParam(string NoSep, string NoApotik, ResepMidwareItemModel Obat);
}
