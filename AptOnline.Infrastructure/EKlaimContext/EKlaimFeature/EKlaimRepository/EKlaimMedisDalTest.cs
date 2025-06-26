using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimMedisDalTest
{
    private readonly EKlaimMedisDal _sut;

    public EKlaimMedisDalTest()
    {
        _sut = new EKlaimMedisDal(FakeAppSetting.GetDatabaseOptions());
    }

    private static EKlaimMedisDto Faker() => new()
    {
        EKlaimId = "A",
        AdlSubAcuteScore = 1,
        AdlChronicScore = 2,
        IcuFlag = 3,
        IcuLos = 4,
        IcuDescription = "B",
        Covid19StatusId = "C",
        Covid19StatusName = "D",
        Covid19TipeNoKartuId = "D",
        Covid19TipeNoKartuName = "F",
        IsPemulasaranJenazah = false,
        IsKantongJenazah = true,
        IsPetiJenazah = false,
        IsPlastikErat = true,
        IsDesinfeksiJenazah = false,
        IsMobilJenazah = false,
        IsDesinfektanMobilJenazah = true,
        IsIsoman = false, 
        Episodes = "G",
        AksesNaat = "H",
        DializerUsageId = "I",
        DializerUsageName = "J",
        JumKantongDarah = 5,
        AlteplaseIndikator = true,
        Sistole = 7,
        Diastole = 8,
        BodyWeight = 9,
        TbIndikatorId = "K",
        TbIndikatorName = "L"
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
        _sut.Delete(Faker());
    }
    
    [Fact]
    public void UT4_GetDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var actual = _sut.GetData(EKlaimModel.Key("A"));
        actual.Value.Should().BeEquivalentTo(Faker());
    }
}