namespace AptOnline.Infrastructure.BillingContext.RoomChargeFeature;

public record RoomChargeBedDto(string Tgl,
    string BedId, string BedName,
    string KelasDkId, string KelasDkName,
    string LayananId, string LayananName,
    string LayananDkId, string LayananDkName);