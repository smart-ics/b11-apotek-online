using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgDiagDalTest
{
    private readonly GrouperIdrgDiagDal _sut;

    private static GrouperIdrgDiagDto Faker()
        => new GrouperIdrgDiagDto
        {
            EKlaimId = "A",
            NoUrut = 1,
            IdrgId = "B",
            Im = true,
            IdrgName = "C"
        };
    
    public GrouperIdrgDiagDalTest()
    {
        _sut = new GrouperIdrgDiagDal(FakeAppSetting.GetDatabaseOptions());
    }
    
    [Fact]
    public void UT1_InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(new List<GrouperIdrgDiagDto> {Faker()});
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
        _sut.Insert(new List<GrouperIdrgDiagDto> {Faker()});
        var actual = _sut.ListData(EKlaimModel.Key("A"));
        actual.Value.Should().ContainEquivalentOf(Faker());
    }
    
    
}