using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Nuna.Lib.ValidationHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimDalTest
{
    private readonly EKlaimDal _sut;

    public EKlaimDalTest()
    {
        _sut = new EKlaimDal(FakeAppSetting.GetDatabaseOptions());
    }

    private EKlaimDto Faker()
        => new EKlaimDto
        {
            EKlaimId = "A",
            EKlaimDate = new DateTime(2022, 1, 1),
            SepNo = "B",
            KartuBpjsNo = "C",
            RegId = "D",
            RegDate = new DateTime(2022, 1, 2),
            PasienId = "E",
            PasienName = "F",
            BirthDate = new DateTime(2022, 1, 3),
            Gender = "G",
            DpjpId = "H",
            DpjpName = "I",
            CaraMasukId = "J",
            CaraMasukName = "K",
            JenisRawatId = "V",
            JenisRawatName = "W",
            KelasJknId = "L",
            KelasJknName = "M",
            KelasJknValue = 1,
            KelasTarifRsId = "N",
            TarifPoliEksekutif = 2,
            UpgradeIndikator = 3,
            AddPaymentProcentage = 4,
            DischargeStatusId = "O",
            DischargeStatusName = "P",
            PayorId = "Q",
            PayorName = "R",
            CoderPegId = "S",
            CoderPegName = "T",
            CoderNik = "U",
            Los = 5,
        };
    [Fact]
    public void UT1_InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
    }

    [Fact]
    public void UT2_UpdateTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Update(Faker());
    }
    
    [Fact]
    public void UT3_DeleteTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Delete(EKlaimModel.Key("A"));
    }
    
    [Fact]
    public void UT4_GetDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var actual =_sut.GetData(EKlaimModel.Key("A"));
        actual.Value.Should().BeEquivalentTo(Faker(), 
            opt => opt
                .Excluding(x => x.SepId)
                .Excluding(x => x.SepDate)
                .Excluding(x => x.KelasTarifRsName));
    }
    
    [Fact]
    public void UT5_ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var actual =_sut.ListData(new Periode(new DateTime(2022, 1, 1)));
        actual.Value.Should().ContainEquivalentOf(Faker(), opt =>
            opt.Excluding(x => x.SepId)
                .Excluding(x => x.SepDate)
                .Excluding(x => x.KelasTarifRsName));
    }
}