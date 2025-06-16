using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.PayorFeature;
using AptOnline.Infrastructure.EKlaimContext.CaraMasukFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.PayorFeature;

public class PayorDalTest
{
    private readonly PayorDal _sut;

    public PayorDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new PayorDal(opt);
    }

    private static PayorType Faker() => new PayorType("A", "B", "C");
    
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
        var actual = _sut.GetData(PayorType.Key("A"));
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