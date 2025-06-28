using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.LayananDkFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.RoomChargeFeature;

public class RoomChargeModel
{
    private readonly List<RoomChargeBedType> _listBed;

    public RoomChargeModel(RegRefference reg)
    {
        Guard.Against.Null(reg, nameof(reg));
        Reg = reg;
        _listBed = new List<RoomChargeBedType>();
    }
    public RegRefference Reg { get; init; }
    public IEnumerable<RoomChargeBedType> ListBed => _listBed;
    public static RoomChargeModel Default => new RoomChargeModel(RegType.Default.ToRefference());
    
    public void AddRoomCharge(DateTime tgl, BedType bed, KelasDkType kelasDk,
        LayananRefference layanan, LayananDkType layananDk)
    {
        Guard.Against.Null(bed, nameof(bed));
        Guard.Against.Null(kelasDk, nameof(kelasDk));
        Guard.Against.Null(layanan, nameof(layanan));
        Guard.Against.Null(layananDk, nameof(layananDk));
        
        var newItem = new RoomChargeBedType(tgl, bed, kelasDk, layanan, layananDk);
        
        _listBed.Add(newItem);
    }
}

public record RoomChargeBedType(
    DateTime Tgl,
    BedType Bed,
    KelasDkType KelasDk,
    LayananRefference Layanan, 
    LayananDkType LayananDk);

