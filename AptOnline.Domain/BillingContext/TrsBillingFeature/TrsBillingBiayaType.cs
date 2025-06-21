namespace AptOnline.Domain.BillingContext.TrsBillingFeature;

public record TrsBillingBiayaType(string TrsId, 
    ReffBiayaType ReffBiaya, decimal Nilai);

public record ReffBiayaType(string ReffBiayaId, ReffBiayaClassEnum ReffClass);

public enum ReffBiayaClassEnum
{
    Tarif,
    Obat,
    Akomodasi,
}