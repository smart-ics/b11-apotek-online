using AptOnline.Domain.BillingContext.RoomChargeFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.KelasRawatFeature;

public record KelasRawatType(string KelasRawatId, string KelasRawatName) : IKelasRawatKey
{
    public static KelasRawatType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new KelasRawatType(id, name);
    }

    public static KelasRawatType Create(RoomChargeModel roomCharge)
    {
        var kelasTertinggi = roomCharge.ListBed.Min(x => x.KelasDk.KelasDkId);
        var result = kelasTertinggi switch
        {
            "1" => KelasRawatType.Create("1", "Kelas 1"),
            "2" => KelasRawatType.Create("1", "Kelas 1"),
            "3" => KelasRawatType.Create("1", "Kelas 1"),
            "4" => KelasRawatType.Create("2", "Kelas 2"),
            "5" => KelasRawatType.Create("3", "Kelas 3"),
            "6" => KelasRawatType.Create("2", "Kelas 2"),
            _ => Default
        };
        return result; 
        /* Kelas DK Mapping:
        1	KELAS VVIP  => 1
        2	KELAS VIP   => 1
        3	KELAS I     => 1
        4	KELAS II    => 2
        5	KELAS III   => 3
        6	KELAS KHUSUS=> 2
         */
    }

    public static KelasRawatType Default => new("-", "-");
    public static IKelasRawatKey Key(string id) => new KelasRawatType(id, "-");
}