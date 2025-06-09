using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Infrastructure.AptolCloudContext.Shared
{
    public class AptolCloudResponseDto
    {
        public object response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }
}
