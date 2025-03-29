using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;

public class PoliBpjsModel : IPoliBpjsKey
{
    public string PoliBpjsId { get; set; }
    public string PoliBpjsName { get; set; }
}