using AptOnline.Domain.EKlaimContext.SkemaTarifFeature;

namespace AptOnline.Domain.BillingContext.TrsBillingFeature;

public record TrsBillingTarifType(string TrsId, string RefBiaya, 
    decimal Nilai, int Modul, SkemaTarifType SkemaTarif);
