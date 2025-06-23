using AptOnline.Domain.BillingContext.RoomChargeFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.KelasJknFeature;

public record KelasJknType : IKelasJknKey
{
    private KelasJknType (string id, string name, int kelasValue)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));

        KelasJknId = id;
        KelasJknName = name;
        KelasValue = kelasValue;
        
    }
    public string KelasJknId { get; init; } 
    public string KelasJknName { get; init; }
    public int KelasValue { get; init; }
    
    public static KelasJknType Get(string kelasJknId)
    {
        var result = kelasJknId switch
        {
            "1" => Kelas1Std, 
            "2" => Kelas2,    
            "3" => Kelas3,    
            _ => Default
        };
        return result;
    }

    public static KelasJknType ConvertFrom(KelasDkType kelasDk)
    {
        var result = kelasDk.KelasDkId switch
        {
            "1" => Kelas1Vip,   //  1-KELAS VVIP  => 1
            "2" => Kelas1Vip,   //  2-KELAS VIP   => 1
            "3" => Kelas1Std,   //  3-KELAS I     => 1
            "4" => Kelas2,      //  4-KELAS II    => 2
            "5" => Kelas3,      //  5-KELAS III   => 3
            "6" => Kelas2,      //  6-KELAS KHUSUS=> 2
            _ => KelasJknType.Default
        };
        return result;
    }

    public static KelasJknType FindKelasTertinggi(RoomChargeModel akomodasi)
    {
        if (akomodasi == null)
            return Kelas3;
        
        var kelasTertinggiDk = akomodasi.ListBed.Min(x => x.KelasDk.KelasDkId);
        return kelasTertinggiDk == null 
            ? Kelas3 
            : ConvertFrom(KelasDkType.Key(kelasTertinggiDk));
    }

    public static KelasJknType Kelas1Vip => new("1", "KELAS I VIP", 4);
    public static KelasJknType Kelas1Std => new("1", "KELAS I", 3);
    public static KelasJknType Kelas2 => new("2", "KELAS II", 2);
    public static KelasJknType Kelas3 => new("3", "KELAS III", 1);
    
    public static IEnumerable<KelasJknType> ListData() => new[] { Kelas1Vip, Kelas1Std, Kelas2, Kelas3 };
    
    public static KelasJknType Default => new("-", "-", 0);
    public static IKelasJknKey Key(string id) => new KelasJknType(id, "-", 0);
}