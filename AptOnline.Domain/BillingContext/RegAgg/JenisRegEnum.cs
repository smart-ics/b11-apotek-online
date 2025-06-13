using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.BillingContext.RegAgg;

public enum JenisRegEnum
{
    RawatJalan,
    RawatInap,
    External,
    RawatDarurat,
    Meninggal,
    ExternalInap,
    Unknown = 99,
}
