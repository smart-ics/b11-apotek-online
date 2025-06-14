using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

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
    
    public static UpgradeKelasIndikatorType Default => new(0, 0);
}

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
}