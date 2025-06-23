namespace AptOnline.Domain.BillingContext.RoomChargeFeature;

public record BedType(string BedId, string BedName)
{
    public static BedType Default = new BedType("-","-");
}