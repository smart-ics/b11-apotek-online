using AptOnline.Domain.BillingContext.RoomChargeFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.UpgradeIndikatorFeature;

public record UpgradeKelasIndikatorType(int UpgradeIndikator, decimal AddPaymentProsentage)
{
    public static UpgradeKelasIndikatorType Create(int upgradeIndikator, decimal addPaymentProsentage)
    {
        Guard.Against.OutOfRange(upgradeIndikator, nameof(upgradeIndikator), 0, 1);
        Guard.Against.OutOfRange(addPaymentProsentage, nameof(addPaymentProsentage), 0, 100);
        
        if (upgradeIndikator == 0 && addPaymentProsentage != 0)
            throw new ArgumentException("Prosentase Payment harus kosong jika tidak naik kelas");
        return new UpgradeKelasIndikatorType(upgradeIndikator, addPaymentProsentage);
    }
    
    public static UpgradeKelasIndikatorType Create(KelasJknType kelasHak, RoomChargeModel akomodasi,
        decimal vipProsentage)
    {
        const int KELAS_VALUE_VIP = 4;
        
        var kelasTertinggiDk = akomodasi.ListBed.Min(x => x.KelasDk.KelasDkId);
        var kelasRawat = kelasTertinggiDk switch
        {
            "1" => KelasJknType.Kelas1Vip,   //  1-KELAS VVIP  => 1
            "2" => KelasJknType.Kelas1Vip,   //  2-KELAS VIP   => 1
            "3" => KelasJknType.Kelas1Std,   //  3-KELAS I     => 1
            "4" => KelasJknType.Kelas2,      //  4-KELAS II    => 2
            "5" => KelasJknType.Kelas3,      //  5-KELAS III   => 3
            "6" => KelasJknType.Kelas2,      //  6-KELAS KHUSUS=> 2
            _ => KelasJknType.Default
        };

        var upgradeIndikator = 0;
        if (kelasRawat.KelasValue > kelasHak.KelasValue)
            upgradeIndikator = 1;
        
        var addPaymentProsentage = kelasRawat.KelasValue == KELAS_VALUE_VIP 
            ? vipProsentage
            : 0;
        
        return Create(upgradeIndikator, addPaymentProsentage);
    }

    public static UpgradeKelasIndikatorType Default => new(0, 0);
}