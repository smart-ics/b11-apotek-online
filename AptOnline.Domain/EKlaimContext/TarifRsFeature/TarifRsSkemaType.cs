using AptOnline.Domain.BillingContext.TrsBillingFeature;
using AptOnline.Domain.EKlaimContext.SkemaTarifJknFeature;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record TarifRsSkemaType(ReffBiayaType ReffBiaya,SkemaTarifJknType SkemaTarifJkn, decimal Nilai);
