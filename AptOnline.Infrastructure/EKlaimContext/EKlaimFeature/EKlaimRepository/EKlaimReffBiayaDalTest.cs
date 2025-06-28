using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimReffBiayaDalTest
{
    private readonly EKlaimReffBIayaDal _sut;

    public EKlaimReffBiayaDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new EKlaimReffBIayaDal(opt);
    }
    
    private EKlaimReffBiayaDto Faker()
    {
        return new EKlaimReffBiayaDto
        {
            EKlaimId = "A",
            NoUrut = 1,
            TrsId = "B",
            ReffBiayaId = "C",
            KetBiaya = "D",
            Nilai = 1,
            SkemaTarifJknId = "E",
            SkemaTarifJknName = "F"
        };
    }
    
    [Fact]
    public void UT1_InsertTest()
    {
        using var trans = TransHelper.NewScope();
        var listModel = new List<EKlaimReffBiayaDto> {Faker()};
        _sut.Insert(listModel);
    }
    
    [Fact]
    public void UT2_DeleteTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Delete(EKlaimModel.Key("A"));
    }

    [Fact]
    public void UT3_ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(new List<EKlaimReffBiayaDto> {Faker()});
        var actual = _sut.ListData(EKlaimModel.Key("A"));
        actual.Value.Should().ContainEquivalentOf(Faker());
    }
}