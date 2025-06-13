using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.CaraMasukFeature;

public class CaraMasukDalTest
{
    private readonly CaraMasukDal _sut;

    public CaraMasukDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new CaraMasukDal(opt);
    }

    private static CaraMasukType Faker() => new CaraMasukType("A", "B");
    
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
        var actual = _sut.GetData(CaraMasukType.Key("A"));
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