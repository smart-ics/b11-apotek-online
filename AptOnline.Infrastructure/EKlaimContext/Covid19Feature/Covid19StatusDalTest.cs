using AptOnline.Domain.EKlaimContext.Covid19Feature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.Covid19Feature;

public class Covid19StatusDalTest
{
    private readonly Covid19StatusDal _sut;

    public Covid19StatusDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new Covid19StatusDal(opt);
    }

    private static Covid19StatusType Faker() => new Covid19StatusType("A", "B");
    
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
        var actual = _sut.GetData(Covid19StatusType.Key("A"));
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