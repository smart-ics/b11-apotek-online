using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Infrastructure.Helpers;
using AptOnline.Infrastructure.SepContext.KelasRawatFeature;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.SepContext.JenisPelayananFeature;

public class JenisPelayananDalTest
{
    private readonly JenisPelayananDal _sut;

    public JenisPelayananDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new JenisPelayananDal(opt);
    }

    private static JenisPelayananType Faker() => new JenisPelayananType("A", "B");
    
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
        var actual = _sut.GetData(JenisPelayananType.Key("A"));
        actual.Value.Should().BeEquivalentTo(Faker());
    }

    [Fact]
    public void UT5_ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var actual = _sut.ListData();
        actual.Value.Should().Contain(Faker());
    }
    
}