namespace AptOnline.Domain.BillingContext.RoomChargeFeature;

public record KelasDkType(string KelasDkId, string KelasDkName)
{
    public static KelasDkType Default => new KelasDkType("-", "-");
    public static KelasDkType Key(string id) => new KelasDkType(id, "-");
}