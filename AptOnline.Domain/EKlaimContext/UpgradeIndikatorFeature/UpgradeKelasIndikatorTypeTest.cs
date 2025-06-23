using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.LayananDkFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.RoomChargeFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext.UpgradeIndikatorFeature;

public class UpgradeKelasIndikatorTypeTest
{
    [Fact]
    public void UT1_GivenNoUpgrade_AndNoAddPayment_ThenOk()
    {
        var actual = UpgradeKelasIndikatorType.Create(0, 0);
        actual.UpgradeIndikator.Should().Be(0);
        actual.AddPaymentProsentage.Should().Be(0);
    }
    [Fact]
    public void UT2_GivenUpgrade_AndNoAddPayment_ThenOk()
    {
        var actual = UpgradeKelasIndikatorType.Create(1, 0);
        actual.UpgradeIndikator.Should().Be(1);
        actual.AddPaymentProsentage.Should().Be(0);
    }

    [Fact]
    public void UT3_GivenUpgrade_AndWithAddPayment_ThenOk()
    {
        var actual = UpgradeKelasIndikatorType.Create(1, 15.5m);
        actual.UpgradeIndikator.Should().Be(1);
        actual.AddPaymentProsentage.Should().Be(15.5m);
    }
    
    private static LayananRefference LynDef => LayananType.Default.ToRefference();
    private static LayananDkType LynDkDef => LayananDkType.Default;
    private static BedType BedDef => BedType.Default;
    private const string KELAS_3_DK = "5";
    private const string KELAS_2_DK = "4";
    private const string KELAS_1_DK = "3";
    private const string KELAS_VIP_DK = "2";
    
    [Fact]
    public void UT4_GivenAkomdasiNaikKelasNonVip_ThenIndikatorTrue_AddPaymentNol()
    {
        var expected = UpgradeKelasIndikatorType.Create(1, 0);
        var akomodasi = new RoomChargeModel(RegType.Default.ToRefference());
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_3_DK,"-"), LynDef, LynDkDef);
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_3_DK,"-"), LynDef, LynDkDef);
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_2_DK,"-"), LynDef, LynDkDef);
        
        var actual = UpgradeKelasIndikatorType.Create(KelasJknType.Kelas3, akomodasi, 10m);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void UT5_GivenAkomdasiNaikKelasVip_ThenIndikatorTrue_AddPaymentAsExpected()
    {
        var expected = UpgradeKelasIndikatorType.Create(1, 10);
        var akomodasi = new RoomChargeModel(RegType.Default.ToRefference());
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_3_DK,"-"), LynDef, LynDkDef);
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_3_DK,"-"), LynDef, LynDkDef);
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_VIP_DK,"-"), LynDef, LynDkDef);
        
        var actual = UpgradeKelasIndikatorType.Create(KelasJknType.Kelas3, akomodasi, 10m);
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void UT6_GivenAkomdasiTidakNaikKelas_ThenIndikatorFalse_AddPayment0()
    {
        var expected = UpgradeKelasIndikatorType.Create(0, 0);
        var akomodasi = new RoomChargeModel(RegType.Default.ToRefference());
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_3_DK,"-"), LynDef, LynDkDef);
        akomodasi.AddRoomCharge(DateTime.Now, BedDef, new KelasDkType(KELAS_3_DK,"-"), LynDef, LynDkDef);
        
        var actual = UpgradeKelasIndikatorType.Create(KelasJknType.Kelas3, akomodasi, 10m);
        
        actual.Should().BeEquivalentTo(expected);
    }    
}