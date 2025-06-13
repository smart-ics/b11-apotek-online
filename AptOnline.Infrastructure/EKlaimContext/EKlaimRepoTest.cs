using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext;

public class EKlaimRepoTest
{
    private readonly EKlaimRepo _sut;

    public EKlaimRepoTest()
    {
        _sut = new EKlaimRepo(FakeAppSetting.GetDatabaseOptions());
    }

    private static EKlaimModel Faker() => EKlaimModel.Load(
        "A", new DateTime(2025,6,6), RegType.Default.ToRefference(), 
        SepType.Default.ToRefference(), PasienType.Default, 
        PesertaBpjsType.Default.ToRefference());
    
    [Fact]
    public void UT1_InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
    }

    [Fact]
    public void UT2_InsertTest()
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
    public void UT4_GetData_ByEklaimId_Test()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var key = EKlaimModel.Key(Faker().EKlaimId);
        var actual = _sut.GetData(key);
        actual.HasValue.Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(Faker(), opt =>
            opt.Excluding(y => y.Sep.SepId)
                .Excluding(y => y.Sep.SepDateTime));
    }
    
    [Fact]
    public void UT6_GetData_ByEklaimId_NotFound_Test()
    {
        using var trans = TransHelper.NewScope();
        var key = EKlaimModel.Key(Faker().EKlaimId);
        var actual = _sut.GetData(key);
        actual.HasValue.Should().BeFalse();
    }

    
    [Fact]
    public void UT5_GetData_BySepNo_Test()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var key = SepType.Key(Faker().Sep.SepNo);
        var actual = _sut.GetData(key);
        actual.HasValue.Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(Faker(), opt =>
            opt.Excluding(y => y.Sep.SepId)
                .Excluding(y => y.Sep.SepDateTime));
    }
    
    [Fact]
    public void UT6_GetData_ByRegId_Test()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var key = RegType.Key(Faker().Reg.RegId);
        var actual = _sut.GetData(key);
        actual.HasValue.Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(Faker(), opt =>
            opt.Excluding(y => y.Sep.SepId)
                .Excluding(y => y.Sep.SepDateTime));
    }
}